using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PirateQuester.Utils;

namespace PirateQuester.Pages;

public partial class Accounts
{
	[Inject]
	AccountManager Acc { get; set; }
	[Inject]
	IJSInProcessRuntime JSRuntime { get; set; }
	[Inject]
	NavigationManager Nav { get; set; }

	Dictionary<string, bool> ShowPrivateKey = new();

	bool Initialized = false;

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
		foreach (string name in Acc.AccountNames)
		{
			ShowPrivateKey.Add(name, false);
		}
		Initialized = true;
	}
}
