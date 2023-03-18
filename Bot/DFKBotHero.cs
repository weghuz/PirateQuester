using DFK;
using PirateQuester.DFK.Contracts;
using PirateQuester.Utils;
using System.Numerics;

namespace PirateQuester.Bot
{
	public class DFKBotHero
	{
		public DFKBotHero(Hero h, DFKBotSettings settings)
		{
			Hero = h;
			Account = h.DFKAccount;
			ID = new BigInteger(long.Parse(h.id));

			var questSettings = settings.HeroQuestSettings.FirstOrDefault(hqs => hqs.HeroId == h.id && hqs.ChainIdentifier == Account.Chain.Identifier);
			var chainQuestSettings = settings.ChainQuestEnabled.FirstOrDefault(cq => cq.Chain.Name == Account.Chain.Name);
			if (chainQuestSettings.QuestEnabled.All(qe => qe.Enabled is false))
			{
				return;
			}
			var chainQuests = QuestContractDefinitions.DFKQuestContracts.First(qc => qc.Chain.Name == Account.Chain.Name).QuestContracts;
			if (questSettings != null)
			{
				StaminaPotionUntilLevel = questSettings.StaminaPotionUntilLevel;
				UseStaminaPotionsAmount = questSettings.UseStaminaPotionsAmount;
				LevelingEnabled = questSettings.LevelingEnabled;
				BotSalePrice = questSettings.BotSalePrice;
				var lvlSettings = questSettings.LevelupSettings;
				if (lvlSettings is not null)
				{
					if (lvlSettings.PrimaryAttribute.HasValue && lvlSettings.SecondaryAttribute.HasValue && lvlSettings.TertiaryAttribute.HasValue)
					{
						LevelUpSetting.MainAttribute = Constants.DFKStats[lvlSettings.PrimaryAttribute.Value];
						LevelUpSetting.SecondaryAttribute = Constants.DFKStats[lvlSettings.SecondaryAttribute.Value];
						LevelUpSetting.TertiaryAttribute = Constants.DFKStats[lvlSettings.TertiaryAttribute.Value];
					}
				}
				if (questSettings.QuestId.HasValue
					&& questSettings.ChainIdentifier == Account.Chain.Identifier
					&& chainQuestSettings.QuestEnabled[questSettings.QuestId.Value].Enabled)
				{
					Quest = chainQuests[questSettings.QuestId.Value];
				}
			}
			List<int> stats = new()
			{
				h.strength + (h.statBoost1 == 0 ? 1 : 0) + (h.statBoost2 == 0 ? 2 : 0),
				h.dexterity + (h.statBoost1 == 14 ? 1 : 0) + (h.statBoost2 == 14 ? 2 : 0),
				h.agility + (h.statBoost1 == 2 ? 1 : 0) + (h.statBoost2 == 2 ? 2 : 0),
				h.vitality + (h.statBoost1 == 10 ? 1 : 0) + (h.statBoost2 == 10 ? 2 : 0),
				h.endurance + (h.statBoost1 == 12 ? 1 : 0) + (h.statBoost2 == 12 ? 2 : 0),
				h.intelligence + (h.statBoost1 == 4 ? 1 : 0) + (h.statBoost2 == 4 ? 2 : 0),
				h.wisdom + (h.statBoost1 == 6 ? 1 : 0) + (h.statBoost2 == 6 ? 2 : 0),
				h.luck + (h.statBoost1 == 8 ? 1 : 0) + (h.statBoost2 == 8 ? 2 : 0),
			};

			int highestStat = stats.Max();

			if (highestStat >= settings.MinTrainingStats[stats.IndexOf(highestStat)].Amount
				&& chainQuestSettings.QuestEnabled[stats.IndexOf(highestStat)].Enabled)
			{
				SuggestedQuest = chainQuests[stats.IndexOf(highestStat)];
			}
			else
			{
				SuggestedQuest = chainQuests
					.FirstOrDefault(q => q.Name.ToLower().Contains(h.profession)
					&& chainQuestSettings.QuestEnabled[q.Id].Enabled);

				if (SuggestedQuest is not null)
				{
					if (SuggestedQuest.Category == "Mining"
						&& Account.LockedPowerTokenBalance is not 0
						&& chainQuestSettings.QuestEnabled.First(qe => qe.QuestId == 9).Enabled)
					{
						var lockedMining = chainQuests.First(qc => qc.Id == 9);
						int lockedMiners = Account.BotHeroes.Where(bh => (bh.SuggestedQuest?.Id ?? 0) == 9).Count();
						if (lockedMiners < lockedMining.MaxHeroesPerQuest(Account) * 3)
						{
							SuggestedQuest = lockedMining;
							return;
						}
					}

					if (SuggestedQuest.Category == "Gardening")
					{
						var gardeningQuests = chainQuests
							.Where(q => q.Category == "Gardening"
							&& chainQuestSettings.QuestEnabled[q.Id].Enabled).ToList();

						int minAssigned = gardeningQuests.Min(gq => Hero.DFKAccount.BotHeroes.Where(botHero => (botHero.SuggestedQuest?.Id ?? 0) == gq.Id).Count());

						SuggestedQuest = gardeningQuests.FirstOrDefault(gq => Hero.DFKAccount.BotHeroes.Where(botHero => (botHero.SuggestedQuest?.Id ?? 0) == gq.Id).Count() == minAssigned);
					}
					return;
				}
				if (Account.LockedPowerTokenBalance is not 0
					&& chainQuestSettings.QuestEnabled[9].Enabled)
				{
					var lockedMining = chainQuests[9];
					try
					{

						int lockedMiners = Account.BotHeroes.Where(bh => (bh.SuggestedQuest?.Id ?? 0) == 9).Count();
						if (lockedMiners < lockedMining.MaxHeroesPerQuest(Account) * 3)
						{
							SuggestedQuest = lockedMining;
							return;
						}
					}
					catch(Exception e)
					{

					}
				}
				for (int i = 0; i < 8; ++i)
				{
					if (stats[i] > settings.MinTrainingStats[i].Amount && chainQuestSettings.QuestEnabled[i].Enabled)
					{
						SuggestedQuest = chainQuests[i];
						return;
					}
				}
				if (chainQuestSettings.QuestEnabled[8].Enabled && h.mining > 0 && Account.BotHeroes.Where(bh => (bh.SuggestedQuest?.Id ?? 0) == 8).Count() < 18)
				{
					SuggestedQuest = chainQuests[8];
					return;
				}
				int foragers = 0;

				foragers = Account.BotHeroes.Where(bh => (bh.SuggestedQuest?.Id ?? 0) == 11).Count();

				var fishers = Account.BotHeroes.Where(bh => (bh.SuggestedQuest?.Id ?? 0) == 10).Count();
				if (foragers > fishers && chainQuestSettings.QuestEnabled[10].Enabled)
				{
					SuggestedQuest = chainQuests[10];
					return;
				}
				else if (chainQuestSettings.QuestEnabled[11].Enabled)
				{
					SuggestedQuest = chainQuests[11];
					return;
				}
			}
		}

		public List<string> FloorEstimateFields { get; set; } = new() { "default" };
		public decimal FloorEstimate { get; set; } = 99999;
        public string FloorEstimateNetwork { get; set; }
        public LevelUpSetting LevelUpSetting { get; set; } = new();
		public int? StaminaPotionUntilLevel { get; set; }
		public int? UseStaminaPotionsAmount { get; set; }
		public DateTime? StaminaPotionedLast { get; set; }
        public bool? LevelingEnabled { get; set; }
		public decimal? BotSalePrice { get; set; }
		public DFKAccount Account { get; set; }
		public BigInteger ID { get; set; }
		public Hero Hero { get; set; }
		public QuestContract GetActiveQuest()
		{
			if (Quest is not null)
			{
				return Quest;
			}
			return SuggestedQuest;
		}

		internal bool GetActiveQuestEquals(int id)
		{
			if (Quest is not null)
			{
				return Quest.Id == id;
			}
			if (SuggestedQuest is not null)
			{
				return SuggestedQuest.Id == id;
			}
			return false;
		}

		public QuestContract SuggestedQuest { get; set; }
		private QuestContract quest;
		public QuestContract Quest { get { return quest; } set { quest = value; } }
	}
}
