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

    public delegate void AddBotLog();

    public static event AddBotLog BotLogAdded;
    public List<DFKAccount> Accounts { get; set; }
    public DFKBotSettings Settings { get; set; }
    public bool Exit {get;set;} = false;
    public DFKBot(List<DFKAccount> accounts, DFKBotSettings settings)
    {
        Accounts = accounts;
        Settings = settings;
    }

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
        foreach(DFKAccount acc in Accounts)
        {
            await acc.UpdateHeroes();
        }
    }

    public async Task StartBot()
    {
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
    }

    public async Task Update()
    {
        foreach(DFKAccount acc in Accounts)
		{
            List<Hero> questingHeroes = acc.BotHeroes.Select(h => h.Hero).Where(h => h.currentQuest != ContractDefinitions.NULL_ADDRESS).ToList();
			Log($"Found {questingHeroes.Count} heroes questing Completing...");
			foreach (Hero h in questingHeroes)
			{
				string okMessage = await new Transaction().CompleteQuest(acc, h);
                Log(okMessage);
			}

			List<DFKBotHero> readyHeroes = acc.BotHeroes.Where(h => h.Hero.StaminaCurrent() >= Settings.MinStamina).ToList();
            Log($"{readyHeroes.Count} heroes ready to quest");
            foreach (QuestContract quest in readyHeroes.Select(r => r.GetActiveQuest()).DistinctBy(q => q.Id))
			{
				List<Hero> readyQuestHeroes = readyHeroes.Where(h =>
					h.GetActiveQuest().Id == quest.Id)
						.Select(h => h.Hero).ToList();
				Log($"Found {readyQuestHeroes.Count} heroes ready to start {quest.Name}.");
				try
				{
					string okMessage = await new Transaction().StartQuest(acc,
						readyQuestHeroes.Select(h => new BigInteger(ulong.Parse(h.id))).ToList(), 
                        quest, Settings.MinStamina/quest.StaminaDrain);
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
