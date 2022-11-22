using PirateQuester.Bot;
using PirateQuester.Utils;

namespace PirateQuester.Services
{
	public class BotService
	{
		public List<DFKBot> RunningBots { get; set; } = new();
		public DFKBotSettings Settings { get; set; } = new();
        public bool Running { get; set; } = false;
		public AccountManager Acc { get; }
		public delegate void BotUpdated();
		public event BotUpdated UpdatedBot;
		public BotService(AccountManager acc)
		{
			Acc = acc;
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
			Running = true;
			RunningBots = new List<DFKBot>();
			foreach (DFKAccount acc in Acc.Accounts)
			{
				DFKBot bot = new();
				RunningBots.Add(bot);
				Console.WriteLine($"Bot added for Account: {acc.Account.Address}");
				try
				{
					bot.StartBot(acc, Settings);
					bot.BotLogAdded += InvokeUpdates;
					bot.HeroesUpdated += InvokeUpdates;
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
				}
			}
			InvokeUpdates();
			return Task.CompletedTask;
		}

	}
}
