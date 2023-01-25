using Nethereum.ABI.FunctionEncoding.Attributes;
using System.Numerics;

namespace PirateQuester.ItemConsumer.ContractDefinition
{
	public partial class SummoningInfo : SummoningInfoBase { }

	public class SummoningInfoBase
	{
		[Parameter("uint256", "summonedTime", 1)]
		public virtual BigInteger SummonedTime { get; set; }
		[Parameter("uint256", "nextSummonTime", 2)]
		public virtual BigInteger NextSummonTime { get; set; }
		[Parameter("uint256", "summonerId", 3)]
		public virtual BigInteger SummonerId { get; set; }
		[Parameter("uint256", "assistantId", 4)]
		public virtual BigInteger AssistantId { get; set; }
		[Parameter("uint32", "summons", 5)]
		public virtual uint Summons { get; set; }
		[Parameter("uint32", "maxSummons", 6)]
		public virtual uint MaxSummons { get; set; }
	}
}
