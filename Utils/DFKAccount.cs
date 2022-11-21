using DFK;
using DFKContracts.HeroCore;
using DFKContracts.QuestCore;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using PirateQuester.Bot;

namespace PirateQuester.Utils
{
    public class DFKAccount
	{
		public delegate void AccountUpdated();

		public static event AccountUpdated UpdatedAccount;
		public async void UpdateBalance()
        {
            Balance = 0;

            Balance += Web3.Convert.FromWei(await Signer.Eth.GetBalance.SendRequestAsync(Account.Address));
        }

        public async Task InitializeAccount()
		{
			UpdateBalance();

            Dictionary<HeroesArgument, string> args = new()
			{
				{ HeroesArgument.owner, Account.Address }
			};
			string request = API.HeroesRequestBuilder(args, "id owner {id name} rarity generation firstName lastName mainClass subClass staminaFullAt level currentQuest strength intelligence wisdom luck agility vitality endurance dexterity stamina profession statBoost1 statBoost2 salePrice xp");
			Heroes = (await API.GetHeroes(request)).ToList();
			BotHeroes = new();
			foreach(Hero h in Heroes)
			{
				h.DFKAccount = this;
				BotHeroes.Add(new DFKBotHero(h));
			}
			UpdatedAccount?.Invoke();
		}

		public async Task UpdateHeroes()
		{
            Dictionary<HeroesArgument, string> args = new()
            {
                { HeroesArgument.owner, Account.Address }
            };
            string request = API.HeroesRequestBuilder(args, "id staminaFullAt level currentQuest strength intelligence wisdom luck agility vitality endurance dexterity stamina salePrice xp");
			List<Hero> updates = (await API.GetHeroes(request)).ToList();
			foreach(Hero h in updates)
			{
				Hero target = Heroes.FirstOrDefault(hero => hero.id == h.id);
				DFKBotHero botHero = BotHeroes.FirstOrDefault(hero => hero.Hero.id == h.id);
				if(target is not null)
				{
					target.UpdateHeroValues(h);
					botHero.Hero.UpdateHeroValues(h);
                }
			}
        }

		public DFKAccount(string name, Account account)
        {
            Name = name;
            Account = account;
			Signer = new Web3(account, "https://subnets.avax.network/defi-kingdoms/dfk-chain/rpc");
			Signer.TransactionManager.UseLegacyAsDefault = false;
			Quest = new QuestCoreService(Signer, "0xE9AbfBC143d7cef74b5b793ec5907fa62ca53154");
			Hero = new HeroCoreService(Signer, "0xEb9B61B145D6489Be575D3603F4a704810e143dF");
			Meditation = new DFKContracts.MeditationCircle.MeditationCircleService(Signer, "0xD507b6b299d9FC835a0Df92f718920D13fA49B47");
		}

		private decimal balance;
        public decimal Balance { get { return Math.Round(balance, 2); } set { balance = value;} }

        public Web3 Signer { get; set; }
		public HeroCoreService Hero { get; set; }
        public QuestCoreService Quest { get; set; }
        public DFKContracts.MeditationCircle.MeditationCircleService Meditation { get; set; }
        public Account Account { get; set; }
        public string Name { get; set; }
        public List<Hero> Heroes { get; set; }
		public List<DFKBotHero> BotHeroes { get; set; }
    }
}