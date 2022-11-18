using DFK;
using PirateQuester.DFK.Contracts;
using System.Numerics;

namespace PirateQuester.Bot
{
    public class DFKBotHero
    {
        public DFKBotHero(Hero h) 
        { 
            Hero = h;
            ID = new BigInteger(long.Parse(h.id));
            SuggestedQuest = ContractDefinitions.DFKQuestContracts.FirstOrDefault(q => q.Name.ToLower().Contains(h.profession));
            if(SuggestedQuest.Category == "Gardening")
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
