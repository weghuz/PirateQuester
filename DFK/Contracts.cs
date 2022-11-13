namespace DFK;

public static class Contracts
{
	public class GardeningQuest
	{
		public int Pool_Id { get; set; }
		public string Pair { get; set; }
		public string Contract { get; set; }
	}
	public static readonly string TRAINING_STRENGTH = "0xb8828c687Fb1C875D5acb4281C5CDf9F49fA4637";
	public static readonly string TRAINING_DEXTERITY = "0x9ec92963d0387bA57D5f2D505319b1c135C6f1D3";
	public static readonly string TRAINING_AGILITY = "0x801b7296f106d8818DA1D04Ed769e5a76e8911fe";
	public static readonly string TRAINING_VITALITY = "0xE3edf52D33F2BB05DBdA5BA73903E27a9B9b7e9d";
	public static readonly string TRAINING_ENDURANCE = "0xBD391e4641E1bce989a246602EcDC746efA9d845";
	public static readonly string TRAINING_WISDOM = "0x0832A218c2202088A1800D424248fC689ae74600";
	public static readonly string TRAINING_INTELLIGENCE = "0xD8cCf866959830a8E397442B5F7DDD790F230962";
	public static readonly string TRAINING_LUCK = "0x81fA8a2bfcd703dc83c5d4bEE1075899448A5CdE";
	public static readonly string GOLD_MINING = "0x75912145f5cFEfb980616FA47B2f103210FaAb94";
	public static readonly string CRYSTAL_MINING = "0x98b3C85ac3cC3EF36Ff25A9229857AbACE3e7410";
	public static readonly string FISHING = "0x407ab39B3675f29A719476af6eb3B9E5d93969E6";
	public static readonly string FORAGING = "0xAd51199B453075C73FA106aFcAAD59f705EF7872";
	public static readonly GardeningQuest[] GARDENING_CONTRACTS = new GardeningQuest[] {
		new()
		{
			Pool_Id = 1,
			Pair = "CRYSTAL-AVAX",
			Contract = "0x8eDA0ceA7a90E794B33708Cc0768727A1A612f3d"
		},
		new()
		{
			Pool_Id = 2,
			Pair = "CRYSTAL-wJEWEL",
			Contract = "0xC4839Fb9A5466878168EaE3fD58c647B71475b61"
		},
		new()
		{
			Pool_Id = 3,
			Pair = "CRYSTAL-USDC",
			Contract = "0x6FEF23498877bC4c3940ebE121dd7D138BdA4e11"
		},
		new()
		{
			Pool_Id = 4,
			Pair = "ETH-USDC",
			Contract = "0xdeF7cBeE7d0B62037616ee26BCAc1C8364f53476"
		},
		new()
		{
			Pool_Id = 5,
			Pair = "wJEWEL-USDC",
			Contract = "0xaac3933Faa3B668304C9276d10CA88853463BD42"
		},
		new()
		{
			Pool_Id = 6,
			Pair = "CRYSTAL-ETH",
			Contract = "0x810e1fF51fDd58c474c66A31013713D1A17BF458"
		},
		new()
		{
			Pool_Id = 7,
			Pair = "CRYSTAL-BTC.b",
			Contract = "0x706916dbC3b66d89632708CC193080ea05E0534A"
		},
		new()
		{
			Pool_Id = 8,
			Pair = "CRYSTAL-KLAY",
			Contract = "0x1fCc67a01525fd715A67bCcbF73665Fb3dBE76c7"
		},
		new()
		{
			Pool_Id = 9,
			Pair = "JEWEL-KLAY",
			Contract = "0x2A70aA48f9dBF859239ae5E7f98fe95aE27A6CD4"
		},
		new()
		{
			Pool_Id = 10,
			Pair = "JEWEL-AVAX",
			Contract = "0xA0d17554F09047d65E0ae0e76CD8923A9525183c"
		},
		new()
		{
			Pool_Id = 11,
			Pair = "JEWEL-BTC.b",
			Contract = "0x3391B9384AC66C7Aa3BF4A75A4f441942B1dCf30"
		},
		new()
		{
			Pool_Id = 12,
			Pair = "JEWEL-ETH",
			Contract = "0xbaEc39Dd81b964B57bc5fa5f5421Cd82185409E6"
		},
		new()
		{
			Pool_Id = 13,
			Pair = "BTC.b-USDC",
			Contract = "0x045838dBfb8026520E872c8298F4Ed542B81Eaca"
		}
	};
}