using DFK;
using Nethereum.Contracts;
using PirateQuester.DFK.Contracts;
using PirateQuester.DFK.Items;
using PirateQuester.QuestCoreV3.ContractDefinition;
using PirateQuester.Services;
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
    public List<QuestEnabled> ChainQuestSettings { get; set; }
    public bool StopBot { get; set; }
    public BotService Bots { get; private set; }
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

    internal Tuple<List<DFKBotLogMessage>, List<QuestReward>> GetLogsAndStop()
    {
        StopBot = true;

        return new Tuple<List<DFKBotLogMessage>, List<QuestReward>>(DFKBotLog, QuestRewards);
    }

    public async Task UpdateHeroes()
    {
        Log("Updating Heroes...");
        await Account.InitializeAccount();
        Log($"Account updated with {Account.BotHeroes.Count} heroes.");
        HeroesUpdated?.Invoke();
    }

    public async void StartBot(DFKAccount account, DFKBotSettings settings, BotService bots)
    {
        Bots = bots;
        IsRunning = true;
        Account = account;
        Settings = settings;
        Log($"Bot added for account {account.Account.Address}!");
        Log($"Welcome to Pirate Quester Bot {Constants.VERSION}!");
        Log("Booting up...");
        Log($"Interval: {Settings.UpdateInterval}");

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
            catch (Exception e)
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
        List<EventLog<RewardMintedEventDTO>> RewardsEventLog = await LogReader.GetQuestRewardLogs(Account);
        List<EventLog<QuestCompletedEventDTO>> CompletedEventLog = await LogReader.GetCompletedQuestsLogs(Account);
        List<QuestReward> questRewards = new();
        foreach (BigInteger questId in CompletedEventLog.Select(el => el.Event.QuestId).Distinct())
        {
            var questReward = QuestRewards.FirstOrDefault(qr => qr.QuestId == questId);
            if (questReward is null)
            {
                questReward = new QuestReward(questId);
                List<RewardMintedEventDTO> rewards = RewardsEventLog.Where(el => el.Event.QuestId == questId).Select(el => el.Event).ToList();

                QuestCompletedEventDTO questLog = CompletedEventLog.Where(el => el.Event.QuestId == questId).Select(el => el.Event).FirstOrDefault();
                var quest = questLog.Quest;

                questReward.StartDateTime = Functions.UnixTimeStampToDateTime(Functions.BigIntToLong(quest.StartAtTime));
                questReward.CompleteDateTime = Functions.UnixTimeStampToDateTime(Functions.BigIntToLong(quest.CompleteAtTime));
                questReward.Quest = QuestContractDefinitions.GetQuestV3ContractFromType(quest.QuestInstanceId, quest.QuestType);
                questReward.Rewards = new();
                foreach (RewardMintedEventDTO reward in rewards)
                {
                    if (questReward.Heroes.Any(id => id == reward.HeroId) is false)
                    {
                        questReward.Heroes.Add(reward.HeroId);
                    }
                    DFKItem item = null;
                    try
                    {
                        item = new DFKItem(ItemContractDefinitions.GetItem(new()
                        {
                            Address = reward.Reward,
                            Chain = Account.Chain
                        }));
                    }
                    catch (Exception e)
                    {
                        Log($"Error getting item {reward.Reward} for quest {questId}.\n{e.Message}\n{e.StackTrace}");
                    }
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
        for (int i = 0; i < 10; i++)
        {
            try
            {
                var quests = (await Account.Quest.GetAccountActiveQuestsQueryAsync(Account.Account.Address)).ReturnValue1.DistinctBy(q => q.Id).ToList();
                foreach (Quest q in quests)
                {
                    q.StartDateTime = Functions.UnixTimeStampToDateTime(Functions.BigIntToLong(q.StartAtTime));
                    q.CompleteDateTime = Functions.UnixTimeStampToDateTime(Functions.BigIntToLong(q.CompleteAtTime));
                }
                return quests;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                await Task.Delay(i * 100);
            }
        }
        return new();
    }

    public int GetMinStaminaBotHero(DFKBotHero h)
    {
        if (h.Quest is null && h.SuggestedQuest is null)
        {
            return 999;
        }
        int stam = ChainQuestSettings.FirstOrDefault(qs => qs.QuestId == (h.Quest?.Id ?? h.SuggestedQuest.Id))?.MinStamina ?? Settings.MinStamina;

        return stam;
    }

    public async Task Update()
    {
        ChainQuestSettings = Settings.ChainQuestEnabled.Find(cqe => cqe.Chain.Name == Account.Chain.Name).QuestEnabled;
        await UpdateCurrentBlock();
        var quests = await GetUpdatedQuests();
        RunningQuests = new(quests);
        if (Settings.QuestHeroes is false)
        {
            Log($"QuestHeroes disabled. Not checking completed quests.");
        }
        else
        {
            Log($"{RunningQuests.Count} Active quests.");
            foreach (Quest q in quests)
            {
                if (DateTime.UtcNow > q.CompleteDateTime)
                {
                    try
                    {
                        List<BigInteger> onSaleHeroIds = new();
                        foreach (DFKBotHero h in Account.BotHeroes.Where(bh => q.Heroes.Any(qh => qh == bh.ID)))
                        {
                            h.StaminaPotionedLast = null;
                            if (h.Hero.salePrice is not null)
                            {
                                onSaleHeroIds.Add(h.ID);
                            }
                        }
                        if (onSaleHeroIds.Count > 0)
                        {
                            Log($"{(onSaleHeroIds.Count == 1 ? "Hero" : "Heroes")} #{string.Join(", #", onSaleHeroIds)} {(onSaleHeroIds.Count == 1 ? "is" : "are")} on sale, can not complete quest.");
                            continue;
                        }
                        Log($"Quest #{q.Id} {q.QuestName} with heroes {string.Join(", ", Account.BotHeroes.Where(bh => q.Heroes.Any(qh => qh == bh.ID)).Select(hero => $"{hero.Hero.id} {hero.Hero.GetRarity()} {hero.Hero.mainClass} {hero.Hero.profession}"))} is ready to complete, completing...");
                        string okMessage = await Transaction.CompleteQuest(Account, q.Heroes.First(), Settings, Account.Chain);

                        Log(okMessage);
                    }
                    catch (Exception e)
                    {
                        Log(e.Message);
                    }
                }
                await Task.Delay(1);

                if (StopBot)
                {
                    return;
                }
            }
        }

        List<Meditation> activeMeditations = (await Account.Meditation.GetActiveMeditationsQueryAsync(Account.Account.Address)).ReturnValue1;

        if (Settings.LevelUp)
        {
            List<DFKBotHero> readyToLevelHeroes = Account.BotHeroes
                .Where(h => (h.LevelingEnabled is null || h.LevelingEnabled.Value)
                && h.Hero.xp >= h.Hero.XpToLevelUp()
                && RunningQuests.Any(rq => rq.Heroes.Contains(h.ID)) is false
                && h.Hero.StaminaCurrent() < GetMinStaminaBotHero(h)
                && h.Hero.salePrice is null
                && !activeMeditations.Any(med => med.HeroId.ToString() == h.Hero.id))
                .ToList();
            Log($"{activeMeditations.Count} Active meditations.");
            foreach (Meditation meditation in activeMeditations)
            {
                if (meditation.StartBlock <= CurrentBlock - 20)
                {
                    DFKBotHero hero = Account.BotHeroes.FirstOrDefault(h => h.ID == meditation.HeroId);
                    LevelUpSetting setting = Settings.LevelUpSettings.FirstOrDefault(levelSetting => levelSetting.HeroClass == hero.Hero.mainClass.ToLower());
                    if (hero.LevelingEnabled is not null && hero.LevelingEnabled.Value is false)
                    {
                        continue;
                    }
                    Log($"Hero {hero.ID} is ready to complete meditating.\nCompleting Meditation...");
                    try
                    {
                        string okMessage = await Transaction.CompleteMeditation(Account, hero.ID, Settings, Account.Chain);
                        Log($"Hero {hero.ID} {hero.Hero.GetRarity()} {hero.Hero.mainClass} {hero.Hero.profession} Leveled up from {hero.Hero.level} to {hero.Hero.level + 1}!\nUsing these settings:\nMain(+1):{hero.LevelUpSetting.MainAttribute?.Name ?? setting.MainAttribute.Name}\nSecondary(50%+1):{hero.LevelUpSetting.SecondaryAttribute?.Name ?? setting.SecondaryAttribute.Name}\nTertiary(50%+1):{hero.LevelUpSetting.TertiaryAttribute?.Name ?? setting.TertiaryAttribute.Name}!");

                        hero.Hero.staminaFullAt = (long)Functions.UnixTime();
                        hero.Hero.level += 1;
                        if (hero.Hero.level % 2 == 0)
                        {
                            hero.Hero.stamina += 1;
                        }

                        Log(okMessage);
                    }
                    catch (Exception e)
                    {
                        Log(e.Message);
                    }

                    if (StopBot)
                    {
                        return;
                    }
                }
            }

            Log($"{readyToLevelHeroes.Count} heroes ready to level up.");
            foreach (DFKBotHero h in readyToLevelHeroes)
            {
                Log($"Hero {h.ID} is ready to level up.\nStarting Meditation...");
                LevelUpSetting setting = Settings.LevelUpSettings.FirstOrDefault(levelSetting => levelSetting.HeroClass == h.Hero.mainClass.ToLower());
                if (setting is null)
                {
                    Log($"Found no levelup settings for {h.Hero.mainClass}.");
                    continue;
                }
                try
                {
                    string okMessage = await Transaction.StartMeditation(Account, h.ID, h.LevelUpSetting.MainAttribute?.Id ?? setting.MainAttribute.Id, h.LevelUpSetting.SecondaryAttribute?.Id ?? setting.SecondaryAttribute.Id, h.LevelUpSetting.TertiaryAttribute?.Id ?? setting.TertiaryAttribute.Id, Settings, Account.Chain);
                    Log($"Hero {h.Hero.GetRarity()} {h.Hero.mainClass} {h.Hero.profession} started meditating with Stat settings: \nMain(+1):{h.LevelUpSetting.MainAttribute?.Name ?? setting.MainAttribute.Name}\nSecondary(50%+1):{h.LevelUpSetting.SecondaryAttribute?.Name ?? setting.SecondaryAttribute.Name}\nTertiary(50%+1):{h.LevelUpSetting.TertiaryAttribute?.Name ?? setting.TertiaryAttribute.Name}!");

                    Log(okMessage);
                }
                catch (Exception e)
                {
                    Log(e.Message);
                    if (e.Message.Contains("burn"))
                    {
                        break;
                    }
                }

                if (StopBot)
                {
                    return;
                }
            }
            await Task.Delay(1);
            if (StopBot)
            {
                return;
            }
        }

        if (StopBot)
        {
            return;
        }

        if (Settings.UseStaminaPotions)
        {
            Log($"Stamina Potions: {Account.StaminaPotionBalance}");

            var staminaPotionHeroes = Account.BotHeroes.Where(hero =>
                (hero.UseStaminaPotionsAmount is not null
                || hero.StaminaPotionUntilLevel is not null)
                && (hero.StaminaPotionedLast != null ? hero.StaminaPotionedLast < DateTime.UtcNow.AddMinutes(-5) : true)
                && hero.Hero.StaminaCurrent() <= hero.Hero.stamina - 20
                && (Settings.ForceStampotOnFullXP ? true : hero.Hero.xp != hero.Hero.XpToLevelUp())
                && !activeMeditations.Any(med => med.HeroId.ToString() == hero.Hero.id)).ToList();

            var StaminaPotionHeroesWaiting = Account.BotHeroes.Where(hero => hero.UseStaminaPotionsAmount is not null
                                                       || hero.StaminaPotionUntilLevel is not null).ToList();

            string staminaPotionAddress = ItemContractDefinitions.InventoryItems.First(item => item.Name == "Stamina Potion").Addresses.First(a => a.Chain.Name == Account.Chain.Name).Address;
            Log($"Heroes ready to be stamina potioned: {string.Join(", ", staminaPotionHeroes.Select(h => h.ID))}");
            if (Account.StaminaPotionBalance == 0 && staminaPotionHeroes.Count() > 0)
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
                    else if (h.StaminaPotionUntilLevel is not null
                        && h.Hero.level < h.StaminaPotionUntilLevel
                        && Account.StaminaPotionBalance > 0
                        && h.Hero.StaminaCurrent() <= h.Hero.stamina - 20)
                    {
                        if (h.Hero.level < h.StaminaPotionUntilLevel.Value)
                        {
                            Log($"Hero #{h.ID} is level {h.Hero.level} XP: {h.Hero.xp}/{h.Hero.XpToLevelUp()}, need to reach level {h.StaminaPotionUntilLevel} to stop using stamina potions. Hero is at {h.Hero.StaminaCurrent()} stamina, using Potion...");
                            try
                            {
                                string okMessage = await Transaction.UseComsumableItem(Account, h.ID, staminaPotionAddress, Settings, Account.Chain);
                                Account.StaminaPotionBalance--;
                                h.StaminaPotionedLast = DateTime.UtcNow;
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
                    else if (h.UseStaminaPotionsAmount is not null
                        && h.UseStaminaPotionsAmount <= 0)
                    {
                        Log($"Hero #{h.ID} has no stamina potions left.");
                        h.UseStaminaPotionsAmount = null;
                        continue;
                    }
                    else if (h.UseStaminaPotionsAmount is not null
                        && h.UseStaminaPotionsAmount > 0
                        && Account.StaminaPotionBalance > 0
                        && h.Hero.StaminaCurrent() <= h.Hero.stamina - 20)
                    {
                        if (h.UseStaminaPotionsAmount.Value > 0)
                        {
                            Log($"Hero #{h.ID} {h.Hero.GetRarity()} {h.Hero.mainClass} {h.Hero.profession} is level {h.Hero.level} XP: {h.Hero.xp}/{h.Hero.XpToLevelUp()} has {h.UseStaminaPotionsAmount} stamina potions left to use. Hero is at {h.Hero.StaminaCurrent()} stamina, using Potion...");
                            try
                            {
                                string okMessage = await Transaction.UseComsumableItem(Account, h.ID, staminaPotionAddress, Settings, Account.Chain);
                                h.UseStaminaPotionsAmount--;
                                var heroSettings = Bots.Settings.HeroQuestSettings.FirstOrDefault(hqs => hqs.HeroId == h.ID.ToString());
                                heroSettings.UseStaminaPotionsAmount = h.UseStaminaPotionsAmount;
                                Account.StaminaPotionBalance -= 1;
                                h.StaminaPotionedLast = DateTime.UtcNow;
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

                    if (StopBot)
                    {
                        return;
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
                    && h.Hero.StaminaCurrent(240) < GetMinStaminaBotHero(h)
                    && !activeMeditations.Any(med => med.HeroId.ToString() == h.Hero.id))
                .ToList();
            Log($"There are {heroesToSell.Count} heroes to sell.");
            foreach (DFKBotHero h in heroesToSell)
            {
                Log($"Selling hero #{h.ID} for {h.BotSalePrice} {(Account.Chain.Name == "DFK" ? "Crystal" : "Jade")}...");
                try
                {
                    string okMessage = await Transaction.StartAuction(Account, h.ID, h.BotSalePrice.Value, Settings, Account.Chain);
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
            foreach (DFKBotHero h in heroesToCancelAuction)
            {
                Log($"Cancelling auction for hero #{h.ID}...");
                try
                {
                    string okMessage = await Transaction.CancelAuction(Account, h.ID, Settings, Account.Chain);
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
            var enabledQuests = Settings.ChainQuestEnabled.Find(cqe => cqe.Chain.Name == Account.Chain.Name).QuestEnabled.Where(q => q.Enabled).ToList();

            List<DFKBotHero> staminaPotHeroes = Account.BotHeroes
                .Where(h => h.Hero.StaminaCurrent(240) >= 5
                && h.Hero.StaminaCurrent(240) <= GetMinStaminaBotHero(h)
                && RunningQuests.SelectMany(rq => rq.Heroes).Contains(h.ID) is false
                && h.Hero.salePrice is null
                && !activeMeditations.Any(med => med.HeroId.ToString() == h.Hero.id)
                && (h.StaminaPotionUntilLevel is not null
                || h.UseStaminaPotionsAmount is not null))
                .ToList();

            if (Settings.UseStaminaPotions)
            {

                Log($"{staminaPotHeroes.Count} are in a stamina pot loop. Questing them fast to use stamina.");
                foreach (QuestContract quest in staminaPotHeroes
                    .Select(r => r.GetActiveQuest())
                    .DistinctBy(q => q.Id)
                    .Where(q => enabledQuests.Select(qe => qe.QuestId).Contains(q.Id)))
                {
                    var questsOfType = RunningQuests.Where(rq => quest.QuestInstanceId == rq.QuestInstanceId && quest.QuestType == rq.QuestType);

                    if (questsOfType.Any(rq => rq.CompleteDateTime >= DateTime.UtcNow.AddHours(4)) || questsOfType.Count() >= 5)
                    {
                        Log("There are too many quests of this kind running to start more.");
                        continue;
                    }

                    List<DFKBotHero> readyStaminaQuestHeroes = staminaPotHeroes.Where(h =>
                        h.GetActiveQuestEquals(quest.Id)
                        && (quest.Category.ToLower() == "training"
                        || h.Hero.profession == quest.Category.ToLower()))
                            .ToList();

                    Log($"Found {readyStaminaQuestHeroes.Count} profession stamina pot loop heroes ready to start {quest.Name}.");
                    for (int i = 0; i <= readyStaminaQuestHeroes.Count; i += quest.MaxHeroesPerQuest(Account))
                    {
                        List<DFKBotHero> heroBatch = readyStaminaQuestHeroes.Skip(i).Take(quest.MaxHeroesPerQuest(Account)).ToList();

                        int heroesSetForQuest = Account.BotHeroes.Where(h =>
                            h.GetActiveQuestEquals(quest.Id)
                            && h.Hero.profession == quest.Category.ToLower()
                            && (h.Hero.salePrice == null || Settings.CancelUnpricedHeroSales))
                            .Count();

                        if (heroBatch is null || heroBatch.Count == 0)
                        {
                            continue;
                        }
                        Log("Stamina pot heroes in loop always quest instantly.");
                        //Order for optimal mining if mining
                        if (quest.Category == "Mining")
                        {
                            heroBatch = heroBatch.OrderByDescending(h => h.Hero.mining + h.Hero.strength + h.Hero.endurance).ToList();
                        }

                        int maxAttempts = heroBatch.Select(h => quest.AvailableAttempts(h.Hero, null)).Min();

                        try
                        {
                            Log($"Starting {quest.Name} for {string.Join(", ", heroBatch.Select(h => $"{h.Hero.id}: {h.Hero.GetRarity()} {h.Hero.mainClass} {h.Hero.profession} {h.Hero.StaminaCurrent(240)}/{h.Hero.stamina}"))} with {maxAttempts} attempts.");
                            string okMessage = await Transaction.StartQuest(Account,
                                heroBatch.Select(h => h.ID).ToList(),
                                quest, maxAttempts, Settings, Account.Chain);
                            Log(okMessage);
                        }
                        catch (Exception e)
                        {
                            Log(e.Message);
                        }

                        if (StopBot)
                        {
                            return;
                        }

                    }
                }
            }

            List<DFKBotHero> readyHeroes = Account.BotHeroes
                .Where(h => h.Hero.StaminaCurrent(240) >= GetMinStaminaBotHero(h)
                && RunningQuests.SelectMany(rq => rq.Heroes).Contains(h.ID) is false
                && h.Hero.salePrice is null
                && !staminaPotHeroes.Select(stp => stp.ID).Contains(h.ID)
                && !activeMeditations.Any(med => med.HeroId.ToString() == h.Hero.id))
                .ToList();
            Log($"{readyHeroes.Count} heroes ready to quest");
            foreach (QuestContract quest in readyHeroes
                .Select(r => r.GetActiveQuest())
                .DistinctBy(q => q.Id)
                .Where(q => enabledQuests.Select(qe => qe.QuestId).Contains(q.Id)))
            {
                var questsOfType = RunningQuests.Where(rq => quest.QuestInstanceId == rq.QuestInstanceId && quest.QuestType == rq.QuestType);
                QuestEnabled questSettings = enabledQuests.FirstOrDefault(qe => qe.QuestId == quest.Id);
                if (questsOfType.Any(rq => rq.CompleteDateTime >= DateTime.UtcNow.AddHours(4)) || questsOfType.Count() >= 5)
                {
                    Log("There are too many quests of this kind running to start more.");
                    continue;
                }
                List<DFKBotHero> readyQuestHeroes = readyHeroes.Where(h =>
                    h.GetActiveQuestEquals(quest.Id)
                    && (quest.Category.ToLower() == "training"
                    || h.Hero.profession == quest.Category.ToLower()))
                        .ToList();

                List<DFKBotHero> readyQuestUnalignedHeroes = readyHeroes.Where(h =>
                    h.GetActiveQuestEquals(quest.Id)
                    && h.Hero.profession != quest.Category.ToLower()
                    && quest.Category.ToLower() != "training")
                        .ToList();

                Log($"Found {readyQuestHeroes.Count} profession aligned heroes ready to start {quest.Name}.");
                for (int i = 0; i <= readyQuestHeroes.Count; i += quest.MaxHeroesPerQuest(Account))
                {
                    List<DFKBotHero> heroBatch = readyQuestHeroes.Skip(i).Take(quest.MaxHeroesPerQuest(Account)).ToList();

                    int heroesSetForQuest = Account.BotHeroes.Where(h =>
                        h.GetActiveQuestEquals(quest.Id)
                        && h.Hero.profession == quest.Category.ToLower()
                        && (h.Hero.salePrice == null || Settings.CancelUnpricedHeroSales))
                        .Count();

                    if (heroBatch.Count < quest.MaxHeroesPerQuest(Account) && heroBatch.Count < heroesSetForQuest)
                    {
                        if (questSettings.QuestEagerly is false && questSettings.QuestInstantly is false)
                        {
                            Log($"Not enough heroes to start {quest.Name}. {heroBatch.Count} heroes {(heroBatch.Count == 1 ? "is" : "are")} ready, {quest.MaxHeroesPerQuest(Account)} are needed.");
                            continue;
                        }
                        else if (questSettings.QuestInstantly is false)
                        {
                            List<Hero> heroesCatchingUp = Account.BotHeroes.Where(h =>
                                h.GetActiveQuestEquals(quest.Id)
                                && h.Hero.StaminaCurrent(240) > GetMinStaminaBotHero(h) - 5
                                && h.Hero.StaminaCurrent(240) < GetMinStaminaBotHero(h)
                                && RunningQuests.SelectMany(rq => rq.Heroes).Contains(h.ID) is false
                                && h.Hero.profession == quest.Category.ToLower())
                                .Select(h => h.Hero)
                                .ToList();
                            if (heroesCatchingUp.Count > 0)
                            {
                                Log($"Heroes are catching up to {string.Join(", ", heroBatch.Select(h => h.ID))} to make a full squad for {quest.Name}.");
                                var checkBatch = heroBatch.Where(h => h.Hero.StaminaCurrent(240) == h.Hero.stamina || CheckStaminaPotioned(h)).ToList();
                                if (checkBatch.Count > 0)
                                {
                                    if (checkBatch.Any(CheckStaminaPotioned))
                                    {
                                        var staminaPotionedHeroes = checkBatch.Where(CheckStaminaPotioned).ToList();
                                        Log($"{string.Join(", ", staminaPotionedHeroes.Select(h => h.ID))} are stamina potioned recently, questing immediately. Questing batch of heroes {string.Join(", ", checkBatch.Select(h => h.ID))}");
                                    }
                                    else
                                    {
                                        Log($"{string.Join(", ", heroBatch.Select(h => h.ID))} are so energetic they don't care.");
                                    }
                                }
                                else
                                {
                                    continue;
                                }
                            }
                        }
                        if (questSettings.QuestInstantly)
                        {
                            Log($"Quest Instantly is enabled. Questing heroes");
                        }
                    }
                    if (heroBatch is null || heroBatch.Count == 0)
                    {
                        continue;
                    }

                    //Order for optimal mining if mining
                    if (quest.Category == "Mining")
                    {
                        heroBatch = heroBatch.OrderByDescending(h => h.Hero.mining + h.Hero.strength + h.Hero.endurance).ToList();
                    }

                    int maxAttempts = heroBatch.Select(h => quest.AvailableAttempts(h.Hero, questSettings.CapAttempts ? GetMinStaminaBotHero(h) : null)).Min();

                    if (questSettings.CapAttempts)
                    {
                        Log($"Cap attempts is enabled. Using {maxAttempts} for {quest.Name} from minimum stamina {GetMinStaminaBotHero(heroBatch.First())}");
                    }
                    if (maxAttempts == 0)
                    {
                        Log("Available attempts too low.");
                        continue;
                    }
                    try
                    {
                        Log($"Starting {quest.Name} for {string.Join(", ", heroBatch.Select(h => $"{h.Hero.id}: {h.Hero.GetRarity()} {h.Hero.mainClass} {h.Hero.profession} {h.Hero.StaminaCurrent(240)}/{h.Hero.stamina}"))} with {maxAttempts} attempts.");
                        string okMessage = await Transaction.StartQuest(Account,
                            heroBatch.Select(h => h.ID).ToList(),
                            quest, maxAttempts, Settings, Account.Chain);
                        Log(okMessage);
                    }
                    catch (Exception e)
                    {
                        Log(e.Message);
                    }

                    if (StopBot)
                    {
                        return;
                    }
                }

                Log($"Found {readyQuestUnalignedHeroes.Count} profession unaligned heroes ready to start {quest.Name}.");
                for (int i = 0; i <= readyQuestUnalignedHeroes.Count; i += quest.MaxHeroesPerQuest(Account))
                {
                    List<DFKBotHero> heroBatch = readyQuestUnalignedHeroes.Skip(i).Take(quest.MaxHeroesPerQuest(Account)).ToList();

                    int heroesSetForQuest = Account.BotHeroes.Where(h =>
                        h.GetActiveQuestEquals(quest.Id)
                        && h.Hero.profession != quest.Category.ToLower()
                        && (h.Hero.salePrice == null || Settings.CancelUnpricedHeroSales))
                        .Count();

                    if (heroBatch.Count < quest.MaxHeroesPerQuest(Account) && heroBatch.Count < heroesSetForQuest)
                    {
                        if (questSettings.QuestEagerly is false && questSettings.QuestInstantly is false)
                        {
                            continue;
                        }
                        else if (questSettings.QuestInstantly is false)
                        {
                            List<Hero> heroesCatchingUp = Account.BotHeroes.Where(h =>
                                h.GetActiveQuestEquals(quest.Id)
                                && h.Hero.profession != quest.Category.ToLower()
                                && h.Hero.StaminaCurrent(240) > GetMinStaminaBotHero(h) - 5
                                && h.Hero.StaminaCurrent(240) < GetMinStaminaBotHero(h)
                                && RunningQuests.SelectMany(rq => rq.Heroes).Contains(h.ID) is false)
                                .Select(h => h.Hero)
                                .ToList();
                            if (heroesCatchingUp.Count > 0)
                            {
                                Log($"Heroes are catching up to {string.Join(", ", heroBatch.Select(h => h.ID))} to make a full squad for {quest.Name}.");
                                var checkBatch = heroBatch.Where(h => h.Hero.StaminaCurrent(240) == h.Hero.stamina || CheckStaminaPotioned(h)).ToList();
                                if (checkBatch.Count > 0)
                                {
                                    if (checkBatch.Any(CheckStaminaPotioned))
                                    {
                                        var staminaPotionedHeroes = checkBatch.Where(CheckStaminaPotioned).ToList();
                                        Log($"{string.Join(", ", staminaPotionedHeroes.Select(h => h.ID))} are stamina potioned recently, questing immediately. Questing batch of heroes {string.Join(", ", checkBatch.Select(h => h.ID))}");
                                    }
                                    else
                                    {
                                        Log($"{string.Join(", ", heroBatch.Select(h => h.ID))} are so energetic they don't care.");
                                    }
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
                        heroBatch = heroBatch.OrderByDescending(h => h.Hero.mining + h.Hero.strength + h.Hero.endurance).ToList();
                    }

                    int maxAttempts = heroBatch.Select(h => quest.AvailableAttempts(h.Hero,
                        questSettings.CapAttempts ? GetMinStaminaBotHero(h) : null)).Min();

                    if (maxAttempts == 0)
                    {
                        Log("Available attempts too low.");
                        continue;
                    }
                    try
                    {
                        Log($"Starting {quest.Name} for {string.Join(", ", heroBatch.Select(h => $"{h.ID}: {h.Hero.GetRarity()} {h.Hero.mainClass} {h.Hero.profession} {h.Hero.StaminaCurrent(240)}/{h.Hero.stamina}"))} with {maxAttempts} attempts.");
                        string okMessage = await Transaction.StartQuest(Account,
                            heroBatch.Select(h => h.ID).ToList(),
                            quest, maxAttempts, Settings, Account.Chain);
                        Log(okMessage);
                    }
                    catch (Exception e)
                    {
                        Log(e.Message);
                    }

                    if (StopBot)
                    {
                        return;
                    }
                }
            }

            await Task.Delay(1);
        }
        Log($"Iteration complete");
    }

    private bool CheckStaminaPotioned(DFKBotHero h)
    {
        return (h.StaminaPotionUntilLevel.HasValue || h.UseStaminaPotionsAmount.HasValue) && h.Hero.StaminaCurrent() > h.Hero.stamina - 20;
    }
}
