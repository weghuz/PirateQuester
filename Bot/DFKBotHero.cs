using DFK;
using PirateQuester.DFK.Contracts;

namespace PirateQuester.Bot
{
    public class DFKBotHero
    {
        public DFKBotHero(Hero h) 
        { 
            Hero = h;
            SuggestedQuest = ContractDefinitions.DFKQuestContracts.FirstOrDefault(q => q.Name.ToLower().Contains(h.profession));
        }
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
