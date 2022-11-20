using DFKContracts.QuestCore.ContractDefinition;
using Microsoft.AspNetCore.Components;
using PirateQuester.Bot;
using PirateQuester.Utils;
using Radzen;
using System.Net.NetworkInformation;
using Radzen.Blazor;
using PirateQuester.Services;

namespace PirateQuester.Pages;

public partial class BotTerminal
{
	[Inject]
	DialogService Dialog { get; set; }
	[Inject]
	AccountManager Acc { get; set; }
	[Inject]
	NavigationManager Nav { get; set; }
	[Inject]
	BotService Bots { get; set; }
	[Parameter]
	public int BotId { get; set; }

	public RadzenDataGrid<Quest> RunningQuestsGrid { get; set; }

	protected override void OnInitialized()
	{
		if(Bots.RunningBots.Count <= BotId + 1)
		{
			Nav.NavigateTo($"CreateAccount");
			return;
		}
		Bots.RunningBots[BotId].BotLogAdded += () =>
		{
			StateHasChanged();
		};
		if (Acc.Accounts.Count == 0)
		{
			Nav.NavigateTo("CreateAccount");
		}
	}
}
