using Nethereum.Contracts.ContractHandlers;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;
using PirateQuester.HeroSale.ContractDefinition;
using System.Numerics;

namespace PirateQuester.HeroSale
{
	public partial class HeroSaleService
	{
		public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Web3 web3, HeroSaleDeployment heroSaleDeployment, CancellationTokenSource cancellationTokenSource = null)
		{
			return web3.Eth.GetContractDeploymentHandler<HeroSaleDeployment>().SendRequestAndWaitForReceiptAsync(heroSaleDeployment, cancellationTokenSource);
		}

		public static Task<string> DeployContractAsync(Web3 web3, HeroSaleDeployment heroSaleDeployment)
		{
			return web3.Eth.GetContractDeploymentHandler<HeroSaleDeployment>().SendRequestAsync(heroSaleDeployment);
		}

		public static async Task<HeroSaleService> DeployContractAndGetServiceAsync(Web3 web3, HeroSaleDeployment heroSaleDeployment, CancellationTokenSource cancellationTokenSource = null)
		{
			var receipt = await DeployContractAndWaitForReceiptAsync(web3, heroSaleDeployment, cancellationTokenSource);
			return new HeroSaleService(web3, receipt.ContractAddress);
		}

		protected Web3 Web3 { get; }

		public ContractHandler ContractHandler { get; }

		public HeroSaleService(Web3 web3, string contractAddress)
		{
			Web3 = web3;
			ContractHandler = web3.Eth.GetContractHandler(contractAddress);
		}

		public Task<byte[]> BidderRoleQueryAsync(BidderRoleFunction bidderRoleFunction, BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<BidderRoleFunction, byte[]>(bidderRoleFunction, blockParameter);
		}


		public Task<byte[]> BidderRoleQueryAsync(BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<BidderRoleFunction, byte[]>(null, blockParameter);
		}

		public Task<byte[]> DefaultAdminRoleQueryAsync(DefaultAdminRoleFunction defaultAdminRoleFunction, BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<DefaultAdminRoleFunction, byte[]>(defaultAdminRoleFunction, blockParameter);
		}


		public Task<byte[]> DefaultAdminRoleQueryAsync(BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<DefaultAdminRoleFunction, byte[]>(null, blockParameter);
		}

		public Task<string> Erc721QueryAsync(Erc721Function erc721Function, BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<Erc721Function, string>(erc721Function, blockParameter);
		}


		public Task<string> Erc721QueryAsync(BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<Erc721Function, string>(null, blockParameter);
		}

		public Task<byte[]> ModeratorRoleQueryAsync(ModeratorRoleFunction moderatorRoleFunction, BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<ModeratorRoleFunction, byte[]>(moderatorRoleFunction, blockParameter);
		}


		public Task<byte[]> ModeratorRoleQueryAsync(BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<ModeratorRoleFunction, byte[]>(null, blockParameter);
		}

		public Task<string> AssistingAuctionQueryAsync(AssistingAuctionFunction assistingAuctionFunction, BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<AssistingAuctionFunction, string>(assistingAuctionFunction, blockParameter);
		}


		public Task<string> AssistingAuctionQueryAsync(BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<AssistingAuctionFunction, string>(null, blockParameter);
		}

		public Task<BigInteger> AuctionIdOffsetQueryAsync(AuctionIdOffsetFunction auctionIdOffsetFunction, BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<AuctionIdOffsetFunction, BigInteger>(auctionIdOffsetFunction, blockParameter);
		}


		public Task<BigInteger> AuctionIdOffsetQueryAsync(BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<AuctionIdOffsetFunction, BigInteger>(null, blockParameter);
		}

		public Task<AuctionsOutputDTO> AuctionsQueryAsync(AuctionsFunction auctionsFunction, BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryDeserializingToObjectAsync<AuctionsFunction, AuctionsOutputDTO>(auctionsFunction, blockParameter);
		}

