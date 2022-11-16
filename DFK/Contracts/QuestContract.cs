namespace PirateQuester.DFK.Contracts;

public class QuestContract : Contract
{
    public string Category { get; set; }
    public string Subcategory { get; set; }
    public int Level { get; set; }
    public int StaminaDrain { get; set; }
}
