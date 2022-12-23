using DFK;
using DFKContracts.QuestCore.ContractDefinition;
using Nethereum.Contracts;
using PirateQuester.DFK.Contracts;
using PirateQuester.DFK.Items;
using PirateQuester.Utils;
using System.Numerics;
using Utils;
using Meditation = DFKContracts.MeditationCircle.ContractDefinition.Meditation;

namespace PirateQuester.Bot;

public class DFKBot
{
	public List<DFKBotLogMessage> DFKBotLog = new();
	public List<Quest> RunningQuests = new();
	public List<QuestReward> QuestRewards = new();
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
            TimeStamp = DateTime.Now.ToLocalTime()
        });
        BotLogAdded?.Invoke();
	}

    public async Task UpdateHeroes()
    {
        Log("Updating Heroes...");
		await Account.InitializeAccount();
        Log($"Account updated with {Account.BotHeroes.Count} heroes.");
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

		
		foreach(DFKBotHero hero in Account.BotHeroes.Where(h => h.Quest is not null))
		{
			Console.WriteLine($"{hero.ID} Prefers {hero.Quest.Name}");
		}
		while (true)
		{
			try
            {
				await account.UpdateBalance();
				Console.WriteLine($"PQT Balance: {account.PQTBalance} {account.Account.Address}");
				if (account.PQTBalance < 1)
                {
                    Log("No PQT Balance, stopping bot.");
                    StopBot = true;
                    break;
                }
                await UpdateHeroes();
				await Update();
				await UpdateQuestRewards();
				if (StopBot)
					break;
				await Task.Delay(1000 * Settings.UpdateInterval);
				if (StopBot)
					break;
			}
			catch(Exception e)
			{
				Log($"{account.Account.Address}\n{e.Message}\n{e.StackTrace}\n{e.InnerException?.Message}");
			}
		}
		IsRunning = false;
		Log($"Bot for account {account.Account.Address} stopped...");
	}

	private async Task UpdateQuestRewards()
	{
		Log($"Updating Questrewards");
		List<EventLog<RewardMintedEventDTO>> EventLog = await LogReader.GetQuestRewardLogs(Account);
		List<QuestReward> questRewards = new();
		foreach (BigInteger questId in EventLog.Select(el => el.Event.QuestId).Distinct())
		{
			if(!QuestRewards.Any(qr => qr.QuestId == questId))
			{
				List<RewardMintedEventDTO> rewards = EventLog.Where(el => el.Event.QuestId == questId).Select(el => el.Event).ToList();
				QuestReward questReward = new QuestReward(questId);
				foreach (RewardMintedEventDTO reward in rewards)
				{
					if(questReward.Heroes.Any(id => id == reward.HeroId) is false)
					{
						questReward.Heroes.Add(reward.HeroId);
					}
					var item = new DFKItem(ItemContractDefinitions.GetItem(new()
                                {
                                    Address = reward.Reward,
                                    Chain = Account.Chain
                                }));
					if (item is not null)
					{
						item.Amount = ulong.Parse(reward.Amount.ToString());
						questReward.Rewards.AddItem(item);
					}
					else
					{
						questReward.Rewards.AddItem(new()
						{
							Addresses = new()
							{
								new()
								{
                                    Address = reward.Reward,
									Chain = Account.Chain
                                }
							},
							Amount = ulong.Parse(reward.Amount.ToString())
						});
					}
				}
				questRewards.Add(questReward);
			}
		}
		questRewards.AddRange(QuestRewards);
		QuestRewards = questRewards.OrderByDescending(q => q.QuestId).ToList();
		Log($"Quest rewards updated.");
	}

	public async Task UpdateCurrentBlock()
	{
		CurrentBlock = Functions.BigIntToLong(await Functions.CurrentBlock(Account.Signer));
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
		Log($"{RunningQuests.Count} Active quests.");
		foreach (Quest q in quests)
		{
			q.StartDateTime = Functions.UnixTimeStampToDateTime(Functions.BigIntToLong(q.StartAtTime));
			q.CompleteDateTime = Functions.UnixTimeStampToDateTime(Functions.BigIntToLong(q.CompleteAtTime));
			var questContract = QuestContractDefinitions.GetQuestContractFromAddress(q.QuestAddress);

			if(DateTime.UtcNow > q.CompleteDateTime)
			{
				try
				{
					List<BigInteger> onSaleHeroIds = new();
					foreach(DFKBotHero h in Account.BotHeroes.Where(bh => q.Heroes.Any(qh => qh == bh.ID)))
					{
						if(h.Hero.salePrice is not null)
						{
							onSaleHeroIds.Add(h.ID);
						}
					}
					if(onSaleHeroIds.Count > 0)
					{
						Log($"{(onSaleHeroIds.Count == 1 ? "Hero" : "Heroes")} #{string.Join(", #", onSaleHeroIds)} {(onSaleHeroIds.Count == 1 ? "is" : "are")} on sale, can not complete quest.");
						continue;
					}
					Log($"Quest #{q.Id} {q.QuestName} is ready to complete, completing...");
					string okMessage = await Transaction.CompleteQuest(Account, q.Heroes.First(), Settings.MaxGasFeeGwei, Settings.CancelTxnDelay);
					Log(okMessage);
				}
				catch (Exception e)
				{
					Log(e.Message);
				}
			}
			await Task.Delay(1);
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
				&& h.Hero.currentQuest == QuestContractDefinitions.NULL_ADDRESS
				&& h.Hero.StaminaCurrent() <= Settings.MinStamina
				&& h.Hero.salePrice is null
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
						string okMessage = await Transaction.CompleteMeditation(Account, hero.ID, Settings.MaxGasFeeGwei, Settings.CancelTxnDelay);
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
					string okMessage = await Transaction.StartMeditation(Account, h.ID, setting.MainAttribute.Id, setting.SecondaryAttribute.Id, setting.TertiaryAttribute.Id, Settings.MaxGasFeeGwei, Settings.CancelTxnDelay);
					Log($"Hero started meditating with Stat settings: \nMain(+1):{setting.MainAttribute.Name}\nSecondary(50%+1):{setting.SecondaryAttribute.Name}\nTertiary(50%+1):{setting.TertiaryAttribute.Name}!");
					Log(okMessage);
				}
				catch (Exception e)
				{
					Log(e.Message);
					if(e.Message.Contains("burn"))
					{
						break;
					}
				}
			}
			await Task.Delay(1);
			if (StopBot)
			{
				return;
			}
		}

		List<DFKBotHero> readyHeroes = Account.BotHeroes
            .Where(h => h.Hero.StaminaCurrent() >= Settings.MinStamina
            && RunningQuests.SelectMany(rq => rq.Heroes).Contains(h.ID) is false
			&& h.Hero.salePrice is null)
            .ToList();
        Log($"{readyHeroes.Count} heroes ready to quest");

		foreach (QuestContract quest in readyHeroes
			.Select(r => r.GetActiveQuest())
			.DistinctBy(q => q.Id)
			.Where(q => Settings.QuestEnabled.All(qe => Settings.QuestEnabled[q.Id].Enabled)))
		{
			var questsOfType = RunningQuests.Where(rq => quest.Address == rq.QuestAddress);
			if (questsOfType.Any(rq =>  rq.CompleteDateTime >= DateTime.UtcNow.AddHours(12)) || questsOfType.Count() >= 10)
			{
				continue;
			}
			List<Hero> readyQuestHeroes = readyHeroes.Where(h =>
				h.GetActiveQuest().Id == quest.Id)
					.Select(h => h.Hero).ToList();
			Log($"Found {readyQuestHeroes.Count} heroes ready to start {quest.Name}.");
            for(int i = 0; i <= readyQuestHeroes.Count; i+= quest.Category != "Gardening" ? 6 : 2)
			{
                List<Hero> heroBatch = readyQuestHeroes.Skip(i).Take(quest.Category != "Gardening" ? 6 : 2).ToList();
				
				if (heroBatch.Count() < (quest.Category != "Gardening" ? 6 : 2))
				{
					List<Hero> heroesCatchingUp = Account.BotHeroes.Where(h =>
						(h.Quest?.Id ?? h.SuggestedQuest.Id) == quest.Id
						&& h.Hero.StaminaCurrent() > Settings.MinStamina - 5 
						&& h.Hero.StaminaCurrent() < Settings.MinStamina 
						&& h.Hero.currentQuest == QuestContractDefinitions.NULL_ADDRESS)
						.Select(h => h.Hero)
						.ToList();
					if (heroesCatchingUp.Count() > 0)
					{
						Log($"Heroes are catching up to {string.Join(", ", heroBatch.Select(h => h.id))} to make a full sqad for {quest.Name}.");
						var checkBatch = heroBatch.Where(h => h.StaminaCurrent() == h.stamina).ToList();
						if (checkBatch.Count() > 0)
						{
							Log($"{string.Join(", ", heroBatch.Select(h => h.id))} are so energetic they don't care.");
						}
						else
						{
							continue;
						}
					}
				}
                if (heroBatch is null || heroBatch.Count == 0)
                {
                    continue;
                }
				
                int maxAttempts = heroBatch.Select(h => quest.AvailableAttempts(h)).Min();
				
                if(maxAttempts == 0)
                {
                    Log("Available attempts too low.");
                    continue;
                }
				try
				{
					Log($"Starting {quest.Name} for {string.Join(", ", heroBatch.Select(h => h.id))}.");
					string okMessage = await Transaction.StartQuest(Account,
						heroBatch.Select(h => new BigInteger(long.Parse(h.id))).ToList(),
						quest, maxAttempts, Settings.MaxGasFeeGwei, Settings.CancelTxnDelay);
					Log(okMessage);
				}
				catch (Exception e)
				{
					Log(e.Message);
				}
			}
			await Task.Delay(1);
		}
		Log($"Iteration complete");
    }
}
