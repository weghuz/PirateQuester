using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PirateQuester.Utils;

namespace PirateQuester.Pages;

public partial class CreateAccount
{
	[Inject]
	AccountManager Acc { get; set; }
	[Inject]
	IJSInProcessRuntime JS { get; set; }
	[Inject]
	NavigationManager Nav { get; set; }
	async Task Create()
	{
		await Acc.Create(model);
		Nav.NavigateTo("Accounts");
	}
}