using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PirateQuester.Utils;
using PirateQuester.ViewModels;

namespace PirateQuester.Pages;

public partial class Accounts
{
	[Inject]
	AccountManager Acc { get; set; }
	[Inject]
	NavigationManager Nav { get; set; }
	public string Password { get; set; }
	
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
        foreach (DFKAccount acc in Acc.Accounts)
        {
			await acc.UpdateBalance();
        }
		StateHasChanged();
	}
}
