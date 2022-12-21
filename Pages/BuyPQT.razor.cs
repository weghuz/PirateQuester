using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Nethereum.Web3;
using PirateQuester.Utils;
using Radzen;
using System.Numerics;

namespace PirateQuester.Pages;

public partial class BuyPQT
{
    [Inject]
    public AccountManager Acc { get; set; }
    [Inject]
    public NavigationManager Nav { get; set; }
    [Inject]
    public IJSInProcessRuntime JS { get; set; }
    [Inject]
    public DialogService Dialog { get; set; }
    public int BuyAmount { get; set; } = 1;
    public decimal PQTPrice { get ; set; }
    public static DFKAccount Account { get; set; }
    protected override async void OnInitialized()
    {
        if (Acc.Accounts.Count is 0 || Account is null)
        {
            Nav.NavigateTo("Login");
        }
        
        await Account.UpdateBalance();
        PQTPrice = Math.Round(Web3.Convert.FromWei(await Account.PQT.PriceQueryAsync()), 2);
        StateHasChanged();
    }
}