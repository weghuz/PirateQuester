using PirateQuester.Utils;

namespace PirateQuester.Services
{
	public class AccountUpdaterService
	{
		public AccountManager Acc { get; set; }
		public AccountUpdaterService(AccountManager acc)
		{
			Acc = acc;
		}

		public async void DoWorkAsync(CancellationToken stoppingToken, Action updateState)
		{
			while (!stoppingToken.IsCancellationRequested)
			{
				Console.WriteLine("Updating Accounts");
				foreach (DFKAccount acc in Acc.Accounts)
				{
					await acc.UpdateBalance();
				}
				updateState();
				await Task.Delay(10000, stoppingToken);
			}
		}

		public async Task UpdateAccounts()
		{
			foreach (DFKAccount acc in Acc.Accounts)
			{
				await acc.UpdateBalance();
			}
		}
	}
}