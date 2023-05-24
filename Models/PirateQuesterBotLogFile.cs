using PirateQuester.Bot;

namespace PirateQuester.Models;

public class PirateQuesterBotLogFile
{
    public string Account { get; set; }
    public string Chain { get; set; }
    public DateTime ExportedTime { get; set; }
    public List<DFKBotLogMessage> Logs { get; set; }
    public List<QuestLog> Quests { get; set; }
}