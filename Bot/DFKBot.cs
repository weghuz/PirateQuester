using DFK;
using DFKContracts.QuestCore.ContractDefinition;
using Nethereum.Web3;
using Org.BouncyCastle.Asn1.Cmp;
using PirateQuester.DFK.Contracts;
using PirateQuester.Utils;
using System.Numerics;
using Utils;
using Meditation = DFKContracts.MeditationCircle.ContractDefinition.Meditation;

namespace PirateQuester.Bot;

public class DFKBot
{
	public List<DFKBotLogMessage> DFKBotLog = new();
	public List<Quest> RunningQuests = new();
	public ulong CurrentBlock { get; set; }
	public delegate void UpdatedHeroes();

    public event UpdatedHeroes HeroesUpdated;
	public delegate void AddBotLog();
	public event AddBotLog BotLogAdded;
    public DFKAccount Account { get; set; }
    public DFKBotSettings Settings { get; set; }
    public bool StopBot { get; set; }
	public bool IsRunning { get; set; } = false;
    public void Log(string message)
    {
        Console.WriteLine(message);
		DFKBotLog.Add(new()
        {
            Id = DFKBotLog.Count + 1,
            Message = message + "\n",
            TimeStamp = DateTime.UtcNow
        });
        BotLogAdded?.Invoke();
	}

    public async Task UpdateHeroes()
    {
        Log("Updating Heroes...");
        await Account.UpdateHeroes();
        HeroesUpdated?.Invoke();
	}

    public async void StartBot(DFKAccount account, DFKBotSettings settings)
	{
		IsRunning = true;
		Account = account;
        Settings = settings;
		Log($"Bot added for account {account.Account.Address}!");
		Log("Welcome to Pirate Quester Bot V0.1!");
		Log("Booting up...");
		Log($"Interval: {Settings.UpdateInterval}");
        while(true)
		{
			await UpdateHeroes();
			await Update();
            if (StopBot)
                break;
            await Task.Delay(Settings.UpdateInterval * 1000);
			if (StopBot)
				break;
		}
		IsRunning = false;
		Log($"Bot for account {account.Account.Address} stopped...");
	}

