using DFKContracts.QuestCore.ContractDefinition;
using Microsoft.AspNetCore.Components;
using PirateQuester.Bot;
using PirateQuester.Utils;
using Radzen;
using System.Net.NetworkInformation;
using Radzen.Blazor;

namespace PirateQuester.Pages;

public partial class Bot
{
    [Inject]
    DialogService Dialog { get; set; }
    [Inject]
    AccountManager Acc { get; set; }
    [Inject]
    NavigationManager Nav { get; set; }
    public RadzenDataGrid<Quest> RunningQuestsGrid { get; set; }
    public static List<DFKBot> RunningBots { get; set; } = null;
    public static DFKBotSettings Settings { get; set; } = new();
    public static bool Running { get; set; } = false;

    protected override void OnInitialized()
    {
        DFKBot.BotLogAdded += () =>
        {
            StateHasChanged();
        };
        if(Acc.Accounts.Count == 0)
        {
            Nav.NavigateTo("CreateAccount");
		}
    }

    public void StopBot()
	{
		foreach (DFKBot bot in RunningBots)
		{
			bot.StopBot();
		}
		Running = false;
	}

    public void RunBot()
    {
        if(RunningBots == null)
		{
            RunningBots = new List<DFKBot>();

			foreach (DFKAccount acc in Acc.Accounts)
			{
                DFKBot bot = new();
                RunningBots.Add(bot);
                bot.StartBot(acc, Settings);
			}
		}
        else
		{
			foreach (DFKBot bot in RunningBots)
			{
				bot.StartBot(bot.Account, Settings);
			}
		}
		Running = true;
	}
}