		public Task<AuctionsOutputDTO> AuctionsQueryAsync(BigInteger returnValue1, BlockParameter blockParameter = null)
		{
			var auctionsFunction = new AuctionsFunction();
			auctionsFunction.ReturnValue1 = returnValue1;

			return ContractHandler.QueryDeserializingToObjectAsync<AuctionsFunction, AuctionsOutputDTO>(auctionsFunction, blockParameter);
		}

		public Task<string> BidRequestAsync(BidFunction bidFunction)
		{
			return ContractHandler.SendRequestAsync(bidFunction);
		}

		public Task<TransactionReceipt> BidRequestAndWaitForReceiptAsync(BidFunction bidFunction, CancellationTokenSource cancellationToken = null)
		{
			return ContractHandler.SendRequestAndWaitForReceiptAsync(bidFunction, cancellationToken);
		}

		public Task<string> BidRequestAsync(BigInteger tokenId, BigInteger bidAmount)
		{
			var bidFunction = new BidFunction();
			bidFunction.TokenId = tokenId;
			bidFunction.BidAmount = bidAmount;

			return ContractHandler.SendRequestAsync(bidFunction);
		}

		public Task<TransactionReceipt> BidRequestAndWaitForReceiptAsync(BigInteger tokenId, BigInteger bidAmount, CancellationTokenSource cancellationToken = null)
		{
			var bidFunction = new BidFunction();
			bidFunction.TokenId = tokenId;
			bidFunction.BidAmount = bidAmount;

			return ContractHandler.SendRequestAndWaitForReceiptAsync(bidFunction, cancellationToken);
		}

		public Task<string> BidForRequestAsync(BidForFunction bidForFunction)
		{
			return ContractHandler.SendRequestAsync(bidForFunction);
		}

		public Task<TransactionReceipt> BidForRequestAndWaitForReceiptAsync(BidForFunction bidForFunction, CancellationTokenSource cancellationToken = null)
		{
			return ContractHandler.SendRequestAndWaitForReceiptAsync(bidForFunction, cancellationToken);
		}

		public Task<string> BidForRequestAsync(string bidder, BigInteger tokenId, BigInteger bidAmount)
		{
			var bidForFunction = new BidForFunction();
			bidForFunction.Bidder = bidder;
			bidForFunction.TokenId = tokenId;
			bidForFunction.BidAmount = bidAmount;

			return ContractHandler.SendRequestAsync(bidForFunction);
		}

		public Task<TransactionReceipt> BidForRequestAndWaitForReceiptAsync(string bidder, BigInteger tokenId, BigInteger bidAmount, CancellationTokenSource cancellationToken = null)
		{
			var bidForFunction = new BidForFunction();
			bidForFunction.Bidder = bidder;
			bidForFunction.TokenId = tokenId;
			bidForFunction.BidAmount = bidAmount;

			return ContractHandler.SendRequestAndWaitForReceiptAsync(bidForFunction, cancellationToken);
		}

		public Task<string> CancelAuctionRequestAsync(CancelAuctionFunction cancelAuctionFunction)
		{
			return ContractHandler.SendRequestAsync(cancelAuctionFunction);
		}

		public Task<TransactionReceipt> CancelAuctionRequestAndWaitForReceiptAsync(CancelAuctionFunction cancelAuctionFunction, CancellationTokenSource cancellationToken = null)
		{
			return ContractHandler.SendRequestAndWaitForReceiptAsync(cancelAuctionFunction, cancellationToken);
		}

		public Task<string> CancelAuctionRequestAsync(BigInteger tokenId)
		{
			var cancelAuctionFunction = new CancelAuctionFunction();
			cancelAuctionFunction.TokenId = tokenId;

			return ContractHandler.SendRequestAsync(cancelAuctionFunction);
		}

		public Task<TransactionReceipt> CancelAuctionRequestAndWaitForReceiptAsync(BigInteger tokenId, CancellationTokenSource cancellationToken = null)
		{
			var cancelAuctionFunction = new CancelAuctionFunction();
			cancelAuctionFunction.TokenId = tokenId;

			return ContractHandler.SendRequestAndWaitForReceiptAsync(cancelAuctionFunction, cancellationToken);
		}

