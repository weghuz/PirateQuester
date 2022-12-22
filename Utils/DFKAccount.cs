using DFK;
using DFKContracts.ERC20;
using DFKContracts.HeroCore;
using DFKContracts.QuestCore;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using PirateQuester.Bot;
using PirateQuester.PirateQuesterToken;
using System.Numerics;

namespace PirateQuester.Utils
{
    public class DFKAccount
	{
		public bool QueryOnChain = false;

		public delegate void AccountUpdated();

		public static event AccountUpdated UpdatedAccount;
		public async Task UpdateBalance()
        {
            int attempt = 0;
            bool retry = true;
            while (retry)
            {
                try
                {
                    PQTBalance = Web3.Convert.FromWei(await PQT.BalanceOfQueryAsync(Account.Address));
                    Balance = Web3.Convert.FromWei(await Signer.Eth.GetBalance.SendRequestAsync(Account.Address));
                    AvaxBalance = Web3.Convert.FromWei(await AvalancheSigner.Eth.GetBalance.SendRequestAsync(Account.Address));
                    retry = false;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine($"If the RPC fails too much, try changing RPCs in /Options");
                    attempt++;
                    if (attempt >= 5)
                    {
                        retry = false;
                    }
                    else
                    {
                        await Task.Delay(attempt * 100);
                    }
                }
            }
        }

        public async Task InitializeAccount(DFKBotSettings settings)
		{
			await UpdateBalance();
			int attempt = 0;
			bool retry = true;
			while (retry)
			{
				try
				{
					if(QueryOnChain)
					{
						await ChainUpdateHeroes();
					}
					else
					{
						await APIInitializeHeroes();
					}
					retry = false;
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
					attempt++;
					if (attempt >= 5)
					{
						QueryOnChain = true;
						retry = false;
						await ChainUpdateHeroes();
					}
					else
					{
						await Task.Delay(attempt * 100);
					}
				}
			}
			BotHeroes = new();
			foreach (Hero h in Heroes)
			{
				h.DFKAccount = this;
				if(h.owner is null)
				{
					h.owner = new()
					{
						Id = Account.Address
					};
				}
				BotHeroes.Add(new DFKBotHero(h, settings));
			}
			UpdatedAccount?.Invoke();
		}
		
		public async Task APIInitializeHeroes()
		{
			Dictionary<HeroesArgument, string> args = new()
			{
				{ HeroesArgument.owner, Account.Address },
                { HeroesArgument.network, Chain.Identifier }
            };
			string request = API.HeroesRequestBuilder(args, "id owner {id name} rarity generation firstName lastName mainClass subClass staminaFullAt level currentQuest strength intelligence wisdom luck agility vitality endurance dexterity stamina profession statBoost1 statBoost2 salePrice xp network");
			Heroes = (await API.GetHeroes(request)).ToList();
		}

		public async Task UpdateHeroes()
		{
			int attempt = 0;
			bool retry = true;
			while (retry)
			{
				try
				{
					if (QueryOnChain)
					{
						await ChainUpdateHeroes();
					}
					else
					{
						await APIUpdateHeroes();
					}
					retry = false;
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
					attempt++;
					if (attempt >= 5)
					{
						QueryOnChain = true;
						retry = false;
						await ChainUpdateHeroes();
					}
					else
					{
						await Task.Delay(attempt * 100);
					}
				}
			}
        }

		public async Task ChainUpdateHeroes()
		{
			List<BigInteger> chainHeroes = new();
			List<Hero> newHeroes = new();
			int attempt = 0;
			bool retry = true;
			while (retry)
			{
				try
				{
					chainHeroes = await Hero.GetUserHeroesQueryAsync(Account.Address);
					retry = false;
				}
				catch (Exception e)
				{
					attempt++;
					if(attempt >= 5)
					{
						retry = false;
					}
					else
					{
						await Task.Delay(attempt * 100);
					}
					Console.WriteLine(e.Message);
				}
			}
			foreach(var heroId in chainHeroes)
			{
				DFKContracts.HeroCore.ContractDefinition.Hero hero;
				attempt = 0;
				retry = true;
				while (retry)
				{
					try
					{
						hero = (await Hero.GetHeroQueryAsync(heroId)).ReturnValue1;
						newHeroes.Add(new(hero));
						Console.WriteLine($"Hero #{heroId} fetch from blockchain {chainHeroes.IndexOf(heroId)}/{chainHeroes.Count()}");
						retry = false;
					}
					catch (Exception e)
					{
						if (attempt >= 5)
						{
							QueryOnChain = true;
							retry = false;
						}
						else
						{
							await Task.Delay(attempt * 100);
						}
						Console.WriteLine(e.Message);
					}
				}
			}
			Heroes = newHeroes;
		}

		public async Task APIUpdateHeroes()
		{
			Dictionary<HeroesArgument, string> args = new()
			{
				{ HeroesArgument.owner, Account.Address },
				{ HeroesArgument.network, Chain.Identifier }
			};
			string request = API.HeroesRequestBuilder(args, "id staminaFullAt level currentQuest strength intelligence wisdom luck agility vitality endurance dexterity stamina salePrice xp network");
			List<Hero> updates = (await API.GetHeroes(request)).ToList();
			foreach (Hero h in updates)
			{
				Hero target = Heroes.FirstOrDefault(hero => hero.id == h.id);
				DFKBotHero botHero = BotHeroes.FirstOrDefault(hero => hero.Hero.id == h.id);
				if (target is not null)
				{
					target.UpdateHeroValues(h);
					botHero.Hero.UpdateHeroValues(h);
				}
			}
		}

		public DFKAccount(string name, Account account, Chain.Chain chain, Chain.Chain Avalanche)
        {
            Name = name;
            Account = account;
			Chain = chain;
            AvalancheSigner = new Web3(account, Avalanche.RPC);
            PQT = new PirateQuesterTokenService(AvalancheSigner, Constants.PQT_ADDRESS);
            Signer = new Web3(account, chain.RPC);
			// Transactions may fail without this.
			Signer.TransactionManager.UseLegacyAsDefault = true;
			Quest = new QuestCoreService(Signer, chain.QuestAddress);
			Hero = new HeroCoreService(Signer, chain.HeroAddress);
			Meditation = new DFKContracts.MeditationCircle.MeditationCircleService(Signer, chain.MeditationAddress);
		}

        public Chain.Chain Chain { get; set; }
        private decimal balance;
        public decimal Balance { get { return Math.Round(balance, 2); } set { balance = value; } }
        private decimal pqtBalance;
        public decimal PQTBalance { get { return Math.Round(pqtBalance, 2); } set { pqtBalance = value; } }

		public decimal avaxBalance;
        public decimal AvaxBalance { get { return Math.Round(avaxBalance, 2); } set { avaxBalance = value; } }
        public Web3 AvalancheSigner { get; set; }
        public Web3 Signer { get; set; }
        public Erc20Service Erc20 { get; set; }
        public HeroCoreService Hero { get; set; }
        public QuestCoreService Quest { get; set; }
        public PirateQuesterTokenService PQT { get; set; }
        public DFKContracts.MeditationCircle.MeditationCircleService Meditation { get; set; }
        public Account Account { get; set; }
        public string Name { get; set; }
        public List<Hero> Heroes { get; set; }
		public List<DFKBotHero> BotHeroes { get; set; }
    }
}