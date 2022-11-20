using DFK;

namespace PirateQuester.Bot
{
    public class DFKBotSettings
    {
        public int UpdateInterval { get; set; } = 180;
        public int MinStamina { get; set; } = 20;
        public int MaxGasFeeGwei { get; set; } = 200;
        public bool LevelUp { get; set; } = false;
		public List<LevelUpSetting> LevelUpSettings { get; set; } = new()
		{
			new()
			{
				HeroClass = "warrior"
			},
			new()
			{
				HeroClass = "knight"
			},
			new()
			{
				HeroClass = "thief"
			},
			new()
			{
				HeroClass = "archer"
			},
			new()
			{
				HeroClass = "pirate"
			},
			new()
			{
				HeroClass = "monk"
			},
			new()
			{
				HeroClass = "priest"
			},
			new()
			{
				HeroClass = "priest"
			},
			new()
			{
				HeroClass = "wizard"
			},
			new()
			{
				HeroClass = "seer"
			},
			new()
			{
				HeroClass = "berserker"
			},
			new()
			{
				HeroClass = "paladin"
			},
			new()
			{
				HeroClass = "darkknight"
			},
			new()
			{
				HeroClass = "ninja"
			},
			new()
			{
				HeroClass = "summoner"
			},
			new()
			{
				HeroClass = "shapeshifter"
			},
			new()
			{
				HeroClass = "dragoon"
			},
			new()
			{
				HeroClass = "dreadknight"
			},
		};
        public List<LevelUpSetting> SuggestedLevelUpSettings { get; set; } = new()
		{
			new()
			{
				HeroClass = "warrior",
				MainAttribute = 0,
				SecondaryAttribute = 1,
				TertiaryAttribute = 2,
			},
			new()
			{
				HeroClass = "knight",
				MainAttribute = 0,
				SecondaryAttribute = 1,
				TertiaryAttribute = 2,
			},
			new()
			{
				HeroClass = "thief",
				MainAttribute = 0,
				SecondaryAttribute = 1,
				TertiaryAttribute = 2,
			},
			new()
			{
				HeroClass = "archer",
				MainAttribute = 0,
				SecondaryAttribute = 1,
				TertiaryAttribute = 2,
			},
			new()
			{
				HeroClass = "pirate",
				MainAttribute = 0,
				SecondaryAttribute = 1,
				TertiaryAttribute = 2,
			},
			new()
			{
				HeroClass = "monk",
				MainAttribute = 0,
				SecondaryAttribute = 1,
				TertiaryAttribute = 2,
			},
			new()
			{
				HeroClass = "priest",
				MainAttribute = 0,
				SecondaryAttribute = 1,
				TertiaryAttribute = 2,
			},
			new()
			{
				HeroClass = "priest",
				MainAttribute = 0,
				SecondaryAttribute = 1,
				TertiaryAttribute = 2,
			},
			new()
			{
				HeroClass = "wizard",
				MainAttribute = 0,
				SecondaryAttribute = 1,
				TertiaryAttribute = 2,
			},
			new()
			{
				HeroClass = "seer",
				MainAttribute = 0,
				SecondaryAttribute = 1,
				TertiaryAttribute = 2,
			},
			new()
			{
				HeroClass = "berserker",
				MainAttribute = 0,
				SecondaryAttribute = 1,
				TertiaryAttribute = 2,
			},
			new()
			{
				HeroClass = "paladin",
				MainAttribute = 0,
				SecondaryAttribute = 1,
				TertiaryAttribute = 2,
			},
			new()
			{
				HeroClass = "darkknight",
				MainAttribute = 0,
				SecondaryAttribute = 1,
				TertiaryAttribute = 2,
			},
			new()
			{
				HeroClass = "ninja",
				MainAttribute = 0,
				SecondaryAttribute = 1,
				TertiaryAttribute = 2,
			},
			new()
			{
				HeroClass = "summoner",
				MainAttribute = 0,
				SecondaryAttribute = 1,
				TertiaryAttribute = 2,
			},
			new()
			{
				HeroClass = "shapeshifter",
				MainAttribute = 0,
				SecondaryAttribute = 1,
				TertiaryAttribute = 2,
			},
			new()
			{
				HeroClass = "dragoon",
				MainAttribute = 0,
				SecondaryAttribute = 1,
				TertiaryAttribute = 2,
			},
			new()
			{
				HeroClass = "dreadknight",
				MainAttribute = 0,
				SecondaryAttribute = 1,
				TertiaryAttribute = 2,
			},
		};
    }
}