using Microsoft.AspNetCore.Components;
using PirateQuester.Services;
using PirateQuester.Utils;

namespace PirateQuester.Pages;

public partial class Accounts
{
	[Inject]
	AccountManager Acc { get; set; }
	[Inject]
	NavigationManager Nav { get; set; }
	[Inject]
	public AccountUpdaterService AccountUpdater { get; set; }
	public CancellationTokenSource cancelWorkToken { get; set; }
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
		cancelWorkToken = new();
		AccountUpdater.DoWorkAsync(cancelWorkToken.Token, () =>
		{
			StateHasChanged();
		});
		StateHasChanged();
	}

	//Implement Dispose and Cancel cancelWorkToken
	public void Dispose()
	{
		cancelWorkToken.Cancel();
	}

}
