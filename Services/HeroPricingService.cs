using DFK;

using DFKHeroPricingAPI;
using Microsoft.JSInterop;
using PirateQuester.Bot;
using PirateQuester.Utils;
using Polly;
using System.Net.Http.Json;
using System.Text.Json;

namespace PirateQuester.Services;

public class HeroPricingService
{
    public IJSInProcessRuntime JS { get; }
    public List<Hero> AuctionData { get; set; }
	public HeroPricingService(IJSInProcessRuntime js)
	{
        JS = js;
    }


    public async Task UpdateHeroPrices(List<DFKAccount> accountsToUpdate)
    {
        var retryPolicy = Policy
        .Handle<Exception>()
        .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        
        await retryPolicy.ExecuteAsync(async () =>
        {
            using HttpClient client = new HttpClient();
            
            var uri = new Uri("https://pricingapi.piratequester.com/priceHeroes");
            var heroes = accountsToUpdate.SelectMany(acc => acc.Heroes).Select(hero => new
            {
                hero.id,
                mainClass = Constants.GetClassId(hero.mainClass),
                subClass = Constants.GetClassId(hero.subClass),
                hero.rarity,
                hero.generation,
                hero.level,
                hero.summonsRemaining,
                hero.maxSummons,
                profession = Constants.GetProfessionId(hero.profession)
            }).ToList();
            HttpResponseMessage response = await client.PostAsJsonAsync(uri, heroes);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();

                var pricedHeroes = JsonSerializer.Deserialize<List<PricedHero>>(data);
                
                foreach (DFKBotHero botHero in accountsToUpdate.SelectMany(acc => acc.BotHeroes))
                {
                    var pricedHero = pricedHeroes.FirstOrDefault(ph => ph.Id.ToString() == botHero.ID.ToString());
                    if (pricedHero is not null)
                    {
                        botHero.FloorEstimate = pricedHero.EstimatedFloorPrice;
                        botHero.FloorEstimateFields = pricedHero.EstimationParameters;
                        botHero.FloorEstimateNetwork = pricedHero.Network;
                    }
                    else
                    {
                        botHero.FloorEstimate = 99999;
                        botHero.FloorEstimateFields = new() { "Failed to fetch price" };
                        botHero.FloorEstimateNetwork = pricedHero.Network;
                    }
                }
            }
            else
            {
                foreach (DFKBotHero botHero in accountsToUpdate.SelectMany(acc => acc.BotHeroes))
                {
                    botHero.FloorEstimate = 999999;
                    botHero.FloorEstimateFields = new() { "Failed to fetch price"};
                }
            }
        });
    }
}
