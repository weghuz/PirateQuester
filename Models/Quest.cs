
namespace PirateQuester.Models;

public class QuestLog
{
	public string QuestId { get; set; }
	public string QuestName { get; set; }
	public List<string> Heroes { get; set; }
	public List<ItemLog> Rewards { get; set; }
}
