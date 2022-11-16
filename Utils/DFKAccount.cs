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
		public async Task LoadHeroes()
		{
			Dictionary<HeroesArgument, string> args = new()
			{
				{ HeroesArgument.owner, Account.Address }
			};
			string request = API.HeroesRequestBuilder(args, "id owner {id name} rarity generation firstName lastName mainClass subClass staminaFullAt level currentQuest strength intelligence wisdom luck agility vitality endurance dexterity stamina profession statBoost1 statBoost2 salePrice");
			Heroes = (await API.GetHeroes(request)).ToList();
			BotHeroes = Heroes.Select(h => new DFKBotHero(h)).ToList();
			foreach(Hero h in Heroes)
			{
				h.DFKAccount = this;
			}
		}

		public async Task UpdateHeroes()
		{
            Dictionary<HeroesArgument, string> args = new()
            {
                { HeroesArgument.owner, Account.Address }
            };
            string request = API.HeroesRequestBuilder(args, "id staminaFullAt level currentQuest strength intelligence wisdom luck agility vitality endurance dexterity stamina salePrice");
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
			Signer.TransactionManager.UseLegacyAsDefault = true;
			Quest = new QuestCoreService(Signer, "0xE9AbfBC143d7cef74b5b793ec5907fa62ca53154");
			Hero = new HeroCoreService(Signer, "0xEb9B61B145D6489Be575D3603F4a704810e143dF");
		}

        public Web3 Signer { get; set; }
        public HeroCoreService Hero { get; set; }
        public QuestCoreService Quest { get; set; }
		public Account Account { get; set; }
        public string Name { get; set; }
        public List<Hero> Heroes { get; set; }
		public List<DFKBotHero> BotHeroes { get; set; }
    }
}