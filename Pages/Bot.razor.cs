using Microsoft.AspNetCore.Components;
using PirateQuester.Utils;
using PirateQuester.Services;
using PirateQuester.Bot;
using Microsoft.JSInterop;
using System.Text.Json;
using PirateQuester.DFK.Items;
using System.Text.Json.Serialization;

namespace PirateQuester.Pages;

public partial class Bot
{
    [Inject]
    AccountManager Acc { get; set; }
    [Inject]
    NavigationManager Nav { get; set; }
    [Inject]
    BotService Bots { get; set; }
    [Inject]
    IJSInProcessRuntime JS { get; set; }
    public List<DFKAccount> AccountsMissingPQT { get; set; }
    public bool ShowDFKQuests { get; set; }

    protected override void OnInitialized()
    {
        if (Acc.Accounts.Count == 0)
        {
            Nav.NavigateTo("Login");
        }
        else if (Acc.Accounts.Any(acc => acc.PQTBalance < 1))
        {
            AccountsMissingPQT = Acc.Accounts.Where(acc => acc.PQTBalance < 1).ToList();
        }

        Bots.UpdatedBot += StateHasChanged;
    }
    private class PirateQuesterLogFile
    {
        public List<PirateQuesterBotLogFile> pirateQuesterBotLogFiles { get; set; } = new();
	}
    
	private class PirateQuesterBotLogFile
	{
        public string Account { get; set; }
        public string Chain { get; set; }
        public DateTime ExportedTime { get; set; }
        public List<DFKBotLogMessage> Logs { get; set; }
        public List<Quest> Quests { get; set; }
	}
    
	public class Quest
	{
        public string QuestId { get; set; }
        public string QuestName { get; set; }
        public List<string> Heroes { get; set; }
        public List<Item> Rewards { get; set; }
	}
	public class Item
	{
        public string Name { get; set; }
        public string Amount { get; set; }
    }

	private void DownloadLogsAndRefreshBots()
    {
		PirateQuesterLogFile pirateQuesterLogFile = new();
        
		foreach (DFKBot bot in Bots.RunningBots)
        {
            (var console, var quests) = bot.GetLogsAndStop();
            
			PirateQuesterBotLogFile pirateQuesterBotLogFile = new()
			{
				Account = $"{bot.Account.Account.Address} {bot.Account.Name}",
				Chain = bot.Account.Chain.Name,
				ExportedTime = DateTime.Now,
				Quests = quests.Select(q => new Quest()
                {
					QuestId = q.QuestId.ToString(),
					QuestName = q.Quest.Name,
                    Heroes = q.Heroes.Select(h => h.ToString()).ToList(),
                    Rewards = q.Rewards.Items.Select(i =>
                    new Item()
                    {
                        Name = i.Name,
                        Amount = i.Amount.ToString()
                    }).ToList()
                }).ToList(),
				Logs = console
			};
			pirateQuesterLogFile.pirateQuesterBotLogFiles.Add(pirateQuesterBotLogFile);
		}
        
		JS.Invoke<string>("download", $"PirateQuesterLog{DateTime.Now:yyyy-MM-ddThh:mm}", JsonSerializer.Serialize(pirateQuesterLogFile, new JsonSerializerOptions()
		{
			ReferenceHandler = ReferenceHandler.IgnoreCycles,
		}));
		Bots.RunBots();
	}
}
