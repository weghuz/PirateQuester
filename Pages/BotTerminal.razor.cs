using Microsoft.AspNetCore.Components;
using PirateQuester.Services;
using PirateQuester.Utils;

namespace PirateQuester.Pages;

public partial class BotTerminal
{
	[Inject]
	AccountManager Acc { get; set; }
	[Inject]
	NavigationManager Nav { get; set; }
	[Inject]
	BotService Bots { get; set; }
	[Parameter]
	public int BotId { get; set; }

	private void UpdateTerminal()
	{
		StateHasChanged();
	}

	protected override void OnInitialized()
	{
		Bots.UpdatedBot += UpdateTerminal;
		if (Bots.RunningBots.Count < BotId + 1)
		{
			Nav.NavigateTo($"Login");
			return;
		}
		if (Acc.Accounts.Count == 0)
		{
			Nav.NavigateTo("CreateAccount");
		}
	}
}