		public Task<string> CancelAuctionWhenPausedRequestAsync(CancelAuctionWhenPausedFunction cancelAuctionWhenPausedFunction)
		{
			return ContractHandler.SendRequestAsync(cancelAuctionWhenPausedFunction);
		}

		public Task<TransactionReceipt> CancelAuctionWhenPausedRequestAndWaitForReceiptAsync(CancelAuctionWhenPausedFunction cancelAuctionWhenPausedFunction, CancellationTokenSource cancellationToken = null)
		{
			return ContractHandler.SendRequestAndWaitForReceiptAsync(cancelAuctionWhenPausedFunction, cancellationToken);
		}

		public Task<string> CancelAuctionWhenPausedRequestAsync(BigInteger tokenId)
		{
			var cancelAuctionWhenPausedFunction = new CancelAuctionWhenPausedFunction();
			cancelAuctionWhenPausedFunction.TokenId = tokenId;

			return ContractHandler.SendRequestAsync(cancelAuctionWhenPausedFunction);
		}

		public Task<TransactionReceipt> CancelAuctionWhenPausedRequestAndWaitForReceiptAsync(BigInteger tokenId, CancellationTokenSource cancellationToken = null)
		{
			var cancelAuctionWhenPausedFunction = new CancelAuctionWhenPausedFunction();
			cancelAuctionWhenPausedFunction.TokenId = tokenId;

			return ContractHandler.SendRequestAndWaitForReceiptAsync(cancelAuctionWhenPausedFunction, cancellationToken);
		}

		public Task<string> CreateAuctionRequestAsync(CreateAuctionFunction createAuctionFunction)
		{
			return ContractHandler.SendRequestAsync(createAuctionFunction);
		}

		public Task<TransactionReceipt> CreateAuctionRequestAndWaitForReceiptAsync(CreateAuctionFunction createAuctionFunction, CancellationTokenSource cancellationToken = null)
		{
			return ContractHandler.SendRequestAndWaitForReceiptAsync(createAuctionFunction, cancellationToken);
		}

		public Task<string> CreateAuctionRequestAsync(BigInteger tokenId, BigInteger startingPrice, BigInteger endingPrice, ulong duration, string winner)
		{
			var createAuctionFunction = new CreateAuctionFunction();
			createAuctionFunction.TokenId = tokenId;
			createAuctionFunction.StartingPrice = startingPrice;
			createAuctionFunction.EndingPrice = endingPrice;
			createAuctionFunction.Duration = duration;
			createAuctionFunction.Winner = winner;

			return ContractHandler.SendRequestAsync(createAuctionFunction);
		}

		public Task<TransactionReceipt> CreateAuctionRequestAndWaitForReceiptAsync(BigInteger tokenId, BigInteger startingPrice, BigInteger endingPrice, ulong duration, string winner, CancellationTokenSource cancellationToken = null)
		{
			var createAuctionFunction = new CreateAuctionFunction();
			createAuctionFunction.TokenId = tokenId;
			createAuctionFunction.StartingPrice = startingPrice;
			createAuctionFunction.EndingPrice = endingPrice;
			createAuctionFunction.Duration = duration;
			createAuctionFunction.Winner = winner;

			return ContractHandler.SendRequestAndWaitForReceiptAsync(createAuctionFunction, cancellationToken);
		}

		public Task<string> FeeAddressesQueryAsync(FeeAddressesFunction feeAddressesFunction, BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<FeeAddressesFunction, string>(feeAddressesFunction, blockParameter);
		}


		public Task<string> FeeAddressesQueryAsync(BigInteger returnValue1, BlockParameter blockParameter = null)
		{
			var feeAddressesFunction = new FeeAddressesFunction();
			feeAddressesFunction.ReturnValue1 = returnValue1;

			return ContractHandler.QueryAsync<FeeAddressesFunction, string>(feeAddressesFunction, blockParameter);
		}

