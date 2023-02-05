using Nethereum.Web3;
using Newtonsoft.Json;
using PirateQuester.DFK.Contracts;
using PirateQuester.Utils;
using System.Numerics;

namespace DFK;
public class Hero
{
	public Hero() { }
	public Hero(DFKContracts.HeroCore.ContractDefinition.Hero hero)
	{
		id = hero.Id.ToString();
		rarity = hero.Info.Rarity;
		generation = hero.Info.Generation;
		firstName = (int)hero.Info.FirstName;
		lastName = (int)hero.Info.LastName;
		mainClass = Constants.GetClass(hero.Info.Class);
		subClass = Constants.GetClass(hero.Info.SubClass);

		staminaFullAt = (long)Functions.BigIntToLong(hero.State.StaminaFullAt);
		level = hero.State.Level;
		currentQuest = hero.State.CurrentQuest;

		strength = hero.Stats.Strength;
		dexterity = hero.Stats.Dexterity;
		agility = hero.Stats.Agility;
		vitality = hero.Stats.Vitality;
		endurance = hero.Stats.Endurance;
		wisdom = hero.Stats.Wisdom;
		intelligence = hero.Stats.Intelligence;
		luck = hero.Stats.Luck;
		stamina = hero.Stats.Stamina;
	}

	public void DecodeGenes()
	{
		byte[] decodedGenes = DecodeRecessiveGenes(BigInteger.Parse(statGenes));
		mainClass = Constants.GetClass(decodedGenes[3]);
		subClass = Constants.GetClass(decodedGenes[7]);
		profession = Constants.GetProfession(decodedGenes[11]);
		statBoost1 = Constants.GetStatBoost(decodedGenes[31]);
		statBoost2 = Constants.GetStatBoost(decodedGenes[35]);
	}

	public static byte[] DecodeRecessiveGenes(BigInteger genesBigInt)
	{
		var abc = "123456789abcdefghijkmnopqrstuvwx";
		var buf = "";
		byte bas = 32;

		while (genesBigInt >= bas)
		{
			BigInteger mod = genesBigInt % bas;
			buf += abc[int.Parse(mod.ToString())];
			genesBigInt = (genesBigInt - mod) / bas;
		}
		buf += abc[int.Parse(genesBigInt.ToString())];
		buf = buf.PadRight(48, '1');
		byte[] result = new byte[48];
		for (int i = 0; i < buf.Length; i += 1)
		{
			result[i] = (byte)abc.IndexOf(buf[i]);
		}
		return result.Reverse().ToArray();
	}

	public int XpToLevelUp()
	{
		var nextLevel = level + 1;
		int xpNeeded;
		if (nextLevel < 6)
		{
			xpNeeded = nextLevel * 1000;
		}
		else if (nextLevel < 9)
		{
			xpNeeded = 4000 + (nextLevel - 5) * 2000;
		}
		else if (level < 16)
		{
			xpNeeded = 12000 + (nextLevel - 9) * 4000;

		}
		else if (level < 36)
		{
			xpNeeded = 40000 + (nextLevel - 16) * 5000;
		}
		else if (level < 56)
		{
			xpNeeded = 140000 + (nextLevel - 36) * 7500;
		}
		else if (level > 56)
		{
			xpNeeded = 290000 + (nextLevel - 56) * 10000;
		}
		else
		{
			xpNeeded = 0;
		}
		return xpNeeded;
	}

	public void UpdateHeroValues(Hero h)
	{
		staminaFullAt = h.staminaFullAt;
		level = h.level;
		xp = h.xp;
		currentQuest = h.currentQuest;
		strength = h.strength;
		dexterity = h.dexterity;
		agility = h.agility;
		vitality = h.vitality;
		endurance = h.endurance;
		wisdom = h.wisdom;
		intelligence = h.intelligence;
		luck = h.luck;
		salePrice = h.salePrice;
		stamina = h.stamina;
	}

	public enum Rarity
	{
		Common,
		Uncommon,
		Rare,
		Legendary,
		Mythic
	}

	public string FullName()
	{
		return $"{(gender == "male" ? HeroData.MaleFirstNames[firstName] : HeroData.FemaleFirstNames[firstName])} {HeroData.LastNames[lastName]}";
	}

	public string Firstname()
	{
		return gender == "male" ? HeroData.MaleFirstNames[firstName] : HeroData.FemaleFirstNames[firstName];
	}

	public string GetCurrentQuestName()
	{
		QuestContract quest = QuestContractDefinitions.DFKQuestContracts.SelectMany(qc => qc.QuestContracts).FirstOrDefault(quest => quest.Address == currentQuest);
		if (quest is not null)
		{
			return $"{quest.Category}{(quest.Subcategory != quest.Category ? $":{quest.Subcategory}" : "")}";
		}
		return "";
	}

	public Rarity GetRarity()
	{
		return (Rarity)rarity;
	}

	public string Lastname()
	{
		return HeroData.LastNames[lastName];
	}

