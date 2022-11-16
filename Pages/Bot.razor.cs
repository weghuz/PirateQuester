using Microsoft.AspNetCore.Components;
using PirateQuester.Bot;
using PirateQuester.Utils;
using Radzen;

namespace PirateQuester.Pages;

public partial class Bot
{
    [Inject]
    DialogService Dialog { get; set; }
    [Inject]
    AccountManager Acc { get; set; }
    [Inject]
    NavigationManager Nav { get; set; }
    public static DFKBot RunningBot { get; set; } = null;
    public static DFKBotSettings Settings { get; set; } = new();
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

    }

    public async Task RunBot()
    {
        if(RunningBot == null)
		{
			RunningBot = new DFKBot(Acc.Accounts, Settings);
            await RunningBot.StartBot();
		}
        else
        {
            DialogWindow("Can only run 1 bot instance.");
		}
    }
}
