using DFK;
using PirateQuester.Utils.Chain;
namespace PirateQuester.DFK.Contracts;

public class QuestContract : Contract
{
    public string Category { get; set; }
    public string Subcategory { get; set; }
    public int Level { get; set; }
    public Chain Chain { get; set; }
    public AvailableAttemptsFunction AvailableAttempts { get; set; }
    public delegate int AvailableAttemptsFunction(Hero h);
    public BlocksPerAttemptFunction BlocksPerAttempt { get; set; }
    public delegate ulong BlocksPerAttemptFunction(Hero h);
}
