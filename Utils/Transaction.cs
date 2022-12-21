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
using PirateQuester.PirateQuesterToken.ContractDefinition;

namespace Utils;

public static class Transaction
{
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
			FinishedTransactions.Add(new()
			{
				Name = $"Completed meditation For Hero: {heroId}",
				TimeStamp = DateTime.Now,
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
				TimeStamp = DateTime.Now,
				TransactionHash = null
			});
			Console.WriteLine($"{e.Message}");
			TransactionAdded?.Invoke();
			throw;
		}
	}

	public static async Task<string> StartMeditation(DFKAccount account, BigInteger heroId, byte stat1, byte stat2, byte stat3, int maxGasFeeGwei = 200, int cancelDelay = 60000)
	{
		TransactionAdded?.Invoke();
		try
		{
			var DFKSHvas = new Erc20Service(account.Signer, ItemContractDefinitions.InventoryItems.First(item => item.ItemEnum == DFKItemEnum.DFKSHVAS).Addresses.First(a => a.Chain.Id == account.Chain.Id).Address);
			var shvasAllowance = await DFKSHvas.AllowanceQueryAsync(account.Account.Address, account.Meditation.ContractHandler.ContractAddress);
            if (shvasAllowance < new BigInteger(100))
			{
				Console.WriteLine($"DFKSHvas not allowed. Setting allowance for meditation circle to use.");
				var approveERC20Function = new DFKContracts.ERC20.ContractDefinition.ApproveFunction()
				{
					Amount = new BigInteger(1000),
					Spender = account.Meditation.ContractHandler.ContractAddress,
					MaxFeePerGas = Web3.Convert.ToWei(maxGasFeeGwei, Nethereum.Util.UnitConversion.EthUnit.Gwei),
					MaxPriorityFeePerGas = 0
				};
				var allowDFKShvasReceipt = await DFKSHvas.ApproveRequestAndWaitForReceiptAsync(approveERC20Function, StopAfterDelay(cancelDelay));
				Console.WriteLine($"DFKSHvas was approved to be used in the meditation circle\n{allowDFKShvasReceipt.GasUsed} gas was used.");
            }
            var DFKMoksha = new Erc20Service(account.Signer, ItemContractDefinitions.InventoryItems.First(item => item.ItemEnum == DFKItemEnum.DFKMOKSHA).Addresses.First(a => a.Chain.Id == account.Chain.Id).Address);
            var mokshaAllowance = await DFKMoksha.AllowanceQueryAsync(account.Account.Address, account.Meditation.ContractHandler.ContractAddress);
            if (mokshaAllowance < new BigInteger(100))
            {
                Console.WriteLine($"DFKMoksha not allowed. Setting allowance for meditation circle to use.");
                var approveERC20Function = new DFKContracts.ERC20.ContractDefinition.ApproveFunction()
                {
                    Amount = new BigInteger(1000),
                    Spender = account.Meditation.ContractHandler.ContractAddress,
                    MaxFeePerGas = Web3.Convert.ToWei(maxGasFeeGwei, Nethereum.Util.UnitConversion.EthUnit.Gwei),
                    MaxPriorityFeePerGas = 0
                };
                var allowDFKMokshaReceipt = await DFKMoksha.ApproveRequestAndWaitForReceiptAsync(approveERC20Function, StopAfterDelay(cancelDelay));
                Console.WriteLine($"DFKMoksha was approved to be used in the meditation circle.\n{allowDFKMokshaReceipt.GasUsed} gas was used.");
            }
            var nativeToken = new Erc20Service(account.Signer, account.Chain.NativeToken);
            var nativeTokenAllowance = await nativeToken.AllowanceQueryAsync(account.Account.Address, account.Meditation.ContractHandler.ContractAddress);
            if (nativeTokenAllowance < Web3.Convert.ToWei(999))
            {
                Console.WriteLine($"Native Token not allowed. Setting allowance for meditation circle to use.");
                var approveERC20Function = new DFKContracts.ERC20.ContractDefinition.ApproveFunction()
                {
                    Amount = Web3.Convert.ToWei(9999),
                    Spender = account.Meditation.ContractHandler.ContractAddress,
                    MaxFeePerGas = Web3.Convert.ToWei(maxGasFeeGwei, Nethereum.Util.UnitConversion.EthUnit.Gwei),
                    MaxPriorityFeePerGas = 0
                };
                var alowNativeTokenReceipt = await nativeToken.ApproveRequestAndWaitForReceiptAsync(approveERC20Function, StopAfterDelay(cancelDelay));
                Console.WriteLine($"Native Token was approved to be used in the meditation circle.\n{alowNativeTokenReceipt.GasUsed} gas was used.");
            }
            if (!await account.Hero.IsApprovedForAllQueryAsync(account.Account.Address, account.Meditation.ContractHandler.ContractAddress))
			{
				Console.WriteLine($"Heroes not approved for meditation (Level up).\nTrying to approve Meditation of heroes.");
				var approvalForAllFunc = new DFKContracts.HeroCore.ContractDefinition.SetApprovalForAllFunction()
				{
					Operator = account.Meditation.ContractHandler.ContractAddress,
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
			FinishedTransactions.Add(new()
			{
				Name = $"Started meditation For Hero: {heroId}",
				TimeStamp = DateTime.Now,
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
				TimeStamp = DateTime.Now,
				TransactionHash = null
			});
			Console.WriteLine($"{e.Message}");
			TransactionAdded?.Invoke();
			throw;
		}
	}

	public static async Task<string> CompleteQuest(DFKAccount account, BigInteger heroId, int maxGasFeeGwei = 200, int cancelDelay = 60000)
	{
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
			FinishedTransactions.Add(new()
			{
				Name = $"Complete Quest For Hero: {heroId}",
				TimeStamp = DateTime.Now,
				TransactionHash = receipt.TransactionHash,
			});
			TransactionAdded?.Invoke();
			if(receipt.Status == new BigInteger(1))
			{
				return $"Completed Quests.\nTxn hash: {receipt.TransactionHash}";
			}
			else
			{
				return $"Transaction Failed.";
			}
        }
		catch(Exception e)
        {
            FinishedTransactions.Add(new()
            {
                Success = false,
                Name = $"Failed Complete Quest: {e.Message}",
                TimeStamp = DateTime.Now,
                TransactionHash = null
            });
            Console.WriteLine($"{e.Message}");
            TransactionAdded?.Invoke();
			throw;
		}
	}

    public static async Task<string> BuyPirateQuesterToken(DFKAccount account, int quantity, int maxGasFeeGwei = 200, int cancelDelay = 60000)
    {
        TransactionAdded?.Invoke();
		
        try
        {
            var handler = account.AvalancheSigner.Eth.GetContractTransactionHandler<BuyFunction>();
            var buyFunction = new BuyFunction()
            {
                AmountToSend = quantity * await account.PQT.PriceQueryAsync(),
                Quantity = (byte)quantity,
                MaxFeePerGas = Web3.Convert.ToWei(maxGasFeeGwei, Nethereum.Util.UnitConversion.EthUnit.Gwei),
                MaxPriorityFeePerGas = 0
            };
            var buyRequestResponse = await account.PQT.BuyRequestAndWaitForReceiptAsync(buyFunction, StopAfterDelay(cancelDelay));
            Console.WriteLine($"Started Quest Txn: Gas: {buyRequestResponse.GasUsed.Value}");
            //var startQuestEvent = questStartResponse.DecodeAllEvents<QuestStartedEventDTO>();
            FinishedTransactions.Add(new()
            {
                Success = true,
                Name = $"Bought one Pirate Quester Token! Have fun with the bot!",
                TimeStamp = DateTime.Now,
                TransactionHash = buyRequestResponse.TransactionHash
            });
            TransactionAdded?.Invoke();
            if (buyRequestResponse.Status == new BigInteger(1))
            {
                return $"Bought one Pirate Quester Token!\nTransaction: {buyRequestResponse.TransactionHash}\nhttps://avascan.info/blockchain/dfk/tx/{buyRequestResponse.TransactionHash}\nGas Paid: {buyRequestResponse.GasUsed}";
            }
            else
            {
                return $"Failed to buy {quantity} Pirate Quester Token";
            }
        }
        catch (Exception e)
        {
            FinishedTransactions.Add(new()
            {
                Success = false,
                Name = $"Failed to buy {quantity} Pirate Quester Token: {e.Message}",
                TimeStamp = DateTime.Now,
                TransactionHash = null
            });
            TransactionAdded?.Invoke();
            throw;
        }
    }
    public static async Task<string> StartQuest(DFKAccount account, List<BigInteger> selectedHeroes, QuestContract quest, int attempts, int maxGasFeeGwei = 200, int cancelDelay = 60000)
	{
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
			FinishedTransactions.Add(new()
            {
                Success = true,
                Name = $"Started Quest {quest.Name}",
				TimeStamp = DateTime.Now,
				TransactionHash = questStartResponse.TransactionHash
			});
            TransactionAdded?.Invoke();
			if (questStartResponse.Status == new BigInteger(1))
			{
				return $"Started Quest: {quest.Name}\nTransaction: {questStartResponse.TransactionHash}\nhttps://avascan.info/blockchain/dfk/tx/{questStartResponse.TransactionHash}\nGas Paid: {questStartResponse.GasUsed}";
			}
			else
			{
				return $"Failed to start Quest: {quest.Name}";
			}
        }
		catch(Exception e)
        {
			FinishedTransactions.Add(new()
			{
				Success = false,
				Name = $"Failed Start Quest {quest.Name}: {e.Message}",
				TimeStamp = DateTime.Now,
				TransactionHash = null
			});
            TransactionAdded?.Invoke();
			throw;
		}
	}
}