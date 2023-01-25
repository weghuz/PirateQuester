using Nethereum.ABI.FunctionEncoding.Attributes;

namespace DFKContracts.HeroCore.ContractDefinition
{
	public partial class HeroStats : HeroStatsBase { }

	public class HeroStatsBase
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
		[Parameter("uint16", "hp", 9)]
		public virtual ushort Hp { get; set; }
		[Parameter("uint16", "mp", 10)]
		public virtual ushort Mp { get; set; }
		[Parameter("uint16", "stamina", 11)]
		public virtual ushort Stamina { get; set; }
	}
}
