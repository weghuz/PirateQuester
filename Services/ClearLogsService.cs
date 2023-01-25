using PirateQuester.Bot;
using PirateQuester.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PirateQuester.Services
{
	public class ClearLogsService
	{
		public BotService Bots { get; set; }
		public CancellationToken CancellationToken { get; }

		public ClearLogsService(BotService bots, CancellationToken cancellationToken)
		{
			Bots = bots;
			CancellationToken = cancellationToken;
		}

		public async void WaitThenClearLogs()
		{
			while (CancellationToken.IsCancellationRequested is false)
			{
				Console.WriteLine($"Waiting {Bots.Settings.ClearLogsInterval * 1000} seconds then clearing logs.");
				await Task.Delay(Bots.Settings.ClearLogsInterval * 1000, CancellationToken);
				if (CancellationToken.IsCancellationRequested is false)
				{
					Console.WriteLine($"Clearing logs....");
					RefreshBots();
				}
			}
			Console.WriteLine($"Clear Logs Service Cancelled.");
		}

		public void RefreshBots()
		{
			if (Bots.Settings.DownloadClearedLogs)
			{
				DownloadLogs();
			}
			Bots.RunBots();
		}

		public void DownloadLogs()
		{

			PirateQuesterLogFile pirateQuesterLogFile = new();

			foreach (DFKBot bot in Bots.RunningBots)
			{
				(var console, var quests) = bot.GetLogsAndStop();

				PirateQuesterBotLogFile pirateQuesterBotLogFile = new()
				{
					Account = $"{bot.Account.Account.Address} {bot.Account.Name}",
					Chain = bot.Account.Chain.Name,
					ExportedTime = DateTime.Now,
					Quests = quests.Select(q => new QuestLog()
					{
						QuestId = q.QuestId.ToString(),
						QuestName = q.Quest.Name,
						Heroes = q.Heroes.Select(h => h.ToString()).ToList(),
						Rewards = q.Rewards.Items.Select(i =>
						new ItemLog()
						{
							Name = i.Name,
							Amount = i.Amount.ToString()
						}).ToList()
					}).ToList(),
					Logs = console
				};
				pirateQuesterLogFile.pirateQuesterBotLogFiles.Add(pirateQuesterBotLogFile);
			}

			Bots.JS.Invoke<string>("download", $"PirateQuesterLog{DateTime.Now:yyyy-MM-ddThh:mm}", JsonSerializer.Serialize(pirateQuesterLogFile, new JsonSerializerOptions()
			{
				ReferenceHandler = ReferenceHandler.IgnoreCycles,
			}));
		}
	}
}