		public Task<BigInteger> FeePercentsQueryAsync(FeePercentsFunction feePercentsFunction, BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<FeePercentsFunction, BigInteger>(feePercentsFunction, blockParameter);
		}


		public Task<BigInteger> FeePercentsQueryAsync(BigInteger returnValue1, BlockParameter blockParameter = null)
		{
			var feePercentsFunction = new FeePercentsFunction();
			feePercentsFunction.ReturnValue1 = returnValue1;

			return ContractHandler.QueryAsync<FeePercentsFunction, BigInteger>(feePercentsFunction, blockParameter);
		}

		public Task<GetAuctionOutputDTO> GetAuctionQueryAsync(GetAuctionFunction getAuctionFunction, BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryDeserializingToObjectAsync<GetAuctionFunction, GetAuctionOutputDTO>(getAuctionFunction, blockParameter);
		}

		public Task<GetAuctionOutputDTO> GetAuctionQueryAsync(BigInteger tokenId, BlockParameter blockParameter = null)
		{
			var getAuctionFunction = new GetAuctionFunction();
			getAuctionFunction.TokenId = tokenId;

			return ContractHandler.QueryDeserializingToObjectAsync<GetAuctionFunction, GetAuctionOutputDTO>(getAuctionFunction, blockParameter);
		}

		public Task<GetAuctionsOutputDTO> GetAuctionsQueryAsync(GetAuctionsFunction getAuctionsFunction, BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryDeserializingToObjectAsync<GetAuctionsFunction, GetAuctionsOutputDTO>(getAuctionsFunction, blockParameter);
		}

		public Task<GetAuctionsOutputDTO> GetAuctionsQueryAsync(List<BigInteger> tokenIds, BlockParameter blockParameter = null)
		{
			var getAuctionsFunction = new GetAuctionsFunction();
			getAuctionsFunction.TokenIds = tokenIds;

			return ContractHandler.QueryDeserializingToObjectAsync<GetAuctionsFunction, GetAuctionsOutputDTO>(getAuctionsFunction, blockParameter);
		}

		public Task<BigInteger> GetCurrentPriceQueryAsync(GetCurrentPriceFunction getCurrentPriceFunction, BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<GetCurrentPriceFunction, BigInteger>(getCurrentPriceFunction, blockParameter);
		}


		public Task<BigInteger> GetCurrentPriceQueryAsync(BigInteger tokenId, BlockParameter blockParameter = null)
		{
			var getCurrentPriceFunction = new GetCurrentPriceFunction();
			getCurrentPriceFunction.TokenId = tokenId;

			return ContractHandler.QueryAsync<GetCurrentPriceFunction, BigInteger>(getCurrentPriceFunction, blockParameter);
		}

		public Task<byte[]> GetRoleAdminQueryAsync(GetRoleAdminFunction getRoleAdminFunction, BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<GetRoleAdminFunction, byte[]>(getRoleAdminFunction, blockParameter);
		}


		public Task<byte[]> GetRoleAdminQueryAsync(byte[] role, BlockParameter blockParameter = null)
		{
			var getRoleAdminFunction = new GetRoleAdminFunction();
			getRoleAdminFunction.Role = role;

			return ContractHandler.QueryAsync<GetRoleAdminFunction, byte[]>(getRoleAdminFunction, blockParameter);
		}

		public Task<List<BigInteger>> GetUserAuctionsQueryAsync(GetUserAuctionsFunction getUserAuctionsFunction, BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<GetUserAuctionsFunction, List<BigInteger>>(getUserAuctionsFunction, blockParameter);
		}


		public Task<List<BigInteger>> GetUserAuctionsQueryAsync(string address, BlockParameter blockParameter = null)
		{
			var getUserAuctionsFunction = new GetUserAuctionsFunction();
			getUserAuctionsFunction.Address = address;

			return ContractHandler.QueryAsync<GetUserAuctionsFunction, List<BigInteger>>(getUserAuctionsFunction, blockParameter);
		}

		public Task<string> GrantRoleRequestAsync(GrantRoleFunction grantRoleFunction)
		{
			return ContractHandler.SendRequestAsync(grantRoleFunction);
		}

