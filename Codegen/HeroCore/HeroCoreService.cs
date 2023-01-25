using DFKContracts.HeroCore.ContractDefinition;
using Nethereum.Contracts.ContractHandlers;
using Nethereum.RPC.Eth.DTOs;
using System.Numerics;

namespace DFKContracts.HeroCore;

public partial class HeroCoreService
{
	public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, HeroCoreDeployment heroCoreDeployment, CancellationTokenSource cancellationTokenSource = null)
	{
		return web3.Eth.GetContractDeploymentHandler<HeroCoreDeployment>().SendRequestAndWaitForReceiptAsync(heroCoreDeployment, cancellationTokenSource);
	}

	public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, HeroCoreDeployment heroCoreDeployment)
	{
		return web3.Eth.GetContractDeploymentHandler<HeroCoreDeployment>().SendRequestAsync(heroCoreDeployment);
	}

	public static async Task<HeroCoreService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, HeroCoreDeployment heroCoreDeployment, CancellationTokenSource cancellationTokenSource = null)
	{
		var receipt = await DeployContractAndWaitForReceiptAsync(web3, heroCoreDeployment, cancellationTokenSource);
		return new HeroCoreService(web3, receipt.ContractAddress);
	}

	protected Nethereum.Web3.Web3 Web3 { get; }

	public ContractHandler ContractHandler { get; }

	public HeroCoreService(Nethereum.Web3.Web3 web3, string contractAddress)
	{
		Web3 = web3;
		ContractHandler = web3.Eth.GetContractHandler(contractAddress);
	}

	public Task<byte[]> BridgeRoleQueryAsync(BridgeRoleFunction bridgeRoleFunction, BlockParameter blockParameter = null)
	{
		return ContractHandler.QueryAsync<BridgeRoleFunction, byte[]>(bridgeRoleFunction, blockParameter);
	}


	public Task<byte[]> BridgeRoleQueryAsync(BlockParameter blockParameter = null)
	{
		return ContractHandler.QueryAsync<BridgeRoleFunction, byte[]>(null, blockParameter);
	}

	public Task<byte[]> DefaultAdminRoleQueryAsync(DefaultAdminRoleFunction defaultAdminRoleFunction, BlockParameter blockParameter = null)
	{
		return ContractHandler.QueryAsync<DefaultAdminRoleFunction, byte[]>(defaultAdminRoleFunction, blockParameter);
	}


	public Task<byte[]> DefaultAdminRoleQueryAsync(BlockParameter blockParameter = null)
	{
		return ContractHandler.QueryAsync<DefaultAdminRoleFunction, byte[]>(null, blockParameter);
	}

	public Task<byte[]> HeroModeratorRoleQueryAsync(HeroModeratorRoleFunction heroModeratorRoleFunction, BlockParameter blockParameter = null)
	{
		return ContractHandler.QueryAsync<HeroModeratorRoleFunction, byte[]>(heroModeratorRoleFunction, blockParameter);
	}


	public Task<byte[]> HeroModeratorRoleQueryAsync(BlockParameter blockParameter = null)
	{
		return ContractHandler.QueryAsync<HeroModeratorRoleFunction, byte[]>(null, blockParameter);
	}

	public Task<byte[]> MinterRoleQueryAsync(MinterRoleFunction minterRoleFunction, BlockParameter blockParameter = null)
	{
		return ContractHandler.QueryAsync<MinterRoleFunction, byte[]>(minterRoleFunction, blockParameter);
	}


	public Task<byte[]> MinterRoleQueryAsync(BlockParameter blockParameter = null)
	{
		return ContractHandler.QueryAsync<MinterRoleFunction, byte[]>(null, blockParameter);
	}

	public Task<byte[]> ModeratorRoleQueryAsync(ModeratorRoleFunction moderatorRoleFunction, BlockParameter blockParameter = null)
	{
		return ContractHandler.QueryAsync<ModeratorRoleFunction, byte[]>(moderatorRoleFunction, blockParameter);
	}


	public Task<byte[]> ModeratorRoleQueryAsync(BlockParameter blockParameter = null)
	{
		return ContractHandler.QueryAsync<ModeratorRoleFunction, byte[]>(null, blockParameter);
	}

	public Task<string> ApproveRequestAsync(ApproveFunction approveFunction)
	{
		return ContractHandler.SendRequestAsync(approveFunction);
	}

	public Task<TransactionReceipt> ApproveRequestAndWaitForReceiptAsync(ApproveFunction approveFunction, CancellationTokenSource cancellationToken = null)
	{
		return ContractHandler.SendRequestAndWaitForReceiptAsync(approveFunction, cancellationToken);
	}

	public Task<string> ApproveRequestAsync(string to, BigInteger tokenId)
	{
		var approveFunction = new ApproveFunction();
		approveFunction.To = to;
		approveFunction.TokenId = tokenId;

		return ContractHandler.SendRequestAsync(approveFunction);
	}

	public Task<TransactionReceipt> ApproveRequestAndWaitForReceiptAsync(string to, BigInteger tokenId, CancellationTokenSource cancellationToken = null)
	{
		var approveFunction = new ApproveFunction();
		approveFunction.To = to;
		approveFunction.TokenId = tokenId;

		return ContractHandler.SendRequestAndWaitForReceiptAsync(approveFunction, cancellationToken);
	}

	public Task<BigInteger> BalanceOfQueryAsync(BalanceOfFunction balanceOfFunction, BlockParameter blockParameter = null)
	{
		return ContractHandler.QueryAsync<BalanceOfFunction, BigInteger>(balanceOfFunction, blockParameter);
	}


	public Task<BigInteger> BalanceOfQueryAsync(string owner, BlockParameter blockParameter = null)
	{
		var balanceOfFunction = new BalanceOfFunction();
		balanceOfFunction.Owner = owner;

		return ContractHandler.QueryAsync<BalanceOfFunction, BigInteger>(balanceOfFunction, blockParameter);
	}

	public Task<string> BridgeMintRequestAsync(BridgeMintFunction bridgeMintFunction)
	{
		return ContractHandler.SendRequestAsync(bridgeMintFunction);
	}

	public Task<TransactionReceipt> BridgeMintRequestAndWaitForReceiptAsync(BridgeMintFunction bridgeMintFunction, CancellationTokenSource cancellationToken = null)
	{
		return ContractHandler.SendRequestAndWaitForReceiptAsync(bridgeMintFunction, cancellationToken);
	}

	public Task<string> BridgeMintRequestAsync(BigInteger id, string to)
	{
		var bridgeMintFunction = new BridgeMintFunction();
		bridgeMintFunction.Id = id;
		bridgeMintFunction.To = to;

		return ContractHandler.SendRequestAsync(bridgeMintFunction);
	}

	public Task<TransactionReceipt> BridgeMintRequestAndWaitForReceiptAsync(BigInteger id, string to, CancellationTokenSource cancellationToken = null)
	{
		var bridgeMintFunction = new BridgeMintFunction();
		bridgeMintFunction.Id = id;
		bridgeMintFunction.To = to;

		return ContractHandler.SendRequestAndWaitForReceiptAsync(bridgeMintFunction, cancellationToken);
	}

	public Task<string> CreateHeroRequestAsync(CreateHeroFunction createHeroFunction)
	{
		return ContractHandler.SendRequestAsync(createHeroFunction);
	}

	public Task<TransactionReceipt> CreateHeroRequestAndWaitForReceiptAsync(CreateHeroFunction createHeroFunction, CancellationTokenSource cancellationToken = null)
	{
		return ContractHandler.SendRequestAndWaitForReceiptAsync(createHeroFunction, cancellationToken);
	}

	public Task<string> CreateHeroRequestAsync(BigInteger statGenes, BigInteger visualGenes, byte rarity, bool shiny, HeroCrystal crystal, BigInteger crystalId)
	{
		var createHeroFunction = new CreateHeroFunction();
		createHeroFunction.StatGenes = statGenes;
		createHeroFunction.VisualGenes = visualGenes;
		createHeroFunction.Rarity = rarity;
		createHeroFunction.Shiny = shiny;
		createHeroFunction.Crystal = crystal;
		createHeroFunction.CrystalId = crystalId;

		return ContractHandler.SendRequestAsync(createHeroFunction);
	}

	public Task<TransactionReceipt> CreateHeroRequestAndWaitForReceiptAsync(BigInteger statGenes, BigInteger visualGenes, byte rarity, bool shiny, HeroCrystal crystal, BigInteger crystalId, CancellationTokenSource cancellationToken = null)
	{
		var createHeroFunction = new CreateHeroFunction();
		createHeroFunction.StatGenes = statGenes;
		createHeroFunction.VisualGenes = visualGenes;
		createHeroFunction.Rarity = rarity;
		createHeroFunction.Shiny = shiny;
		createHeroFunction.Crystal = crystal;
		createHeroFunction.CrystalId = crystalId;

		return ContractHandler.SendRequestAndWaitForReceiptAsync(createHeroFunction, cancellationToken);
	}

	public Task<string> GetApprovedQueryAsync(GetApprovedFunction getApprovedFunction, BlockParameter blockParameter = null)
	{
		return ContractHandler.QueryAsync<GetApprovedFunction, string>(getApprovedFunction, blockParameter);
	}


	public Task<string> GetApprovedQueryAsync(BigInteger tokenId, BlockParameter blockParameter = null)
	{
		var getApprovedFunction = new GetApprovedFunction();
		getApprovedFunction.TokenId = tokenId;

		return ContractHandler.QueryAsync<GetApprovedFunction, string>(getApprovedFunction, blockParameter);
	}

	public Task<GetHeroOutputDTO> GetHeroQueryAsync(GetHeroFunction getHeroFunction, BlockParameter blockParameter = null)
	{
		return ContractHandler.QueryDeserializingToObjectAsync<GetHeroFunction, GetHeroOutputDTO>(getHeroFunction, blockParameter);
	}

	public Task<GetHeroOutputDTO> GetHeroQueryAsync(BigInteger id, BlockParameter blockParameter = null)
	{
		var getHeroFunction = new GetHeroFunction();
		getHeroFunction.Id = id;

		return ContractHandler.QueryDeserializingToObjectAsync<GetHeroFunction, GetHeroOutputDTO>(getHeroFunction, blockParameter);
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

	public Task<List<BigInteger>> GetUserHeroesQueryAsync(GetUserHeroesFunction getUserHeroesFunction, BlockParameter blockParameter = null)
	{
		return ContractHandler.QueryAsync<GetUserHeroesFunction, List<BigInteger>>(getUserHeroesFunction, blockParameter);
	}

	public Task<List<BigInteger>> GetUserHeroesQueryAsync(string address, BlockParameter blockParameter = null)
	{
		var getUserHeroesFunction = new GetUserHeroesFunction();
		getUserHeroesFunction.Address = address;

		return ContractHandler.QueryAsync<GetUserHeroesFunction, List<BigInteger>>(getUserHeroesFunction, blockParameter);
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

	public Task<HeroesOutputDTO> HeroesQueryAsync(HeroesFunction heroesFunction, BlockParameter blockParameter = null)
	{
		return ContractHandler.QueryDeserializingToObjectAsync<HeroesFunction, HeroesOutputDTO>(heroesFunction, blockParameter);
	}

	public Task<HeroesOutputDTO> HeroesQueryAsync(BigInteger returnValue1, BlockParameter blockParameter = null)
	{
		var heroesFunction = new HeroesFunction();
		heroesFunction.ReturnValue1 = returnValue1;

		return ContractHandler.QueryDeserializingToObjectAsync<HeroesFunction, HeroesOutputDTO>(heroesFunction, blockParameter);
	}

	public Task<string> InitializeRequestAsync(InitializeFunction initializeFunction)
	{
		return ContractHandler.SendRequestAsync(initializeFunction);
	}

	public Task<TransactionReceipt> InitializeRequestAndWaitForReceiptAsync(InitializeFunction initializeFunction, CancellationTokenSource cancellationToken = null)
	{
		return ContractHandler.SendRequestAndWaitForReceiptAsync(initializeFunction, cancellationToken);
	}

	public Task<string> InitializeRequestAsync(string name, string symbol, string statScience)
	{
		var initializeFunction = new InitializeFunction();
		initializeFunction.Name = name;
		initializeFunction.Symbol = symbol;
		initializeFunction.StatScience = statScience;

		return ContractHandler.SendRequestAsync(initializeFunction);
	}

	public Task<TransactionReceipt> InitializeRequestAndWaitForReceiptAsync(string name, string symbol, string statScience, CancellationTokenSource cancellationToken = null)
	{
		var initializeFunction = new InitializeFunction();
		initializeFunction.Name = name;
		initializeFunction.Symbol = symbol;
		initializeFunction.StatScience = statScience;

		return ContractHandler.SendRequestAndWaitForReceiptAsync(initializeFunction, cancellationToken);
	}

	public Task<bool> IsApprovedForAllQueryAsync(IsApprovedForAllFunction isApprovedForAllFunction, BlockParameter blockParameter = null)
	{
		return ContractHandler.QueryAsync<IsApprovedForAllFunction, bool>(isApprovedForAllFunction, blockParameter);
	}


	public Task<bool> IsApprovedForAllQueryAsync(string owner, string @operator, BlockParameter blockParameter = null)
	{
		var isApprovedForAllFunction = new IsApprovedForAllFunction();
		isApprovedForAllFunction.Owner = owner;
		isApprovedForAllFunction.Operator = @operator;

		return ContractHandler.QueryAsync<IsApprovedForAllFunction, bool>(isApprovedForAllFunction, blockParameter);
	}

	public Task<string> NameQueryAsync(NameFunction nameFunction, BlockParameter blockParameter = null)
	{
		return ContractHandler.QueryAsync<NameFunction, string>(nameFunction, blockParameter);
	}


	public Task<string> NameQueryAsync(BlockParameter blockParameter = null)
	{
		return ContractHandler.QueryAsync<NameFunction, string>(null, blockParameter);
	}

	public Task<BigInteger> NextHeroIdQueryAsync(NextHeroIdFunction nextHeroIdFunction, BlockParameter blockParameter = null)
	{
		return ContractHandler.QueryAsync<NextHeroIdFunction, BigInteger>(nextHeroIdFunction, blockParameter);
	}


	public Task<BigInteger> NextHeroIdQueryAsync(BlockParameter blockParameter = null)
	{
		return ContractHandler.QueryAsync<NextHeroIdFunction, BigInteger>(null, blockParameter);
	}

	public Task<string> OwnerOfQueryAsync(OwnerOfFunction ownerOfFunction, BlockParameter blockParameter = null)
	{
		return ContractHandler.QueryAsync<OwnerOfFunction, string>(ownerOfFunction, blockParameter);
	}


	public Task<string> OwnerOfQueryAsync(BigInteger tokenId, BlockParameter blockParameter = null)
	{
		var ownerOfFunction = new OwnerOfFunction();
		ownerOfFunction.TokenId = tokenId;

		return ContractHandler.QueryAsync<OwnerOfFunction, string>(ownerOfFunction, blockParameter);
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

	public Task<string> SafeTransferFromRequestAsync(SafeTransferFromFunction safeTransferFromFunction)
	{
		return ContractHandler.SendRequestAsync(safeTransferFromFunction);
	}

	public Task<TransactionReceipt> SafeTransferFromRequestAndWaitForReceiptAsync(SafeTransferFromFunction safeTransferFromFunction, CancellationTokenSource cancellationToken = null)
	{
		return ContractHandler.SendRequestAndWaitForReceiptAsync(safeTransferFromFunction, cancellationToken);
	}

	public Task<string> SafeTransferFromRequestAsync(string from, string to, BigInteger tokenId)
	{
		var safeTransferFromFunction = new SafeTransferFromFunction();
		safeTransferFromFunction.From = from;
		safeTransferFromFunction.To = to;
		safeTransferFromFunction.TokenId = tokenId;

		return ContractHandler.SendRequestAsync(safeTransferFromFunction);
	}

	public Task<TransactionReceipt> SafeTransferFromRequestAndWaitForReceiptAsync(string from, string to, BigInteger tokenId, CancellationTokenSource cancellationToken = null)
	{
		var safeTransferFromFunction = new SafeTransferFromFunction();
		safeTransferFromFunction.From = from;
		safeTransferFromFunction.To = to;
		safeTransferFromFunction.TokenId = tokenId;

		return ContractHandler.SendRequestAndWaitForReceiptAsync(safeTransferFromFunction, cancellationToken);
	}

	public Task<string> SafeTransferFromRequestAsync(SafeTransferFrom1Function safeTransferFrom1Function)
	{
		return ContractHandler.SendRequestAsync(safeTransferFrom1Function);
	}

	public Task<TransactionReceipt> SafeTransferFromRequestAndWaitForReceiptAsync(SafeTransferFrom1Function safeTransferFrom1Function, CancellationTokenSource cancellationToken = null)
	{
		return ContractHandler.SendRequestAndWaitForReceiptAsync(safeTransferFrom1Function, cancellationToken);
	}

	public Task<string> SafeTransferFromRequestAsync(string from, string to, BigInteger tokenId, byte[] data)
	{
		var safeTransferFrom1Function = new SafeTransferFrom1Function();
		safeTransferFrom1Function.From = from;
		safeTransferFrom1Function.To = to;
		safeTransferFrom1Function.TokenId = tokenId;
		safeTransferFrom1Function.Data = data;

		return ContractHandler.SendRequestAsync(safeTransferFrom1Function);
	}

	public Task<TransactionReceipt> SafeTransferFromRequestAndWaitForReceiptAsync(string from, string to, BigInteger tokenId, byte[] data, CancellationTokenSource cancellationToken = null)
	{
		var safeTransferFrom1Function = new SafeTransferFrom1Function();
		safeTransferFrom1Function.From = from;
		safeTransferFrom1Function.To = to;
		safeTransferFrom1Function.TokenId = tokenId;
		safeTransferFrom1Function.Data = data;

		return ContractHandler.SendRequestAndWaitForReceiptAsync(safeTransferFrom1Function, cancellationToken);
	}

	public Task<string> SetApprovalForAllRequestAsync(SetApprovalForAllFunction setApprovalForAllFunction)
	{
		return ContractHandler.SendRequestAsync(setApprovalForAllFunction);
	}

	public Task<TransactionReceipt> SetApprovalForAllRequestAndWaitForReceiptAsync(SetApprovalForAllFunction setApprovalForAllFunction, CancellationTokenSource cancellationToken = null)
	{
		return ContractHandler.SendRequestAndWaitForReceiptAsync(setApprovalForAllFunction, cancellationToken);
	}

	public Task<string> SetApprovalForAllRequestAsync(string @operator, bool approved)
	{
		var setApprovalForAllFunction = new SetApprovalForAllFunction();
		setApprovalForAllFunction.Operator = @operator;
		setApprovalForAllFunction.Approved = approved;

		return ContractHandler.SendRequestAsync(setApprovalForAllFunction);
	}

	public Task<TransactionReceipt> SetApprovalForAllRequestAndWaitForReceiptAsync(string @operator, bool approved, CancellationTokenSource cancellationToken = null)
	{
		var setApprovalForAllFunction = new SetApprovalForAllFunction();
		setApprovalForAllFunction.Operator = @operator;
		setApprovalForAllFunction.Approved = approved;

		return ContractHandler.SendRequestAndWaitForReceiptAsync(setApprovalForAllFunction, cancellationToken);
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

	public Task<string> SymbolQueryAsync(SymbolFunction symbolFunction, BlockParameter blockParameter = null)
	{
		return ContractHandler.QueryAsync<SymbolFunction, string>(symbolFunction, blockParameter);
	}

	public Task<string> SymbolQueryAsync(BlockParameter blockParameter = null)
	{
		return ContractHandler.QueryAsync<SymbolFunction, string>(null, blockParameter);
	}

	public Task<BigInteger> TokenByIndexQueryAsync(TokenByIndexFunction tokenByIndexFunction, BlockParameter blockParameter = null)
	{
		return ContractHandler.QueryAsync<TokenByIndexFunction, BigInteger>(tokenByIndexFunction, blockParameter);
	}

	public Task<BigInteger> TokenByIndexQueryAsync(BigInteger index, BlockParameter blockParameter = null)
	{
		var tokenByIndexFunction = new TokenByIndexFunction();
		tokenByIndexFunction.Index = index;

		return ContractHandler.QueryAsync<TokenByIndexFunction, BigInteger>(tokenByIndexFunction, blockParameter);
	}

	public Task<BigInteger> TokenOfOwnerByIndexQueryAsync(TokenOfOwnerByIndexFunction tokenOfOwnerByIndexFunction, BlockParameter blockParameter = null)
	{
		return ContractHandler.QueryAsync<TokenOfOwnerByIndexFunction, BigInteger>(tokenOfOwnerByIndexFunction, blockParameter);
	}

	public Task<BigInteger> TokenOfOwnerByIndexQueryAsync(string owner, BigInteger index, BlockParameter blockParameter = null)
	{
		var tokenOfOwnerByIndexFunction = new TokenOfOwnerByIndexFunction();
		tokenOfOwnerByIndexFunction.Owner = owner;
		tokenOfOwnerByIndexFunction.Index = index;

		return ContractHandler.QueryAsync<TokenOfOwnerByIndexFunction, BigInteger>(tokenOfOwnerByIndexFunction, blockParameter);
	}

	public Task<string> TokenURIQueryAsync(TokenURIFunction tokenURIFunction, BlockParameter blockParameter = null)
	{
		return ContractHandler.QueryAsync<TokenURIFunction, string>(tokenURIFunction, blockParameter);
	}


	public Task<string> TokenURIQueryAsync(BigInteger tokenId, BlockParameter blockParameter = null)
	{
		var tokenURIFunction = new TokenURIFunction();
		tokenURIFunction.TokenId = tokenId;

		return ContractHandler.QueryAsync<TokenURIFunction, string>(tokenURIFunction, blockParameter);
	}

	public Task<BigInteger> TotalSupplyQueryAsync(TotalSupplyFunction totalSupplyFunction, BlockParameter blockParameter = null)
	{
		return ContractHandler.QueryAsync<TotalSupplyFunction, BigInteger>(totalSupplyFunction, blockParameter);
	}


	public Task<BigInteger> TotalSupplyQueryAsync(BlockParameter blockParameter = null)
	{
		return ContractHandler.QueryAsync<TotalSupplyFunction, BigInteger>(null, blockParameter);
	}

	public Task<string> TransferFromRequestAsync(TransferFromFunction transferFromFunction)
	{
		return ContractHandler.SendRequestAsync(transferFromFunction);
	}

	public Task<TransactionReceipt> TransferFromRequestAndWaitForReceiptAsync(TransferFromFunction transferFromFunction, CancellationTokenSource cancellationToken = null)
	{
		return ContractHandler.SendRequestAndWaitForReceiptAsync(transferFromFunction, cancellationToken);
	}

	public Task<string> TransferFromRequestAsync(string from, string to, BigInteger tokenId)
	{
		var transferFromFunction = new TransferFromFunction();
		transferFromFunction.From = from;
		transferFromFunction.To = to;
		transferFromFunction.TokenId = tokenId;

		return ContractHandler.SendRequestAsync(transferFromFunction);
	}

	public Task<TransactionReceipt> TransferFromRequestAndWaitForReceiptAsync(string from, string to, BigInteger tokenId, CancellationTokenSource cancellationToken = null)
	{
		var transferFromFunction = new TransferFromFunction();
		transferFromFunction.From = from;
		transferFromFunction.To = to;
		transferFromFunction.TokenId = tokenId;

		return ContractHandler.SendRequestAndWaitForReceiptAsync(transferFromFunction, cancellationToken);
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

	public Task<string> UpdateHeroRequestAsync(UpdateHeroFunction updateHeroFunction)
	{
		return ContractHandler.SendRequestAsync(updateHeroFunction);
	}

	public Task<TransactionReceipt> UpdateHeroRequestAndWaitForReceiptAsync(UpdateHeroFunction updateHeroFunction, CancellationTokenSource cancellationToken = null)
	{
		return ContractHandler.SendRequestAndWaitForReceiptAsync(updateHeroFunction, cancellationToken);
	}

	public Task<string> UpdateHeroRequestAsync(Hero hero)
	{
		var updateHeroFunction = new UpdateHeroFunction();
		updateHeroFunction.Hero = hero;

		return ContractHandler.SendRequestAsync(updateHeroFunction);
	}

	public Task<TransactionReceipt> UpdateHeroRequestAndWaitForReceiptAsync(Hero hero, CancellationTokenSource cancellationToken = null)
	{
		var updateHeroFunction = new UpdateHeroFunction();
		updateHeroFunction.Hero = hero;

		return ContractHandler.SendRequestAndWaitForReceiptAsync(updateHeroFunction, cancellationToken);
	}
}
