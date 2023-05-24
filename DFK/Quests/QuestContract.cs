using DFK;
using PirateQuester.Utils;
using PirateQuester.Utils.Chain;
using System.Numerics;

namespace PirateQuester.DFK.Contracts;

public class QuestContract : Contract
{
    public string Category { get; set; }
    public string Subcategory { get; set; }
    public BigInteger QuestInstanceId { get; set; }
    public byte QuestType { get; set; }
    public int Level { get; set; }
    public Chain Chain { get; set; }
    public MaxHeroesPerQuestFunction MaxHeroesPerQuest { get; set; }
    public delegate int MaxHeroesPerQuestFunction(DFKAccount account);
    public AvailableAttemptsFunction AvailableAttempts { get; set; }
    public delegate int AvailableAttemptsFunction(Hero h, int? stamina = null);
    public BlocksPerAttemptFunction BlocksPerAttempt { get; set; }
    public delegate ulong BlocksPerAttemptFunction(Hero h);
}
