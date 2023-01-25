using DFK;
using PirateQuester.Bot;
using PirateQuester.Utils;

namespace PirateQuester.Services
{
	public class HeroPricingService
	{
		public AccountManager Acc { get; }
		public List<Hero> AuctionData { get; set; }
		public HeroPricingService(AccountManager acc)
		{
			Acc = acc;
		}

		public static async Task<List<Hero>> GetAuctionData(int skip)
		{
            List<Hero> newHeroes;
            Dictionary<HeroesArgument, string> args = new()
				{
					{ HeroesArgument.salePrice_not, "null" },
					{ HeroesArgument.skip, skip.ToString() },
					{ HeroesArgument.first, 1000.ToString() }
				};

			string request = API.HeroesRequestBuilder(args, "id rarity generation statGenes level salePrice network mining foraging fishing gardening maxSummons summonsRemaining");

			newHeroes = (await API.GetHeroes(request)).ToList();
			// Decode all hero genes.
			try
			{
				foreach (Hero h in newHeroes)
				{
					h.DecodeGenes();
				}
			}
			catch (Exception)
			{
				
			}
            return newHeroes;
		}

		public void UpdateHeroFloorEstimates()
		{
			foreach (DFKBotHero hero in Acc.Accounts.SelectMany(a => a.BotHeroes))
			{
				if (hero.Hero.generation == 0)
				{
					GetHeroFloorEstimateGen0(hero);
				}
				else if (hero.Hero.generation == 1 && hero.Hero.summonsRemaining == 10)
				{
					GetHeroFloorEstimateGen1(hero);
				}
				else
				{
					GetHeroFloorEstimate(hero);
				}
			}
		}

		private void GetHeroFloorEstimateGen0(DFKBotHero botHero)
		{
			Hero hero = botHero.Hero;
			botHero.FloorEstimateFields = new() { "generation", "rarity", "mainClass", "subClass" };
			List<Hero> floorHeroes = AuctionData
				.Where(h =>
					h.generation <= hero.generation
					&& h.mainClass == hero.mainClass
					&& h.rarity >= hero.rarity
					&& h.subClass == hero.subClass)
				.ToList();
			if (floorHeroes.Count <= 2)
			{
				botHero.FloorEstimateFields = new() { "generation", "rarity", "mainClass" };
				floorHeroes = AuctionData
					.Where(h =>
					h.generation <= hero.generation
					&& h.mainClass == hero.mainClass
					&& h.rarity >= hero.rarity)
					.ToList();
			}
			if (floorHeroes.Count <= 1)
			{
				botHero.FloorEstimateFields = new() { "generation", "rarity" };
				floorHeroes = AuctionData
					.Where(h =>
					h.generation <= hero.generation
					&& h.rarity >= hero.rarity)
					.ToList();
			}
			if (floorHeroes.Count <= 1)
			{
				botHero.FloorEstimateFields = new() { "generation" };
				floorHeroes = AuctionData
					.Where(h =>
					h.generation <= hero.generation)
					.ToList();
			}
			if (floorHeroes.Count == 0)
			{
				botHero.FloorEstimateFields = new();
				botHero.FloorEstimate = 99999;
				return;
			}

			botHero.FloorEstimate = floorHeroes.Select(h => h.SalePrice(3)).Min();
		}

		private void GetHeroFloorEstimateGen1(DFKBotHero botHero)
		{
			Hero hero = botHero.Hero;
			botHero.FloorEstimateFields = new() { "generation", "summonsRemaining", "rarity", "level", "subClass" };
			List<Hero> floorHeroes = AuctionData
				.Where(h =>
					h.subClass == hero.subClass
					&& h.level >= hero.level
					&& h.summonsRemaining >= hero.summonsRemaining
					&& h.generation <= hero.generation
					&& h.rarity >= hero.rarity)
				.ToList();
			if (floorHeroes.Count <= 3)
			{
				botHero.FloorEstimateFields = new() { "generation", "summonsRemaining", "rarity", "level" };
				floorHeroes = AuctionData
					.Where(h =>
					h.level >= hero.level
					&& h.summonsRemaining >= hero.summonsRemaining
					&& h.generation <= hero.generation
					&& h.rarity >= hero.rarity)
					.ToList();
			}
			if (floorHeroes.Count <= 2)
			{
				botHero.FloorEstimateFields = new() { "generation", "summonsRemaining", "rarity" };
				floorHeroes = AuctionData
					.Where(h =>
					h.summonsRemaining >= hero.summonsRemaining
					&& h.generation <= hero.generation
					&& h.rarity >= hero.rarity)
					.ToList();
			}
			if (floorHeroes.Count <= 1)
			{
				botHero.FloorEstimateFields = new() { "generation", "summonsRemaining" };
				floorHeroes = AuctionData
					.Where(h =>
					h.summonsRemaining >= hero.summonsRemaining
					&& h.generation <= hero.generation)
					.ToList();
			}
			if (floorHeroes.Count <= 1)
			{
				botHero.FloorEstimateFields = new() { "generation" };
				floorHeroes = AuctionData
					.Where(h =>
					h.generation <= hero.generation)
					.ToList();
			}
			if (floorHeroes.Count == 0)
			{
				botHero.FloorEstimateFields = new();
				botHero.FloorEstimate = 99999;
				return;
			}

			botHero.FloorEstimate = floorHeroes.Select(h => h.SalePrice(3)).Min();
		}

		private void GetHeroFloorEstimate(DFKBotHero botHero)
		{
			Hero hero = botHero.Hero;
			botHero.FloorEstimateFields = new() { "rarity", "mainClass", "subClass", "profession" };
			List<Hero> floorHeroes = AuctionData
				.Where(h =>
					h.profession == hero.profession
					&& h.level >= hero.level
					&& h.mainClass == hero.mainClass
					&& h.rarity >= hero.rarity)
				.ToList();
			if (floorHeroes.Count <= 2)
			{
				botHero.FloorEstimateFields = new() { "mainClass", "rarity", "level" };
				floorHeroes = AuctionData
					.Where(h =>
					h.level >= hero.level
					&& h.mainClass == hero.mainClass
					&& h.rarity >= hero.rarity)
					.ToList();
			}
			if (floorHeroes.Count <= 1)
			{
				botHero.FloorEstimateFields = new() { "rarity", "level" };
				floorHeroes = AuctionData
					.Where(h =>
					h.rarity >= hero.rarity
					&& h.level >= hero.level)
					.ToList();
			}
			if (floorHeroes.Count <= 1)
			{
				botHero.FloorEstimateFields = new() { "level" };
				floorHeroes = AuctionData
					.Where(h =>
					h.level >= hero.level)
					.ToList();
			}
			if (floorHeroes.Count == 0)
			{
				botHero.FloorEstimateFields = new();
				botHero.FloorEstimate = 99999;
				return;
			}

			botHero.FloorEstimate = floorHeroes.Select(h => h.SalePrice(3)).Min();
		}

		public async void UpdateHeroPrices()
		{
			AuctionData = new();
			int skip = 0;
			while (true)
			{
				var newHeroes = await GetAuctionData(skip);
				AuctionData.AddRange(newHeroes);
				if (newHeroes.Count == 0)
				{
					break;
				}
				skip += 1000;
			}
			UpdateHeroFloorEstimates();
		}
	}
}
