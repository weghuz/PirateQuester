using PirateQuester.DFK.Contracts;

namespace PirateQuester.Bot
{
	public class HeroQuestSetting
	{
        public HeroQuestSetting() { }
        public string HeroId { get; set; }
		public int? QuestId { get; set; }
		public string ChainIdentifier { get; set; }
        public decimal? BotSalePrice { get; set; }
        public bool? LevelingEnabled { get; set; }
        public int? StaminaPotionUntilLevel { get; set; }
        public int? UseStaminaPotionsAmount { get; set; }
        public HeroLevelupSettings LevelupSettings { get; set; }
	}
}