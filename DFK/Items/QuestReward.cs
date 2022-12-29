using DFK;
using DFKContracts.QuestCore.ContractDefinition;
using PirateQuester.DFK.Contracts;
using System.Numerics;

namespace PirateQuester.DFK.Items
{
	public class QuestReward
	{
		public QuestReward()
		{

		}
		public QuestReward(BigInteger Id)
		{
			QuestId = Id;
		}
		public List<QuestSkillUpEventDTO> SkillUps { get; set; } = new();
		public DateTime? StartDateTime { get; set; }
		public DateTime? CompleteDateTime { get; set; }
		public QuestContract Quest { get; set; }
		public BigInteger QuestId { get; set; }
		public List<BigInteger> Heroes { get; set; } = new();
		public DFKInventory Rewards { get; set; }
    }
}
