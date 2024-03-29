﻿using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PirateQuester.Services;
using PirateQuester.Utils;

namespace PirateQuester.Pages;

public partial class Bot
{
    [Inject]
    AccountManager Acc { get; set; }
    [Inject]
    NavigationManager Nav { get; set; }
    [Inject]
    BotService Bots { get; set; }
    [Inject]
    IJSInProcessRuntime JS { get; set; }
    public List<DFKAccount> AccountsMissingPQT { get; set; }
    public static bool ShowDFKQuests { get; set; } = true;

    protected override void OnInitialized()
    {
        if (Acc.Accounts.Count == 0)
        {
            Nav.NavigateTo("Login");
        }

        Bots.UpdatedBot += StateHasChanged;
    }

    public void RefreshBots()
    {
        if (Bots.ClearLogsService is not null)
        {
            Bots.ClearLogsService.RefreshBots();
        }
    }

}
