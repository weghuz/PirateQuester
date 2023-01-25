using Nethereum.ABI.FunctionEncoding.Attributes;
using System.Numerics;

namespace PirateQuester.ItemConsumer.ContractDefinition
{
	public partial class HeroInfo : HeroInfoBase { }

	public class HeroInfoBase
	{
		[Parameter("uint256", "statGenes", 1)]
		public virtual BigInteger StatGenes { get; set; }
		[Parameter("uint256", "visualGenes", 2)]
		public virtual BigInteger VisualGenes { get; set; }
		[Parameter("uint8", "rarity", 3)]
		public virtual byte Rarity { get; set; }
		[Parameter("bool", "shiny", 4)]
		public virtual bool Shiny { get; set; }
		[Parameter("uint16", "generation", 5)]
		public virtual ushort Generation { get; set; }
		[Parameter("uint32", "firstName", 6)]
		public virtual uint FirstName { get; set; }
		[Parameter("uint32", "lastName", 7)]
		public virtual uint LastName { get; set; }
		[Parameter("uint8", "shinyStyle", 8)]
		public virtual byte ShinyStyle { get; set; }
		[Parameter("uint8", "class", 9)]
		public virtual byte Class { get; set; }
		[Parameter("uint8", "subClass", 10)]
		public virtual byte SubClass { get; set; }
	}
}
