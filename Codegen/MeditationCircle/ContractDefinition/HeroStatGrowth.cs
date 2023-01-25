using Nethereum.ABI.FunctionEncoding.Attributes;

namespace DFKContracts.MeditationCircle.ContractDefinition
{
	public partial class HeroStatGrowth : HeroStatGrowthBase { }

	public class HeroStatGrowthBase
	{
		[Parameter("uint16", "strength", 1)]
		public virtual ushort Strength { get; set; }
		[Parameter("uint16", "intelligence", 2)]
		public virtual ushort Intelligence { get; set; }
		[Parameter("uint16", "wisdom", 3)]
		public virtual ushort Wisdom { get; set; }
		[Parameter("uint16", "luck", 4)]
		public virtual ushort Luck { get; set; }
		[Parameter("uint16", "agility", 5)]
		public virtual ushort Agility { get; set; }
		[Parameter("uint16", "vitality", 6)]
		public virtual ushort Vitality { get; set; }
		[Parameter("uint16", "endurance", 7)]
		public virtual ushort Endurance { get; set; }
		[Parameter("uint16", "dexterity", 8)]
		public virtual ushort Dexterity { get; set; }
		[Parameter("uint16", "hpSm", 9)]
		public virtual ushort HpSm { get; set; }
		[Parameter("uint16", "hpRg", 10)]
		public virtual ushort HpRg { get; set; }
		[Parameter("uint16", "hpLg", 11)]
		public virtual ushort HpLg { get; set; }
		[Parameter("uint16", "mpSm", 12)]
		public virtual ushort MpSm { get; set; }
		[Parameter("uint16", "mpRg", 13)]
		public virtual ushort MpRg { get; set; }
		[Parameter("uint16", "mpLg", 14)]
		public virtual ushort MpLg { get; set; }
	}
}
