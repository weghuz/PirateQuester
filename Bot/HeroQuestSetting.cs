using PirateQuester.DFK.Contracts;

namespace PirateQuester.Bot
{
	public class HeroQuestSetting
	{
        public HeroQuestSetting() { }
        public string HeroId { get; set; }
		public int QuestId { get; set; }
		public string ChainIdentifier { get; set; }
	}
}