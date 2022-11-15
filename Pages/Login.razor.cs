using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PirateQuester.Utils;
using PirateQuester.ViewModels;

namespace PirateQuester.Pages;

public partial class Login
{
	[Inject]
	public AccountManager Acc { get; set; }
	[Inject]
	public NavigationManager Nav { get; set; }
	[Inject]
	public IJSInProcessRuntime JS { get; set; }
	private LoginViewModel Model { get; set; } = new();

    async Task LoginAccount()
	{
		if (Model.Password.Length < 8)
		{
			JS.InvokeVoid("alert", "Password needs to be at least 8 characters");
		}
		if (await Acc.Login(Model))
		{
			Nav.NavigateTo("Accounts");
		}
	}

	List<string> GetLoggedOutAccountNames()
	{
		return Acc.AccountNames.Where(accName => Acc.Accounts.Select(a => a.Name).Any(name => name == accName) is false).ToList();
	}
	protected override void OnInitialized()
	{
		if (GetLoggedOutAccountNames().Count == 0)
		{
			Nav.NavigateTo("Accounts");
		}
	}
}
