using DFKContracts.QuestCore.ContractDefinition;
using PirateQuester.DFK.Contracts;
using DFK;
using PirateQuester.Utils;
using BigInteger = System.Numerics.BigInteger;
using Nethereum.Web3;

namespace Utils;

public class Transaction
{
	private static List<PendingTransaction> pendingTransactions = new List<PendingTransaction>();
	public static List<PendingTransaction> PendingTransactions
	{
		get
		{
			return pendingTransactions;
		}
		set
		{
			TransactionAdded?.Invoke();
			pendingTransactions = value;
		}
	}
	private static List<FinishedTransaction> finishedTransactions = new List<FinishedTransaction>();
	public static List<FinishedTransaction> FinishedTransactions
	{
		get
		{
			return finishedTransactions;
		}
		set
		{
			TransactionAdded?.Invoke();
			finishedTransactions = value;
		}
	}
	public delegate void AddTransaction();

	public static event AddTransaction TransactionAdded;

    public async Task<string> CompleteQuest(DFKAccount account, BigInteger heroId)
	{
		PendingTransaction pendingTransaction = new()
		{
			Name = "Complete Quest",
			TimeStamp = DateTime.UtcNow,
		};
        PendingTransactions.Add(pendingTransaction);
        TransactionAdded?.Invoke();
		try
        {

			var handler = account.Signer.Eth.GetContractTransactionHandler<CompleteQuestFunction>();
			var questCompleteFunc = new CompleteQuestFunction()
			{
				GasPrice = Web3.Convert.ToWei(7, Nethereum.Util.UnitConversion.EthUnit.Gwei),
				HeroId = heroId
			};
			questCompleteFunc.Gas = await handler.EstimateGasAsync(account.Quest.ContractHandler.ContractAddress, questCompleteFunc);
			var receipt = await account.Quest.CompleteQuestRequestAndWaitForReceiptAsync(questCompleteFunc);
            PendingTransactions.Remove(pendingTransaction);
			FinishedTransactions.Add(new()
			{
				Name = $"Complete Quest For Hero: {heroId}",
				TimeStamp = DateTime.UtcNow,
				TransactionHash = receipt.TransactionHash
			});
			TransactionAdded?.Invoke();
			if(receipt.Status == new BigInteger(1))
			{
				return $"Completed Quests.\nTxn hash: {receipt.TransactionHash}";
			}
			else
			{
				return $"Transaction Failed.\nTxn hash: {receipt.TransactionHash}";
			}
        }
		catch(Exception e)
        {
            FinishedTransactions.Add(new()
            {
                Success = false,
                Name = $"Failed Complete Quest: {e.Message}",
                TimeStamp = DateTime.UtcNow,
                TransactionHash = null
            });
            Console.WriteLine($"{e.Message}");
            PendingTransactions.Remove(pendingTransaction);
            TransactionAdded?.Invoke();
            return $"{e.Message}";
        }
	}

	public async Task<string> StartQuest(DFKAccount account, List<BigInteger> selectedHeroes, QuestContract quest, int attempts)
	{
        var pendingTxn = new PendingTransaction()
        {
            Name = $"Start Quest: {quest.Name}",
            TimeStamp = DateTime.UtcNow,
        };
        PendingTransactions.Add(pendingTxn);
        TransactionAdded?.Invoke();
        bool isApproved = await account.Hero.IsApprovedForAllQueryAsync(account.Account.Address, quest.Address);
		Console.WriteLine($"Is approved: {isApproved}");
		
		if (isApproved is false)
		{
			string approveAllResponse = await account.Hero.SetApprovalForAllRequestAsync(quest.Address, true);
			Console.WriteLine($"Set Approved for {quest.Name}: {approveAllResponse}");
		}
		Console.WriteLine($"Starting quest {quest.Name} with {attempts} attempts.");
		try
        {
			var handler = account.Signer.Eth.GetContractTransactionHandler<StartQuestFunction>();
            var questStartFunc = new StartQuestFunction()
            {
				GasPrice = Web3.Convert.ToWei(7, Nethereum.Util.UnitConversion.EthUnit.Gwei),
				HeroIds = selectedHeroes,
                QuestAddress = quest.Address,
                Attempts = (byte)attempts,
                Level = (byte)quest.Level
            };
			questStartFunc.Gas = await handler.EstimateGasAsync(account.Quest.ContractHandler.ContractAddress, questStartFunc);
			var questStartResponse = await account.Quest.StartQuestRequestAndWaitForReceiptAsync(questStartFunc);
			PendingTransactions.Remove(pendingTxn);
			FinishedTransactions.Add(new()
            {
                Success = true,
                Name = $"Start Quest {quest.Name}",
				TimeStamp = DateTime.UtcNow,
				TransactionHash = questStartResponse.TransactionHash
			});
            TransactionAdded?.Invoke();
			if (questStartResponse.Status == new BigInteger(1))
			{
				return $"Started Quest: {quest.Name}\nTransaction: {questStartResponse.TransactionHash}\nhttps://avascan.info/blockchain/dfk/tx/{questStartResponse.TransactionHash}";
			}
			else
			{
				return $"Failed to start Quest: {quest.Name}\nTransaction: {questStartResponse.TransactionHash}\nhttps://avascan.info/blockchain/dfk/tx/{questStartResponse.TransactionHash}";
			}
        }
		catch(Exception e)
        {
            PendingTransactions.Remove(pendingTxn);
			FinishedTransactions.Add(new()
			{
				Success = false,
				Name = $"Failed Start Quest {quest.Name}: {e.Message}",
				TimeStamp = DateTime.UtcNow,
				TransactionHash = null
			});
            TransactionAdded?.Invoke();
            return $"{e.Message}";
        }
	}
}