using PirateQuester.Utils.Chain;
using System.Net.NetworkInformation;

namespace PirateQuester.Utils
{
	public static class Constants
	{
        public static List<Chain.Chain> ChainsList { get; } = new()
        {
            new()
            {
                Id = 53935,
                Name = "DFK",
                RPC = "https://subnets.avax.network/defi-kingdoms/dfk-chain/rpc",
                Chains = Chains.DFK
            },
            new()
            {
                Id = 8217,
                Name = "Klaytn",
                RPC = "https://klaytn.rpc.defikingdoms.com",
                Chains = Chains.Klaytn
            }
        };
		public static List<DFKStat> DFKStats { get; } = new()
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
        public static List<string> Classes { get; } = new()
        {
            "Warrior",
            "Knight",
            "Thief",
            "Archer",
            "Priest",
            "Wizard",
            "Monk",
            "Pirate",
            "Berserker",
            "Seer",
            "Legionnaire",
            "Scholar",
            "Paladin",
            "DarkKnight",
            "Summoner",
            "Ninja",
            "Shapeshifter",
            "Bard",
            "Dragoon",
            "Sage",
            "Spellbow",
            "DreadKnight"
        };
		public static string GetClass(int id) => id switch {
            0 => "Warrior",
            1 => "Knight",
            2 => "Thief",
            3 => "Archer",
            4 => "Priest",
            5 => "Wizard",
            6 => "Monk",
            7 => "Pirate",
            8 => "Berserker",
            9 => "Seer",
            10 => "Legionnaire",
            11 => "Scholar",
            16 => "Paladin",
            17 => "DarkKnight",
            18 => "Summoner",
            19 => "Ninja",
            20 => "Shapeshifter",
            21 => "Bard",
            24 => "Dragoon",
            25 => "Sage",
            26 => "Spellbow",
            28 => "DreadKnight",
            _ => "Warrior"
        };
        public static string GetProfession(int id) => id switch
        {
            0 => "mining",
            2 => "gardening",
            4 => "fishing",
            _ => "foraging"
        };
        public static string GetStatBoost(int id) => id switch
        {
            2 => "AGI",
            4 => "INT",
            6 => "WIS",
            8 => "LCK",
            10 => "VIT",
            12 => "END",
            14 => "DEX",
            _ => "STR",
        };
    }
}
