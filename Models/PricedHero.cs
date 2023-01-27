using System.Text.Json.Serialization;

namespace DFKHeroPricingAPI;

public class PricedHero
{
    [JsonPropertyName("id")]
    public long Id { get; set; }
    [JsonPropertyName("estimatedFloorPrice")]
    public decimal EstimatedFloorPrice { get; set; }
    [JsonPropertyName("estimationParameters")]
    public List<string> EstimationParameters { get; set; }
    [JsonPropertyName("network")]
    public string Network { get; set; }
}