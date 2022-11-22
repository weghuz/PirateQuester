using DFK;
using PirateQuester.DFK.Contracts;
using System.Numerics;

namespace PirateQuester.Bot
{
    public class DFKBotHero
    {
        public DFKBotHero(Hero h, DFKBotSettings settings) 
        { 
            Hero = h;
            ID = new BigInteger(long.Parse(h.id));
			List<int> stats = new()
			{
				h.strength + (h.statBoost1 == "STR" ? 1 : 0) + (h.statBoost2 == "STR" ? 2 : 0),
				h.dexterity + (h.statBoost1 == "DEX" ? 1 : 0) + (h.statBoost2 == "DEX" ? 2 : 0),
				h.agility + (h.statBoost1 == "AGI" ? 1 : 0) + (h.statBoost2 == "AGI" ? 2 : 0),
				h.vitality + (h.statBoost1 == "VIT" ? 1 : 0) + (h.statBoost2 == "VIT" ? 2 : 0),
				h.endurance + (h.statBoost1 == "END" ? 1 : 0) + (h.statBoost2 == "END" ? 2 : 0),
				h.intelligence + (h.statBoost1 == "INT" ? 1 : 0) + (h.statBoost2 == "INT" ? 2 : 0),
				h.wisdom + (h.statBoost1 == "WIS" ? 1 : 0) + (h.statBoost2 == "WIS" ? 2 : 0),
				h.luck + (h.statBoost1 == "LCK" ? 1 : 0) + (h.statBoost2 == "LCK" ? 2 : 0),
			};
			int highestStat = stats.Max();

			if (highestStat >= settings.MinTrainingStats[stats.IndexOf(highestStat)].Amount)
			{
				SuggestedQuest = ContractDefinitions.DFKQuestContracts[stats.IndexOf(highestStat)];
			}
			else
			{
				SuggestedQuest = ContractDefinitions.DFKQuestContracts.FirstOrDefault(q => q.Name.ToLower().Contains(h.profession));
				if (SuggestedQuest.Category == "Gardening")
				{
					int assigned = Hero.DFKAccount.BotHeroes.Where(botHero => botHero.SuggestedQuest.Id == SuggestedQuest.Id).Count();
					if (assigned == 0)
					{
						return;
					}
					foreach (QuestContract quest in ContractDefinitions.DFKQuestContracts.Where(q => q.Category == "Gardening"))
					{
						int currentQuestAssigned = Hero.DFKAccount.BotHeroes.Where(botHero => botHero.SuggestedQuest.Id == quest.Id).Count();

						if (currentQuestAssigned < assigned)
						{
							SuggestedQuest = quest;
							break;
						}

					}
				}
			}
		}
		public BigInteger ID { get; set; }
		public Hero Hero { get; set; }
        public QuestContract GetActiveQuest()
        {
            if(Quest is not null)
            {
                return Quest;
            }
            return SuggestedQuest;
        }
        public QuestContract SuggestedQuest { get; set; }
        public QuestContract Quest { get; set; }
    }
}
