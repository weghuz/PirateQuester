using Microsoft.JSInterop;
using Newtonsoft.Json;
using PirateQuester.DFK.Contracts;

namespace DFK;
public class Hero
{
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
        QuestContract quest = ContractDefinitions.DFKQuestContracts.FirstOrDefault(quest => quest.Address == currentQuest);
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
        decimal fixedSalePrice = long.Parse(salePrice) / 1000000000000000000;
        return Math.Round(fixedSalePrice, decimals);
    }

    public static Hero Deserialize(string json)
    {
        return JsonConvert.DeserializeObject<Hero>(json);
    }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }

    public int StaminaCurrent(IJSInProcessRuntime js)
    {
        long now = js.Invoke<long>("GetTime", "") / 1000;
		now += 3600;
		if (now >= staminaFullAt)
        {
            return stamina;
        }
        else
        {
			decimal staminaLeft = (staminaFullAt - now) / 1200 ;
            Console.WriteLine(staminaLeft);
            return (int)Math.Round(staminaLeft, 0);
        }
    }

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
    public string subClass { get; set; }
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
    public string passive1 { get; set; }
    public string passive2 { get; set; }
    public string active1 { get; set; }
    public string active2 { get; set; }
    public string statBoost1 { get; set; }
    public string statBoost2 { get; set; }
    public string statsUnknown1 { get; set; }
    public string element { get; set; }
    public string statsUnknown2 { get; set; }
    public string gender { get; set; }
    public string headAppendage { get; set; }
    public string backAppendage { get; set; }
    public string background { get; set; }
    public string hairStyle { get; set; }
    public string hairColor { get; set; }
    public string visualUnknown1 { get; set; }
    public string eyeColor { get; set; }
    public string skinColor { get; set; }
    public string appendageColor { get; set; }
    public string backAppendageColor { get; set; }
    public string visualUnknown2 { get; set; }
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