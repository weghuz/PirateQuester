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

    public void RunBot()
	{
		Running = true;
		if (RunningBots == null)
		{
			RunningBots = new List<DFKBot>();

			foreach (DFKAccount acc in Acc.Accounts)
			{
                DFKBot bot = new();
                RunningBots.Add(bot);
                Console.WriteLine($"Bot added for Account: {acc.Account.Address}");
                try
				{
					bot.StartBot(acc, Settings);
				}
                catch(Exception e)
                {
                    Console.WriteLine(e);
                }
			}
		}
        else
		{
			foreach (DFKBot bot in RunningBots)
			{
				try
				{
					bot.StartBot(bot.Account, Settings);
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
				}
			}
		}
	}
}
