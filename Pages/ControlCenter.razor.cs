using DFK;
using PirateQuester.DFK.Contracts;
using PirateQuester.Utils;
using Radzen.Blazor;
using Radzen;
using System.Text;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Utils;
using System.Numerics;
using PirateQuester.Bot;
using PirateQuester.Services;

namespace PirateQuester.Pages;

public partial class ControlCenter
{

	[Inject]
	public AccountManager Acc { get; set; }
	[Inject]
	public NavigationManager Nav { get; set; }
	[Inject]
	public IJSInProcessRuntime JS { get; set; }
	[Inject]
	public DialogService Dialog { get; set; }
	[Inject]
	public BotService Bots { get; set; }
	public List<DFKBotHero> TableHeroes { get; set; } = new();
	public string SelectedQuestName { get; set; }

    bool Loading = false;
	IList<DFKBotHero> SelectedHeroes = new List<DFKBotHero>();
	RadzenDataGrid<DFKBotHero> heroes;

	protected override void OnInitialized()
	{
		TableHeroes = Acc.Accounts.SelectMany(a => a.BotHeroes).ToList();
		if (Acc.Accounts.Count == 0)
		{
			Nav.NavigateTo("CreateAccount");
		}
		foreach(DFKBot bot in Bots.RunningBots)
		{
			bot.HeroesUpdated += StateHasChanged;
		};
	}

	public void SetQuestPreference()
	{
		foreach(DFKBotHero h in SelectedHeroes)
		{
			h.Quest = QuestContractDefinitions.GetQuestContract(SelectedQuestName);
		}
	}
}
