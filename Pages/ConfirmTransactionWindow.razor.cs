using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Nethereum.Web3;
using PirateQuester.PirateQuesterToken;
using PirateQuester.Services;
using PirateQuester.Utils;
using Radzen;
using System.Numerics;
using Utils;

namespace PirateQuester.Pages;

public partial class ConfirmTransactionWindow
{
    [Inject]
    public AccountManager Acc { get; set; }
    [Inject]
    public NavigationManager Nav { get; set; }
    [Inject]
    public BotService Bots { get; set; }
    [Inject]
    public IJSInProcessRuntime JS { get; set; }
    [Inject]
    public DialogService Dialog { get; set; }
    
    public static int? BuyAmount { get; set; }
    public decimal PQTPrice { get ; set; }
    public DFKAccount Account { get; set; }
    public bool IsBuying { get; set; }

    public async void BuyPirateQuesterToken()
    {
        IsBuying = true;
        string response = await Transaction.BuyPirateQuesterToken(Account, BuyAmount.Value, Bots.Settings.MaxGasFeeGwei, Bots.Settings.CancelTxnDelay);
        IsBuying = false;
        if (response.Contains("failed"))
		{
			JS.Invoke<string>("alert", "The transaction failed, make sure you have enough AVAX to pay for gas on top of the price!");
			StateHasChanged();
		}
        else
        {
            JS.Invoke<string>("alert", "Thank you for buying a Pirate Quester Token!");
            Dialog.Close();
            Nav.NavigateTo("Accounts");
        }
    }
    
    protected override async void OnInitialized()
    {
        IsBuying = false;
        Account = BuyPQT.Account;
        PQTPrice = Web3.Convert.FromWei(await Account.PQT.PriceQueryAsync());
        if (Acc.Accounts.Count is 0 || BuyAmount is null || Account is null)
        {
            Nav.NavigateTo("Login");
        }
        StateHasChanged();
    }
}