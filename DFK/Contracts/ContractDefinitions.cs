namespace PirateQuester.DFK.Contracts;

public static class ContractDefinitions
{
	public static QuestContract GetQuestContract(int questContractId)
	{
		return DFKQuestContracts.FirstOrDefault(quest => quest.Id == questContractId);
	}
	public static QuestContract GetQuestContract(QuestType questContract)
	{
		return DFKQuestContracts.FirstOrDefault(quest => quest.Id == (int)questContract);
	}
	public static QuestContract GetQuestContract(string questContractName)
	{
		return DFKQuestContracts.FirstOrDefault(quest => quest.Name == questContractName);
	}
	public static QuestContract GetQuestContractFromAddress(string questContractAddress)
	{
		return DFKQuestContracts.FirstOrDefault(quest => quest.Address == questContractAddress);
	}
	public static readonly string NULL_ADDRESS = "0x0000000000000000000000000000000000000000";
	public static readonly List<QuestContract> DFKQuestContracts = new()
    {
        new QuestContract
        {
            Id = 1,
            Address = "0xb8828c687Fb1C875D5acb4281C5CDf9F49fA4637",
            Name = "TRAINING_STRENGTH",
            Category = "Training",
            Subcategory = "Strength",
            Level = 1,
			AvailableAttempts = (hero) => hero.StaminaCurrent()/5,
            BlocksPerAttempt = (hero) => 10
        },
        new QuestContract
        {
            Id = 2,
            Address = "0x9ec92963d0387bA57D5f2D505319b1c135C6f1D3",
            Name = "TRAINING_DEXTERITY",
            Category = "Training",
            Subcategory = "Dexterity",
            Level = 1,
			AvailableAttempts = (hero) => hero.StaminaCurrent()/5,
			BlocksPerAttempt = (hero) => 10
		},
        new QuestContract
        {
            Id = 3,
            Address = "0x801b7296f106d8818DA1D04Ed769e5a76e8911fe",
            Name = "TRAINING_AGILITY",
            Category = "Training",
            Subcategory = "Agility",
            Level = 1,
			AvailableAttempts = (hero) => hero.StaminaCurrent()/5,
			BlocksPerAttempt = (hero) => 10
		},
        new QuestContract
        {
            Id = 4,
            Address = "0xE3edf52D33F2BB05DBdA5BA73903E27a9B9b7e9d",
            Name = "TRAINING_VITALITY",
            Category = "Training",
            Subcategory = "Vitality",
            Level = 1,
			AvailableAttempts = (hero) => hero.StaminaCurrent()/5,
			BlocksPerAttempt = (hero) => 10
		},
        new QuestContract
        {
            Id = 5,
            Address = "0xBD391e4641E1bce989a246602EcDC746efA9d845",
            Name = "TRAINING_ENDURANCE",
            Category = "Training",
            Subcategory = "Endurance",
            Level = 1,
			AvailableAttempts = (hero) => hero.StaminaCurrent()/5,
			BlocksPerAttempt = (hero) => 10
		},
        new QuestContract
        {
            Id = 6,
            Address = "0x0832A218c2202088A1800D424248fC689ae74600",
            Name = "TRAINING_WISDOM",
            Category = "Training",
            Subcategory = "Wisdom",
            Level = 1,
			AvailableAttempts = (hero) => hero.StaminaCurrent()/5,
			BlocksPerAttempt = (hero) => 10
		},
        new QuestContract
        {
            Id = 7,
            Address = "0xD8cCf866959830a8E397442B5F7DDD790F230962",
            Name = "TRAINING_INTELLIGENCE",
            Category = "Training",
            Subcategory = "Intelligence",
            Level = 1,
			AvailableAttempts = (hero) => hero.StaminaCurrent()/5,
			BlocksPerAttempt = (hero) => 10
		},
        new QuestContract
        {
            Id = 8,
            Address = "0x81fA8a2bfcd703dc83c5d4bEE1075899448A5CdE",
            Name = "TRAINING_LUCK",
            Category = "Training",
            Subcategory = "Luck",
            Level = 1,
			AvailableAttempts = (hero) => hero.StaminaCurrent()/5,
			BlocksPerAttempt = (hero) => 10
		},
        new QuestContract
        {
            Id = 9,
            Address = "0x75912145f5cFEfb980616FA47B2f103210FaAb94",
            Name = "GOLD_MINING",
            Category = "Mining",
            Subcategory = "Gold",
            Level = 0,
			AvailableAttempts = (hero) => 1,
			BlocksPerAttempt = (hero) => hero.profession == "mining" ? hero.StaminaCurrent() * 300 : hero.StaminaCurrent() * 360
		},
        new QuestContract
        {
            Id = 10,
            Address = "0x98b3C85ac3cC3EF36Ff25A9229857AbACE3e7410",
            Name = "CRYSTAL_MINING",
            Category = "Mining",
            Subcategory = "Crystal",
            Level = 0,
			AvailableAttempts = (hero) => 1,
			BlocksPerAttempt = (hero) => hero.profession == "mining" ? hero.StaminaCurrent() * 300 : hero.StaminaCurrent() * 360
		},
        new QuestContract
        {
            Id = 11,
            Address = "0x407ab39B3675f29A719476af6eb3B9E5d93969E6",
            Name = "FISHING",
            Category = "Fishing",
            Subcategory = "Fishing",
            Level = 0,
			AvailableAttempts = (hero) => hero.profession == "fishing" ? hero.StaminaCurrent()/5 : hero.StaminaCurrent()/7,
			BlocksPerAttempt = (hero) => 10
		},
        new QuestContract
        {
            Id = 12,
            Address = "0xAd51199B453075C73FA106aFcAAD59f705EF7872",
            Name = "FORAGING",
            Category = "Foraging",
            Subcategory = "Foraging",
            Level = 0,
			AvailableAttempts = (hero) => hero.profession == "foraging" ? hero.StaminaCurrent()/5 : hero.StaminaCurrent()/7,
			BlocksPerAttempt = (hero) => 10
		},
        new QuestContract
        {
            Id = 13,
            Address = "0x8eDA0ceA7a90E794B33708Cc0768727A1A612f3d",
            Name = "GARDENING_CRYSTAL-AVAX",
            Category = "Gardening",
            Subcategory = "Crystal-Avax",
            Level = 0,
			AvailableAttempts = (hero) => 1,
			BlocksPerAttempt = (hero) => hero.profession == "gardening" ? hero.StaminaCurrent() * 300 : hero.StaminaCurrent() * 360
		},
        new QuestContract
        {
            Id = 14,
            Address = "0xC4839Fb9A5466878168EaE3fD58c647B71475b61",
            Name = "GARDENING_CRYSTAL-wJEWEL",
            Category = "Gardening",
            Subcategory = "Crystal-wJewel",
            Level = 0,
			AvailableAttempts = (hero) => 1,
			BlocksPerAttempt = (hero) => hero.profession == "gardening" ? hero.StaminaCurrent() * 300 : hero.StaminaCurrent() * 360
		},
        new QuestContract
        {
            Id = 15,
            Address = "0x6FEF23498877bC4c3940ebE121dd7D138BdA4e11",
            Name = "GARDENING_CRYSTAL-USDC",
            Category = "Gardening",
            Subcategory = "Crystal-USDC",
            Level = 0,
			AvailableAttempts = (hero) => 1,
			BlocksPerAttempt = (hero) => hero.profession == "gardening" ? hero.StaminaCurrent() * 300 : hero.StaminaCurrent() * 360
		},
        new QuestContract
        {
            Id = 16,
            Address = "0xdeF7cBeE7d0B62037616ee26BCAc1C8364f53476",
            Name = "GARDENING_ETH-USDC",
            Category = "Gardening",
            Subcategory = "Eth-USDC",
            Level = 0,
			AvailableAttempts = (hero) => 1,
			BlocksPerAttempt = (hero) => hero.profession == "gardening" ? hero.StaminaCurrent() * 300 : hero.StaminaCurrent() * 360
		},
        new QuestContract
        {
            Id = 17,
            Address = "0xaac3933Faa3B668304C9276d10CA88853463BD42",
            Name = "GARDENING_wJEWEL-USDC",
            Category = "Gardening",
            Subcategory = "wJewel-USDC",
            Level = 0,
			AvailableAttempts = (hero) => 1,
			BlocksPerAttempt = (hero) => hero.profession == "gardening" ? hero.StaminaCurrent() * 300 : hero.StaminaCurrent() * 360
		},
        new QuestContract
        {
            Id = 17,
            Address = "0x810e1fF51fDd58c474c66A31013713D1A17BF458",
            Name = "GARDENING_CRYSTAL-ETH",
            Category = "Gardening",
            Subcategory = "Crystal-Eth",
            Level = 0,
			AvailableAttempts = (hero) => 1,
			BlocksPerAttempt = (hero) => hero.profession == "gardening" ? hero.StaminaCurrent() * 300 : hero.StaminaCurrent() * 360
		},
        new QuestContract
        {
            Id = 18,
            Address = "0x706916dbC3b66d89632708CC193080ea05E0534A",
            Name = "GARDENING_CRYSTAL-BTC.b",
            Category = "Gardening",
            Subcategory = "Crystal-Btc.b",
            Level = 0,
			AvailableAttempts = (hero) => 1,
			BlocksPerAttempt = (hero) => hero.profession == "gardening" ? hero.StaminaCurrent() * 300 : hero.StaminaCurrent() * 360
		},
        new QuestContract
        {
            Id = 19,
            Address = "0x1fCc67a01525fd715A67bCcbF73665Fb3dBE76c7",
            Name = "GARDENING_CRYSTAL-KLAY",
            Category = "Gardening",
            Subcategory = "Crystal-Klay",
            Level = 0,
			AvailableAttempts = (hero) => 1,
			BlocksPerAttempt = (hero) => hero.profession == "gardening" ? hero.StaminaCurrent() * 300 : hero.StaminaCurrent() * 360
		},
        new QuestContract
        {
            Id = 20,
            Address = "0x2A70aA48f9dBF859239ae5E7f98fe95aE27A6CD4",
            Name = "GARDENING_JEWEL-KLAY",
            Category = "Gardening",
            Subcategory = "Jewel-Klay",
            Level = 0,
			AvailableAttempts = (hero) => 1,
			BlocksPerAttempt = (hero) => hero.profession == "gardening" ? hero.StaminaCurrent() * 300 : hero.StaminaCurrent() * 360
		},
        new QuestContract
        {
            Id = 21,
            Address = "0xA0d17554F09047d65E0ae0e76CD8923A9525183c",
            Name = "GARDENING_JEWEL-AVAX",
            Category = "Gardening",
            Subcategory = "Jewel-Avax",
            Level = 0,
			AvailableAttempts = (hero) => 1,
			BlocksPerAttempt = (hero) => hero.profession == "gardening" ? hero.StaminaCurrent() * 300 : hero.StaminaCurrent() * 360
		},
        new QuestContract
        {
            Id = 22,
            Address = "0x3391B9384AC66C7Aa3BF4A75A4f441942B1dCf30",
            Name = "GARDENING_JEWEL-BTC.b",
            Category = "Gardening",
            Subcategory = "Jewel-Btc.b",
            Level = 0,
			AvailableAttempts = (hero) => 1,
			BlocksPerAttempt = (hero) => hero.profession == "gardening" ? hero.StaminaCurrent() * 300 : hero.StaminaCurrent() * 360
		},
        new QuestContract
        {
            Id = 22,
            Address = "0xbaEc39Dd81b964B57bc5fa5f5421Cd82185409E6",
            Name = "GARDENING_JEWEL-ETH",
            Category = "Gardening",
            Subcategory = "Jewel-Eth",
            Level = 0,
			AvailableAttempts = (hero) => 1,
			BlocksPerAttempt = (hero) => hero.profession == "gardening" ? hero.StaminaCurrent() * 300 : hero.StaminaCurrent() * 360
		},
        new QuestContract
        {
            Id = 23,
            Address = "0x045838dBfb8026520E872c8298F4Ed542B81Eaca",
            Name = "GARDENING_BTC.b-USDC",
            Category = "Gardening",
            Subcategory = "Btc.b-USDC",
            Level = 0,
			AvailableAttempts = (hero) => 1,
			BlocksPerAttempt = (hero) => hero.profession == "gardening" ? hero.StaminaCurrent() * 300 : hero.StaminaCurrent() * 360
		},
    };
}