		public Task<TransactionReceipt> GrantRoleRequestAndWaitForReceiptAsync(GrantRoleFunction grantRoleFunction, CancellationTokenSource cancellationToken = null)
		{
			return ContractHandler.SendRequestAndWaitForReceiptAsync(grantRoleFunction, cancellationToken);
		}

		public Task<string> GrantRoleRequestAsync(byte[] role, string account)
		{
			var grantRoleFunction = new GrantRoleFunction();
			grantRoleFunction.Role = role;
			grantRoleFunction.Account = account;

			return ContractHandler.SendRequestAsync(grantRoleFunction);
		}

		public Task<TransactionReceipt> GrantRoleRequestAndWaitForReceiptAsync(byte[] role, string account, CancellationTokenSource cancellationToken = null)
		{
			var grantRoleFunction = new GrantRoleFunction();
			grantRoleFunction.Role = role;
			grantRoleFunction.Account = account;

			return ContractHandler.SendRequestAndWaitForReceiptAsync(grantRoleFunction, cancellationToken);
		}

		public Task<bool> HasRoleQueryAsync(HasRoleFunction hasRoleFunction, BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<HasRoleFunction, bool>(hasRoleFunction, blockParameter);
		}


		public Task<bool> HasRoleQueryAsync(byte[] role, string account, BlockParameter blockParameter = null)
		{
			var hasRoleFunction = new HasRoleFunction();
			hasRoleFunction.Role = role;
			hasRoleFunction.Account = account;

			return ContractHandler.QueryAsync<HasRoleFunction, bool>(hasRoleFunction, blockParameter);
		}

		public Task<string> InitializeRequestAsync(InitializeFunction initializeFunction)
		{
			return ContractHandler.SendRequestAsync(initializeFunction);
		}

		public Task<TransactionReceipt> InitializeRequestAndWaitForReceiptAsync(InitializeFunction initializeFunction, CancellationTokenSource cancellationToken = null)
		{
			return ContractHandler.SendRequestAndWaitForReceiptAsync(initializeFunction, cancellationToken);
		}

		public Task<string> InitializeRequestAsync(string heroCoreAddress, string crystalAddress, BigInteger cut, string assistingAuctionAddress, BigInteger auctionIdOffset)
		{
			var initializeFunction = new InitializeFunction();
			initializeFunction.HeroCoreAddress = heroCoreAddress;
			initializeFunction.CrystalAddress = crystalAddress;
			initializeFunction.Cut = cut;
			initializeFunction.AssistingAuctionAddress = assistingAuctionAddress;
			initializeFunction.AuctionIdOffset = auctionIdOffset;

			return ContractHandler.SendRequestAsync(initializeFunction);
		}

		public Task<TransactionReceipt> InitializeRequestAndWaitForReceiptAsync(string heroCoreAddress, string crystalAddress, BigInteger cut, string assistingAuctionAddress, BigInteger auctionIdOffset, CancellationTokenSource cancellationToken = null)
		{
			var initializeFunction = new InitializeFunction();
			initializeFunction.HeroCoreAddress = heroCoreAddress;
			initializeFunction.CrystalAddress = crystalAddress;
			initializeFunction.Cut = cut;
			initializeFunction.AssistingAuctionAddress = assistingAuctionAddress;
			initializeFunction.AuctionIdOffset = auctionIdOffset;

			return ContractHandler.SendRequestAndWaitForReceiptAsync(initializeFunction, cancellationToken);
		}

		public Task<bool> IsOnAuctionQueryAsync(IsOnAuctionFunction isOnAuctionFunction, BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<IsOnAuctionFunction, bool>(isOnAuctionFunction, blockParameter);
		}


		public Task<bool> IsOnAuctionQueryAsync(BigInteger tokenId, BlockParameter blockParameter = null)
		{
			var isOnAuctionFunction = new IsOnAuctionFunction();
			isOnAuctionFunction.TokenId = tokenId;

			return ContractHandler.QueryAsync<IsOnAuctionFunction, bool>(isOnAuctionFunction, blockParameter);
		}

