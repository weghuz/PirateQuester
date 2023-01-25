using Nethereum.ABI.FunctionEncoding.Attributes;

namespace PirateQuester.ItemConsumer.ContractDefinition
{
	public partial class HeroProfessions : HeroProfessionsBase { }

	public class HeroProfessionsBase
	{
		[Parameter("uint16", "mining", 1)]
		public virtual ushort Mining { get; set; }
		[Parameter("uint16", "gardening", 2)]
		public virtual ushort Gardening { get; set; }
		[Parameter("uint16", "foraging", 3)]
		public virtual ushort Foraging { get; set; }
		[Parameter("uint16", "fishing", 4)]
		public virtual ushort Fishing { get; set; }
	}
}
