using DFKContracts.QuestCore.ContractDefinition;
using PirateQuester.DFK.Contracts;
using PirateQuester.Utils;
using BigInteger = System.Numerics.BigInteger;
using Nethereum.Web3;
using DFKContracts.MeditationCircle.ContractDefinition;
using DFKContracts.ERC20.ContractDefinition;
using DFKContracts.ERC20;
using static PirateQuester.DFK.Contracts.QuestContractDefinitions;
using Nethereum.Contracts;
using PirateQuester.DFK.Items;

namespace Utils;

public static class Transaction
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

	public static CancellationTokenSource StopAfterDelay(int cancelDelay)
	{
		CancellationTokenSource cancelToken = new();
		cancelToken.CancelAfter(cancelDelay);
		return cancelToken;
	}

	public static async Task<string> CompleteMeditation(DFKAccount account, BigInteger heroId, int maxGasFeeGwei = 200, int cancelDelay = 60000)
	{
		PendingTransaction pendingTransaction = new()
		{
			Name = $"Starting meditation For: {heroId}",
			TimeStamp = DateTime.UtcNow,
		};
		PendingTransactions.Add(pendingTransaction);
		TransactionAdded?.Invoke();
		try
		{
			var handler = account.Signer.Eth.GetContractTransactionHandler<CompleteMeditationFunction>();
			var completeMeditationFunc = new CompleteMeditationFunction()
			{
				HeroId = heroId,
				MaxFeePerGas = Web3.Convert.ToWei(maxGasFeeGwei, Nethereum.Util.UnitConversion.EthUnit.Gwei),
				MaxPriorityFeePerGas = 0
			};

			var receipt = await account.Meditation.CompleteMeditationRequestAndWaitForReceiptAsync(completeMeditationFunc, StopAfterDelay(cancelDelay));
			Console.WriteLine($"Complete Meditation Txn: Gas: {receipt.GasUsed.Value}");
			PendingTransactions.Remove(pendingTransaction);
			FinishedTransactions.Add(new()
			{
				Name = $"Completed meditation For Hero: {heroId}",
				TimeStamp = DateTime.UtcNow,
				TransactionHash = receipt.TransactionHash,
			});
			TransactionAdded?.Invoke();
			if (receipt.Status == new BigInteger(1))
			{
				return $"Completed Meditating.\nTxn hash: {receipt.TransactionHash}";
			}
			else
			{
				return $"Completed Meditating.\nTxn hash: {receipt.TransactionHash}";
			}
		}
		catch (Exception e)
		{
			FinishedTransactions.Add(new()
			{
				Success = false,
				Name = $"Failed to completed meditating: {e.Message}",
				TimeStamp = DateTime.UtcNow,
				TransactionHash = null
			});
			Console.WriteLine($"{e.Message}");
			PendingTransactions.Remove(pendingTransaction);
			TransactionAdded?.Invoke();
			throw;
		}
	}

	public static async Task<string> StartMeditation(DFKAccount account, BigInteger heroId, byte stat1, byte stat2, byte stat3, int maxGasFeeGwei = 200, int cancelDelay = 60000)
	{
		PendingTransaction pendingTransaction = new()
		{
			Name = $"Starting meditation For: {heroId}",
			TimeStamp = DateTime.UtcNow,
		};
		PendingTransactions.Add(pendingTransaction);
		TransactionAdded?.Invoke();
		try
		{
			var DFKSHvas = new Erc20Service(account.Signer, ItemContractDefinitions.InventoryItems[(int)DFKItemEnum.DFKSHVAS].Address);
			if (await DFKSHvas.AllowanceQueryAsync(account.Account.Address, DFKSHvas.ContractHandler.ContractAddress) < 1)
			{
				Console.WriteLine($"DFKSHvas not allowed. Setting allowance for meditation circle to use..");
				var approveERC20Function = new ApproveFunction()
				{
					Amount = new BigInteger(100000),
					Spender = ItemContractDefinitions.InventoryItems[(int)DFKItemEnum.DFKSHVAS].Address,
					MaxFeePerGas = Web3.Convert.ToWei(maxGasFeeGwei, Nethereum.Util.UnitConversion.EthUnit.Gwei),
					MaxPriorityFeePerGas = 0
				};
				var allowDFKShvasReceipt = await DFKSHvas.ApproveRequestAndWaitForReceiptAsync(approveERC20Function, StopAfterDelay(cancelDelay));
				Console.WriteLine($"DFKSHvas was approved to be used in the meditation circle\n{allowDFKShvasReceipt.GasUsed} gas was used.");
			}
			var DFKMoksha = new Erc20Service(account.Signer, ItemContractDefinitions.InventoryItems[(int)DFKItemEnum.DFKMOKSHA].Address);
			if (await DFKMoksha.AllowanceQueryAsync(account.Account.Address, DFKMoksha.ContractHandler.ContractAddress) < 1)
			{
				Console.WriteLine($"DFKMoksha not allowed. Setting allowance for meditation circle to use..");
				var approveERC20Function = new ApproveFunction()
				{
					Amount = new BigInteger(100000),
					Spender = ItemContractDefinitions.InventoryItems[(int)DFKItemEnum.DFKMOKSHA].Address,
					MaxFeePerGas = Web3.Convert.ToWei(maxGasFeeGwei, Nethereum.Util.UnitConversion.EthUnit.Gwei),
					MaxPriorityFeePerGas = 0
				};
				var allowDFKMokshaReceipt = await DFKMoksha.ApproveRequestAndWaitForReceiptAsync(approveERC20Function, StopAfterDelay(cancelDelay));
				Console.WriteLine($"DFKMoksha was approved to be used in the meditation circle.\n{allowDFKMokshaReceipt.GasUsed} gas was used.");
			}
			if (!await account.Hero.IsApprovedForAllQueryAsync(account.Account.Address, "0xD507b6b299d9FC835a0Df92f718920D13fA49B47"))
			{
				Console.WriteLine($"Heroes not approved for meditation (Level up).\nTrying to approve Meditation of heroes.");
				var approvalForAllFunc = new DFKContracts.HeroCore.ContractDefinition.SetApprovalForAllFunction()
				{
					Operator = "0xD507b6b299d9FC835a0Df92f718920D13fA49B47",
					Approved = true,
					MaxFeePerGas = Web3.Convert.ToWei(maxGasFeeGwei, Nethereum.Util.UnitConversion.EthUnit.Gwei),
					MaxPriorityFeePerGas = 0
				};

				var approvalReceipt = await account.Hero.SetApprovalForAllRequestAndWaitForReceiptAsync(approvalForAllFunc, StopAfterDelay(cancelDelay));
				Console.WriteLine($"Heroes approved for meditation. {approvalReceipt.TransactionHash}");
			}
			var handler = account.Signer.Eth.GetContractTransactionHandler<StartMeditationFunction>();
			var startMeditationFunc = new StartMeditationFunction()
			{
				AttunementCrystal = NULL_ADDRESS,
				HeroId = heroId,
				PrimaryStat = stat1,
				SecondaryStat = stat2,
				TertiaryStat = stat3,
				MaxFeePerGas = Web3.Convert.ToWei(maxGasFeeGwei, Nethereum.Util.UnitConversion.EthUnit.Gwei),
				MaxPriorityFeePerGas = 0
			};

			var receipt = await account.Meditation.StartMeditationRequestAndWaitForReceiptAsync(startMeditationFunc, StopAfterDelay(cancelDelay));
			Console.WriteLine($"Started Meditation Txn: Gas: {receipt.GasUsed.Value}");
			PendingTransactions.Remove(pendingTransaction);
			FinishedTransactions.Add(new()
			{
				Name = $"Started meditation For Hero: {heroId}",
				TimeStamp = DateTime.UtcNow,
				TransactionHash = receipt.TransactionHash,
			});
			TransactionAdded?.Invoke();
			if (receipt.Status == new BigInteger(1))
			{
				return $"Started Meditating.\nTxn hash: {receipt.TransactionHash}";
			}
			else
			{
				return $"Started Meditating.\nTxn hash: {receipt.TransactionHash}";
			}
		}
		catch (Exception e)
		{
			FinishedTransactions.Add(new()
			{
				Success = false,
				Name = $"Failed to start meditating: {e.Message}",
				TimeStamp = DateTime.UtcNow,
				TransactionHash = null
			});
			Console.WriteLine($"{e.Message}");
			PendingTransactions.Remove(pendingTransaction);
			TransactionAdded?.Invoke();
			throw;
		}
	}

	public static async Task<string> CompleteQuest(DFKAccount account, BigInteger heroId, int maxGasFeeGwei = 200, int cancelDelay = 60000)
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
				HeroId = heroId,
				MaxFeePerGas = Web3.Convert.ToWei(maxGasFeeGwei, Nethereum.Util.UnitConversion.EthUnit.Gwei),
				MaxPriorityFeePerGas = 0
			};

			var receipt = await account.Quest.CompleteQuestRequestAndWaitForReceiptAsync(questCompleteFunc, StopAfterDelay(cancelDelay));
			Console.WriteLine($"Completed Quest Txn: Gas: {receipt.GasUsed.Value}");
			//var completeQuestEvent = receipt.DecodeAllEvents<QuestCompletedEventDTO>();
			PendingTransactions.Remove(pendingTransaction);
			FinishedTransactions.Add(new()
			{
				Name = $"Complete Quest For Hero: {heroId}",
				TimeStamp = DateTime.UtcNow,
				TransactionHash = receipt.TransactionHash,
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
			throw;
		}
	}

	public static async Task<string> StartQuest(DFKAccount account, List<BigInteger> selectedHeroes, QuestContract quest, int attempts, int maxGasFeeGwei = 200, int cancelDelay = 60000)
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
				HeroIds = selectedHeroes,
                QuestAddress = quest.Address,
                Attempts = (byte)attempts,
                Level = (byte)quest.Level,
				MaxFeePerGas = Web3.Convert.ToWei(maxGasFeeGwei, Nethereum.Util.UnitConversion.EthUnit.Gwei),
				MaxPriorityFeePerGas = 0
			};
			var questStartResponse = await account.Quest.StartQuestRequestAndWaitForReceiptAsync(questStartFunc, StopAfterDelay(cancelDelay));
			Console.WriteLine($"Started Quest Txn: Gas: {questStartResponse.GasUsed.Value}");
			//var startQuestEvent = questStartResponse.DecodeAllEvents<QuestStartedEventDTO>();
			PendingTransactions.Remove(pendingTxn);
			FinishedTransactions.Add(new()
            {
                Success = true,
                Name = $"Started Quest {quest.Name}",
				TimeStamp = DateTime.UtcNow,
				TransactionHash = questStartResponse.TransactionHash
			});
            TransactionAdded?.Invoke();
			if (questStartResponse.Status == new BigInteger(1))
			{
				return $"Started Quest: {quest.Name}\nTransaction: {questStartResponse.TransactionHash}\nhttps://avascan.info/blockchain/dfk/tx/{questStartResponse.TransactionHash}\nGas Paid: {questStartResponse.GasUsed}";
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
			throw;
		}
	}
}