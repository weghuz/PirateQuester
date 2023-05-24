using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PirateQuester.Bot;
using PirateQuester.DFK.Contracts;
using PirateQuester.Services;
using PirateQuester.Utils;
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
    public BotService Bots { get; set; }
    [Inject]
    public HeroPricingService PricingService { get; set; }
    public List<DFKBotHero> TableHeroes { get; set; } = new();
    public bool SetQuest { get; set; }
    public bool SetPrice { get; set; }
    public bool SetStampot { get; set; }
    public bool SetLevelup { get; set; }
    public int? SelectedDFKQuest { get; set; }
    public int? SelectedKlaytnQuest { get; set; }
    public int? StaminaPotLevel { get; set; }
    public int? StaminaPotAmount { get; set; }
    public string LevelingEnabled { get; set; } = "true";
    public SfGrid<DFKBotHero> HeroGridReference { get; set; }
    public int SelectedHeroCount { get; set; }
    public decimal? SalePrice { get; set; }
    public LevelUpSetting LevelSettings { get; set; } = new();
    public static int PageSize { get; set; } = 50;
    protected override void OnInitialized()
    {
        TableHeroes = Acc.Accounts.SelectMany(a => a.BotHeroes).ToList();
        if (Acc.Accounts.Count == 0)
        {
            Nav.NavigateTo("Login");
        }
        foreach (DFKBot bot in Bots.RunningBots)
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
        if (!SetQuest && !SetPrice && !SetStampot && !SetLevelup)
        {
            JS.InvokeVoid("alert", "you need to select at least one setting to apply.");
            return;
        }
        foreach (DFKBotHero h in HeroGridReference.SelectedRecords)
        {
            var selectedQuest = h.Account.Chain.Name == "DFK" ? SelectedDFKQuest : SelectedKlaytnQuest;
            if (SetQuest)
            {
                if (selectedQuest.HasValue)
                {
                    h.Quest = QuestContractDefinitions.GetQuestContract(selectedQuest.Value, h.Account.Chain.ChainEnum);
                }
                else
                {
                    h.Quest = null;
                }
            }
            if (SetLevelup)
            {
                h.LevelingEnabled = LevelingEnabled == "true" ? true : LevelingEnabled == "false" ? false : null;
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
            }
            if (SetPrice)
            {
                if (SalePrice < h.FloorEstimate * (Bots.Settings.WarnFloorPercentage / 100))
                {
                    JS.InvokeVoid("alert", $"Not allowed to sell under {(Bots.Settings.WarnFloorPercentage)}% of floor");
                    return;
                }
                h.BotSalePrice = SalePrice;
            }
            if (SetStampot)
            {
                h.StaminaPotionUntilLevel = StaminaPotLevel;
                h.UseStaminaPotionsAmount = StaminaPotAmount;
            }
            var setting = Bots.Settings.HeroQuestSettings.FirstOrDefault(hqs => hqs.HeroId == h.ID.ToString());
            if (setting != null)
            {
                if (SetStampot)
                {
                    setting.StaminaPotionUntilLevel = StaminaPotLevel;
                    setting.UseStaminaPotionsAmount = StaminaPotAmount;
                }
                if (SetLevelup)
                {
                    setting.LevelingEnabled = LevelingEnabled == "true" ? true : LevelingEnabled == "false" ? false : null;
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
                if (SetQuest)
                {
                    setting.QuestId = h.Quest?.Id;
                }
                if (SetPrice)
                {
                    setting.BotSalePrice = SalePrice;
                }
                setting.ChainIdentifier = h.Account.Chain.Identifier;
            }
            else
            {
                HeroQuestSetting newSettings = new()
                {
                    QuestId = SetQuest ? h.Quest?.Id : null,
                    ChainIdentifier = h.Account.Chain.Identifier,
                    HeroId = h.ID.ToString(),
                    BotSalePrice = SetPrice ? SalePrice : null,
                    LevelingEnabled = SetLevelup ? LevelingEnabled == "true" ? true : LevelingEnabled == "false" ? false : null : null,
                    StaminaPotionUntilLevel = SetStampot ? StaminaPotLevel : null,
                    UseStaminaPotionsAmount = SetStampot ? StaminaPotAmount : null
                };
                if (SetLevelup)
                {
                    if (LevelSettings.MainAttribute is not null
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
                }
                Bots.Settings.HeroQuestSettings.Add(newSettings);
            }
        }
        Bots.SaveSettings();
        HeroGridReference.Refresh();
        StateHasChanged();
    }
    public async void ClearFilters()
    {
        await HeroGridReference.ClearFilteringAsync();
        await HeroGridReference.ClearSortingAsync();
    }

    public async void ResetSelection()
    {
        SelectedDFKQuest = null;
        SelectedKlaytnQuest = null;
        SalePrice = null;
        StaminaPotLevel = null;
        StaminaPotAmount = null;
        LevelingEnabled = "true";
        LevelSettings = new();
        await HeroGridReference.ClearSelectionAsync();
    }

    public void ClearAllPreference()
    {
        if (!SetQuest && !SetPrice && !SetStampot && !SetLevelup)
        {
            JS.InvokeVoid("alert", "you need to select at least one setting to clear.");
            return;
        }
        foreach (DFKBotHero h in HeroGridReference.SelectedRecords)
        {
            if (SetQuest)
            {
                h.Quest = null;
            }
            if (SetLevelup)
            {
                h.LevelUpSetting = new();
                h.LevelingEnabled = null;
            }
            if (SetPrice)
            {
                h.BotSalePrice = null;
            }
            if (SetStampot)
            {
                h.StaminaPotionUntilLevel = null;
                h.UseStaminaPotionsAmount = null;
            }
            var setting = Bots.Settings.HeroQuestSettings.FirstOrDefault(hqs => hqs.HeroId == h.ID.ToString());
            if (setting != null)
            {
                if (SetQuest && SetPrice && SetLevelup && SetStampot)
                {
                    Bots.Settings.HeroQuestSettings.Remove(setting);
                }
                else
                {
                    if (SetQuest)
                    {
                        setting.QuestId = null;
                    }
                    if (SetPrice)
                    {
                        setting.BotSalePrice = null;
                    }
                    if (SetLevelup)
                    {
                        setting.LevelingEnabled = null;
                        setting.LevelupSettings = null;
                    }
                    if (SetStampot)
                    {
                        setting.StaminaPotionUntilLevel = null;
                        setting.UseStaminaPotionsAmount = null;
                    }
                    if (setting.QuestId is null && setting.BotSalePrice is null && setting.LevelingEnabled is null && setting.LevelupSettings is null && setting.StaminaPotionUntilLevel is null && setting.UseStaminaPotionsAmount is null)
                    {
                        Bots.Settings.HeroQuestSettings.Remove(setting);
                    }
                }
            }
        }
        Bots.SaveSettings();
        HeroGridReference.Refresh();
        StateHasChanged();
    }
}
