using PirateQuester.Utils;

namespace PirateQuester.Bot
{
	public class DFKBotSettingsDTO
	{
		public int ClearLogsInterval { get; set; }
		public bool DownloadClearedLogs { get; set; }
		public List<DFKStatAmount> MinTrainingStats { get; set; }
		public bool CancelUnpricedHeroSales { get; set; }
		public bool SellHeroes { get; set; } = true;
		public int CancelTxnDelay { get; set; }
		public int UpdateInterval { get; set; }
		public int MinStamina { get; set; }
		public int MaxGasFeeGwei { get; set; }
        public int MaxGasFeeGweiKlaytn { get; set; }
        public int MaxPriorityFeeGwei { get; set; }
        public int MaxPriorityFeeGweiKlaytn { get; set; }
        public bool LevelUp { get; set; }
		public bool ForceStampotOnFullXP { get; set; }
		public bool UseStaminaPotions { get; set; }
		public bool QuestHeroes { get; set; }
		public List<HeroQuestSetting> HeroQuestSettings { get; set; } = new();
		public List<ChainQuestEnabled> ChainQuestEnabled { get; set; }
		public List<LevelUpSetting> LevelUpSettings { get; set; }
	}
}
