using Microsoft.AspNetCore.Components;
using PirateQuester.Utils;

namespace PirateQuester.Pages;

public partial class Accounts
{
    [Inject]
    AccountManager Acc { get; set; }
    [Inject]
    NavigationManager Nav { get; set; }
    public string Password { get; set; }
    public static CancellationTokenSource CancelSource { get; set; } = new();
    public static event Action UpdateAccountsAction;
    public static bool Initiated { get; set; } = false;
    protected override void OnInitialized()
    {
        if (Acc.Accounts.Count == 0)
        {
            if (Acc.AccountNames.Count > 0)
            {
                Nav.NavigateTo("Login");
            }
            else
            {
                Nav.NavigateTo("CreateAccount");
            }
        }
        if (Initiated is false)
        {
            Initiated = true;
            UpdateAccountsAction = async () =>
            {
                await Task.Delay(10000);
                foreach (DFKAccount acc in Acc.Accounts)
                {
                    await acc.UpdateBalance();
                }
                StateHasChanged();
                UpdateAccountsAction?.Invoke();
            };
            UpdateAccountsAction?.Invoke();
        }
    }
}
