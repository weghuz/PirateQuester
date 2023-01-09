using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PirateQuester.Services;
using PirateQuester.Utils;

namespace PirateQuester.Pages;

public partial class CreateAccount
{
	[Inject]
	AccountManager Acc { get; set; }
	[Inject]
	NavigationManager Nav { get; set; }
	[Inject]
	BotService Bots { get; set; }
	public bool Creating { get; set; }
	async Task Create()
	{
		Creating = true;
		await Acc.Create(model, Bots.Settings);
        Nav.NavigateTo("Accounts");
	}
}