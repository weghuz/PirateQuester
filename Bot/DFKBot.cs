using DFK;
using DFKContracts.QuestCore.ContractDefinition;
using Org.BouncyCastle.Asn1.Cmp;
using PirateQuester.DFK.Contracts;
using PirateQuester.Utils;
using System.Numerics;
using Utils;

namespace PirateQuester.Bot;

public class DFKBot
{
    public static List<DFKBotLogMessage> DFKBotLog = new();
	public static List<Quest> RunningQuests = new();

	public delegate void AddBotLog();
	public delegate void UpdatedHeroes();

    public static event UpdatedHeroes HeroesUpdated;
	public static event AddBotLog BotLogAdded;
    public DFKAccount Account { get; set; }
    public DFKBotSettings Settings { get; set; }
    public bool Exit {get;set;} = false;

    public static void Log(string message)
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
    public void StopBot()
    {
        Exit = true;
    }
    public async void StartBot(DFKAccount account, DFKBotSettings settings)
	{
		Account = account;
        Settings = settings;
		Exit = false;
		Log("Welcome to Pirate Quester Bot V0.1!");
		Log("Booting up...");
		Log($"Interval: {Settings.UpdateInterval}");
        while(true)
		{
			await UpdateHeroes();
			await Update();
            if (Exit)
                break;
            await Task.Delay(Settings.UpdateInterval * 1000);
		}
        Log("Bot stopped...");
	}
    public static long UnixTime()
    {
        return DateTimeOffset.UtcNow.ToUnixTimeSeconds();
	}

    public async Task Update()
    {
        var quests = await Account.Quest.GetAccountActiveQuestsQueryAsync(Account.Account.Address);
        RunningQuests = quests.ReturnValue1;
		foreach (Quest q in quests.ReturnValue1)
		{
			if (q.CompleteAtTime <= UnixTime())
			{
                try
				{
					Log($"Quest #{q.Id} is ready to complete, completing...");
					string okMessage = await new Transaction().CompleteQuest(Account, q.Heroes.First());
					Log(okMessage);
				}
                catch(Exception e)
				{
					Log(e.Message);
				}
			}
        }

		List<DFKBotHero> readyHeroes = Account.BotHeroes
            .Where(h => h.Hero.StaminaCurrent() >= Settings.MinStamina 
            && h.Hero.currentQuest == ContractDefinitions.NULL_ADDRESS)
            .ToList();
        Log($"{readyHeroes.Count} heroes ready to quest");
        foreach (QuestContract quest in readyHeroes.Select(r => r.GetActiveQuest()).DistinctBy(q => q.Id))
		{
			List<Hero> readyQuestHeroes = readyHeroes.Where(h =>
				h.GetActiveQuest().Id == quest.Id)
					.Select(h => h.Hero).ToList();

			Log($"Found {readyQuestHeroes.Count} heroes ready to start {quest.Name}.");
            for(int i = 0; i <= readyQuestHeroes.Count; i+= quest.Category != "Gardening" ? 6 : 1)
			{
                List<Hero> heroBatch = readyQuestHeroes.Skip(i).Take(6).ToList();
                int maxAttempts = heroBatch.Min(h => quest.AvailableAttempts(h));
                if(maxAttempts == 0)
                {
                    Log("Available attempts too low.");
                    continue;
                }
				try
				{
					string okMessage = await new Transaction().StartQuest(Account,
						readyQuestHeroes.Select(h => new BigInteger(ulong.Parse(h.id))).ToList(),
						quest, maxAttempts);
					Log(okMessage);
				}
				catch (Exception e)
				{
					Log(e.Message);
				}
			}
		}
    }
}
