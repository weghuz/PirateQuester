using Microsoft.AspNetCore.Components;
using PirateQuester.Utils;
using Radzen;
using PirateQuester.Services;
using Nethereum.JsonRpc.WebSocketStreamingClient;
using Nethereum.RPC.Reactive.Eth.Subscriptions;
using Newtonsoft.Json;

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


    protected override void OnInitialized()
    {
        if(Acc.Accounts.Count == 0)
        {
            Nav.NavigateTo("CreateAccount");
		}
        Bots.UpdatedBot += StateHasChanged;
    }
}
