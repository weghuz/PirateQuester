using DFK;
using DFKContracts.ERC20;
using DFKContracts.HeroCore;
using DFKContracts.QuestCore;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using PirateQuester.Bot;
using PirateQuester.DFK.Items;
using PirateQuester.HeroSale;
using PirateQuester.ItemConsumer;
using PirateQuester.PirateQuesterToken;
using PirateQuester.PowerToken;
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
					Balance = Web3.Convert.FromWei(await Signer.Eth.GetBalance.SendRequestAsync(Account.Address));
					var consumableItem = ItemContractDefinitions.InventoryItems.First(item => item.Name == "Stamina Potion");
					var ConsumableItem = new Erc20Service(Signer, consumableItem.Addresses.First(a => a.Chain.Id == Chain.Id).Address);
					StaminaPotionBalance = int.Parse((await ConsumableItem.BalanceOfQueryAsync(Account.Address)).ToString());
					PowerTokenBalance = Web3.Convert.FromWei(await PowerTokenService.BalanceOfQueryAsync(Account.Address));
					LockedPowerTokenBalance = Web3.Convert.FromWei(await PowerTokenService.LockOfQueryAsync(Account.Address));
					AvaxBalance = Web3.Convert.FromWei(await AvalancheSigner.Eth.GetBalance.SendRequestAsync(Account.Address));
					PQTBalance = Web3.Convert.FromWei(await PQT.BalanceOfQueryAsync(Account.Address));
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

        public async Task InitializeAccount()
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
				
				BotHeroes.Add(new DFKBotHero(h, Settings));
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
			string request = API.HeroesRequestBuilder(args, "id owner {id name} rarity generation firstName lastName mainClassStr subClassStr professionStr staminaFullAt level currentQuest strength intelligence wisdom luck agility vitality endurance dexterity stamina statBoost1 statBoost2 salePrice xp network mining foraging fishing gardening mainClassStr subClassStr professionStr statBoost1StrDeprecated statBoost2StrDeprecated");
			var heroes = (await API.GetHeroes(request)).ToList();
			foreach (Hero h in heroes)
			{
				h.mainClass = null;
                if (h.mainClass is null || h.mainClass is "")
				{
					h.mainClass = h.mainClassStr;
				}
				h.subClass = "";

                if (h.subClass is null || h.subClass is "")
                {
                    h.subClass = h.subClassStr;
                }

				if (h.profession is null || h.profession is "")
                {
                    h.profession = h.professionStr;
                }

				if (h.statBoost1 is null || h.statBoost1 is "")
                {
                    h.statBoost1 = h.statBoost1StrDeprecated;
                }

				if (h.statBoost2 is null || h.statBoost2 is "")
                {
                    h.statBoost2 = h.statBoost2StrDeprecated;
                }
            }
			Heroes = heroes;
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

		public DFKAccount(string name, Account account, Chain.Chain chain, Chain.Chain Avalanche, DFKBotSettings settings)
        {
			Settings = settings;
            Name = name;
            Account = account;
			Chain = chain;
			AvalancheSigner = new Web3(new Account(account.PrivateKey), Avalanche.RPC);
			PQT = new PirateQuesterTokenService(AvalancheSigner, Constants.PQT_ADDRESS);
			// Transactions may fail without this.
			Signer = new Web3(account, chain.RPC);
			Signer.TransactionManager.UseLegacyAsDefault = true;
			PowerTokenService = new PowerTokenService(Signer, chain.NativeToken);
			Quest = new QuestCoreService(Signer, chain.QuestAddress);
			Auction = new HeroSaleService(Signer, chain.HeroSale);
            Hero = new HeroCoreService(Signer, chain.HeroAddress);
			Meditation = new DFKContracts.MeditationCircle.MeditationCircleService(Signer, chain.MeditationAddress);
			ItemConsumer = new ItemConsumerService(Signer, chain.ItemConsumer);
		}

        public DFKBotSettings Settings { get; set; }
        public Chain.Chain Chain { get; set; }
		private decimal balance;
		public decimal Balance { get { return Math.Round(balance, 2); } set { balance = value; } }
		private decimal powerTokenBalance;
		public decimal PowerTokenBalance { get { return Math.Round(powerTokenBalance, 2); } set { powerTokenBalance = value; } }
		public decimal lockedPowerTokenBalance;
		public decimal LockedPowerTokenBalance { get { return Math.Round(lockedPowerTokenBalance, 2); } set { lockedPowerTokenBalance = value; } }
		private decimal pqtBalance;
        public decimal PQTBalance { get { return Math.Round(pqtBalance, 2); } set { pqtBalance = value; } }

		public decimal avaxBalance;
		public decimal AvaxBalance { get { return Math.Round(avaxBalance, 2); } set { avaxBalance = value; } }
		public int StaminaPotionBalance { get; set; }
		public Web3 AvalancheSigner { get; }
        public Web3 Signer { get; }
        public Erc20Service Erc20 { get; }
		public HeroSaleService Auction { get; }
		public PowerTokenService PowerTokenService { get; }
        public HeroCoreService Hero { get; }
        public ItemConsumerService ItemConsumer { get; }
        public QuestCoreService Quest { get; }
        public PirateQuesterTokenService PQT { get; }
        public DFKContracts.MeditationCircle.MeditationCircleService Meditation { get; }
        public Account Account { get; }
        public string Name { get; set; }
        public List<Hero> Heroes { get; set; }
		public List<DFKBotHero> BotHeroes { get; set; }
	}
}