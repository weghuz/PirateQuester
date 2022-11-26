using Microsoft.AspNetCore.Components;
using PirateQuester.Utils;
using Radzen;
using PirateQuester.Services;
using Nethereum.JsonRpc.WebSocketStreamingClient;
using Nethereum.RPC.Reactive.Eth.Subscriptions;
using Newtonsoft.Json;
using PirateQuester.Bot;
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


	protected override void OnAfterRender(bool firstRender)
	{
        if(firstRender)
        {
			var settingsJson = JS.Invoke<string>("localStorage.getItem", "DFKBotSettings");
			if (settingsJson is not null)
			{
				Bots.Settings = JsonConvert.DeserializeObject<DFKBotSettings>(settingsJson);
			}
            else
            {
                Bots.Settings.MinTrainingStats = new()
                {
                    { new(0, 30) },
                    { new(1, 30) },
                    { new(2, 30) },
                    { new(3, 30) },
                    { new(4, 30) },
                    { new(5, 30) },
                    { new(6, 30) },
                    { new(7, 30) }
                };
            }
            StateHasChanged();
		}
	}
	protected override void OnInitialized()
    {
        if(Acc.Accounts.Count == 0)
        {
            Nav.NavigateTo("CreateAccount");
		}
        Bots.UpdatedBot += StateHasChanged;
    }
}
