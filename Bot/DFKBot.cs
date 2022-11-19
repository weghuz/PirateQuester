using DFK;
using DFKContracts.QuestCore.ContractDefinition;
using Nethereum.Web3;
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
	public static ulong CurrentBlock { get; set; }
	public delegate void AddBotLog();
	public delegate void UpdatedHeroes();

    public static event UpdatedHeroes HeroesUpdated;
	public static event AddBotLog BotLogAdded;
    public DFKAccount Account { get; set; }
    public DFKBotSettings Settings { get; set; }
    public bool Exit {get;set;} = false;
	public List<QueuedQuest> QuestQueue { get; set; } = new();

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
			if (Exit)
				break;
		}
        Log("Bot stopped...");
	}
    public static ulong UnixTime()
    {
        return (ulong)DateTimeOffset.UtcNow.ToUnixTimeSeconds();
	}
	public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
	{
		// Unix timestamp is seconds past epoch
		DateTime dateTime = new(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
		dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
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
				RunningQuests = quests;
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
		var quests = await GetUpdatedQuests();
		await UpdateCurrentBlock();
		foreach (Quest q in quests.OrderBy(quest => quest.Id))
		{
			QuestContract questContract = ContractDefinitions.GetQuestContractFromAddress(q.QuestAddress);
			List<Hero> heroes = new();
			foreach (Hero h in Account.Heroes)
			{
				foreach (BigInteger id in q.Heroes)
				{
					var one = id.ToString();
					var two = h.id;
					if (one == two)
					{
						heroes.Add(h);
						continue;
					}
				}
			}
			QueuedQuest queuedQuest = QuestQueue.FirstOrDefault(queuedQuest => queuedQuest.QuestId == q.Id);
			if (queuedQuest is not null)
			{
				Log($"Check quest completion Found Queued Quest: {CurrentBlock - BigIntToLong(q.StartBlock)} > {questContract.BlocksPerAttempt(heroes.First()) * q.Attempts * q.Heroes.Count * queuedQuest.Queue}");
				q.CompleteBlock = BigIntToLong(q.StartBlock) + (ulong)(questContract.BlocksPerAttempt(heroes.FirstOrDefault()) * q.Attempts * (questContract.Category == "Fishing" || questContract.Category == "Foraging" ? q.Heroes.Count : 1) * queuedQuest.Queue);
				if (CurrentBlock - BigIntToLong(q.StartBlock) > (ulong)(questContract.BlocksPerAttempt(heroes.FirstOrDefault()) * q.Attempts * (questContract.Category == "Fishing" || questContract.Category == "Foraging" ? q.Heroes.Count : 1) * queuedQuest.Queue))
				{
					try
					{
						Log($"Quest #{q.Id} {q.QuestName()} is ready to complete, completing...");
						string okMessage = await new Transaction().CompleteQuest(Account, q.Heroes.First());
						Log(okMessage);
						quests.Remove(q);
					}
					catch (Exception e)
					{
						Log(e.Message);
					}
				}
			}
			else
			{
				Log($"Check quest completion: {CurrentBlock - BigIntToLong(q.StartBlock)} > {questContract.BlocksPerAttempt(heroes.First()) * q.Attempts * q.Heroes.Count}");

				q.CompleteBlock = BigIntToLong(q.StartBlock) + (ulong)(questContract.BlocksPerAttempt(heroes.FirstOrDefault()) * (questContract.Category == "Fishing" || questContract.Category == "Foraging" ? q.Heroes.Count : 1));
				if (CurrentBlock - BigIntToLong(q.StartBlock) > (ulong)(questContract.BlocksPerAttempt(heroes.FirstOrDefault()) * (questContract.Category == "Fishing" || questContract.Category == "Foraging" ? q.Heroes.Count : 1))
					|| CurrentBlock - BigIntToLong(q.StartBlock) > (long)12000)
				{
					try
					{
						Log($"Quest #{q.Id} {q.QuestName()} is ready to complete, completing...");
						string okMessage = await new Transaction().CompleteQuest(Account, q.Heroes.First());
						Log(okMessage);
						quests.Remove(q);
					}
					catch (Exception e)
					{
						if (e.Message.Contains("time"))
						{
							Log($"Quest wasn't complete, establishing new quest queue.");
							List<Quest> questsToQueue = quests.Where(quest => quest.Id >= q.Id && quest.QuestAddress == q.QuestAddress).ToList();
							foreach (Quest questToQueue in questsToQueue)
							{
								queuedQuest = new()
								{
									Contract = questContract,
									QuestId = questToQueue.Id,
									Queue = questsToQueue.IndexOf(questToQueue) + 2
								};
								QuestQueue.Add(queuedQuest);
								quests.Remove(questToQueue);
								questToQueue.CompleteBlock = BigIntToLong(questToQueue.StartBlock) + (ulong)(questContract.BlocksPerAttempt(heroes.FirstOrDefault()) * questToQueue.Attempts * (questContract.Category == "Fishing" || questContract.Category == "Foraging" ? questToQueue.Heroes.Count : 1) * queuedQuest.Queue);
							}
							continue;
						}
						Log(e.Message);
					}
				}
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
			int questsOfType = quests.Where(currentQuest => currentQuest.QuestAddress == quest.Address).Count();
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
						quest, maxAttempts);
					Log(okMessage);
					Log($"Queueing quest {quest.Name} #{questsOfType + 1}");
					QuestQueue.Add(new(){
						Contract = quest,
						QuestId = quest.Id,
						Queue = questsOfType + 1
					});
				}
				catch (Exception e)
				{
					Log(e.Message);
				}
			}
		}
    }
}