	public decimal SalePrice(int decimals)
	{
		if (salePrice is not null)
		{
			BigInteger price = BigInteger.Parse(salePrice);
			decimal fixedSalePrice = Web3.Convert.FromWei(price);
			return Math.Round(fixedSalePrice, decimals);
		}
		return 0;
	}

	public static Hero Deserialize(string json)
	{
		return JsonConvert.DeserializeObject<Hero>(json);
	}

	public override string ToString()
	{
		return JsonConvert.SerializeObject(this);
	}

	public int StaminaCurrent(int offset = 0)
	{
		long now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
		if (now >= staminaFullAt)
		{
			return stamina;
		}
		else
		{
			decimal staminaLeft = (staminaFullAt - now + offset) / 1200;
			return stamina - (int)Math.Floor(staminaLeft) - 1;
		}
	}

	public DFKAccount DFKAccount { get; set; }
	public string id { get; set; }
	public string numberId { get; set; }
	public Profile owner { get; set; }
	public Profile previousOwner { get; set; }
	public Profile creator { get; set; }
	public string statGenes { get; set; }
	public string visualGenes { get; set; }
	public int rarity { get; set; }
	public bool shiny { get; set; }
	public int generation { get; set; }
	public int firstName { get; set; }
	public int lastName { get; set; }
	public int shinyStyle { get; set; }
	public string mainClass { get; set; }
	public string mainClassStr { get; set; }
	public string subClass { get; set; }
	public string subClassStr { get; set; }
	public int summonedTime { get; set; }
	public int nextSummonTime { get; set; }
	public Hero summonerId { get; set; }
	public Hero assistantId { get; set; }
	public int summons { get; set; }
	public int maxSummons { get; set; }
	public long staminaFullAt { get; set; }
	public int hpFullAt { get; set; }
	public int mpFullAt { get; set; }
	public int level { get; set; }
	public int xp { get; set; }
	public string currentQuest { get; set; }
	public int sp { get; set; }
	public string status { get; set; }
	public int strength { get; set; }
	public int intelligence { get; set; }
	public int wisdom { get; set; }
	public int luck { get; set; }
	public int agility { get; set; }
	public int vitality { get; set; }
	public int endurance { get; set; }
	public int dexterity { get; set; }
	public int hp { get; set; }
	public int mp { get; set; }
	public int stamina { get; set; }
	public int strengthGrowthP { get; set; }
	public int intelligenceGrowthP { get; set; }
	public int wisdomGrowthP { get; set; }
	public int luckGrowthP { get; set; }
	public int agilityGrowthP { get; set; }
	public int vitalityGrowthP { get; set; }
	public int enduranceGrowthP { get; set; }
	public int dexterityGrowthP { get; set; }
	public int strengthGrowthS { get; set; }
	public int intelligenceGrowthS { get; set; }
	public int wisdomGrowthS { get; set; }
	public int luckGrowthS { get; set; }
	public int agilityGrowthS { get; set; }
	public int vitalityGrowthS { get; set; }
	public int enduranceGrowthS { get; set; }
	public int dexterityGrowthS { get; set; }
	public int hpSmGrowth { get; set; }
	public int hpRgGrowth { get; set; }
	public int hpLgGrowth { get; set; }
	public int mpSmGrowth { get; set; }
	public int mpRgGrowth { get; set; }
	public int mpLgGrowth { get; set; }
	public int mining { get; set; }
	public int gardening { get; set; }
	public int foraging { get; set; }
	public int fishing { get; set; }
	public string profession { get; set; }
	public string professionStr { get; set; }
	public string passive1 { get; set; }
	public string passive2 { get; set; }
	public string active1 { get; set; }
	public string active2 { get; set; }
	public string statBoost1 { get; set; }
	public string statBoost1StrDeprecated { get; set; }
	public string statBoost2 { get; set; }
	public string statBoost2StrDeprecated { get; set; }
	public string statsUnknown1 { get; set; }
	public string statsUnknown2 { get; set; }
	public string element { get; set; }
	public string gender { get; set; }
	public string headAppendage { get; set; }
	public string backAppendage { get; set; }
	public string background { get; set; }
	public string hairStyle { get; set; }
	public string hairColor { get; set; }
	public string visualUnknown1 { get; set; }
	public string visualUnknown2 { get; set; }
	public string eyeColor { get; set; }
	public string skinColor { get; set; }
	public string appendageColor { get; set; }
	public string backAppendageColor { get; set; }
	public Auction assistingAuction { get; set; }
	public string assistingPrice { get; set; }
	public Auction saleAuction { get; set; }
	public string salePrice { get; set; }
	public Profile privateAuctionProfile { get; set; }
	public int summonsRemaining { get; set; }
	public string pjStatus { get; set; }
	public int? pjLevel { get; set; }
	public Profile pjOwner { get; set; }
	public int? pjClaimStamp { get; set; }
	public string network { get; set; }
	public string originRealm { get; set; }
}