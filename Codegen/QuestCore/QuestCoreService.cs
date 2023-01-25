using DFKContracts.QuestCore.ContractDefinition;
using Nethereum.Contracts.ContractHandlers;
using Nethereum.RPC.Eth.DTOs;
using System.Numerics;

namespace DFKContracts.QuestCore
{
	public partial class QuestCoreService
	{
		public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, QuestCoreDeployment questCoreDeployment, CancellationTokenSource cancellationTokenSource = null)
		{
			return web3.Eth.GetContractDeploymentHandler<QuestCoreDeployment>().SendRequestAndWaitForReceiptAsync(questCoreDeployment, cancellationTokenSource);
		}

		public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, QuestCoreDeployment questCoreDeployment)
		{
			return web3.Eth.GetContractDeploymentHandler<QuestCoreDeployment>().SendRequestAsync(questCoreDeployment);
		}

		public static async Task<QuestCoreService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, QuestCoreDeployment questCoreDeployment, CancellationTokenSource cancellationTokenSource = null)
		{
			var receipt = await DeployContractAndWaitForReceiptAsync(web3, questCoreDeployment, cancellationTokenSource);
			return new QuestCoreService(web3, receipt.ContractAddress);
		}

		protected Nethereum.Web3.Web3 Web3 { get; }

		public ContractHandler ContractHandler { get; }

		public QuestCoreService(Nethereum.Web3.Web3 web3, string contractAddress)
		{
			Web3 = web3;
			ContractHandler = web3.Eth.GetContractHandler(contractAddress);
		}

		public Task<byte[]> CancelerRoleQueryAsync(CancelerRoleFunction cancelerRoleFunction, BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<CancelerRoleFunction, byte[]>(cancelerRoleFunction, blockParameter);
		}


		public Task<byte[]> CancelerRoleQueryAsync(BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<CancelerRoleFunction, byte[]>(null, blockParameter);
		}

		public Task<byte[]> DefaultAdminRoleQueryAsync(DefaultAdminRoleFunction defaultAdminRoleFunction, BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<DefaultAdminRoleFunction, byte[]>(defaultAdminRoleFunction, blockParameter);
		}


		public Task<byte[]> DefaultAdminRoleQueryAsync(BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<DefaultAdminRoleFunction, byte[]>(null, blockParameter);
		}

		public Task<byte[]> ModeratorRoleQueryAsync(ModeratorRoleFunction moderatorRoleFunction, BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<ModeratorRoleFunction, byte[]>(moderatorRoleFunction, blockParameter);
		}


		public Task<byte[]> ModeratorRoleQueryAsync(BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<ModeratorRoleFunction, byte[]>(null, blockParameter);
		}

		public Task<string> ActivateQuestRequestAsync(ActivateQuestFunction activateQuestFunction)
		{
			return ContractHandler.SendRequestAsync(activateQuestFunction);
		}

		public Task<TransactionReceipt> ActivateQuestRequestAndWaitForReceiptAsync(ActivateQuestFunction activateQuestFunction, CancellationTokenSource cancellationToken = null)
		{
			return ContractHandler.SendRequestAndWaitForReceiptAsync(activateQuestFunction, cancellationToken);
		}

		public Task<string> ActivateQuestRequestAsync(string questAddress)
		{
			var activateQuestFunction = new ActivateQuestFunction();
			activateQuestFunction.QuestAddress = questAddress;

			return ContractHandler.SendRequestAsync(activateQuestFunction);
		}

		public Task<TransactionReceipt> ActivateQuestRequestAndWaitForReceiptAsync(string questAddress, CancellationTokenSource cancellationToken = null)
		{
			var activateQuestFunction = new ActivateQuestFunction();
			activateQuestFunction.QuestAddress = questAddress;

			return ContractHandler.SendRequestAndWaitForReceiptAsync(activateQuestFunction, cancellationToken);
		}

		public Task<ActiveAccountQuestsOutputDTO> ActiveAccountQuestsQueryAsync(ActiveAccountQuestsFunction activeAccountQuestsFunction, BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryDeserializingToObjectAsync<ActiveAccountQuestsFunction, ActiveAccountQuestsOutputDTO>(activeAccountQuestsFunction, blockParameter);
		}

		public Task<ActiveAccountQuestsOutputDTO> ActiveAccountQuestsQueryAsync(string returnValue1, string returnValue2, BigInteger returnValue3, BlockParameter blockParameter = null)
		{
			var activeAccountQuestsFunction = new ActiveAccountQuestsFunction();
			activeAccountQuestsFunction.ReturnValue1 = returnValue1;
			activeAccountQuestsFunction.ReturnValue2 = returnValue2;
			activeAccountQuestsFunction.ReturnValue3 = returnValue3;

			return ContractHandler.QueryDeserializingToObjectAsync<ActiveAccountQuestsFunction, ActiveAccountQuestsOutputDTO>(activeAccountQuestsFunction, blockParameter);
		}

		public Task<string> CancelQuestRequestAsync(CancelQuestFunction cancelQuestFunction)
		{
			return ContractHandler.SendRequestAsync(cancelQuestFunction);
		}

		public Task<TransactionReceipt> CancelQuestRequestAndWaitForReceiptAsync(CancelQuestFunction cancelQuestFunction, CancellationTokenSource cancellationToken = null)
		{
			return ContractHandler.SendRequestAndWaitForReceiptAsync(cancelQuestFunction, cancellationToken);
		}

		public Task<string> CancelQuestRequestAsync(BigInteger heroId)
		{
			var cancelQuestFunction = new CancelQuestFunction();
			cancelQuestFunction.HeroId = heroId;

			return ContractHandler.SendRequestAsync(cancelQuestFunction);
		}

		public Task<TransactionReceipt> CancelQuestRequestAndWaitForReceiptAsync(BigInteger heroId, CancellationTokenSource cancellationToken = null)
		{
			var cancelQuestFunction = new CancelQuestFunction();
			cancelQuestFunction.HeroId = heroId;

			return ContractHandler.SendRequestAndWaitForReceiptAsync(cancelQuestFunction, cancellationToken);
		}

		public Task<string> ClearActiveQuestsRequestAsync(ClearActiveQuestsFunction clearActiveQuestsFunction)
		{
			return ContractHandler.SendRequestAsync(clearActiveQuestsFunction);
		}

		public Task<TransactionReceipt> ClearActiveQuestsRequestAndWaitForReceiptAsync(ClearActiveQuestsFunction clearActiveQuestsFunction, CancellationTokenSource cancellationToken = null)
		{
			return ContractHandler.SendRequestAndWaitForReceiptAsync(clearActiveQuestsFunction, cancellationToken);
		}

		public Task<string> ClearActiveQuestsRequestAsync(string questAddress)
		{
			var clearActiveQuestsFunction = new ClearActiveQuestsFunction();
			clearActiveQuestsFunction.QuestAddress = questAddress;

			return ContractHandler.SendRequestAsync(clearActiveQuestsFunction);
		}

		public Task<TransactionReceipt> ClearActiveQuestsRequestAndWaitForReceiptAsync(string questAddress, CancellationTokenSource cancellationToken = null)
		{
			var clearActiveQuestsFunction = new ClearActiveQuestsFunction();
			clearActiveQuestsFunction.QuestAddress = questAddress;

			return ContractHandler.SendRequestAndWaitForReceiptAsync(clearActiveQuestsFunction, cancellationToken);
		}

		public Task<string> ClearActiveQuestsAndHeroesRequestAsync(ClearActiveQuestsAndHeroesFunction clearActiveQuestsAndHeroesFunction)
		{
			return ContractHandler.SendRequestAsync(clearActiveQuestsAndHeroesFunction);
		}

		public Task<string> ClearActiveQuestsAndHeroesRequestAsync()
		{
			return ContractHandler.SendRequestAsync<ClearActiveQuestsAndHeroesFunction>();
		}

		public Task<TransactionReceipt> ClearActiveQuestsAndHeroesRequestAndWaitForReceiptAsync(ClearActiveQuestsAndHeroesFunction clearActiveQuestsAndHeroesFunction, CancellationTokenSource cancellationToken = null)
		{
			return ContractHandler.SendRequestAndWaitForReceiptAsync(clearActiveQuestsAndHeroesFunction, cancellationToken);
		}

		public Task<TransactionReceipt> ClearActiveQuestsAndHeroesRequestAndWaitForReceiptAsync(CancellationTokenSource cancellationToken = null)
		{
			return ContractHandler.SendRequestAndWaitForReceiptAsync<ClearActiveQuestsAndHeroesFunction>(null, cancellationToken);
		}

		public Task<string> ClearActiveQuestsAndHeroesWithOffsetRequestAsync(ClearActiveQuestsAndHeroesWithOffsetFunction clearActiveQuestsAndHeroesWithOffsetFunction)
		{
			return ContractHandler.SendRequestAsync(clearActiveQuestsAndHeroesWithOffsetFunction);
		}

		public Task<TransactionReceipt> ClearActiveQuestsAndHeroesWithOffsetRequestAndWaitForReceiptAsync(ClearActiveQuestsAndHeroesWithOffsetFunction clearActiveQuestsAndHeroesWithOffsetFunction, CancellationTokenSource cancellationToken = null)
		{
			return ContractHandler.SendRequestAndWaitForReceiptAsync(clearActiveQuestsAndHeroesWithOffsetFunction, cancellationToken);
		}

		public Task<string> ClearActiveQuestsAndHeroesWithOffsetRequestAsync(BigInteger offset, BigInteger amount)
		{
			var clearActiveQuestsAndHeroesWithOffsetFunction = new ClearActiveQuestsAndHeroesWithOffsetFunction();
			clearActiveQuestsAndHeroesWithOffsetFunction.Offset = offset;
			clearActiveQuestsAndHeroesWithOffsetFunction.Amount = amount;

			return ContractHandler.SendRequestAsync(clearActiveQuestsAndHeroesWithOffsetFunction);
		}

		public Task<TransactionReceipt> ClearActiveQuestsAndHeroesWithOffsetRequestAndWaitForReceiptAsync(BigInteger offset, BigInteger amount, CancellationTokenSource cancellationToken = null)
		{
			var clearActiveQuestsAndHeroesWithOffsetFunction = new ClearActiveQuestsAndHeroesWithOffsetFunction();
			clearActiveQuestsAndHeroesWithOffsetFunction.Offset = offset;
			clearActiveQuestsAndHeroesWithOffsetFunction.Amount = amount;

			return ContractHandler.SendRequestAndWaitForReceiptAsync(clearActiveQuestsAndHeroesWithOffsetFunction, cancellationToken);
		}

		public Task<string> CompleteQuestRequestAsync(CompleteQuestFunction completeQuestFunction)
		{
			return ContractHandler.SendRequestAsync(completeQuestFunction);
		}

		public Task<TransactionReceipt> CompleteQuestRequestAndWaitForReceiptAsync(CompleteQuestFunction completeQuestFunction, CancellationTokenSource cancellationToken = null)
		{
			return ContractHandler.SendRequestAndWaitForReceiptAsync(completeQuestFunction, cancellationToken);
		}

		public Task<string> CompleteQuestRequestAsync(BigInteger heroId)
		{
			var completeQuestFunction = new CompleteQuestFunction();
			completeQuestFunction.HeroId = heroId;

			return ContractHandler.SendRequestAsync(completeQuestFunction);
		}

		public Task<TransactionReceipt> CompleteQuestRequestAndWaitForReceiptAsync(BigInteger heroId, CancellationTokenSource cancellationToken = null)
		{
			var completeQuestFunction = new CompleteQuestFunction();
			completeQuestFunction.HeroId = heroId;

			return ContractHandler.SendRequestAndWaitForReceiptAsync(completeQuestFunction, cancellationToken);
		}

		public Task<GetAccountActiveQuestsOutputDTO> GetAccountActiveQuestsQueryAsync(GetAccountActiveQuestsFunction getAccountActiveQuestsFunction, BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryDeserializingToObjectAsync<GetAccountActiveQuestsFunction, GetAccountActiveQuestsOutputDTO>(getAccountActiveQuestsFunction, blockParameter);
		}

		public Task<GetAccountActiveQuestsOutputDTO> GetAccountActiveQuestsQueryAsync(string account, BlockParameter blockParameter = null)
		{
			var getAccountActiveQuestsFunction = new GetAccountActiveQuestsFunction();
			getAccountActiveQuestsFunction.Account = account;

			return ContractHandler.QueryDeserializingToObjectAsync<GetAccountActiveQuestsFunction, GetAccountActiveQuestsOutputDTO>(getAccountActiveQuestsFunction, blockParameter);
		}

		public Task<BigInteger> GetCurrentStaminaQueryAsync(GetCurrentStaminaFunction getCurrentStaminaFunction, BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<GetCurrentStaminaFunction, BigInteger>(getCurrentStaminaFunction, blockParameter);
		}


		public Task<BigInteger> GetCurrentStaminaQueryAsync(BigInteger heroId, BlockParameter blockParameter = null)
		{
			var getCurrentStaminaFunction = new GetCurrentStaminaFunction();
			getCurrentStaminaFunction.HeroId = heroId;

			return ContractHandler.QueryAsync<GetCurrentStaminaFunction, BigInteger>(getCurrentStaminaFunction, blockParameter);
		}

		public Task<GetHeroQuestOutputDTO> GetHeroQuestQueryAsync(GetHeroQuestFunction getHeroQuestFunction, BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryDeserializingToObjectAsync<GetHeroQuestFunction, GetHeroQuestOutputDTO>(getHeroQuestFunction, blockParameter);
		}

		public Task<GetHeroQuestOutputDTO> GetHeroQuestQueryAsync(BigInteger heroId, BlockParameter blockParameter = null)
		{
			var getHeroQuestFunction = new GetHeroQuestFunction();
			getHeroQuestFunction.HeroId = heroId;

			return ContractHandler.QueryDeserializingToObjectAsync<GetHeroQuestFunction, GetHeroQuestOutputDTO>(getHeroQuestFunction, blockParameter);
		}

		public Task<List<string>> GetQuestContractsQueryAsync(GetQuestContractsFunction getQuestContractsFunction, BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<GetQuestContractsFunction, List<string>>(getQuestContractsFunction, blockParameter);
		}


		public Task<List<string>> GetQuestContractsQueryAsync(BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<GetQuestContractsFunction, List<string>>(null, blockParameter);
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

		public Task<string> GetRoleMemberQueryAsync(GetRoleMemberFunction getRoleMemberFunction, BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<GetRoleMemberFunction, string>(getRoleMemberFunction, blockParameter);
		}


		public Task<string> GetRoleMemberQueryAsync(byte[] role, BigInteger index, BlockParameter blockParameter = null)
		{
			var getRoleMemberFunction = new GetRoleMemberFunction();
			getRoleMemberFunction.Role = role;
			getRoleMemberFunction.Index = index;

			return ContractHandler.QueryAsync<GetRoleMemberFunction, string>(getRoleMemberFunction, blockParameter);
		}

		public Task<BigInteger> GetRoleMemberCountQueryAsync(GetRoleMemberCountFunction getRoleMemberCountFunction, BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<GetRoleMemberCountFunction, BigInteger>(getRoleMemberCountFunction, blockParameter);
		}


		public Task<BigInteger> GetRoleMemberCountQueryAsync(byte[] role, BlockParameter blockParameter = null)
		{
			var getRoleMemberCountFunction = new GetRoleMemberCountFunction();
			getRoleMemberCountFunction.Role = role;

			return ContractHandler.QueryAsync<GetRoleMemberCountFunction, BigInteger>(getRoleMemberCountFunction, blockParameter);
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

		public Task<string> HeroCoreQueryAsync(HeroCoreFunction heroCoreFunction, BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<HeroCoreFunction, string>(heroCoreFunction, blockParameter);
		}


		public Task<string> HeroCoreQueryAsync(BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<HeroCoreFunction, string>(null, blockParameter);
		}

		public Task<BigInteger> HeroToQuestQueryAsync(HeroToQuestFunction heroToQuestFunction, BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<HeroToQuestFunction, BigInteger>(heroToQuestFunction, blockParameter);
		}


		public Task<BigInteger> HeroToQuestQueryAsync(BigInteger returnValue1, BlockParameter blockParameter = null)
		{
			var heroToQuestFunction = new HeroToQuestFunction();
			heroToQuestFunction.ReturnValue1 = returnValue1;

			return ContractHandler.QueryAsync<HeroToQuestFunction, BigInteger>(heroToQuestFunction, blockParameter);
		}

		public Task<string> InitializeRequestAsync(InitializeFunction initializeFunction)
		{
			return ContractHandler.SendRequestAsync(initializeFunction);
		}

		public Task<TransactionReceipt> InitializeRequestAndWaitForReceiptAsync(InitializeFunction initializeFunction, CancellationTokenSource cancellationToken = null)
		{
			return ContractHandler.SendRequestAndWaitForReceiptAsync(initializeFunction, cancellationToken);
		}

		public Task<string> InitializeRequestAsync(string heroCoreAddress)
		{
			var initializeFunction = new InitializeFunction();
			initializeFunction.HeroCoreAddress = heroCoreAddress;

			return ContractHandler.SendRequestAsync(initializeFunction);
		}

		public Task<TransactionReceipt> InitializeRequestAndWaitForReceiptAsync(string heroCoreAddress, CancellationTokenSource cancellationToken = null)
		{
			var initializeFunction = new InitializeFunction();
			initializeFunction.HeroCoreAddress = heroCoreAddress;

			return ContractHandler.SendRequestAndWaitForReceiptAsync(initializeFunction, cancellationToken);
		}

		public Task<bool> IsQuestQueryAsync(IsQuestFunction isQuestFunction, BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<IsQuestFunction, bool>(isQuestFunction, blockParameter);
		}


		public Task<bool> IsQuestQueryAsync(string returnValue1, BlockParameter blockParameter = null)
		{
			var isQuestFunction = new IsQuestFunction();
			isQuestFunction.ReturnValue1 = returnValue1;

			return ContractHandler.QueryAsync<IsQuestFunction, bool>(isQuestFunction, blockParameter);
		}

		public Task<string> MultiCompleteQuestRequestAsync(MultiCompleteQuestFunction multiCompleteQuestFunction)
		{
			return ContractHandler.SendRequestAsync(multiCompleteQuestFunction);
		}

		public Task<TransactionReceipt> MultiCompleteQuestRequestAndWaitForReceiptAsync(MultiCompleteQuestFunction multiCompleteQuestFunction, CancellationTokenSource cancellationToken = null)
		{
			return ContractHandler.SendRequestAndWaitForReceiptAsync(multiCompleteQuestFunction, cancellationToken);
		}

		public Task<string> MultiCompleteQuestRequestAsync(List<BigInteger> heroIds)
		{
			var multiCompleteQuestFunction = new MultiCompleteQuestFunction();
			multiCompleteQuestFunction.HeroIds = heroIds;

			return ContractHandler.SendRequestAsync(multiCompleteQuestFunction);
		}

		public Task<TransactionReceipt> MultiCompleteQuestRequestAndWaitForReceiptAsync(List<BigInteger> heroIds, CancellationTokenSource cancellationToken = null)
		{
			var multiCompleteQuestFunction = new MultiCompleteQuestFunction();
			multiCompleteQuestFunction.HeroIds = heroIds;

			return ContractHandler.SendRequestAndWaitForReceiptAsync(multiCompleteQuestFunction, cancellationToken);
		}

		public Task<string> MultiStartQuestRequestAsync(MultiStartQuestFunction multiStartQuestFunction)
		{
			return ContractHandler.SendRequestAsync(multiStartQuestFunction);
		}

		public Task<TransactionReceipt> MultiStartQuestRequestAndWaitForReceiptAsync(MultiStartQuestFunction multiStartQuestFunction, CancellationTokenSource cancellationToken = null)
		{
			return ContractHandler.SendRequestAndWaitForReceiptAsync(multiStartQuestFunction, cancellationToken);
		}

		public Task<string> MultiStartQuestRequestAsync(List<string> questAddress, List<List<BigInteger>> heroIds, List<byte> attempts, List<byte> level)
		{
			var multiStartQuestFunction = new MultiStartQuestFunction();
			multiStartQuestFunction.QuestAddress = questAddress;
			multiStartQuestFunction.HeroIds = heroIds;
			multiStartQuestFunction.Attempts = attempts;
			multiStartQuestFunction.Level = level;

			return ContractHandler.SendRequestAsync(multiStartQuestFunction);
		}

		public Task<TransactionReceipt> MultiStartQuestRequestAndWaitForReceiptAsync(List<string> questAddress, List<List<BigInteger>> heroIds, List<byte> attempts, List<byte> level, CancellationTokenSource cancellationToken = null)
		{
			var multiStartQuestFunction = new MultiStartQuestFunction();
			multiStartQuestFunction.QuestAddress = questAddress;
			multiStartQuestFunction.HeroIds = heroIds;
			multiStartQuestFunction.Attempts = attempts;
			multiStartQuestFunction.Level = level;

			return ContractHandler.SendRequestAndWaitForReceiptAsync(multiStartQuestFunction, cancellationToken);
		}

		public Task<bool> PausedQueryAsync(PausedFunction pausedFunction, BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<PausedFunction, bool>(pausedFunction, blockParameter);
		}


		public Task<bool> PausedQueryAsync(BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<PausedFunction, bool>(null, blockParameter);
		}

		public Task<BigInteger> QuestCounterQueryAsync(QuestCounterFunction questCounterFunction, BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<QuestCounterFunction, BigInteger>(questCounterFunction, blockParameter);
		}


		public Task<BigInteger> QuestCounterQueryAsync(BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<QuestCounterFunction, BigInteger>(null, blockParameter);
		}

		public Task<string> QuestRewarderQueryAsync(QuestRewarderFunction questRewarderFunction, BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<QuestRewarderFunction, string>(questRewarderFunction, blockParameter);
		}


		public Task<string> QuestRewarderQueryAsync(BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<QuestRewarderFunction, string>(null, blockParameter);
		}

		public Task<QuestsOutputDTO> QuestsQueryAsync(QuestsFunction questsFunction, BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryDeserializingToObjectAsync<QuestsFunction, QuestsOutputDTO>(questsFunction, blockParameter);
		}

		public Task<QuestsOutputDTO> QuestsQueryAsync(BigInteger returnValue1, BlockParameter blockParameter = null)
		{
			var questsFunction = new QuestsFunction();
			questsFunction.ReturnValue1 = returnValue1;

			return ContractHandler.QueryDeserializingToObjectAsync<QuestsFunction, QuestsOutputDTO>(questsFunction, blockParameter);
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

		public Task<string> SetQuestRewarderRequestAsync(SetQuestRewarderFunction setQuestRewarderFunction)
		{
			return ContractHandler.SendRequestAsync(setQuestRewarderFunction);
		}

		public Task<TransactionReceipt> SetQuestRewarderRequestAndWaitForReceiptAsync(SetQuestRewarderFunction setQuestRewarderFunction, CancellationTokenSource cancellationToken = null)
		{
			return ContractHandler.SendRequestAndWaitForReceiptAsync(setQuestRewarderFunction, cancellationToken);
		}

		public Task<string> SetQuestRewarderRequestAsync(string questRewarder)
		{
			var setQuestRewarderFunction = new SetQuestRewarderFunction();
			setQuestRewarderFunction.QuestRewarder = questRewarder;

			return ContractHandler.SendRequestAsync(setQuestRewarderFunction);
		}

		public Task<TransactionReceipt> SetQuestRewarderRequestAndWaitForReceiptAsync(string questRewarder, CancellationTokenSource cancellationToken = null)
		{
			var setQuestRewarderFunction = new SetQuestRewarderFunction();
			setQuestRewarderFunction.QuestRewarder = questRewarder;

			return ContractHandler.SendRequestAndWaitForReceiptAsync(setQuestRewarderFunction, cancellationToken);
		}

		public Task<string> SetTimePerStaminaRequestAsync(SetTimePerStaminaFunction setTimePerStaminaFunction)
		{
			return ContractHandler.SendRequestAsync(setTimePerStaminaFunction);
		}

		public Task<TransactionReceipt> SetTimePerStaminaRequestAndWaitForReceiptAsync(SetTimePerStaminaFunction setTimePerStaminaFunction, CancellationTokenSource cancellationToken = null)
		{
			return ContractHandler.SendRequestAndWaitForReceiptAsync(setTimePerStaminaFunction, cancellationToken);
		}

		public Task<string> SetTimePerStaminaRequestAsync(BigInteger timePerStamina)
		{
			var setTimePerStaminaFunction = new SetTimePerStaminaFunction();
			setTimePerStaminaFunction.TimePerStamina = timePerStamina;

			return ContractHandler.SendRequestAsync(setTimePerStaminaFunction);
		}

		public Task<TransactionReceipt> SetTimePerStaminaRequestAndWaitForReceiptAsync(BigInteger timePerStamina, CancellationTokenSource cancellationToken = null)
		{
			var setTimePerStaminaFunction = new SetTimePerStaminaFunction();
			setTimePerStaminaFunction.TimePerStamina = timePerStamina;

			return ContractHandler.SendRequestAndWaitForReceiptAsync(setTimePerStaminaFunction, cancellationToken);
		}

		public Task<string> StartQuestRequestAsync(StartQuestFunction startQuestFunction)
		{
			return ContractHandler.SendRequestAsync(startQuestFunction);
		}

		public Task<TransactionReceipt> StartQuestRequestAndWaitForReceiptAsync(StartQuestFunction startQuestFunction, CancellationTokenSource cancellationToken = null)
		{
			return ContractHandler.SendRequestAndWaitForReceiptAsync(startQuestFunction, cancellationToken);
		}

		public Task<string> StartQuestRequestAsync(List<BigInteger> heroIds, string questAddress, byte attempts, byte level)
		{
			var startQuestFunction = new StartQuestFunction();
			startQuestFunction.HeroIds = heroIds;
			startQuestFunction.QuestAddress = questAddress;
			startQuestFunction.Attempts = attempts;
			startQuestFunction.Level = level;

			return ContractHandler.SendRequestAsync(startQuestFunction);
		}

		public Task<TransactionReceipt> StartQuestRequestAndWaitForReceiptAsync(List<BigInteger> heroIds, string questAddress, byte attempts, byte level, CancellationTokenSource cancellationToken = null)
		{
			var startQuestFunction = new StartQuestFunction();
			startQuestFunction.HeroIds = heroIds;
			startQuestFunction.QuestAddress = questAddress;
			startQuestFunction.Attempts = attempts;
			startQuestFunction.Level = level;

			return ContractHandler.SendRequestAndWaitForReceiptAsync(startQuestFunction, cancellationToken);
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

		public Task<BigInteger> TimePerStaminaQueryAsync(TimePerStaminaFunction timePerStaminaFunction, BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<TimePerStaminaFunction, BigInteger>(timePerStaminaFunction, blockParameter);
		}


		public Task<BigInteger> TimePerStaminaQueryAsync(BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<TimePerStaminaFunction, BigInteger>(null, blockParameter);
		}
	}
}
