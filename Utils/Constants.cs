namespace PirateQuester.Utils
{
	public static class Constants
	{
		public static readonly string PagingSummaryFormat = "Displaying page {0} of {1} (total {2} records)";
		public static List<DFKStat> DFKStats = new()
		{
			{ new(0, "Strength") },
			{ new(1, "Dexterity") },
			{ new(2, "Agility") },
			{ new(3, "Vitality") },
			{ new(4, "Endurance") },
			{ new(5, "Intelligence") },
			{ new(6, "Wisdom") },
			{ new(7, "Luck") }
		};
		public static List<string> Classes = new()
		{
			  "Warrior",
			  "Knight",
			  "Thief",
			  "Archer",
			  "Pirate",
			  "Monk",
			  "Priest",
			  "Wizard",
			  "Seer",
			  "Berserker",
			  "Paladin",
			  "DarkKnight",
			  "Ninja",
			  "Summoner",
			  "Shapeshifter",
			  "Dragoon",
			  "Sage",
			  "DreadKnight"
		};
	}
}