		public Task<byte[]> OnERC721ReceivedQueryAsync(OnERC721ReceivedFunction onERC721ReceivedFunction, BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<OnERC721ReceivedFunction, byte[]>(onERC721ReceivedFunction, blockParameter);
		}


		public Task<byte[]> OnERC721ReceivedQueryAsync(string returnValue1, string returnValue2, BigInteger returnValue3, byte[] returnValue4, BlockParameter blockParameter = null)
		{
			var onERC721ReceivedFunction = new OnERC721ReceivedFunction();
			onERC721ReceivedFunction.ReturnValue1 = returnValue1;
			onERC721ReceivedFunction.ReturnValue2 = returnValue2;
			onERC721ReceivedFunction.ReturnValue3 = returnValue3;
			onERC721ReceivedFunction.ReturnValue4 = returnValue4;

			return ContractHandler.QueryAsync<OnERC721ReceivedFunction, byte[]>(onERC721ReceivedFunction, blockParameter);
		}

		public Task<BigInteger> OwnerCutQueryAsync(OwnerCutFunction ownerCutFunction, BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<OwnerCutFunction, BigInteger>(ownerCutFunction, blockParameter);
		}


		public Task<BigInteger> OwnerCutQueryAsync(BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<OwnerCutFunction, BigInteger>(null, blockParameter);
		}

		public Task<string> PauseRequestAsync(PauseFunction pauseFunction)
		{
			return ContractHandler.SendRequestAsync(pauseFunction);
		}

		public Task<string> PauseRequestAsync()
		{
			return ContractHandler.SendRequestAsync<PauseFunction>();
		}

		public Task<TransactionReceipt> PauseRequestAndWaitForReceiptAsync(PauseFunction pauseFunction, CancellationTokenSource cancellationToken = null)
		{
			return ContractHandler.SendRequestAndWaitForReceiptAsync(pauseFunction, cancellationToken);
		}

		public Task<TransactionReceipt> PauseRequestAndWaitForReceiptAsync(CancellationTokenSource cancellationToken = null)
		{
			return ContractHandler.SendRequestAndWaitForReceiptAsync<PauseFunction>(null, cancellationToken);
		}

		public Task<bool> PausedQueryAsync(PausedFunction pausedFunction, BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<PausedFunction, bool>(pausedFunction, blockParameter);
		}


		public Task<bool> PausedQueryAsync(BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<PausedFunction, bool>(null, blockParameter);
		}

		public Task<string> PowerTokenQueryAsync(PowerTokenFunction powerTokenFunction, BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<PowerTokenFunction, string>(powerTokenFunction, blockParameter);
		}


		public Task<string> PowerTokenQueryAsync(BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<PowerTokenFunction, string>(null, blockParameter);
		}

		public Task<string> RenounceRoleRequestAsync(RenounceRoleFunction renounceRoleFunction)
		{
			return ContractHandler.SendRequestAsync(renounceRoleFunction);
		}

		public Task<TransactionReceipt> RenounceRoleRequestAndWaitForReceiptAsync(RenounceRoleFunction renounceRoleFunction, CancellationTokenSource cancellationToken = null)
		{
			return ContractHandler.SendRequestAndWaitForReceiptAsync(renounceRoleFunction, cancellationToken);
		}

		public Task<string> RenounceRoleRequestAsync(byte[] role, string account)
		{
			var renounceRoleFunction = new RenounceRoleFunction();
			renounceRoleFunction.Role = role;
			renounceRoleFunction.Account = account;

			return ContractHandler.SendRequestAsync(renounceRoleFunction);
		}

		public Task<TransactionReceipt> RenounceRoleRequestAndWaitForReceiptAsync(byte[] role, string account, CancellationTokenSource cancellationToken = null)
		{
			var renounceRoleFunction = new RenounceRoleFunction();
			renounceRoleFunction.Role = role;
			renounceRoleFunction.Account = account;

			return ContractHandler.SendRequestAndWaitForReceiptAsync(renounceRoleFunction, cancellationToken);
		}

