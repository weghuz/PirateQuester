using Newtonsoft.Json;
using System.Text;

namespace DFK;

public static class API
{
	private static HttpClient Client = new();
	private const string URL = "https://defi-kingdoms-community-api-gateway-co06z8vi.uc.gateway.dev/graphql";
	private static string HeroFields = @"id numberId owner {id name} previousOwner {id name} creator {id name} statGenes visualGenes rarity shiny generation firstName lastName shinyStyle mainClass subClass summonedTime nextSummonTime summonerId { id } assistantId { id } summons maxSummons staminaFullAt hpFullAt mpFullAt level xp currentQuest sp status strength intelligence wisdom luck agility vitality endurance dexterity hp mp stamina strengthGrowthP intelligenceGrowthP wisdomGrowthP luckGrowthP agilityGrowthP vitalityGrowthP enduranceGrowthP dexterityGrowthP strengthGrowthS intelligenceGrowthS wisdomGrowthS luckGrowthS agilityGrowthS vitalityGrowthS enduranceGrowthS dexterityGrowthS hpSmGrowth hpRgGrowth hpLgGrowth mpSmGrowth mpRgGrowth mpLgGrowth mining gardening foraging fishing profession passive1 passive2 active1 active2 statBoost1 statBoost2 statsUnknown1 element statsUnknown2 gender headAppendage backAppendage background hairStyle hairColor visualUnknown1 eyeColor skinColor appendageColor backAppendageColor visualUnknown2 assistingAuction {id} assistingPrice saleAuction {id} salePrice privateAuctionProfile {id} summonsRemaining pjStatus pjLevel pjOwner {id} pjClaimStamp network originRealm";
	public static void SetHeroFields(string NewFields)
	{
		HeroFields = NewFields;
	}

	public static async Task<Hero[]> GetHeroesAddress(string address)
	{
		Dictionary<HeroesArgument, string> request = new();
		request.Add(HeroesArgument.owner, address);
		string query = API.HeroesRequestBuilder(request);
		Hero[] heroes = await API.GetHeroes(query);
		return heroes;
	}

	public static async Task<Hero> GetHero(string id)
	{
		var data = new StringContent(HeroRequestBuilder(id), Encoding.UTF8, "application/json");

		for (int i = 0; i < 10; i++)
		{
			try
			{
				var httpData = await Client.PostAsync(URL, data);
				var response = JsonConvert.DeserializeObject<HeroResponse>(await httpData.Content.ReadAsStringAsync());
				return response.data.hero;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				await Task.Delay(i * 100);
			}
		}
		return new();
	}

	public static async Task<Hero[]> GetHeroes(string request)
	{
		var data = new StringContent(request, Encoding.UTF8, "application/json");

		for (int i = 0; i < 5; i++)
		{
			try
			{
				var httpData = await Client.PostAsync(URL, data);
				var response = JsonConvert.DeserializeObject<HeroesResponse>(await httpData.Content.ReadAsStringAsync());
				foreach (Hero hero in response.data.heroes)
				{
					hero.profession = hero.professionStr;
					hero.mainClass = hero.mainClassStr;
					hero.subClass = hero.subClassStr;
					//switch
					//{
					//  0 => "mining",
					//	2 => "gardening",
					//	4 => "fishing",
					//  6 => "foraging"
					//	_ => "foraging"
					//}
				}
				return response.data.heroes;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				await Task.Delay(i * i * 100);
			}
		}
		return Array.Empty<Hero>();
	}

	public static string HeroRequestBuilder(string id, string heroFields = null)
	{
		StringBuilder request = new();
		request.Append($"{{hero(id: \"{id}\"){{{(heroFields is not null ? heroFields : HeroFields)}}}}}");
		// cmd.W(request.ToString());
		return JsonConvert.SerializeObject(
			new
			{
				query = request.ToString()
			});
	}

	private static string GetArgument(HeroesArgument key, Dictionary<HeroesArgument, string> args)
	{
		KeyValuePair<HeroesArgument, string> pair = args.FirstOrDefault(pair => pair.Key == key);
		if (pair.Value is not null)
		{
			if (Enum.GetName(typeof(HeroesArgument), pair.Key).EndsWith("_in"))
			{
				return $"{Enum.GetName(typeof(HeroesArgument), pair.Key)}:{pair.Value}, ";
			}
			List<HeroesArgument> strings = new()
			{
				HeroesArgument.owner,
				HeroesArgument.owner_in,
				HeroesArgument.pjStatus,
				HeroesArgument.network
			};
			if (strings.Contains(key))
			{
				return $"{Enum.GetName(typeof(HeroesArgument), pair.Key)}:\"{pair.Value}\", ";
			}
			else
			{
				return $"{Enum.GetName(typeof(HeroesArgument), pair.Key)}:{pair.Value}, ";
			}
		}
		return "";
	}

	public static string HeroesRequestBuilder(Dictionary<HeroesArgument, string> args, string heroFields = null)
	{
		StringBuilder request = new();
		request.Append("{heroes(");
		request.Append(GetArgument(HeroesArgument.first, args));
		request.Append(GetArgument(HeroesArgument.skip, args));
		request.Append(GetArgument(HeroesArgument.orderBy, args));
		if (args.Select(arg => arg.Key).Where(arg => new HeroesArgument[] { HeroesArgument.first, HeroesArgument.orderBy }.All(allowedArg => allowedArg != arg)).Count() > 0)
		{
			request.Append("where: { ");
			request.Append(GetArgument(HeroesArgument.salePrice_not, args));
			request.Append(GetArgument(HeroesArgument.owner, args));
			request.Append(GetArgument(HeroesArgument.owner_in, args));
			request.Append(GetArgument(HeroesArgument.network, args));
			request.Append(GetArgument(HeroesArgument.background, args));
			request.Append(GetArgument(HeroesArgument.profession, args));
			request.Append(GetArgument(HeroesArgument.pjStatus, args));
			request.Append(GetArgument(HeroesArgument.rarity, args));
			request.Append(GetArgument(HeroesArgument.active1, args));
			request.Append(GetArgument(HeroesArgument.active2, args));
			request.Append(GetArgument(HeroesArgument.passive1, args));
			request.Append(GetArgument(HeroesArgument.passive2, args));
			request.Append(GetArgument(HeroesArgument.strength, args));
			request.Append(GetArgument(HeroesArgument.dexterity, args));
			request.Append(GetArgument(HeroesArgument.agility, args));
			request.Append(GetArgument(HeroesArgument.vitality, args));
			request.Append(GetArgument(HeroesArgument.endurance, args));
			request.Append(GetArgument(HeroesArgument.intelligence, args));
			request.Append(GetArgument(HeroesArgument.wisdom, args));
			request.Append(GetArgument(HeroesArgument.luck, args));
			request.Append(GetArgument(HeroesArgument.mining, args));
			request.Append(GetArgument(HeroesArgument.gardening, args));
			request.Append(GetArgument(HeroesArgument.fishing, args));
			request.Append(GetArgument(HeroesArgument.foraging, args));
			request.Append("}");
		}
		request.Append($"){{{(heroFields is not null ? heroFields : HeroFields)}}}}}");
		// cmd.W(request.ToString());
		return JsonConvert.SerializeObject(
			new
			{
				query = request.ToString()
			});
	}
}