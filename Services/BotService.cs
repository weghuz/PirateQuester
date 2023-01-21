using Microsoft.JSInterop;
using PirateQuester.Bot;
using PirateQuester.Utils;
using System.Text.Json;

namespace PirateQuester.Services
{
	public partial class BotService
	{
		public List<DFKBot> RunningBots { get; set; } = new();
		public DFKBotSettings Settings { get; set; } = new();
        public bool Running { get; set; } = false;
        public IJSInProcessRuntime JS { get; set; }
        public AccountManager Acc { get; }
		public CancellationTokenSource ClearLogsCancellationTokenSource { get; set; }
        public ClearLogsService ClearLogsService { get; set; }
        public delegate void BotUpdated();
		public event BotUpdated UpdatedBot;

        public BotService(AccountManager acc, IJSInProcessRuntime js)
		{
			JS = js;
			Acc = acc;

			var settingsJson = JS.Invoke<string>("localStorage.getItem", "DFKBotSettings");
			ImportBotSettings(settingsJson);
		}

		public void ImportBotSettings(string settingsJson)
		{
            if (settingsJson is not null)
            {
                try
                {
                    var settings = JsonSerializer.Deserialize<DFKBotSettingsDTO>(settingsJson);
                    Settings.CancelTxnDelay = settings.CancelTxnDelay;
                    Settings.ChainQuestEnabled = settings.ChainQuestEnabled ?? new()
                    {
                        new()
                        {
                            Chain = Constants.ChainsList[0],
                            QuestEnabled = Enumerable.Range(0, 25).Select(i => new QuestEnabled() { Enabled = true, QuestId = i }).ToList()
                        },
                        new ()
                        {
                            Chain = Constants.ChainsList[1],
                            QuestEnabled = Enumerable.Range(0, 23).Select(i => new QuestEnabled() { Enabled = true, QuestId = i }).ToList()
                        }
                    };
                    Settings.UpdateInterval = settings.UpdateInterval;
                    Settings.LevelUp = settings.LevelUp;
                    Settings.MaxGasFeeGwei = settings.MaxGasFeeGwei;
                    Settings.HeroQuestSettings = settings.HeroQuestSettings;
                    Settings.MinStamina = settings.MinStamina;
                    Settings.MinTrainingStats = settings.MinTrainingStats;
                    Settings.LevelUpSettings = settings.LevelUpSettings;
                    Settings.UseStaminaPotions = settings.UseStaminaPotions;
                    Settings.QuestHeroes = settings.QuestHeroes;
                    Settings.ForceStampotOnFullXP = settings.ForceStampotOnFullXP;
                    Settings.ClearLogsInterval = settings.ClearLogsInterval;
                    Settings.DownloadClearedLogs = settings.DownloadClearedLogs;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Settings = new();
                }
            }
        }

		public void SaveSettings()
		{
			DFKBotSettingsDTO dto = new();
            dto.ClearLogsInterval = Settings.ClearLogsInterval;
            dto.DownloadClearedLogs = Settings.DownloadClearedLogs;
			dto.CancelTxnDelay = Settings.CancelTxnDelay;
			dto.ChainQuestEnabled = Settings.ChainQuestEnabled;
			dto.UpdateInterval = Settings.UpdateInterval;
			dto.LevelUp = Settings.LevelUp;
			dto.MaxGasFeeGwei = Settings.MaxGasFeeGwei;
			dto.MinStamina = Settings.MinStamina;
			dto.MinTrainingStats = Settings.MinTrainingStats;
			dto.LevelUpSettings = Settings.LevelUpSettings;
			dto.HeroQuestSettings = Settings.HeroQuestSettings;
			dto.UseStaminaPotions = Settings.UseStaminaPotions;
			dto.ForceStampotOnFullXP = Settings.ForceStampotOnFullXP;
			dto.QuestHeroes = Settings.QuestHeroes;
			JS.InvokeVoid("localStorage.setItem", new string[] { "DFKBotSettings", JsonSerializer.Serialize(dto) });
		}

		public bool CheckRunning()
		{
			return RunningBots.Any(bot => bot.IsRunning) && Running;
		}

		public void StopBots()
		{
			Running = false;
			Console.WriteLine($"Attempting to stop bots work.");
			foreach (DFKBot bot in RunningBots)
			{
				bot.StopBot = true;
			}
		}

		public void InvokeUpdates()
		{
			UpdatedBot?.Invoke();
		}

		public Task RunBots()
		{
			SaveSettings();
			Running = true;
			RunningBots = new List<DFKBot>();
			foreach (DFKAccount acc in Acc.Accounts)
			{
				Console.WriteLine(acc.PQTBalance);
				if (acc.PQTBalance < 1)
				{
					Console.WriteLine($"No PQT Balance to run account {acc.Name}");
					continue;
				}
				if(acc.Heroes.Count == 0)
				{
					Console.WriteLine($"No heroes for {acc.Name}:{acc.Account.Address}");
					continue;
				}
				DFKBot bot = new();
				RunningBots.Add(bot);
				Console.WriteLine($"Bot added for Account: {acc.Account.Address}");
				try
				{
					bot.StartBot(acc, Settings, this);
					bot.BotLogAdded += InvokeUpdates;
					bot.HeroesUpdated += InvokeUpdates;
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
				}
			}
			InvokeUpdates();
			if (Settings.ClearLogsInterval > 0)
			{
				if(ClearLogsCancellationTokenSource is not null)
				{
					ClearLogsCancellationTokenSource.Cancel();
				}
				ClearLogsCancellationTokenSource = new();
				ClearLogsService = new(this, ClearLogsCancellationTokenSource.Token);
				ClearLogsService.WaitThenClearLogs();
			}
			
			return Task.CompletedTask;
		}

	}
}
