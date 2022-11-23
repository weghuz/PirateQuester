using DFK;
using DFKContracts.QuestCore.ContractDefinition;
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
		public BigInteger QuestId { get; set; }
        public List<BigInteger> Heroes { get; set; }
        public DFKInventory Rewards { get; set; }
    }
}
