using DFKContracts.QuestCore.ContractDefinition;
using Microsoft.AspNetCore.Components;
using PirateQuester.Bot;
using PirateQuester.Utils;
using Radzen;
using System.Net.NetworkInformation;
using Radzen.Blazor;
using PirateQuester.Services;
using PirateQuester.DFK.Items;
using Microsoft.JSInterop;
using Newtonsoft.Json;

namespace PirateQuester.Pages;

public partial class BotTerminal
{
	[Inject]
	AccountManager Acc { get; set; }
	[Inject]
	NavigationManager Nav { get; set; }
	[Inject]
	BotService Bots { get; set; }
	[Inject]
	TooltipService Tooltip { get; set; }
	[Parameter]
	public int BotId { get; set; }

	private void UpdateTerminal()
	{

		Console.WriteLine("Terminal Updated");
	}
	protected override void OnInitialized()
	{
		Bots.UpdatedBot += UpdateTerminal;
		if (Bots.RunningBots.Count < BotId + 1)
		{
			Nav.NavigateTo($"CreateAccount");
			return;
		}
		if (Acc.Accounts.Count == 0)
		{
			Nav.NavigateTo("CreateAccount");
		}
	}
}
