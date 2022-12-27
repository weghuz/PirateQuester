using PirateQuester.DFK.Contracts;
using PirateQuester.Utils;
using Radzen;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PirateQuester.Bot;
using PirateQuester.Services;
using Syncfusion.Blazor.Grids;

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
	public int? SelectedDFKQuest { get; set; }
	public int? SelectedKlaytnQuest { get; set; }
	public SfGrid<DFKBotHero> HeroGridReference { get; set; }
	public int SelectedHeroCount { get; set; }
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

	public void RowSelectingHandler(RowSelectingEventArgs<DFKBotHero> args)
	{
		SelectedHeroCount = HeroGridReference.SelectedRecords.Count + 1;
	}
	public void RowDeselectHandler(RowDeselectEventArgs<DFKBotHero> args)
	{
		SelectedHeroCount = HeroGridReference.SelectedRecords.Count;
	}

	public void SetQuestPreference()
	{
		foreach (DFKBotHero h in HeroGridReference.SelectedRecords)
		{
			var selectedQuest = h.Account.Chain.Name == "DFK" ? SelectedDFKQuest : SelectedKlaytnQuest;
			if (selectedQuest.HasValue)
			{
				h.Quest = QuestContractDefinitions.GetQuestContract(selectedQuest.Value, h.Account.Chain.ChainEnum);
				var setting = Bots.Settings.HeroQuestSettings.FirstOrDefault(hqs => hqs.HeroId == h.ID.ToString());
				if (setting != null)
				{
					setting.QuestId = h.Quest.Id;
					setting.ChainIdentifier = h.Account.Chain.Identifier;
				}
				else
				{
					Bots.Settings.HeroQuestSettings.Add(new()
					{
						QuestId = h.Quest.Id,
						ChainIdentifier = h.Account.Chain.Identifier,
						HeroId = h.ID.ToString()
					});
				}
			}
		}
		Bots.SaveSettings();
		HeroGridReference.Refresh();
		StateHasChanged();
	}
	public void ClearQuestPreference()
	{
		StateHasChanged();
		foreach (DFKBotHero h in HeroGridReference.SelectedRecords)
		{
			h.Quest = null;
			var setting = Bots.Settings.HeroQuestSettings.FirstOrDefault(hqs => hqs.HeroId == h.ID.ToString());
			if(setting != null)
			{
				Bots.Settings.HeroQuestSettings.Remove(setting);
			}
		}
		Bots.SaveSettings();
		HeroGridReference.Refresh();
		StateHasChanged();
	}
}
