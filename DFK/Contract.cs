using PirateQuester.DFK.Items;

namespace PirateQuester.DFK;

public class Contract
{

    public int Id;
	public List<ChainContract> Addresses { get; set; } = new();
	public string Name { get; set; }
}