		public Task<string> RevokeRoleRequestAsync(RevokeRoleFunction revokeRoleFunction)
		{
			return ContractHandler.SendRequestAsync(revokeRoleFunction);
		}

		public Task<TransactionReceipt> RevokeRoleRequestAndWaitForReceiptAsync(RevokeRoleFunction revokeRoleFunction, CancellationTokenSource cancellationToken = null)
		{
			return ContractHandler.SendRequestAndWaitForReceiptAsync(revokeRoleFunction, cancellationToken);
		}

		public Task<string> RevokeRoleRequestAsync(byte[] role, string account)
		{
			var revokeRoleFunction = new RevokeRoleFunction();
			revokeRoleFunction.Role = role;
			revokeRoleFunction.Account = account;

			return ContractHandler.SendRequestAsync(revokeRoleFunction);
		}

		public Task<TransactionReceipt> RevokeRoleRequestAndWaitForReceiptAsync(byte[] role, string account, CancellationTokenSource cancellationToken = null)
		{
			var revokeRoleFunction = new RevokeRoleFunction();
			revokeRoleFunction.Role = role;
			revokeRoleFunction.Account = account;

			return ContractHandler.SendRequestAndWaitForReceiptAsync(revokeRoleFunction, cancellationToken);
		}

		public Task<string> SetAuctionIDOffsetRequestAsync(SetAuctionIDOffsetFunction setAuctionIDOffsetFunction)
		{
			return ContractHandler.SendRequestAsync(setAuctionIDOffsetFunction);
		}

		public Task<TransactionReceipt> SetAuctionIDOffsetRequestAndWaitForReceiptAsync(SetAuctionIDOffsetFunction setAuctionIDOffsetFunction, CancellationTokenSource cancellationToken = null)
		{
			return ContractHandler.SendRequestAndWaitForReceiptAsync(setAuctionIDOffsetFunction, cancellationToken);
		}

		public Task<string> SetAuctionIDOffsetRequestAsync(BigInteger auctionIdOffset)
		{
			var setAuctionIDOffsetFunction = new SetAuctionIDOffsetFunction();
			setAuctionIDOffsetFunction.AuctionIdOffset = auctionIdOffset;

			return ContractHandler.SendRequestAsync(setAuctionIDOffsetFunction);
		}

		public Task<TransactionReceipt> SetAuctionIDOffsetRequestAndWaitForReceiptAsync(BigInteger auctionIdOffset, CancellationTokenSource cancellationToken = null)
		{
			var setAuctionIDOffsetFunction = new SetAuctionIDOffsetFunction();
			setAuctionIDOffsetFunction.AuctionIdOffset = auctionIdOffset;

			return ContractHandler.SendRequestAndWaitForReceiptAsync(setAuctionIDOffsetFunction, cancellationToken);
		}

		public Task<string> SetERC721RequestAsync(SetERC721Function setERC721Function)
		{
			return ContractHandler.SendRequestAsync(setERC721Function);
		}

		public Task<TransactionReceipt> SetERC721RequestAndWaitForReceiptAsync(SetERC721Function setERC721Function, CancellationTokenSource cancellationToken = null)
		{
			return ContractHandler.SendRequestAndWaitForReceiptAsync(setERC721Function, cancellationToken);
		}

		public Task<string> SetERC721RequestAsync(string newERC721Address)
		{
			var setERC721Function = new SetERC721Function();
			setERC721Function.NewERC721Address = newERC721Address;

			return ContractHandler.SendRequestAsync(setERC721Function);
		}

		public Task<TransactionReceipt> SetERC721RequestAndWaitForReceiptAsync(string newERC721Address, CancellationTokenSource cancellationToken = null)
		{
			var setERC721Function = new SetERC721Function();
			setERC721Function.NewERC721Address = newERC721Address;

			return ContractHandler.SendRequestAndWaitForReceiptAsync(setERC721Function, cancellationToken);
		}

