using Nethereum.ABI.FunctionEncoding.Attributes;
using System.Numerics;

namespace PirateQuester.ItemConsumer.ContractDefinition
{
	public partial class Hero : HeroBase { }

	public class HeroBase
	{
		[Parameter("uint256", "id", 1)]
		public virtual BigInteger Id { get; set; }
		[Parameter("tuple", "summoningInfo", 2)]
		public virtual SummoningInfo SummoningInfo { get; set; }
		[Parameter("tuple", "info", 3)]
		public virtual HeroInfo Info { get; set; }
		[Parameter("tuple", "state", 4)]
		public virtual HeroState State { get; set; }
		[Parameter("tuple", "stats", 5)]
		public virtual HeroStats Stats { get; set; }
		[Parameter("tuple", "primaryStatGrowth", 6)]
		public virtual HeroStatGrowth PrimaryStatGrowth { get; set; }
		[Parameter("tuple", "secondaryStatGrowth", 7)]
		public virtual HeroStatGrowth SecondaryStatGrowth { get; set; }
		[Parameter("tuple", "professions", 8)]
		public virtual HeroProfessions Professions { get; set; }
	}
}
