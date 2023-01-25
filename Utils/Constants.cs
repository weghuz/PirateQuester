using PirateQuester.Utils.Chain;

namespace PirateQuester.Utils
{
	public static class Constants
	{
		public static string DARK_THEME { get; } = "css/fabric-dark.css";
		public static string THEME { get; } = "css/fabric.css";
		public static string VERSION { get; } = "2.1.0";
		public static string PQT_ADDRESS { get; } = "0xAC2b4Ffe04AB34e43e780Dad5C8DEac47B7db775";
		public static List<Chain.Chain> ChainsList { get; } = new()
		{
			new()
			{
				Id = 0,
				ChainId = 53935,
				Name = "DFK",
				Identifier = "dfk",
				RPC = "https://subnets.avax.network/defi-kingdoms/dfk-chain/rpc",
				ChainEnum = ChainEnum.DFK,
				Enabled = true,
				HeroAddress = "0xEb9B61B145D6489Be575D3603F4a704810e143dF",
				QuestAddress = "0xE9AbfBC143d7cef74b5b793ec5907fa62ca53154",
				MeditationAddress = "0xD507b6b299d9FC835a0Df92f718920D13fA49B47",
				QuestRewarder = "0x08D93Db24B783F8eBb68D7604bF358F5027330A6",
				HeroSale = "0xc390fAA4C7f66E4D62E59C231D5beD32Ff77BEf0",
				NativeToken = "0x04b9dA42306B023f3572e106B11D82aAd9D32EBb",
			},
			new()
			{
				Id = 1,
				ChainId = 8217,
				Name = "Klaytn",
				Identifier = "kla",
				RPC = "https://klaytn.rpc.defikingdoms.com",
				ChainEnum = ChainEnum.Klaytn,
				Enabled = true,
				HeroAddress = "0x268CC8248FFB72Cd5F3e73A9a20Fa2FF40EfbA61",
				QuestAddress = "0x8dc58d6327E1f65b18B82EDFb01A361f3AAEf624",
				MeditationAddress = "0xdbEE8C336B06f2d30a6d2bB3817a3Ae0E34f4900",
				QuestRewarder = "0x3fAB563BD19CaFbf8717Cd99a605b3661Cf3391f",
				HeroSale = "0x7F2B66DB2D02f642a9eb8d13Bc998d441DDe17A8",
				NativeToken = "0xB3F5867E277798b50ba7A71C0b24FDcA03045eDF",
			},

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
		public static string GetClass(int id) => id switch
		{
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
