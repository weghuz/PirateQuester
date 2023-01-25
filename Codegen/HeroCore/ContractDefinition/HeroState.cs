using Nethereum.ABI.FunctionEncoding.Attributes;
using System.Numerics;

namespace DFKContracts.HeroCore.ContractDefinition
{
	public partial class HeroState : HeroStateBase { }

	public class HeroStateBase
	{
		[Parameter("uint256", "staminaFullAt", 1)]
		public virtual BigInteger StaminaFullAt { get; set; }
		[Parameter("uint256", "hpFullAt", 2)]
		public virtual BigInteger HpFullAt { get; set; }
		[Parameter("uint256", "mpFullAt", 3)]
		public virtual BigInteger MpFullAt { get; set; }
		[Parameter("uint16", "level", 4)]
		public virtual ushort Level { get; set; }
		[Parameter("uint64", "xp", 5)]
		public virtual ulong Xp { get; set; }
		[Parameter("address", "currentQuest", 6)]
		public virtual string CurrentQuest { get; set; }
		[Parameter("uint8", "sp", 7)]
		public virtual byte Sp { get; set; }
		[Parameter("uint8", "status", 8)]
		public virtual byte Status { get; set; }
	}
}
