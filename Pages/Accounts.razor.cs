using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PirateQuester.Utils;

namespace PirateQuester.Pages;

public partial class Accounts
{
	[Inject]
	AccountManager Acc { get; set; }
	[Inject]
	NavigationManager Nav { get; set; }
	[Inject]
	AccountSettings AccountSettings { get; set; }

	Dictionary<string, bool> ShowPrivateKey = new();
	public string Password { get; set; }

	bool Initialized = false;
	void BuyPirateQuesterToken(int accountIndex)
	{
        BuyPQT.Account = Acc.Accounts[accountIndex];
        Nav.NavigateTo($"/BuyPQT");
	}
	public void TryRevealKey(string name, string password)
	{
		if(Acc.CheckPassword(name, password))
		{
			ShowPrivateKey[name] = true;
			StateHasChanged();
		}
	}
	protected override async Task OnInitializedAsync()
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
		foreach (string name in Acc.AccountNames)
		{
			ShowPrivateKey.Add(name, false);
		}
        foreach (DFKAccount acc in Acc.Accounts)
        {
			await acc.UpdateBalance();
        }
		StateHasChanged();
        Initialized = true;
	}
}
