using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PirateQuester.Services;
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
	[Inject]
	public AccountSettings AccSettings { get; set; }
	[Inject]
	public BotService Bots { get; set; }
	private LoginViewModel Model { get; set; } = new();
	public bool LoggingIn { get; set; } = false;

	protected override void OnInitialized()
	{
		if (Acc.AccountNames.Count == 0)
		{
			Nav.NavigateTo("CreateAccount");
		}
	}

	async Task LoginAccount()
	{
		LoggingIn = true;
		if (Model.Password is null || Model.Password.Length < 8)
		{
			JS.InvokeVoid("alert", "Password needs to be at least 8 characters");
			LoggingIn = false;
		}
		if (await Acc.Login(Model, Bots.Settings))
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
