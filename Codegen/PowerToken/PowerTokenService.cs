using Nethereum.Contracts.ContractHandlers;
using Nethereum.RPC.Eth.DTOs;
using PirateQuester.PowerToken.ContractDefinition;
using System.Numerics;

namespace PirateQuester.PowerToken
{
	public partial class PowerTokenService
	{
		public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, PowerTokenDeployment powerTokenDeployment, CancellationTokenSource cancellationTokenSource = null)
		{
			return web3.Eth.GetContractDeploymentHandler<PowerTokenDeployment>().SendRequestAndWaitForReceiptAsync(powerTokenDeployment, cancellationTokenSource);
		}

		public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, PowerTokenDeployment powerTokenDeployment)
		{
			return web3.Eth.GetContractDeploymentHandler<PowerTokenDeployment>().SendRequestAsync(powerTokenDeployment);
		}

		public static async Task<PowerTokenService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, PowerTokenDeployment powerTokenDeployment, CancellationTokenSource cancellationTokenSource = null)
		{
			var receipt = await DeployContractAndWaitForReceiptAsync(web3, powerTokenDeployment, cancellationTokenSource);
			return new PowerTokenService(web3, receipt.ContractAddress);
		}

		protected Nethereum.Web3.Web3 Web3 { get; }

		public ContractHandler ContractHandler { get; }

		public PowerTokenService(Nethereum.Web3.Web3 web3, string contractAddress)
		{
			Web3 = web3;
			ContractHandler = web3.Eth.GetContractHandler(contractAddress);
		}

		public Task<string> AddAuthorizedRequestAsync(AddAuthorizedFunction addAuthorizedFunction)
		{
			return ContractHandler.SendRequestAsync(addAuthorizedFunction);
		}

		public Task<TransactionReceipt> AddAuthorizedRequestAndWaitForReceiptAsync(AddAuthorizedFunction addAuthorizedFunction, CancellationTokenSource cancellationToken = null)
		{
			return ContractHandler.SendRequestAndWaitForReceiptAsync(addAuthorizedFunction, cancellationToken);
		}

		public Task<string> AddAuthorizedRequestAsync(string toAdd)
		{
			var addAuthorizedFunction = new AddAuthorizedFunction();
			addAuthorizedFunction.ToAdd = toAdd;

			return ContractHandler.SendRequestAsync(addAuthorizedFunction);
		}

		public Task<TransactionReceipt> AddAuthorizedRequestAndWaitForReceiptAsync(string toAdd, CancellationTokenSource cancellationToken = null)
		{
			var addAuthorizedFunction = new AddAuthorizedFunction();
			addAuthorizedFunction.ToAdd = toAdd;

			return ContractHandler.SendRequestAndWaitForReceiptAsync(addAuthorizedFunction, cancellationToken);
		}

		public Task<BigInteger> AllowanceQueryAsync(AllowanceFunction allowanceFunction, BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<AllowanceFunction, BigInteger>(allowanceFunction, blockParameter);
		}


		public Task<BigInteger> AllowanceQueryAsync(string owner, string spender, BlockParameter blockParameter = null)
		{
			var allowanceFunction = new AllowanceFunction();
			allowanceFunction.Owner = owner;
			allowanceFunction.Spender = spender;

			return ContractHandler.QueryAsync<AllowanceFunction, BigInteger>(allowanceFunction, blockParameter);
		}

		public Task<string> ApproveRequestAsync(ApproveFunction approveFunction)
		{
			return ContractHandler.SendRequestAsync(approveFunction);
		}

		public Task<TransactionReceipt> ApproveRequestAndWaitForReceiptAsync(ApproveFunction approveFunction, CancellationTokenSource cancellationToken = null)
		{
			return ContractHandler.SendRequestAndWaitForReceiptAsync(approveFunction, cancellationToken);
		}

		public Task<string> ApproveRequestAsync(string spender, BigInteger amount)
		{
			var approveFunction = new ApproveFunction();
			approveFunction.Spender = spender;
			approveFunction.Amount = amount;

			return ContractHandler.SendRequestAsync(approveFunction);
		}

		public Task<TransactionReceipt> ApproveRequestAndWaitForReceiptAsync(string spender, BigInteger amount, CancellationTokenSource cancellationToken = null)
		{
			var approveFunction = new ApproveFunction();
			approveFunction.Spender = spender;
			approveFunction.Amount = amount;

			return ContractHandler.SendRequestAndWaitForReceiptAsync(approveFunction, cancellationToken);
		}

		public Task<bool> AuthorizedQueryAsync(AuthorizedFunction authorizedFunction, BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<AuthorizedFunction, bool>(authorizedFunction, blockParameter);
		}


		public Task<bool> AuthorizedQueryAsync(string returnValue1, BlockParameter blockParameter = null)
		{
			var authorizedFunction = new AuthorizedFunction();
			authorizedFunction.ReturnValue1 = returnValue1;

			return ContractHandler.QueryAsync<AuthorizedFunction, bool>(authorizedFunction, blockParameter);
		}

		public Task<BigInteger> BalanceOfQueryAsync(BalanceOfFunction balanceOfFunction, BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<BalanceOfFunction, BigInteger>(balanceOfFunction, blockParameter);
		}


		public Task<BigInteger> BalanceOfQueryAsync(string account, BlockParameter blockParameter = null)
		{
			var balanceOfFunction = new BalanceOfFunction();
			balanceOfFunction.Account = account;

			return ContractHandler.QueryAsync<BalanceOfFunction, BigInteger>(balanceOfFunction, blockParameter);
		}

		public Task<BigInteger> CanUnlockAmountQueryAsync(CanUnlockAmountFunction canUnlockAmountFunction, BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<CanUnlockAmountFunction, BigInteger>(canUnlockAmountFunction, blockParameter);
		}


		public Task<BigInteger> CanUnlockAmountQueryAsync(string holder, BlockParameter blockParameter = null)
		{
			var canUnlockAmountFunction = new CanUnlockAmountFunction();
			canUnlockAmountFunction.Holder = holder;

			return ContractHandler.QueryAsync<CanUnlockAmountFunction, BigInteger>(canUnlockAmountFunction, blockParameter);
		}

		public Task<BigInteger> CapQueryAsync(CapFunction capFunction, BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<CapFunction, BigInteger>(capFunction, blockParameter);
		}


		public Task<BigInteger> CapQueryAsync(BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<CapFunction, BigInteger>(null, blockParameter);
		}

		public Task<BigInteger> CirculatingSupplyQueryAsync(CirculatingSupplyFunction circulatingSupplyFunction, BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<CirculatingSupplyFunction, BigInteger>(circulatingSupplyFunction, blockParameter);
		}


		public Task<BigInteger> CirculatingSupplyQueryAsync(BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<CirculatingSupplyFunction, BigInteger>(null, blockParameter);
		}

		public Task<byte> DecimalsQueryAsync(DecimalsFunction decimalsFunction, BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<DecimalsFunction, byte>(decimalsFunction, blockParameter);
		}


		public Task<byte> DecimalsQueryAsync(BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<DecimalsFunction, byte>(null, blockParameter);
		}

		public Task<string> DecreaseAllowanceRequestAsync(DecreaseAllowanceFunction decreaseAllowanceFunction)
		{
			return ContractHandler.SendRequestAsync(decreaseAllowanceFunction);
		}

		public Task<TransactionReceipt> DecreaseAllowanceRequestAndWaitForReceiptAsync(DecreaseAllowanceFunction decreaseAllowanceFunction, CancellationTokenSource cancellationToken = null)
		{
			return ContractHandler.SendRequestAndWaitForReceiptAsync(decreaseAllowanceFunction, cancellationToken);
		}

		public Task<string> DecreaseAllowanceRequestAsync(string spender, BigInteger subtractedValue)
		{
			var decreaseAllowanceFunction = new DecreaseAllowanceFunction();
			decreaseAllowanceFunction.Spender = spender;
			decreaseAllowanceFunction.SubtractedValue = subtractedValue;

			return ContractHandler.SendRequestAsync(decreaseAllowanceFunction);
		}

		public Task<TransactionReceipt> DecreaseAllowanceRequestAndWaitForReceiptAsync(string spender, BigInteger subtractedValue, CancellationTokenSource cancellationToken = null)
		{
			var decreaseAllowanceFunction = new DecreaseAllowanceFunction();
			decreaseAllowanceFunction.Spender = spender;
			decreaseAllowanceFunction.SubtractedValue = subtractedValue;

			return ContractHandler.SendRequestAndWaitForReceiptAsync(decreaseAllowanceFunction, cancellationToken);
		}

		public Task<string> IncreaseAllowanceRequestAsync(IncreaseAllowanceFunction increaseAllowanceFunction)
		{
			return ContractHandler.SendRequestAsync(increaseAllowanceFunction);
		}

		public Task<TransactionReceipt> IncreaseAllowanceRequestAndWaitForReceiptAsync(IncreaseAllowanceFunction increaseAllowanceFunction, CancellationTokenSource cancellationToken = null)
		{
			return ContractHandler.SendRequestAndWaitForReceiptAsync(increaseAllowanceFunction, cancellationToken);
		}

		public Task<string> IncreaseAllowanceRequestAsync(string spender, BigInteger addedValue)
		{
			var increaseAllowanceFunction = new IncreaseAllowanceFunction();
			increaseAllowanceFunction.Spender = spender;
			increaseAllowanceFunction.AddedValue = addedValue;

			return ContractHandler.SendRequestAsync(increaseAllowanceFunction);
		}

		public Task<TransactionReceipt> IncreaseAllowanceRequestAndWaitForReceiptAsync(string spender, BigInteger addedValue, CancellationTokenSource cancellationToken = null)
		{
			var increaseAllowanceFunction = new IncreaseAllowanceFunction();
			increaseAllowanceFunction.Spender = spender;
			increaseAllowanceFunction.AddedValue = addedValue;

			return ContractHandler.SendRequestAndWaitForReceiptAsync(increaseAllowanceFunction, cancellationToken);
		}

		public Task<BigInteger> LastUnlockTimeQueryAsync(LastUnlockTimeFunction lastUnlockTimeFunction, BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<LastUnlockTimeFunction, BigInteger>(lastUnlockTimeFunction, blockParameter);
		}


		public Task<BigInteger> LastUnlockTimeQueryAsync(string returnValue1, BlockParameter blockParameter = null)
		{
			var lastUnlockTimeFunction = new LastUnlockTimeFunction();
			lastUnlockTimeFunction.ReturnValue1 = returnValue1;

			return ContractHandler.QueryAsync<LastUnlockTimeFunction, BigInteger>(lastUnlockTimeFunction, blockParameter);
		}

		public Task<string> LockRequestAsync(LockFunction @lockFunction)
		{
			return ContractHandler.SendRequestAsync(@lockFunction);
		}

		public Task<TransactionReceipt> LockRequestAndWaitForReceiptAsync(LockFunction @lockFunction, CancellationTokenSource cancellationToken = null)
		{
			return ContractHandler.SendRequestAndWaitForReceiptAsync(@lockFunction, cancellationToken);
		}

		public Task<string> LockRequestAsync(string holder, BigInteger amount)
		{
			var @lockFunction = new LockFunction();
			@lockFunction.Holder = holder;
			@lockFunction.Amount = amount;

			return ContractHandler.SendRequestAsync(@lockFunction);
		}

		public Task<TransactionReceipt> LockRequestAndWaitForReceiptAsync(string holder, BigInteger amount, CancellationTokenSource cancellationToken = null)
		{
			var @lockFunction = new LockFunction();
			@lockFunction.Holder = holder;
			@lockFunction.Amount = amount;

			return ContractHandler.SendRequestAndWaitForReceiptAsync(@lockFunction, cancellationToken);
		}

		public Task<BigInteger> LockFromTimeQueryAsync(LockFromTimeFunction lockFromTimeFunction, BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<LockFromTimeFunction, BigInteger>(lockFromTimeFunction, blockParameter);
		}


		public Task<BigInteger> LockFromTimeQueryAsync(BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<LockFromTimeFunction, BigInteger>(null, blockParameter);
		}

		public Task<string> LockFromUpdateRequestAsync(LockFromUpdateFunction lockFromUpdateFunction)
		{
			return ContractHandler.SendRequestAsync(lockFromUpdateFunction);
		}

		public Task<TransactionReceipt> LockFromUpdateRequestAndWaitForReceiptAsync(LockFromUpdateFunction lockFromUpdateFunction, CancellationTokenSource cancellationToken = null)
		{
			return ContractHandler.SendRequestAndWaitForReceiptAsync(lockFromUpdateFunction, cancellationToken);
		}

		public Task<string> LockFromUpdateRequestAsync(BigInteger lockFromTime)
		{
			var lockFromUpdateFunction = new LockFromUpdateFunction();
			lockFromUpdateFunction.LockFromTime = lockFromTime;

			return ContractHandler.SendRequestAsync(lockFromUpdateFunction);
		}

		public Task<TransactionReceipt> LockFromUpdateRequestAndWaitForReceiptAsync(BigInteger lockFromTime, CancellationTokenSource cancellationToken = null)
		{
			var lockFromUpdateFunction = new LockFromUpdateFunction();
			lockFromUpdateFunction.LockFromTime = lockFromTime;

			return ContractHandler.SendRequestAndWaitForReceiptAsync(lockFromUpdateFunction, cancellationToken);
		}

		public Task<BigInteger> LockOfQueryAsync(LockOfFunction lockOfFunction, BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<LockOfFunction, BigInteger>(lockOfFunction, blockParameter);
		}


		public Task<BigInteger> LockOfQueryAsync(string holder, BlockParameter blockParameter = null)
		{
			var lockOfFunction = new LockOfFunction();
			lockOfFunction.Holder = holder;

			return ContractHandler.QueryAsync<LockOfFunction, BigInteger>(lockOfFunction, blockParameter);
		}

		public Task<BigInteger> LockToTimeQueryAsync(LockToTimeFunction lockToTimeFunction, BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<LockToTimeFunction, BigInteger>(lockToTimeFunction, blockParameter);
		}


		public Task<BigInteger> LockToTimeQueryAsync(BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<LockToTimeFunction, BigInteger>(null, blockParameter);
		}

		public Task<string> LockToUpdateRequestAsync(LockToUpdateFunction lockToUpdateFunction)
		{
			return ContractHandler.SendRequestAsync(lockToUpdateFunction);
		}

		public Task<TransactionReceipt> LockToUpdateRequestAndWaitForReceiptAsync(LockToUpdateFunction lockToUpdateFunction, CancellationTokenSource cancellationToken = null)
		{
			return ContractHandler.SendRequestAndWaitForReceiptAsync(lockToUpdateFunction, cancellationToken);
		}

		public Task<string> LockToUpdateRequestAsync(BigInteger lockToTime)
		{
			var lockToUpdateFunction = new LockToUpdateFunction();
			lockToUpdateFunction.LockToTime = lockToTime;

			return ContractHandler.SendRequestAsync(lockToUpdateFunction);
		}

		public Task<TransactionReceipt> LockToUpdateRequestAndWaitForReceiptAsync(BigInteger lockToTime, CancellationTokenSource cancellationToken = null)
		{
			var lockToUpdateFunction = new LockToUpdateFunction();
			lockToUpdateFunction.LockToTime = lockToTime;

			return ContractHandler.SendRequestAndWaitForReceiptAsync(lockToUpdateFunction, cancellationToken);
		}

		public Task<string> ManualMintRequestAsync(ManualMintFunction manualMintFunction)
		{
			return ContractHandler.SendRequestAsync(manualMintFunction);
		}

		public Task<TransactionReceipt> ManualMintRequestAndWaitForReceiptAsync(ManualMintFunction manualMintFunction, CancellationTokenSource cancellationToken = null)
		{
			return ContractHandler.SendRequestAndWaitForReceiptAsync(manualMintFunction, cancellationToken);
		}

		public Task<string> ManualMintRequestAsync(string to, BigInteger amount)
		{
			var manualMintFunction = new ManualMintFunction();
			manualMintFunction.To = to;
			manualMintFunction.Amount = amount;

			return ContractHandler.SendRequestAsync(manualMintFunction);
		}

		public Task<TransactionReceipt> ManualMintRequestAndWaitForReceiptAsync(string to, BigInteger amount, CancellationTokenSource cancellationToken = null)
		{
			var manualMintFunction = new ManualMintFunction();
			manualMintFunction.To = to;
			manualMintFunction.Amount = amount;

			return ContractHandler.SendRequestAndWaitForReceiptAsync(manualMintFunction, cancellationToken);
		}

		public Task<BigInteger> ManualMintLimitQueryAsync(ManualMintLimitFunction manualMintLimitFunction, BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<ManualMintLimitFunction, BigInteger>(manualMintLimitFunction, blockParameter);
		}


		public Task<BigInteger> ManualMintLimitQueryAsync(BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<ManualMintLimitFunction, BigInteger>(null, blockParameter);
		}

		public Task<BigInteger> ManualMintedQueryAsync(ManualMintedFunction manualMintedFunction, BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<ManualMintedFunction, BigInteger>(manualMintedFunction, blockParameter);
		}


		public Task<BigInteger> ManualMintedQueryAsync(BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<ManualMintedFunction, BigInteger>(null, blockParameter);
		}

		public Task<BigInteger> MaxTransferAmountQueryAsync(MaxTransferAmountFunction maxTransferAmountFunction, BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<MaxTransferAmountFunction, BigInteger>(maxTransferAmountFunction, blockParameter);
		}


		public Task<BigInteger> MaxTransferAmountQueryAsync(BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<MaxTransferAmountFunction, BigInteger>(null, blockParameter);
		}

		public Task<ushort> MaxTransferAmountRateQueryAsync(MaxTransferAmountRateFunction maxTransferAmountRateFunction, BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<MaxTransferAmountRateFunction, ushort>(maxTransferAmountRateFunction, blockParameter);
		}


		public Task<ushort> MaxTransferAmountRateQueryAsync(BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<MaxTransferAmountRateFunction, ushort>(null, blockParameter);
		}

		public Task<string> MintRequestAsync(MintFunction mintFunction)
		{
			return ContractHandler.SendRequestAsync(mintFunction);
		}

		public Task<TransactionReceipt> MintRequestAndWaitForReceiptAsync(MintFunction mintFunction, CancellationTokenSource cancellationToken = null)
		{
			return ContractHandler.SendRequestAndWaitForReceiptAsync(mintFunction, cancellationToken);
		}

		public Task<string> MintRequestAsync(string to, BigInteger amount)
		{
			var mintFunction = new MintFunction();
			mintFunction.To = to;
			mintFunction.Amount = amount;

			return ContractHandler.SendRequestAsync(mintFunction);
		}

		public Task<TransactionReceipt> MintRequestAndWaitForReceiptAsync(string to, BigInteger amount, CancellationTokenSource cancellationToken = null)
		{
			var mintFunction = new MintFunction();
			mintFunction.To = to;
			mintFunction.Amount = amount;

			return ContractHandler.SendRequestAndWaitForReceiptAsync(mintFunction, cancellationToken);
		}

		public Task<string> NameQueryAsync(NameFunction nameFunction, BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<NameFunction, string>(nameFunction, blockParameter);
		}


		public Task<string> NameQueryAsync(BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<NameFunction, string>(null, blockParameter);
		}

		public Task<string> OwnerQueryAsync(OwnerFunction ownerFunction, BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<OwnerFunction, string>(ownerFunction, blockParameter);
		}


		public Task<string> OwnerQueryAsync(BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<OwnerFunction, string>(null, blockParameter);
		}

		public Task<string> RemoveAuthorizedRequestAsync(RemoveAuthorizedFunction removeAuthorizedFunction)
		{
			return ContractHandler.SendRequestAsync(removeAuthorizedFunction);
		}

		public Task<TransactionReceipt> RemoveAuthorizedRequestAndWaitForReceiptAsync(RemoveAuthorizedFunction removeAuthorizedFunction, CancellationTokenSource cancellationToken = null)
		{
			return ContractHandler.SendRequestAndWaitForReceiptAsync(removeAuthorizedFunction, cancellationToken);
		}

		public Task<string> RemoveAuthorizedRequestAsync(string toRemove)
		{
			var removeAuthorizedFunction = new RemoveAuthorizedFunction();
			removeAuthorizedFunction.ToRemove = toRemove;

			return ContractHandler.SendRequestAsync(removeAuthorizedFunction);
		}

		public Task<TransactionReceipt> RemoveAuthorizedRequestAndWaitForReceiptAsync(string toRemove, CancellationTokenSource cancellationToken = null)
		{
			var removeAuthorizedFunction = new RemoveAuthorizedFunction();
			removeAuthorizedFunction.ToRemove = toRemove;

			return ContractHandler.SendRequestAndWaitForReceiptAsync(removeAuthorizedFunction, cancellationToken);
		}

		public Task<string> RenounceOwnershipRequestAsync(RenounceOwnershipFunction renounceOwnershipFunction)
		{
			return ContractHandler.SendRequestAsync(renounceOwnershipFunction);
		}

		public Task<string> RenounceOwnershipRequestAsync()
		{
			return ContractHandler.SendRequestAsync<RenounceOwnershipFunction>();
		}

		public Task<TransactionReceipt> RenounceOwnershipRequestAndWaitForReceiptAsync(RenounceOwnershipFunction renounceOwnershipFunction, CancellationTokenSource cancellationToken = null)
		{
			return ContractHandler.SendRequestAndWaitForReceiptAsync(renounceOwnershipFunction, cancellationToken);
		}

		public Task<TransactionReceipt> RenounceOwnershipRequestAndWaitForReceiptAsync(CancellationTokenSource cancellationToken = null)
		{
			return ContractHandler.SendRequestAndWaitForReceiptAsync<RenounceOwnershipFunction>(null, cancellationToken);
		}

		public Task<string> SetExcludedFromAntiWhaleRequestAsync(SetExcludedFromAntiWhaleFunction setExcludedFromAntiWhaleFunction)
		{
			return ContractHandler.SendRequestAsync(setExcludedFromAntiWhaleFunction);
		}

		public Task<TransactionReceipt> SetExcludedFromAntiWhaleRequestAndWaitForReceiptAsync(SetExcludedFromAntiWhaleFunction setExcludedFromAntiWhaleFunction, CancellationTokenSource cancellationToken = null)
		{
			return ContractHandler.SendRequestAndWaitForReceiptAsync(setExcludedFromAntiWhaleFunction, cancellationToken);
		}

		public Task<string> SetExcludedFromAntiWhaleRequestAsync(string account, bool excluded)
		{
			var setExcludedFromAntiWhaleFunction = new SetExcludedFromAntiWhaleFunction();
			setExcludedFromAntiWhaleFunction.Account = account;
			setExcludedFromAntiWhaleFunction.Excluded = excluded;

			return ContractHandler.SendRequestAsync(setExcludedFromAntiWhaleFunction);
		}

		public Task<TransactionReceipt> SetExcludedFromAntiWhaleRequestAndWaitForReceiptAsync(string account, bool excluded, CancellationTokenSource cancellationToken = null)
		{
			var setExcludedFromAntiWhaleFunction = new SetExcludedFromAntiWhaleFunction();
			setExcludedFromAntiWhaleFunction.Account = account;
			setExcludedFromAntiWhaleFunction.Excluded = excluded;

			return ContractHandler.SendRequestAndWaitForReceiptAsync(setExcludedFromAntiWhaleFunction, cancellationToken);
		}

		public Task<string> SymbolQueryAsync(SymbolFunction symbolFunction, BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<SymbolFunction, string>(symbolFunction, blockParameter);
		}


		public Task<string> SymbolQueryAsync(BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<SymbolFunction, string>(null, blockParameter);
		}

		public Task<BigInteger> TotalBalanceOfQueryAsync(TotalBalanceOfFunction totalBalanceOfFunction, BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<TotalBalanceOfFunction, BigInteger>(totalBalanceOfFunction, blockParameter);
		}


		public Task<BigInteger> TotalBalanceOfQueryAsync(string holder, BlockParameter blockParameter = null)
		{
			var totalBalanceOfFunction = new TotalBalanceOfFunction();
			totalBalanceOfFunction.Holder = holder;

			return ContractHandler.QueryAsync<TotalBalanceOfFunction, BigInteger>(totalBalanceOfFunction, blockParameter);
		}

		public Task<BigInteger> TotalLockQueryAsync(TotalLockFunction totalLockFunction, BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<TotalLockFunction, BigInteger>(totalLockFunction, blockParameter);
		}


		public Task<BigInteger> TotalLockQueryAsync(BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<TotalLockFunction, BigInteger>(null, blockParameter);
		}

		public Task<BigInteger> TotalSupplyQueryAsync(TotalSupplyFunction totalSupplyFunction, BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<TotalSupplyFunction, BigInteger>(totalSupplyFunction, blockParameter);
		}


		public Task<BigInteger> TotalSupplyQueryAsync(BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<TotalSupplyFunction, BigInteger>(null, blockParameter);
		}

		public Task<string> TransferRequestAsync(TransferFunction transferFunction)
		{
			return ContractHandler.SendRequestAsync(transferFunction);
		}

		public Task<TransactionReceipt> TransferRequestAndWaitForReceiptAsync(TransferFunction transferFunction, CancellationTokenSource cancellationToken = null)
		{
			return ContractHandler.SendRequestAndWaitForReceiptAsync(transferFunction, cancellationToken);
		}

		public Task<string> TransferRequestAsync(string to, BigInteger amount)
		{
			var transferFunction = new TransferFunction();
			transferFunction.To = to;
			transferFunction.Amount = amount;

			return ContractHandler.SendRequestAsync(transferFunction);
		}

		public Task<TransactionReceipt> TransferRequestAndWaitForReceiptAsync(string to, BigInteger amount, CancellationTokenSource cancellationToken = null)
		{
			var transferFunction = new TransferFunction();
			transferFunction.To = to;
			transferFunction.Amount = amount;

			return ContractHandler.SendRequestAndWaitForReceiptAsync(transferFunction, cancellationToken);
		}

		public Task<string> TransferAllRequestAsync(TransferAllFunction transferAllFunction)
		{
			return ContractHandler.SendRequestAsync(transferAllFunction);
		}

		public Task<TransactionReceipt> TransferAllRequestAndWaitForReceiptAsync(TransferAllFunction transferAllFunction, CancellationTokenSource cancellationToken = null)
		{
			return ContractHandler.SendRequestAndWaitForReceiptAsync(transferAllFunction, cancellationToken);
		}

		public Task<string> TransferAllRequestAsync(string to)
		{
			var transferAllFunction = new TransferAllFunction();
			transferAllFunction.To = to;

			return ContractHandler.SendRequestAsync(transferAllFunction);
		}

		public Task<TransactionReceipt> TransferAllRequestAndWaitForReceiptAsync(string to, CancellationTokenSource cancellationToken = null)
		{
			var transferAllFunction = new TransferAllFunction();
			transferAllFunction.To = to;

			return ContractHandler.SendRequestAndWaitForReceiptAsync(transferAllFunction, cancellationToken);
		}

		public Task<BigInteger> TransferAllIntervalQueryAsync(TransferAllIntervalFunction transferAllIntervalFunction, BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<TransferAllIntervalFunction, BigInteger>(transferAllIntervalFunction, blockParameter);
		}


		public Task<BigInteger> TransferAllIntervalQueryAsync(BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<TransferAllIntervalFunction, BigInteger>(null, blockParameter);
		}

		public Task<string> TransferAllIntervalUpdateRequestAsync(TransferAllIntervalUpdateFunction transferAllIntervalUpdateFunction)
		{
			return ContractHandler.SendRequestAsync(transferAllIntervalUpdateFunction);
		}

		public Task<TransactionReceipt> TransferAllIntervalUpdateRequestAndWaitForReceiptAsync(TransferAllIntervalUpdateFunction transferAllIntervalUpdateFunction, CancellationTokenSource cancellationToken = null)
		{
			return ContractHandler.SendRequestAndWaitForReceiptAsync(transferAllIntervalUpdateFunction, cancellationToken);
		}

		public Task<string> TransferAllIntervalUpdateRequestAsync(BigInteger transferAllInterval)
		{
			var transferAllIntervalUpdateFunction = new TransferAllIntervalUpdateFunction();
			transferAllIntervalUpdateFunction.TransferAllInterval = transferAllInterval;

			return ContractHandler.SendRequestAsync(transferAllIntervalUpdateFunction);
		}

		public Task<TransactionReceipt> TransferAllIntervalUpdateRequestAndWaitForReceiptAsync(BigInteger transferAllInterval, CancellationTokenSource cancellationToken = null)
		{
			var transferAllIntervalUpdateFunction = new TransferAllIntervalUpdateFunction();
			transferAllIntervalUpdateFunction.TransferAllInterval = transferAllInterval;

			return ContractHandler.SendRequestAndWaitForReceiptAsync(transferAllIntervalUpdateFunction, cancellationToken);
		}

		public Task<BigInteger> TransferAllTrackerQueryAsync(TransferAllTrackerFunction transferAllTrackerFunction, BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<TransferAllTrackerFunction, BigInteger>(transferAllTrackerFunction, blockParameter);
		}


		public Task<BigInteger> TransferAllTrackerQueryAsync(string returnValue1, BlockParameter blockParameter = null)
		{
			var transferAllTrackerFunction = new TransferAllTrackerFunction();
			transferAllTrackerFunction.ReturnValue1 = returnValue1;

			return ContractHandler.QueryAsync<TransferAllTrackerFunction, BigInteger>(transferAllTrackerFunction, blockParameter);
		}

		public Task<string> TransferFromRequestAsync(TransferFromFunction transferFromFunction)
		{
			return ContractHandler.SendRequestAsync(transferFromFunction);
		}

		public Task<TransactionReceipt> TransferFromRequestAndWaitForReceiptAsync(TransferFromFunction transferFromFunction, CancellationTokenSource cancellationToken = null)
		{
			return ContractHandler.SendRequestAndWaitForReceiptAsync(transferFromFunction, cancellationToken);
		}

		public Task<string> TransferFromRequestAsync(string from, string to, BigInteger amount)
		{
			var transferFromFunction = new TransferFromFunction();
			transferFromFunction.From = from;
			transferFromFunction.To = to;
			transferFromFunction.Amount = amount;

			return ContractHandler.SendRequestAsync(transferFromFunction);
		}

		public Task<TransactionReceipt> TransferFromRequestAndWaitForReceiptAsync(string from, string to, BigInteger amount, CancellationTokenSource cancellationToken = null)
		{
			var transferFromFunction = new TransferFromFunction();
			transferFromFunction.From = from;
			transferFromFunction.To = to;
			transferFromFunction.Amount = amount;

			return ContractHandler.SendRequestAndWaitForReceiptAsync(transferFromFunction, cancellationToken);
		}

		public Task<string> TransferOwnershipRequestAsync(TransferOwnershipFunction transferOwnershipFunction)
		{
			return ContractHandler.SendRequestAsync(transferOwnershipFunction);
		}

		public Task<TransactionReceipt> TransferOwnershipRequestAndWaitForReceiptAsync(TransferOwnershipFunction transferOwnershipFunction, CancellationTokenSource cancellationToken = null)
		{
			return ContractHandler.SendRequestAndWaitForReceiptAsync(transferOwnershipFunction, cancellationToken);
		}

		public Task<string> TransferOwnershipRequestAsync(string newOwner)
		{
			var transferOwnershipFunction = new TransferOwnershipFunction();
			transferOwnershipFunction.NewOwner = newOwner;

			return ContractHandler.SendRequestAsync(transferOwnershipFunction);
		}

		public Task<TransactionReceipt> TransferOwnershipRequestAndWaitForReceiptAsync(string newOwner, CancellationTokenSource cancellationToken = null)
		{
			var transferOwnershipFunction = new TransferOwnershipFunction();
			transferOwnershipFunction.NewOwner = newOwner;

			return ContractHandler.SendRequestAndWaitForReceiptAsync(transferOwnershipFunction, cancellationToken);
		}

		public Task<string> UnlockRequestAsync(UnlockFunction unlockFunction)
		{
			return ContractHandler.SendRequestAsync(unlockFunction);
		}

		public Task<string> UnlockRequestAsync()
		{
			return ContractHandler.SendRequestAsync<UnlockFunction>();
		}

		public Task<TransactionReceipt> UnlockRequestAndWaitForReceiptAsync(UnlockFunction unlockFunction, CancellationTokenSource cancellationToken = null)
		{
			return ContractHandler.SendRequestAndWaitForReceiptAsync(unlockFunction, cancellationToken);
		}

		public Task<TransactionReceipt> UnlockRequestAndWaitForReceiptAsync(CancellationTokenSource cancellationToken = null)
		{
			return ContractHandler.SendRequestAndWaitForReceiptAsync<UnlockFunction>(null, cancellationToken);
		}

		public Task<string> UnlockForUserRequestAsync(UnlockForUserFunction unlockForUserFunction)
		{
			return ContractHandler.SendRequestAsync(unlockForUserFunction);
		}

		public Task<TransactionReceipt> UnlockForUserRequestAndWaitForReceiptAsync(UnlockForUserFunction unlockForUserFunction, CancellationTokenSource cancellationToken = null)
		{
			return ContractHandler.SendRequestAndWaitForReceiptAsync(unlockForUserFunction, cancellationToken);
		}

		public Task<string> UnlockForUserRequestAsync(string account, BigInteger amount)
		{
			var unlockForUserFunction = new UnlockForUserFunction();
			unlockForUserFunction.Account = account;
			unlockForUserFunction.Amount = amount;

			return ContractHandler.SendRequestAsync(unlockForUserFunction);
		}

		public Task<TransactionReceipt> UnlockForUserRequestAndWaitForReceiptAsync(string account, BigInteger amount, CancellationTokenSource cancellationToken = null)
		{
			var unlockForUserFunction = new UnlockForUserFunction();
			unlockForUserFunction.Account = account;
			unlockForUserFunction.Amount = amount;

			return ContractHandler.SendRequestAndWaitForReceiptAsync(unlockForUserFunction, cancellationToken);
		}

		public Task<BigInteger> UnlockedSupplyQueryAsync(UnlockedSupplyFunction unlockedSupplyFunction, BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<UnlockedSupplyFunction, BigInteger>(unlockedSupplyFunction, blockParameter);
		}


		public Task<BigInteger> UnlockedSupplyQueryAsync(BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<UnlockedSupplyFunction, BigInteger>(null, blockParameter);
		}

		public Task<string> UpdateMaxTransferAmountRateRequestAsync(UpdateMaxTransferAmountRateFunction updateMaxTransferAmountRateFunction)
		{
			return ContractHandler.SendRequestAsync(updateMaxTransferAmountRateFunction);
		}

		public Task<TransactionReceipt> UpdateMaxTransferAmountRateRequestAndWaitForReceiptAsync(UpdateMaxTransferAmountRateFunction updateMaxTransferAmountRateFunction, CancellationTokenSource cancellationToken = null)
		{
			return ContractHandler.SendRequestAndWaitForReceiptAsync(updateMaxTransferAmountRateFunction, cancellationToken);
		}

		public Task<string> UpdateMaxTransferAmountRateRequestAsync(ushort maxTransferAmountRate)
		{
			var updateMaxTransferAmountRateFunction = new UpdateMaxTransferAmountRateFunction();
			updateMaxTransferAmountRateFunction.MaxTransferAmountRate = maxTransferAmountRate;

			return ContractHandler.SendRequestAsync(updateMaxTransferAmountRateFunction);
		}

		public Task<TransactionReceipt> UpdateMaxTransferAmountRateRequestAndWaitForReceiptAsync(ushort maxTransferAmountRate, CancellationTokenSource cancellationToken = null)
		{
			var updateMaxTransferAmountRateFunction = new UpdateMaxTransferAmountRateFunction();
			updateMaxTransferAmountRateFunction.MaxTransferAmountRate = maxTransferAmountRate;

			return ContractHandler.SendRequestAndWaitForReceiptAsync(updateMaxTransferAmountRateFunction, cancellationToken);
		}
	}
}
