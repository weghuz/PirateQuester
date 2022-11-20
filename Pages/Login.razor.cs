using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PirateQuester.Shared;
using PirateQuester.Utils;
using PirateQuester.ViewModels;
using Utils;

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
	public bool LoggingIn { get; set; } = false;

    protected override void OnInitialized()
    {

    }

    async Task LoginAccount()
	{
		LoggingIn = true;
		if (Model.Password.Length < 8)
		{
			JS.InvokeVoid("alert", "Password needs to be at least 8 characters");
			LoggingIn = false;
		}
		if (await Acc.Login(Model))
		{
			Nav.NavigateTo("Bot");
		}
		else
		{
			LoggingIn = false;
		}
	}

	List<string> GetLoggedOutAccountNames()
	{
		return Acc.AccountNames.Where(accName => Acc.Accounts.Select(a => a.Name).Any(name => name == accName) is false).ToList();
	}
}