		public Task<string> SetFeesRequestAsync(SetFeesFunction setFeesFunction)
		{
			return ContractHandler.SendRequestAsync(setFeesFunction);
		}

		public Task<TransactionReceipt> SetFeesRequestAndWaitForReceiptAsync(SetFeesFunction setFeesFunction, CancellationTokenSource cancellationToken = null)
		{
			return ContractHandler.SendRequestAndWaitForReceiptAsync(setFeesFunction, cancellationToken);
		}

		public Task<string> SetFeesRequestAsync(List<string> feeAddresses, List<BigInteger> feePercents)
		{
			var setFeesFunction = new SetFeesFunction();
			setFeesFunction.FeeAddresses = feeAddresses;
			setFeesFunction.FeePercents = feePercents;

			return ContractHandler.SendRequestAsync(setFeesFunction);
		}

		public Task<TransactionReceipt> SetFeesRequestAndWaitForReceiptAsync(List<string> feeAddresses, List<BigInteger> feePercents, CancellationTokenSource cancellationToken = null)
		{
			var setFeesFunction = new SetFeesFunction();
			setFeesFunction.FeeAddresses = feeAddresses;
			setFeesFunction.FeePercents = feePercents;

			return ContractHandler.SendRequestAndWaitForReceiptAsync(setFeesFunction, cancellationToken);
		}

		public Task<bool> SupportsInterfaceQueryAsync(SupportsInterfaceFunction supportsInterfaceFunction, BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<SupportsInterfaceFunction, bool>(supportsInterfaceFunction, blockParameter);
		}


		public Task<bool> SupportsInterfaceQueryAsync(byte[] interfaceId, BlockParameter blockParameter = null)
		{
			var supportsInterfaceFunction = new SupportsInterfaceFunction();
			supportsInterfaceFunction.InterfaceId = interfaceId;

			return ContractHandler.QueryAsync<SupportsInterfaceFunction, bool>(supportsInterfaceFunction, blockParameter);
		}

		public Task<BigInteger> TotalAuctionsQueryAsync(TotalAuctionsFunction totalAuctionsFunction, BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<TotalAuctionsFunction, BigInteger>(totalAuctionsFunction, blockParameter);
		}


		public Task<BigInteger> TotalAuctionsQueryAsync(BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<TotalAuctionsFunction, BigInteger>(null, blockParameter);
		}

		public Task<string> UnpauseRequestAsync(UnpauseFunction unpauseFunction)
		{
			return ContractHandler.SendRequestAsync(unpauseFunction);
		}

		public Task<string> UnpauseRequestAsync()
		{
			return ContractHandler.SendRequestAsync<UnpauseFunction>();
		}

		public Task<TransactionReceipt> UnpauseRequestAndWaitForReceiptAsync(UnpauseFunction unpauseFunction, CancellationTokenSource cancellationToken = null)
		{
			return ContractHandler.SendRequestAndWaitForReceiptAsync(unpauseFunction, cancellationToken);
		}

		public Task<TransactionReceipt> UnpauseRequestAndWaitForReceiptAsync(CancellationTokenSource cancellationToken = null)
		{
			return ContractHandler.SendRequestAndWaitForReceiptAsync<UnpauseFunction>(null, cancellationToken);
		}

		public Task<BigInteger> UserAuctionsQueryAsync(UserAuctionsFunction userAuctionsFunction, BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<UserAuctionsFunction, BigInteger>(userAuctionsFunction, blockParameter);
		}


		public Task<BigInteger> UserAuctionsQueryAsync(string returnValue1, BigInteger returnValue2, BlockParameter blockParameter = null)
		{
			var userAuctionsFunction = new UserAuctionsFunction();
			userAuctionsFunction.ReturnValue1 = returnValue1;
			userAuctionsFunction.ReturnValue2 = returnValue2;

			return ContractHandler.QueryAsync<UserAuctionsFunction, BigInteger>(userAuctionsFunction, blockParameter);
		}
	}
}
