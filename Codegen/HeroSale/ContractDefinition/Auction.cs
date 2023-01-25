using Nethereum.ABI.FunctionEncoding.Attributes;
using System.Numerics;

namespace PirateQuester.HeroSale.ContractDefinition
{
	public partial class Auction : AuctionBase { }

	public class AuctionBase
	{
		[Parameter("address", "seller", 1)]
		public virtual string Seller { get; set; }
		[Parameter("uint256", "tokenId", 2)]
		public virtual BigInteger TokenId { get; set; }
		[Parameter("uint128", "startingPrice", 3)]
		public virtual BigInteger StartingPrice { get; set; }
		[Parameter("uint128", "endingPrice", 4)]
		public virtual BigInteger EndingPrice { get; set; }
		[Parameter("uint64", "duration", 5)]
		public virtual ulong Duration { get; set; }
		[Parameter("uint64", "startedAt", 6)]
		public virtual ulong StartedAt { get; set; }
		[Parameter("address", "winner", 7)]
		public virtual string Winner { get; set; }
		[Parameter("bool", "open", 8)]
		public virtual bool Open { get; set; }
	}
}
