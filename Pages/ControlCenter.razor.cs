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
	public decimal? SalePrice { get; set; }
	public LevelUpSetting LevelSettings { get; set; } = new();

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

	public async void RefreshTableHeroes()
	{
		foreach (DFKAccount acc in Acc.Accounts)
		{
			await acc.InitializeAccount();
		}
		TableHeroes = Acc.Accounts.SelectMany(a => a.BotHeroes).ToList();
		await HeroGridReference.Refresh();
		StateHasChanged();
	}

	public void SetAllPreferences()
	{
		foreach (DFKBotHero h in HeroGridReference.SelectedRecords)
		{
			var selectedQuest = h.Account.Chain.Name == "DFK" ? SelectedDFKQuest : SelectedKlaytnQuest;
			if (selectedQuest.HasValue)
			{
				h.Quest = QuestContractDefinitions.GetQuestContract(selectedQuest.Value, h.Account.Chain.ChainEnum);
			}
			else
			{
				h.Quest = null;
			}
			
			if (LevelSettings.MainAttribute is not null
				&& LevelSettings.SecondaryAttribute is not null
				&& LevelSettings.TertiaryAttribute is not null)
			{
				h.LevelUpSetting = new()
				{
					MainAttribute = LevelSettings.MainAttribute,
					SecondaryAttribute = LevelSettings.SecondaryAttribute,
					TertiaryAttribute = LevelSettings.TertiaryAttribute
				};
			}
			h.BotSalePrice = SalePrice;
			var setting = Bots.Settings.HeroQuestSettings.FirstOrDefault(hqs => hqs.HeroId == h.ID.ToString());
			if (setting != null)
			{
				setting.QuestId = h.Quest?.Id;
				setting.ChainIdentifier = h.Account.Chain.Identifier;
				setting.BotSalePrice = SalePrice;
				if (LevelSettings.MainAttribute is not null 
					&& LevelSettings.SecondaryAttribute is not null 
					&& LevelSettings.TertiaryAttribute is not null)
				{
					setting.LevelupSettings = new()
					{
						PrimaryAttribute = LevelSettings.MainAttribute?.Id,
						SecondaryAttribute = LevelSettings.SecondaryAttribute?.Id,
						TertiaryAttribute = LevelSettings.TertiaryAttribute?.Id,
					};
				}
			}
			else
			{
				HeroQuestSetting newSettings = new()
				{
					QuestId = h.Quest?.Id,
					ChainIdentifier = h.Account.Chain.Identifier,
					HeroId = h.ID.ToString(),
					BotSalePrice = SalePrice
				};
				if(LevelSettings.MainAttribute is not null
					&& LevelSettings.SecondaryAttribute is not null
					&& LevelSettings.TertiaryAttribute is not null)
				{
					newSettings.LevelupSettings = new()
					{
						PrimaryAttribute = LevelSettings.MainAttribute?.Id,
						SecondaryAttribute = LevelSettings.SecondaryAttribute?.Id,
						TertiaryAttribute = LevelSettings.TertiaryAttribute?.Id,
					};
				}
				Bots.Settings.HeroQuestSettings.Add(newSettings);
			}
		}
		Bots.SaveSettings();
		HeroGridReference.Refresh();
		StateHasChanged();
	}

	public void SetQuestPreference()
	{
		foreach (DFKBotHero h in HeroGridReference.SelectedRecords)
		{
			var selectedQuest = h.Account.Chain.Name == "DFK" ? SelectedDFKQuest : SelectedKlaytnQuest;
			if (selectedQuest.HasValue)
			{
				h.Quest = QuestContractDefinitions.GetQuestContract(selectedQuest.Value, h.Account.Chain.ChainEnum);
			}
			else
			{
				h.Quest = null;
			}
			var setting = Bots.Settings.HeroQuestSettings.FirstOrDefault(hqs => hqs.HeroId == h.ID.ToString());
			if (setting != null)
			{
				setting.QuestId = h.Quest?.Id;
				setting.ChainIdentifier = h.Account.Chain.Identifier;
			}
			else
			{
				Bots.Settings.HeroQuestSettings.Add(new()
				{
					QuestId = h.Quest?.Id,
					ChainIdentifier = h.Account.Chain.Identifier,
					HeroId = h.ID.ToString()
				});
			}
		}
		Bots.SaveSettings();
		HeroGridReference.Refresh();
		StateHasChanged();
	}

	public void SetLevelupPreference()
	{
		if(LevelSettings.MainAttribute is null || LevelSettings.SecondaryAttribute is null || LevelSettings.TertiaryAttribute is null)
		{
			JS.Invoke<string>("alert", "Please select all attributes");
			return;
		}
		foreach (DFKBotHero h in HeroGridReference.SelectedRecords)
		{
			h.LevelUpSetting = new()
			{
				MainAttribute = LevelSettings.MainAttribute,
				SecondaryAttribute = LevelSettings.SecondaryAttribute,
				TertiaryAttribute = LevelSettings.TertiaryAttribute
			};
			var setting = Bots.Settings.HeroQuestSettings.FirstOrDefault(hqs => hqs.HeroId == h.ID.ToString());
			if (setting != null)
			{
				setting.BotSalePrice = SalePrice;
				setting.ChainIdentifier = h.Account.Chain.Identifier;
			}
			else
			{
				Bots.Settings.HeroQuestSettings.Add(new()
				{
					BotSalePrice = SalePrice,
					ChainIdentifier = h.Account.Chain.Identifier,
					HeroId = h.ID.ToString()
				});
			}
		}
		Bots.SaveSettings();
		HeroGridReference.Refresh();
		StateHasChanged();
	}

	public void SetSalePreference()
	{
		foreach (DFKBotHero h in HeroGridReference.SelectedRecords)
		{
			h.BotSalePrice = SalePrice;
			var setting = Bots.Settings.HeroQuestSettings.FirstOrDefault(hqs => hqs.HeroId == h.ID.ToString());
			if (setting != null)
			{
				setting.BotSalePrice = SalePrice;
				setting.ChainIdentifier = h.Account.Chain.Identifier;
			}
			else
			{
				Bots.Settings.HeroQuestSettings.Add(new()
				{
					BotSalePrice = SalePrice,
					ChainIdentifier = h.Account.Chain.Identifier,
					HeroId = h.ID.ToString()
				});
			}
		}
		Bots.SaveSettings();
		HeroGridReference.Refresh();
		StateHasChanged();
	}
	
	public async void ResetSelection()
	{
		SelectedDFKQuest = null;
		SelectedKlaytnQuest = null;
		SalePrice = null;
		LevelSettings = new();
		await HeroGridReference.ClearSelectionAsync();
	}
	
	public void ClearLevelSettingsPreference()
	{
		StateHasChanged();
		foreach (DFKBotHero h in HeroGridReference.SelectedRecords)
		{
			h.LevelUpSetting = new();
			var setting = Bots.Settings.HeroQuestSettings.FirstOrDefault(hqs => hqs.HeroId == h.ID.ToString());
			if (setting != null)
			{
				setting.LevelupSettings = null;
			}
		}
		Bots.SaveSettings();
		HeroGridReference.Refresh();
		StateHasChanged();
	}
	
	public void ClearSalePricePreference()
	{
		StateHasChanged();
		foreach (DFKBotHero h in HeroGridReference.SelectedRecords)
		{
			h.BotSalePrice = null;
			var setting = Bots.Settings.HeroQuestSettings.FirstOrDefault(hqs => hqs.HeroId == h.ID.ToString());
			if (setting != null)
			{
				setting.BotSalePrice = null;
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
			if (setting != null)
			{
				setting.QuestId = null;
			}
		}
		Bots.SaveSettings();
		HeroGridReference.Refresh();
		StateHasChanged();
	}
	
	public void ClearAllPreference()
	{
		StateHasChanged();
		foreach (DFKBotHero h in HeroGridReference.SelectedRecords)
		{
			h.Quest = null;
			h.LevelUpSetting = new();
			h.BotSalePrice = null;
			var setting = Bots.Settings.HeroQuestSettings.FirstOrDefault(hqs => hqs.HeroId == h.ID.ToString());
			if (setting != null)
			{
				Bots.Settings.HeroQuestSettings.Remove(setting);
			}
		}
		Bots.SaveSettings();
		HeroGridReference.Refresh();
		StateHasChanged();
	}
}
