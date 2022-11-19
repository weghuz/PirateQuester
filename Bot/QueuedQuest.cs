using DFKContracts.QuestCore.ContractDefinition;
using PirateQuester.DFK.Contracts;
using System.Numerics;

namespace PirateQuester.Bot
{
	public class QueuedQuest
	{
        public QuestContract Contract { get; set; }
        public int Queue { get; set; }
        public BigInteger QuestId { get; set; }
    }
}