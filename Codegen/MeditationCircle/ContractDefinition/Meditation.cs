using Nethereum.ABI.FunctionEncoding.Attributes;
using System.Numerics;

namespace DFKContracts.MeditationCircle.ContractDefinition
{
	public partial class Meditation : MeditationBase { }

	public class MeditationBase
	{
		[Parameter("uint256", "id", 1)]
		public virtual BigInteger Id { get; set; }
		[Parameter("address", "player", 2)]
		public virtual string Player { get; set; }
		[Parameter("uint256", "heroId", 3)]
		public virtual BigInteger HeroId { get; set; }
		[Parameter("uint8", "primaryStat", 4)]
		public virtual byte PrimaryStat { get; set; }
		[Parameter("uint8", "secondaryStat", 5)]
		public virtual byte SecondaryStat { get; set; }
		[Parameter("uint8", "tertiaryStat", 6)]
		public virtual byte TertiaryStat { get; set; }
		[Parameter("address", "attunementCrystal", 7)]
		public virtual string AttunementCrystal { get; set; }
		[Parameter("uint256", "startBlock", 8)]
		public virtual BigInteger StartBlock { get; set; }
		[Parameter("uint8", "status", 9)]
		public virtual byte Status { get; set; }
	}
}
