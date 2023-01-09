using DFK;
using DFKContracts.QuestCore.ContractDefinition;
using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;
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


		//account.Signer.Processing.Logs.CreateProcessor<QuestRewardEventDTO>((log) => Console.WriteLine(log.Log.Data));

		await account.InitializeAccount(settings);
		while (true)
		{
			await UpdateHeroes();
			await Update();
            if (StopBot)
                break;
			await UpdateQuestRewards();
			await Task.Delay(1000 * Settings.UpdateInterval);
			if (StopBot)
				break;
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
					var item = ItemContractDefinitions.GetItem(reward.Reward);
					if (item is not null)
					{
						item.Amount = ulong.Parse(reward.Amount.ToString());
						questReward.Rewards.AddItem(item);
					}
					else
					{
						questReward.Rewards.AddItem(new()
						{
							Address = reward.Reward,
							Amount = ulong.Parse(reward.Amount.ToString())
						});
					}
				}
				questRewards.Add(questReward);
			}
		}
		questRewards.AddRange(QuestRewards);
		QuestRewards = questRewards;
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

	public int GetMinStaminaBotHero(DFKBotHero h)
	{
		if(h.Quest is null && h.SuggestedQuest is null)
		{
			return 999;
		}
		int stam = ChainQuestSettings.FirstOrDefault(qs => qs.QuestId == (h.Quest?.Id ?? h.SuggestedQuest.Id))?.MinStamina ?? Settings.MinStamina;
		
        return stam;
	}

	public async Task Update()
    {
		await UpdateCurrentBlock();
		var quests = await GetUpdatedQuests();
		RunningQuests = new(quests);
		foreach (Quest q in quests)
		{
			q.StartDateTime = Functions.UnixTimeStampToDateTime(Functions.BigIntToLong(q.StartAtTime));
			q.CompleteDateTime = Functions.UnixTimeStampToDateTime(Functions.BigIntToLong(q.CompleteAtTime));
			var questContract = QuestContractDefinitions.GetQuestContractFromAddress(q.QuestAddress);

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

		if (Settings.UseStaminaPotions)
		{
			Log($"Stamina Potions: {Account.StaminaPotionBalance}");

			var staminaPotionHeroes = Account.BotHeroes.Where(hero => 
				(hero.UseStaminaPotionsAmount is not null 
				|| hero.StaminaPotionUntilLevel is not null)
				&& hero.Hero.StaminaCurrent() <= 5
				&& (Settings.ForceStampotOnFullXP ? true : hero.Hero.xp != hero.Hero.XpToLevelUp())
				&& !activeMeditations.Any(med => med.HeroId.ToString() == hero.Hero.id)).ToList();

			string staminaPotionAddress = ItemContractDefinitions.InventoryItems.First(item => item.Name == "Stamina Potion").Addresses.First(a => a.Chain.Name == Account.Chain.Name).Address;
			Log($"Heroes ready to be stamina potioned: {string.Join(", ", staminaPotionHeroes.Select(h => h.ID))}");
			if (Account.StaminaPotionBalance == 0 && staminaPotionHeroes.Count > 0)
			{
				Log($"The account is completely out of stamina potions.");
			}
			else
			{
				foreach (DFKBotHero h in staminaPotionHeroes)
				{
					if (h.StaminaPotionUntilLevel is not null && h.Hero.level >= h.StaminaPotionUntilLevel)
					{
						Log($"Hero #{h.ID} is level {h.Hero.level}, no longer using stamina potions.");
						var heroSettings = Bots.Settings.HeroQuestSettings.FirstOrDefault(hqs => hqs.HeroId == h.ID.ToString());
						h.StaminaPotionUntilLevel = null;
						heroSettings.StaminaPotionUntilLevel = h.StaminaPotionUntilLevel;
						continue;
					}
					else if (h.StaminaPotionUntilLevel is not null && Account.StaminaPotionBalance > 0 && h.Hero.StaminaCurrent() <= 5)
					{
						if(h.Hero.level < h.StaminaPotionUntilLevel.Value)
						{
							Log($"Hero #{h.ID} is level {h.Hero.level}, need to reach level {h.StaminaPotionUntilLevel} to stop using stamina potions. Hero is at 5 stamina or less. Using Potion...");
							try
							{
								string okMessage = await Transaction.UseComsumableItem(Account, h.ID, staminaPotionAddress, Settings.MaxGasFeeGwei, Settings.CancelTxnDelay);
								h.Hero.staminaFullAt -= 30000;
								h.Hero.StaminaPotioned = true;
								Account.StaminaPotionBalance--;
								Log(okMessage);
							}
							catch (Exception e)
							{
								Log(e.Message);
							}
						}
						else
						{
							Log($"Hero #{h.ID} reached level {h.Hero.level}, no longer using stamina potions.");
						}
					}
					if (h.UseStaminaPotionsAmount is not null && h.UseStaminaPotionsAmount <= 0)
					{
						Log($"Hero #{h.ID} has no stamina potions left.");
						h.UseStaminaPotionsAmount = null;
						continue;
					}
					else if (h.UseStaminaPotionsAmount is not null && Account.StaminaPotionBalance > 0 && h.Hero.StaminaCurrent() <= 5)
					{
						if(h.UseStaminaPotionsAmount.Value > 0)
						{
							Log($"Hero #{h.ID} has {h.UseStaminaPotionsAmount} stamina potions left to use. Hero is at 5 stamina or less. Using Potion...");
							try
							{
								string okMessage = await Transaction.UseComsumableItem(Account, h.ID, staminaPotionAddress, Settings.MaxGasFeeGwei, Settings.CancelTxnDelay);
								h.Hero.staminaFullAt -= 30000;
								h.UseStaminaPotionsAmount--;
								var heroSettings = Bots.Settings.HeroQuestSettings.FirstOrDefault(hqs => hqs.HeroId == h.ID.ToString());
								heroSettings.UseStaminaPotionsAmount = h.UseStaminaPotionsAmount;
								Account.StaminaPotionBalance -= 1;
								h.Hero.StaminaPotioned = true;
								Log(okMessage);
							}
							catch (Exception e)
							{
								Log(e.Message);
							}
						}
						else
						{
							Log($"Used all stamina potions for hero #{h.ID}.");
						}
					}
				}
			}
			Bots.SaveSettings();
		}

		if (StopBot)
		{
			return;
		}

		if (Settings.SellHeroes)
		{
			List<DFKBotHero> heroesToSell = Account.BotHeroes
				.Where(h => h.BotSalePrice is not null
					&& h.Hero.salePrice is null
					&& RunningQuests.Any(rq => rq.Heroes.Contains(h.ID)) is false
					&& h.Hero.StaminaCurrent() < GetMinStaminaBotHero(h)
					&& !activeMeditations.Any(med => med.HeroId.ToString() == h.Hero.id))
				.ToList();
			Log($"There are {heroesToSell.Count} heroes to sell.");
			foreach(DFKBotHero h in heroesToSell)
			{
                Log($"Selling hero #{h.ID} for {h.BotSalePrice} {(Account.Chain.Name == "DFK" ? "Crystal" : "Jade")}...");
                try
                {
                    string okMessage = await Transaction.StartAuction(Account, h.ID, h.BotSalePrice.Value, Settings.MaxGasFeeGwei, Settings.CancelTxnDelay);
                    h.Hero.salePrice = h.BotSalePrice.Value.ToString();
                    Log(okMessage);
                }
                catch (Exception e)
                {
                    Log(e.Message);
                }
            }
            List<DFKBotHero> heroesToCancelAuction = Account.BotHeroes
				.Where(h => (Settings.CancelUnpricedHeroSales ? h.Hero.salePrice is not null : h.BotSalePrice is not null) 
					&& h.Hero.salePrice is not null
					&& h.Hero.StaminaCurrent() >= GetMinStaminaBotHero(h)
					&& !activeMeditations.Any(med => med.HeroId.ToString() == h.Hero.id))
				.ToList();
            Log($"There are {heroesToCancelAuction.Count} heroes on auction that need to be cancelled to quest.");
			foreach(DFKBotHero h in heroesToCancelAuction)
			{
                Log($"Cancelling auction for hero #{h.ID}...");
                try
                {
                    string okMessage = await Transaction.CancelAuction(Account, h.ID, Settings.MaxGasFeeGwei, Settings.CancelTxnDelay);
					h.Hero.salePrice = null;
                    Log(okMessage);
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

		if (Settings.QuestHeroes is false)
		{
			Log($"QuestHeroes disabled. Not questing heroes.");
		}
		else
		{
			List<DFKBotHero> readyHeroes = Account.BotHeroes
				.Where(h => (h.Hero.StaminaCurrent() == h.Hero.stamina ? h.Hero.stamina : h.Hero.StaminaCurrent() - 1) >= GetMinStaminaBotHero(h)
				&& RunningQuests.SelectMany(rq => rq.Heroes).Contains(h.ID) is false
				&& h.Hero.salePrice is null
				&& !activeMeditations.Any(med => med.HeroId.ToString() == h.Hero.id))
				.ToList();
			Log($"{readyHeroes.Count} heroes ready to quest");
			var enabledQuests = Settings.ChainQuestEnabled.Find(cqe => cqe.Chain.Name == Account.Chain.Name).QuestEnabled.Where(q => q.Enabled).ToList();
			foreach (QuestContract quest in readyHeroes
				.Select(r => r.GetActiveQuest())
				.DistinctBy(q => q.Id)
				.Where(q => enabledQuests.Select(qe => qe.QuestId).Contains(q.Id)))
			{
				var questsOfType = RunningQuests.Where(rq => quest.Address == rq.QuestAddress);
				QuestEnabled questSettings = enabledQuests.FirstOrDefault(qe => qe.QuestId == quest.Id);
				if (questsOfType.Any(rq => rq.CompleteDateTime >= DateTime.UtcNow.AddHours(12)) || questsOfType.Count() >= 10)
				{
					continue;
				}
				List<Hero> readyQuestHeroes = readyHeroes.Where(h =>
					h.GetActiveQuest().Id == quest.Id
					&& (quest.Category.ToLower() == "training"
					|| h.Hero.profession == quest.Category.ToLower()))
						.Select(h => h.Hero).ToList();
				List<Hero> readyQuestUnalignedHeroes = readyHeroes.Where(h =>
					h.GetActiveQuest().Id == quest.Id
					&& h.Hero.profession != quest.Category.ToLower()
					&& quest.Category.ToLower() != "training")
						.Select(h => h.Hero).ToList();

				Log($"Found {readyQuestHeroes.Count} profession aligned heroes ready to start {quest.Name}.");
				for (int i = 0; i <= readyQuestHeroes.Count; i += quest.MaxHeroesPerQuest(Account))
				{
					List<Hero> heroBatch = readyQuestHeroes.Skip(i).Take(quest.MaxHeroesPerQuest(Account)).ToList();

					int heroesSetForQuest = Account.BotHeroes.Where(h =>
						h.GetActiveQuest().Id == quest.Id
						&& h.Hero.profession == quest.Category.ToLower()
						&& (h.Hero.salePrice == null || Settings.CancelUnpricedHeroSales))
						.Count();

					if (heroBatch.Count() < quest.MaxHeroesPerQuest(Account) && heroBatch.Count() < heroesSetForQuest)
					{
						if (enabledQuests.FirstOrDefault(qe => qe.QuestId == quest.Id).QuestEagerly is false)
						{
							Log($"Not enough heroes to start {quest.Name}. {heroBatch.Count()} heroes {(heroBatch.Count() == 1 ? "is" : "are")} ready, {quest.MaxHeroesPerQuest(Account)} are needed.");
							continue;
						}
						else
						{
							List<Hero> heroesCatchingUp = Account.BotHeroes.Where(h =>
								h.GetActiveQuest().Id == quest.Id
								&& h.Hero.StaminaCurrent() > GetMinStaminaBotHero(h) - 5
								&& h.Hero.StaminaCurrent() < GetMinStaminaBotHero(h)
								&& RunningQuests.SelectMany(rq => rq.Heroes).Contains(h.ID) is false
								&& h.Hero.profession == quest.Category.ToLower())
								.Select(h => h.Hero)
								.ToList();
							if (heroesCatchingUp.Count() > 0)
							{
								Log($"Heroes are catching up to {string.Join(", ", heroBatch.Select(h => h.id))} to make a full sqad for {quest.Name}.");
								var checkBatch = heroBatch.Where(h => h.StaminaCurrent() == h.stamina || h.StaminaPotioned).ToList();
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
					}
					if (heroBatch is null || heroBatch.Count == 0)
					{
						continue;
					}

					//Order for optimal mining if mining
					if (quest.Category == "Mining")
					{
						heroBatch = heroBatch.OrderByDescending(h => h.mining + h.strength + h.endurance).ToList();
					}

					int maxAttempts = heroBatch.Select(h => quest.AvailableAttempts(h)).Min();

					if (maxAttempts == 0)
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

				Log($"Found {readyQuestUnalignedHeroes.Count} profession unaligned heroes ready to start {quest.Name}.");
				for (int i = 0; i <= readyQuestUnalignedHeroes.Count; i += quest.MaxHeroesPerQuest(Account))
				{
					List<Hero> heroBatch = readyQuestUnalignedHeroes.Skip(i).Take(quest.MaxHeroesPerQuest(Account)).ToList();

					int heroesSetForQuest = Account.BotHeroes.Where(h =>
						h.GetActiveQuest().Id == quest.Id
						&& h.Hero.profession != quest.Category.ToLower()
						&& (h.Hero.salePrice == null || Settings.CancelUnpricedHeroSales))
						.Count();

					if (heroBatch.Count() < quest.MaxHeroesPerQuest(Account) && heroBatch.Count() < heroesSetForQuest)
					{
						if (enabledQuests.FirstOrDefault(qe => qe.QuestId == quest.Id).QuestEagerly is false)
						{
							Log($"Not enough heroes to start {quest.Name}. {heroBatch.Count()} heroes {(heroBatch.Count() == 1 ? "is" : "are")} ready, {quest.MaxHeroesPerQuest(Account)} are needed.");
							continue;
						}
						else
						{
							List<Hero> heroesCatchingUp = Account.BotHeroes.Where(h =>
								h.GetActiveQuest().Id == quest.Id
								&& h.Hero.profession != quest.Category.ToLower()
								&& h.Hero.StaminaCurrent() > GetMinStaminaBotHero(h) - 5
								&& h.Hero.StaminaCurrent() < GetMinStaminaBotHero(h)
								&& RunningQuests.SelectMany(rq => rq.Heroes).Contains(h.ID) is false)
								.Select(h => h.Hero)
								.ToList();
							if (heroesCatchingUp.Count() > 0)
							{
								Log($"Heroes are catching up to {string.Join(", ", heroBatch.Select(h => h.id))} to make a full sqad for {quest.Name}.");
								var checkBatch = heroBatch.Where(h => h.StaminaCurrent() == h.stamina || h.StaminaPotioned).ToList();
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
					}
					if (heroBatch is null || heroBatch.Count == 0)
					{
						continue;
					}
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
			await Task.Delay(1);
		}
		Log($"Iteration complete");
    }
}