    public static ulong UnixTime()
    {
        return (ulong)DateTimeOffset.UtcNow.ToUnixTimeSeconds();
	}
	public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
	{
		// Unix timestamp is seconds past epoch
		DateTime dateTime = new(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
		dateTime = dateTime.AddSeconds(unixTimeStamp);
		return dateTime;
	}

	public async Task UpdateCurrentBlock()
	{
		CurrentBlock = BigIntToLong(await Account.Signer.Eth.Blocks.GetBlockNumber.SendRequestAsync());
	}

	public static ulong BigIntToLong(BigInteger bigInt)
	{
		return ulong.Parse(bigInt.ToString());
	}
	public async Task<List<Quest>> GetUpdatedQuests()
	{
		for(int i = 0; i < 10; i++)
		{
			try
			{
				var quests = (await Account.Quest.GetAccountActiveQuestsQueryAsync(Account.Account.Address)).ReturnValue1.DistinctBy(q => q.Id).ToList();
				return quests;
			}
			catch(Exception e)
			{
				Console.WriteLine(e.Message);
				await Task.Delay(i * 100);
			}
		}
		return new();
	}
	public async Task Update()
    {
		await UpdateCurrentBlock();
		var quests = await GetUpdatedQuests();
		RunningQuests = new(quests);
		foreach (Quest q in quests)
		{
			q.StartDateTime = UnixTimeStampToDateTime(BigIntToLong(q.StartAtTime));
			q.CompleteDateTime = UnixTimeStampToDateTime(BigIntToLong(q.CompleteAtTime));
			var questContract = ContractDefinitions.GetQuestContractFromAddress(q.QuestAddress);

			if(DateTime.UtcNow > q.CompleteDateTime)
			{
				try
				{
					Log($"Quest #{q.Id} {q.QuestName()} is ready to complete, completing...");
					string okMessage = await new Transaction().CompleteQuest(Account, q.Heroes.First(), Settings.MaxGasFeeGwei);
					Log(okMessage);
					RunningQuests.RemoveAll(remQ => remQ.Id == q.Id);
				}
				catch (Exception e)
				{
					Log(e.Message);
				}
			}
		}

		if (StopBot)
		{
			return;
		}
		if(Settings.LevelUp)
		{
			List<Meditation> activeMeditations = (await Account.Meditation.GetActiveMeditationsQueryAsync(Account.Account.Address)).ReturnValue1;
			List<DFKBotHero> readyToLevelHeroes = Account.BotHeroes
				.Where(h => h.Hero.xp >= h.Hero.XpToLevelUp()
				&& h.Hero.currentQuest == ContractDefinitions.NULL_ADDRESS
				&& h.Hero.StaminaCurrent() <= Settings.MinStamina
				&& !activeMeditations.Any(med => med.HeroId.ToString() == h.Hero.id))
				.ToList();
			Log($"{activeMeditations.Count} Active meditations.");
			foreach (Meditation meditation in activeMeditations)
			{
				if(meditation.StartBlock <= CurrentBlock - 20)
				{
					DFKBotHero hero = Account.BotHeroes.FirstOrDefault(h => h.ID == meditation.HeroId);
					Log($"Hero {hero.ID} is ready to complete meditating.\nCompleting Meditation...");
					try
					{
						string okMessage = await new Transaction().CompleteMeditation(Account, hero.ID, Settings.MaxGasFeeGwei);
						Log($"Hero {hero.ID} Leveled up from {hero.Hero.level} to {hero.Hero.level + 1}!");
						Log(okMessage);
					}
					catch (Exception e)
					{
						Log(e.Message);
					}
				}
			}

			Log($"{readyToLevelHeroes.Count} heroes ready to level up.");
			foreach (DFKBotHero h in readyToLevelHeroes)
			{
				Log($"Hero {h.ID} is ready to level up.\nStarting Meditation...");
				LevelUpSetting setting = Settings.LevelUpSettings.FirstOrDefault(levelSetting => levelSetting.HeroClass == h.Hero.mainClass.ToLower());
				if(setting is null)
				{
					Log($"Found no levelup settings for {h.Hero.mainClass}.");
					continue;
				}
				try
				{
					string okMessage = await new Transaction().StartMeditation(Account, h.ID, setting.MainAttribute.Id, setting.SecondaryAttribute.Id, setting.TertiaryAttribute.Id, Settings.MaxGasFeeGwei);
					Log($"Hero started meditating with Stat settings: \nMain(+1):{setting.MainAttribute.Name}\nSecondary(50%+1):{setting.SecondaryAttribute.Name}\nTertiary(50%+1):{setting.TertiaryAttribute.Name}!");
					Log(okMessage);
				}
				catch (Exception e)
				{
					Log(e.Message);
				}
			}
			if (StopBot)
			{
				return;
			}
		}

		List<DFKBotHero> readyHeroes = Account.BotHeroes
            .Where(h => h.Hero.StaminaCurrent() >= Settings.MinStamina
            && h.Hero.currentQuest == ContractDefinitions.NULL_ADDRESS
			&& h.Hero.salePrice is null)
            .ToList();
        Log($"{readyHeroes.Count} heroes ready to quest");

		foreach (QuestContract quest in readyHeroes.Select(r => r.GetActiveQuest()).DistinctBy(q => q.Id))
		{
			List<Hero> readyQuestHeroes = readyHeroes.Where(h =>
				h.GetActiveQuest().Id == quest.Id)
					.Select(h => h.Hero).ToList();
			Log($"Found {readyQuestHeroes.Count} heroes ready to start {quest.Name}.");
            for(int i = 0; i <= readyQuestHeroes.Count; i+= quest.Category != "Gardening" ? 6 : 2)
			{
                List<Hero> heroBatch = readyQuestHeroes.Skip(i).Take(quest.Category != "Gardening" ? 6 : 2).ToList();
				if(heroBatch.Count == 0)
				{
					continue;
				}
                int maxAttempts = heroBatch.Min(h => quest.AvailableAttempts(h));
                if(maxAttempts == 0)
                {
                    Log("Available attempts too low.");
                    continue;
                }
				try
				{
					Log($"Starting {quest.Name} for {string.Join(", ", heroBatch.Select(h => h.id))}.");
					string okMessage = await new Transaction().StartQuest(Account,
						heroBatch.Select(h => new BigInteger(long.Parse(h.id))).ToList(),
						quest, maxAttempts, Settings.MaxGasFeeGwei);
					Log(okMessage);
				}
				catch (Exception e)
				{
					Log(e.Message);
				}
			}
		}
		Log($"Iteration complete");
    }
}
