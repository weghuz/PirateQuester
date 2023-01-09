using DFKContracts.QuestCore.ContractDefinition;
using PirateQuester.DFK.Contracts;
using PirateQuester.Utils;
using BigInteger = System.Numerics.BigInteger;
using Nethereum.Web3;
using DFKContracts.MeditationCircle.ContractDefinition;
using DFKContracts.ERC20;
using static PirateQuester.DFK.Contracts.QuestContractDefinitions;
using PirateQuester.DFK.Items;
using PirateQuester.PirateQuesterToken.ContractDefinition;
using PirateQuester.HeroSale.ContractDefinition;
using static Nethereum.Util.UnitConversion;
using PirateQuester.ItemConsumer.ContractDefinition;

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
				MaxFeePerGas = Web3.Convert.ToWei(maxGasFeeGwei, EthUnit.Gwei),
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
                    MaxFeePerGas = Web3.Convert.ToWei(maxGasFeeGwei, EthUnit.Gwei),
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
                    MaxFeePerGas = Web3.Convert.ToWei(maxGasFeeGwei, EthUnit.Gwei),
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
					MaxFeePerGas = Web3.Convert.ToWei(maxGasFeeGwei, EthUnit.Gwei),
					MaxPriorityFeePerGas = 0
				};

				var approvalReceipt = await account.Hero.SetApprovalForAllRequestAndWaitForReceiptAsync(approvalForAllFunc, StopAfterDelay(cancelDelay));
				Console.WriteLine($"Heroes approved for meditation. {approvalReceipt.TransactionHash}");
			}
			var handler = account.Signer.Eth.GetContractTransactionHandler<StartMeditationFunction>();
            // PQ STATS ASSIGNMENT
            //{ new(0, "Strength") },
            //{ new(1, "Dexterity") },
            //{ new(2, "Agility") },
            //{ new(3, "Vitality") },
            //{ new(4, "Endurance") },
            //{ new(5, "Intelligence") },
            //{ new(6, "Wisdom") },
            //{ new(7, "Luck") }
            // MEDITATION CIRCLE STATS ASSIGNMENT
            //0: "Strength",
            //1: "Agility",
            //2: "Intelligence",
            //3: "Wisdom",
            //4: "Luck",
            //5: "Vitality",
            //6: "Endurance",
            //7: "Dexterity",
            byte fixedStat1 = stat1 switch
            {
                0 => 0,
                1 => 7,
                2 => 1,
                3 => 5,
                4 => 6,
                5 => 2,
                6 => 3,
                7 => 4,
                _ => 0
            }; 
            byte fixedStat2 = stat2 switch
            {
                0 => 0,
                1 => 7,
                2 => 1,
                3 => 5,
                4 => 6,
                5 => 2,
                6 => 3,
                7 => 4,
                _ => 0
            }; 
            byte fixedStat3 = stat3 switch
            {
                0 => 0,
                1 => 7,
                2 => 1,
                3 => 5,
                4 => 6,
                5 => 2,
                6 => 3,
                7 => 4,
                _ => 0
            };

            var startMeditationFunc = new StartMeditationFunction()
			{
				AttunementCrystal = NULL_ADDRESS,
				HeroId = heroId,
				PrimaryStat = fixedStat1,
				SecondaryStat = fixedStat2,
				TertiaryStat = fixedStat3,
				MaxFeePerGas = Web3.Convert.ToWei(maxGasFeeGwei, EthUnit.Gwei),
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
				MaxFeePerGas = Web3.Convert.ToWei(maxGasFeeGwei, EthUnit.Gwei),
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
			Console.WriteLine("Chain: " + account.Chain.Name);
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
                MaxFeePerGas = Web3.Convert.ToWei(maxGasFeeGwei, EthUnit.Gwei),
                MaxPriorityFeePerGas = 0
            };
            var buyRequestResponse = await account.PQT.BuyRequestAndWaitForReceiptAsync(buyFunction, StopAfterDelay(cancelDelay));
            Console.WriteLine($"Started Buy PQT Txn: Gas: {buyRequestResponse.GasUsed.Value}");
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
                return $"Bought one Pirate Quester Token!\nTransaction: {buyRequestResponse.TransactionHash}\nhttps://snowtrace.io/tx/{buyRequestResponse.TransactionHash}\nGas Paid: {buyRequestResponse.GasUsed}";
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
                MaxFeePerGas = Web3.Convert.ToWei(maxGasFeeGwei, EthUnit.Gwei),
                MaxPriorityFeePerGas = 0,
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
        catch (Exception e)
        {
            Console.WriteLine(account.Chain.Name);
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

    public static async Task<string> CancelAuction(DFKAccount account, 
        BigInteger heroId,
        int maxGasFeeGwei = 200, 
        int cancelDelay = 60000)
    {
        TransactionAdded?.Invoke();
        bool isApproved = await account.Hero.IsApprovedForAllQueryAsync(account.Account.Address, account.Chain.HeroSale);
        Console.WriteLine($"Is approved: {isApproved}");

        if (isApproved is false)
        {
            string approveAllResponse = await account.Hero.SetApprovalForAllRequestAsync(account.Chain.HeroSale, true);
            Console.WriteLine($"Set Approved for {account.Chain.HeroSale}: {approveAllResponse}");
        }
        Console.WriteLine($"Cancelling auction for hero {heroId}.");
        try
        {
            var handler = account.Signer.Eth.GetContractTransactionHandler<CancelAuctionFunction>();
            var cancelAuctionFunc = new CancelAuctionFunction()
            {
                MaxFeePerGas = Web3.Convert.ToWei(maxGasFeeGwei, EthUnit.Gwei),
                MaxPriorityFeePerGas = 0,
                TokenId = heroId,
                FromAddress = account.Account.Address
            };

            var cancelAuctionResponse = await account.Auction.CancelAuctionRequestAndWaitForReceiptAsync(cancelAuctionFunc, StopAfterDelay(cancelDelay));
            Console.WriteLine($"Started Auction Txn: {cancelAuctionResponse.TransactionHash} Gas: {cancelAuctionResponse.GasUsed.Value}");
            //var startQuestEvent = questStartResponse.DecodeAllEvents<QuestStartedEventDTO>();
            FinishedTransactions.Add(new()
            {
                Success = true,
                Name = $"Cancelled Auction for {heroId}",
                TimeStamp = DateTime.Now,
                TransactionHash = cancelAuctionResponse.TransactionHash
            });
            TransactionAdded?.Invoke();
            if (cancelAuctionResponse.Status == new BigInteger(1))
            {
                return $"Cancelled Auction for {heroId}\nhttps://avascan.info/blockchain/dfk/tx/{cancelAuctionResponse.TransactionHash}\nGas Paid: {cancelAuctionResponse.GasUsed}";
            }
            else
            {
                return $"Failed to Cancelled Auction: for {heroId}";
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(account.Chain.Name);
            FinishedTransactions.Add(new()
            {
                Success = false,
                Name = $"Failed to Cancelled Auction: for {heroId}\n" +
                    $"{e.Message}\n" +
                    $"{e.StackTrace}",
                TimeStamp = DateTime.Now,
                TransactionHash = null
            });
            TransactionAdded?.Invoke();
            throw;
        }
    }

    public static async Task<string> StartAuction(DFKAccount account, BigInteger heroId, decimal salePrice, int maxGasFeeGwei = 200, int cancelDelay = 60000)
    {
        TransactionAdded?.Invoke();
        bool isApproved = await account.Hero.IsApprovedForAllQueryAsync(account.Account.Address, account.Chain.HeroSale);
        Console.WriteLine($"Is approved: {isApproved}");

        if (isApproved is false)
        {
            string approveAllResponse = await account.Hero.SetApprovalForAllRequestAsync(account.Chain.HeroSale, true);
            Console.WriteLine($"Set Approved for {account.Chain.HeroSale}: {approveAllResponse}");
        }
        Console.WriteLine($"Selling hero {heroId} for {salePrice} {(account.Chain.Name == "DFK" ? "Crystal" : "Jade")}.");
        try
        {
            var handler = account.Signer.Eth.GetContractTransactionHandler<CreateAuctionFunction>();
            var startAuctionFunc = new CreateAuctionFunction()
            {
                StartingPrice = Web3.Convert.ToWei(salePrice, EthUnit.Ether),
                MaxFeePerGas = Web3.Convert.ToWei(maxGasFeeGwei, EthUnit.Gwei),
                MaxPriorityFeePerGas = 0,
                TokenId = heroId,
                EndingPrice = Web3.Convert.ToWei(salePrice, EthUnit.Ether),
                Duration = 1337,
                Winner = ""
            };

            var startAuctionResponse = await account.Auction.CreateAuctionRequestAndWaitForReceiptAsync(startAuctionFunc, StopAfterDelay(cancelDelay));
            Console.WriteLine($"Started Auction Txn: {startAuctionResponse.TransactionHash} Gas: {startAuctionResponse.GasUsed.Value}");
            //var startQuestEvent = questStartResponse.DecodeAllEvents<QuestStartedEventDTO>();
            FinishedTransactions.Add(new()
            {
                Success = true,
                Name = $"Started Auction for {heroId} for {salePrice} {(account.Chain.Name == "DFK" ? "Crystal" : "Jade")}",
                TimeStamp = DateTime.Now,
                TransactionHash = startAuctionResponse.TransactionHash
            });
            TransactionAdded?.Invoke();
            if (startAuctionResponse.Status == new BigInteger(1))
            {
                return $"Started Auction for {heroId} for {salePrice} {(account.Chain.Name == "DFK" ? "Crystal" : "Jade")}\nTransaction: {startAuctionResponse.TransactionHash}\nhttps://avascan.info/blockchain/dfk/tx/{startAuctionResponse.TransactionHash}\nGas Paid: {startAuctionResponse.GasUsed}";
            }
            else
            {
                return $"Failed to Start Auction: for {heroId} for {salePrice} {(account.Chain.Name == "DFK" ? "Crystal" : "Jade")}";
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(account.Chain.Name);
            FinishedTransactions.Add(new()
            {
                Success = false,
                Name = $"Failed to Start Auction: for {heroId} for {salePrice} {(account.Chain.Name == "DFK" ? "Crystal" : "Jade")}\n" +
                    $"{e.Message}\n" +
                    $"{e.StackTrace}",
                TimeStamp = DateTime.Now,
                TransactionHash = null
            });
            TransactionAdded?.Invoke();
            throw;
        }
    }

    public static async Task<string> UseComsumableItem(DFKAccount account, BigInteger heroId, string consumableAddress, int maxGasFeeGwei = 200, int cancelDelay = 60000)
    {
        TransactionAdded?.Invoke();
        var consumableItem = ItemContractDefinitions.GetItem(new() { Address = consumableAddress, Chain = account.Chain });
		//Check approved for item consumer contract
        
		var ConsumableItem = new Erc20Service(account.Signer, consumableItem.Addresses.First(a => a.Chain.Id == account.Chain.Id).Address);
		var ConsumableAllowance = await ConsumableItem.AllowanceQueryAsync(account.Account.Address, account.Meditation.ContractHandler.ContractAddress);
		Console.WriteLine($"Allowance for Stampotions are {ConsumableAllowance}");
		if (ConsumableAllowance < 1000)
		{
			Console.WriteLine($"{consumableItem.Name} not allowed. Setting allowance for Item Consumer to use.");
			var approveERC20Function = new DFKContracts.ERC20.ContractDefinition.ApproveFunction()
			{
				Amount = new BigInteger(10000),
				Spender = account.Chain.ItemConsumer,
				MaxFeePerGas = Web3.Convert.ToWei(maxGasFeeGwei, Nethereum.Util.UnitConversion.EthUnit.Gwei),
				MaxPriorityFeePerGas = 0
			};
			var allowConsumableItemReceipt = await ConsumableItem.ApproveRequestAndWaitForReceiptAsync(approveERC20Function, StopAfterDelay(cancelDelay));
			Console.WriteLine($"{consumableItem.Name} was approved to be used in the meditation circle\n{allowConsumableItemReceipt.GasUsed} gas was used.");
		}
        
        Console.WriteLine($"Consuming 1 {consumableItem.Name} for Hero {heroId}.");
        try
        {
            var handler = account.Signer.Eth.GetContractTransactionHandler<ConsumeItemFunction>();
            var hero = (await account.Hero.GetHeroQueryAsync(heroId)).ReturnValue1;

			var consumeFunc = new ConsumeItemFunction()
            {
                MaxFeePerGas = Web3.Convert.ToWei(maxGasFeeGwei, EthUnit.Gwei),
                MaxPriorityFeePerGas = 0,
                HeroId = heroId,
                ConsumableAddress = consumableAddress
            };

            var consumeResponse = await account.ItemConsumer.ConsumeItemRequestAndWaitForReceiptAsync(consumeFunc, StopAfterDelay(cancelDelay));
            Console.WriteLine($"Started Auction Txn: {consumeResponse.TransactionHash} Gas: {consumeResponse.GasUsed.Value}");
            //var startQuestEvent = questStartResponse.DecodeAllEvents<QuestStartedEventDTO>();
            FinishedTransactions.Add(new()
            {
                Success = true,
                Name = $"Consumed 1 {consumableItem.Name} for Hero {heroId}.",
                TimeStamp = DateTime.Now,
                TransactionHash = consumeResponse.TransactionHash
            });
            TransactionAdded?.Invoke();
            if (consumeResponse.Status == new BigInteger(1))
            {
                return $"Consumed 1 {consumableItem.Name} for Hero {heroId}.\nTransaction: {consumeResponse.TransactionHash}\nhttps://avascan.info/blockchain/dfk/tx/{consumeResponse.TransactionHash}\nGas Paid: {consumeResponse.GasUsed}";
            }
            else
            {
                return $"Failed to Consume item: {consumableItem.Name} for Hero {heroId}";
            }
        }
        catch (Exception e)
        {
            FinishedTransactions.Add(new()
            {
                Success = false,
                Name = $"Failed to Consume item: {consumableItem.Name} for Hero {heroId}\n{e.Message}\n{e.StackTrace}",
                TimeStamp = DateTime.Now,
                TransactionHash = null
            });
            TransactionAdded?.Invoke();
            throw;
        }
    }
}