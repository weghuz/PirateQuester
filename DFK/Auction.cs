namespace DFK;
public class Auction
{
    public string id { get; set; }
    public Profile seller { get; set; }
    public Hero tokenId { get; set; }
    public string startingPrice { get; set; }
    public string endingPrice { get; set; }
    public int duration { get; set; }
    public int startedAt { get; set; }
    public Profile winner { get; set; }
    public int endedAt { get; set; }
    public bool open { get; set; }
    public string purchasePrice { get; set; }
}