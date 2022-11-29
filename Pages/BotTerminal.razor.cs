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
	[Inject]
	IJSInProcessRuntime JS { get; set; }
	[Parameter]
	public int BotId { get; set; }
    public RadzenDataGrid<QuestReward> QuestRewardDataGrid { get; set; }
    public RadzenDataGrid<Quest> RunningQuestsGrid { get; set; }
	DataGridSettings _questRewardsDataGridSettings;
	public DataGridSettings QuestRewardsDataGridSettings 
	{
		get
		{ 
			return _questRewardsDataGridSettings; 
		} 
		set
		{
			JS.InvokeVoid("localStorage.setItem", new string[] { "QuestRewardsDataGridSettings", JsonConvert.SerializeObject(value) });
			_questRewardsDataGridSettings = value;  
		}
	}
	DataGridSettings _runningQuestsDataGridSettings;
	public DataGridSettings RunningQuestsDataGridSettings
	{
		get
		{
			return _runningQuestsDataGridSettings;
		}
		set
		{
			JS.InvokeVoid("localStorage.setItem", new string[] { "RunningQuestsDataGridSettings", JsonConvert.SerializeObject(value) });
			_runningQuestsDataGridSettings = value;
		}
	}

	protected override void OnAfterRender(bool firstRender)
	{
		Console.WriteLine("render");
		if (firstRender)
		{
			Console.WriteLine("first");
			try
			{
				var questRewardsDataGridSettingsJson = JS.Invoke<string>("localStorage.getItem", "QuestRewardsDataGridSettings");
				if (questRewardsDataGridSettingsJson != null)
				{
					QuestRewardsDataGridSettings = JsonConvert.DeserializeObject<DataGridSettings>(questRewardsDataGridSettingsJson);
				}
				var runningQuestsDataGridSettingsJson = JS.Invoke<string>("localStorage.getItem", "RunningQuestsDataGridSettings");
				if (runningQuestsDataGridSettingsJson != null)
				{
					RunningQuestsDataGridSettings = JsonConvert.DeserializeObject<DataGridSettings>(questRewardsDataGridSettingsJson);
				}
			}
			catch(Exception e)
			{
				Console.WriteLine(e.Message);
			}
			StateHasChanged();
		}
	}

	protected override void OnInitialized()
	{
		Bots.UpdatedBot += StateHasChanged;
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
