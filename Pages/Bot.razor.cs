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

    public void RefreshBots()
    {
		if (Bots.ClearLogsService is not null)
		{
            Bots.ClearLogsService.RefreshBots();
		}
	}

}
