using Nethereum.ABI.FunctionEncoding.Attributes;
using System.Numerics;

namespace DFKContracts.HeroCore.ContractDefinition
{
	public partial class HeroCrystal : HeroCrystalBase { }

	public class HeroCrystalBase
	{
		[Parameter("address", "owner", 1)]
		public virtual string Owner { get; set; }
		[Parameter("uint256", "summonerId", 2)]
		public virtual BigInteger SummonerId { get; set; }
		[Parameter("uint256", "assistantId", 3)]
		public virtual BigInteger AssistantId { get; set; }
		[Parameter("uint16", "generation", 4)]
		public virtual ushort Generation { get; set; }
		[Parameter("uint256", "createdBlock", 5)]
		public virtual BigInteger CreatedBlock { get; set; }
		[Parameter("uint256", "heroId", 6)]
		public virtual BigInteger HeroId { get; set; }
		[Parameter("uint8", "summonerTears", 7)]
		public virtual byte SummonerTears { get; set; }
		[Parameter("uint8", "assistantTears", 8)]
		public virtual byte AssistantTears { get; set; }
		[Parameter("address", "enhancementStone", 9)]
		public virtual string EnhancementStone { get; set; }
		[Parameter("uint32", "maxSummons", 10)]
		public virtual uint MaxSummons { get; set; }
		[Parameter("uint32", "firstName", 11)]
		public virtual uint FirstName { get; set; }
		[Parameter("uint32", "lastName", 12)]
		public virtual uint LastName { get; set; }
		[Parameter("uint8", "shinyStyle", 13)]
		public virtual byte ShinyStyle { get; set; }
	}
}
