using Microsoft.AspNetCore.Components;
using PirateQuester.Utils;
using Radzen;
using PirateQuester.Services;
using Microsoft.JSInterop;

namespace PirateQuester.Pages;

public partial class Bot
{
    [Inject]
    DialogService Dialog { get; set; }
    [Inject]
    AccountManager Acc { get; set; }
    [Inject]
    NavigationManager Nav { get; set; }
    [Inject]
    BotService Bots { get; set; }
    [Inject]
    IJSInProcessRuntime JS { get; set; }
    public List<DFKAccount> AccountsMissingPQT { get; set; }

    protected override void OnInitialized()
    {
        if(Acc.Accounts.Count == 0)
        {
            Nav.NavigateTo("CreateAccount");
		}
        else if (Acc.Accounts.Any(acc => acc.PQTBalance < 1))
        {
            AccountsMissingPQT = Acc.Accounts.Where(acc => acc.PQTBalance < 1).ToList();
        }
        
        Bots.UpdatedBot += StateHasChanged;
    }
}
