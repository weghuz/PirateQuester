using DFKContracts.MeditationCircle.ContractDefinition;
using Nethereum.Contracts.ContractHandlers;
using Nethereum.RPC.Eth.DTOs;
using System.Numerics;

namespace DFKContracts.MeditationCircle
{
	public partial class MeditationCircleService
	{
		public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, MeditationCircleDeployment meditationCircleDeployment, CancellationTokenSource cancellationTokenSource = null)
		{
			return web3.Eth.GetContractDeploymentHandler<MeditationCircleDeployment>().SendRequestAndWaitForReceiptAsync(meditationCircleDeployment, cancellationTokenSource);
		}

		public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, MeditationCircleDeployment meditationCircleDeployment)
		{
			return web3.Eth.GetContractDeploymentHandler<MeditationCircleDeployment>().SendRequestAsync(meditationCircleDeployment);
		}

		public static async Task<MeditationCircleService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, MeditationCircleDeployment meditationCircleDeployment, CancellationTokenSource cancellationTokenSource = null)
		{
			var receipt = await DeployContractAndWaitForReceiptAsync(web3, meditationCircleDeployment, cancellationTokenSource);
			return new MeditationCircleService(web3, receipt.ContractAddress);
		}

		protected Nethereum.Web3.Web3 Web3 { get; }

		public ContractHandler ContractHandler { get; }

		public MeditationCircleService(Nethereum.Web3.Web3 web3, string contractAddress)
		{
			Web3 = web3;
			ContractHandler = web3.Eth.GetContractHandler(contractAddress);
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

		public Task<List<ushort>> GetRequiredRunesQueryAsync(GetRequiredRunesFunction getRequiredRunesFunction, BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<GetRequiredRunesFunction, List<ushort>>(getRequiredRunesFunction, blockParameter);
		}


		public Task<List<ushort>> GetRequiredRunesQueryAsync(ushort level, BlockParameter blockParameter = null)
		{
			var getRequiredRunesFunction = new GetRequiredRunesFunction();
			getRequiredRunesFunction.Level = level;

			return ContractHandler.QueryAsync<GetRequiredRunesFunction, List<ushort>>(getRequiredRunesFunction, blockParameter);
		}

		public Task<bool> ActiveAttunementCrystalsQueryAsync(ActiveAttunementCrystalsFunction activeAttunementCrystalsFunction, BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<ActiveAttunementCrystalsFunction, bool>(activeAttunementCrystalsFunction, blockParameter);
		}


		public Task<bool> ActiveAttunementCrystalsQueryAsync(string returnValue1, BlockParameter blockParameter = null)
		{
			var activeAttunementCrystalsFunction = new ActiveAttunementCrystalsFunction();
			activeAttunementCrystalsFunction.ReturnValue1 = returnValue1;

			return ContractHandler.QueryAsync<ActiveAttunementCrystalsFunction, bool>(activeAttunementCrystalsFunction, blockParameter);
		}

		public Task<string> AddAttunementCrystalRequestAsync(AddAttunementCrystalFunction addAttunementCrystalFunction)
		{
			return ContractHandler.SendRequestAsync(addAttunementCrystalFunction);
		}

		public Task<TransactionReceipt> AddAttunementCrystalRequestAndWaitForReceiptAsync(AddAttunementCrystalFunction addAttunementCrystalFunction, CancellationTokenSource cancellationToken = null)
		{
			return ContractHandler.SendRequestAndWaitForReceiptAsync(addAttunementCrystalFunction, cancellationToken);
		}

		public Task<string> AddAttunementCrystalRequestAsync(string address)
		{
			var addAttunementCrystalFunction = new AddAttunementCrystalFunction();
			addAttunementCrystalFunction.Address = address;

			return ContractHandler.SendRequestAsync(addAttunementCrystalFunction);
		}

		public Task<TransactionReceipt> AddAttunementCrystalRequestAndWaitForReceiptAsync(string address, CancellationTokenSource cancellationToken = null)
		{
			var addAttunementCrystalFunction = new AddAttunementCrystalFunction();
			addAttunementCrystalFunction.Address = address;

			return ContractHandler.SendRequestAndWaitForReceiptAsync(addAttunementCrystalFunction, cancellationToken);
		}

		public Task<string> AdminRemoveRequestAsync(AdminRemoveFunction adminRemoveFunction)
		{
			return ContractHandler.SendRequestAsync(adminRemoveFunction);
		}

		public Task<TransactionReceipt> AdminRemoveRequestAndWaitForReceiptAsync(AdminRemoveFunction adminRemoveFunction, CancellationTokenSource cancellationToken = null)
		{
			return ContractHandler.SendRequestAndWaitForReceiptAsync(adminRemoveFunction, cancellationToken);
		}

		public Task<string> AdminRemoveRequestAsync(BigInteger heroId)
		{
			var adminRemoveFunction = new AdminRemoveFunction();
			adminRemoveFunction.HeroId = heroId;

			return ContractHandler.SendRequestAsync(adminRemoveFunction);
		}

		public Task<TransactionReceipt> AdminRemoveRequestAndWaitForReceiptAsync(BigInteger heroId, CancellationTokenSource cancellationToken = null)
		{
			var adminRemoveFunction = new AdminRemoveFunction();
			adminRemoveFunction.HeroId = heroId;

			return ContractHandler.SendRequestAndWaitForReceiptAsync(adminRemoveFunction, cancellationToken);
		}

		public Task<string> CompleteMeditationRequestAsync(CompleteMeditationFunction completeMeditationFunction)
		{
			return ContractHandler.SendRequestAsync(completeMeditationFunction);
		}

		public Task<TransactionReceipt> CompleteMeditationRequestAndWaitForReceiptAsync(CompleteMeditationFunction completeMeditationFunction, CancellationTokenSource cancellationToken = null)
		{
			return ContractHandler.SendRequestAndWaitForReceiptAsync(completeMeditationFunction, cancellationToken);
		}

		public Task<string> CompleteMeditationRequestAsync(BigInteger heroId)
		{
			var completeMeditationFunction = new CompleteMeditationFunction();
			completeMeditationFunction.HeroId = heroId;

			return ContractHandler.SendRequestAsync(completeMeditationFunction);
		}

		public Task<TransactionReceipt> CompleteMeditationRequestAndWaitForReceiptAsync(BigInteger heroId, CancellationTokenSource cancellationToken = null)
		{
			var completeMeditationFunction = new CompleteMeditationFunction();
			completeMeditationFunction.HeroId = heroId;

			return ContractHandler.SendRequestAndWaitForReceiptAsync(completeMeditationFunction, cancellationToken);
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

		public Task<GetActiveMeditationsOutputDTO> GetActiveMeditationsQueryAsync(GetActiveMeditationsFunction getActiveMeditationsFunction, BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryDeserializingToObjectAsync<GetActiveMeditationsFunction, GetActiveMeditationsOutputDTO>(getActiveMeditationsFunction, blockParameter);
		}

		public Task<GetActiveMeditationsOutputDTO> GetActiveMeditationsQueryAsync(string address, BlockParameter blockParameter = null)
		{
			var getActiveMeditationsFunction = new GetActiveMeditationsFunction();
			getActiveMeditationsFunction.Address = address;

			return ContractHandler.QueryDeserializingToObjectAsync<GetActiveMeditationsFunction, GetActiveMeditationsOutputDTO>(getActiveMeditationsFunction, blockParameter);
		}

		public Task<GetHeroMeditationOutputDTO> GetHeroMeditationQueryAsync(GetHeroMeditationFunction getHeroMeditationFunction, BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryDeserializingToObjectAsync<GetHeroMeditationFunction, GetHeroMeditationOutputDTO>(getHeroMeditationFunction, blockParameter);
		}

		public Task<GetHeroMeditationOutputDTO> GetHeroMeditationQueryAsync(BigInteger heroId, BlockParameter blockParameter = null)
		{
			var getHeroMeditationFunction = new GetHeroMeditationFunction();
			getHeroMeditationFunction.HeroId = heroId;

			return ContractHandler.QueryDeserializingToObjectAsync<GetHeroMeditationFunction, GetHeroMeditationOutputDTO>(getHeroMeditationFunction, blockParameter);
		}

		public Task<GetMeditationOutputDTO> GetMeditationQueryAsync(GetMeditationFunction getMeditationFunction, BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryDeserializingToObjectAsync<GetMeditationFunction, GetMeditationOutputDTO>(getMeditationFunction, blockParameter);
		}

		public Task<GetMeditationOutputDTO> GetMeditationQueryAsync(BigInteger id, BlockParameter blockParameter = null)
		{
			var getMeditationFunction = new GetMeditationFunction();
			getMeditationFunction.Id = id;

			return ContractHandler.QueryDeserializingToObjectAsync<GetMeditationFunction, GetMeditationOutputDTO>(getMeditationFunction, blockParameter);
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

		public Task<BigInteger> HeroToMeditationQueryAsync(HeroToMeditationFunction heroToMeditationFunction, BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<HeroToMeditationFunction, BigInteger>(heroToMeditationFunction, blockParameter);
		}


		public Task<BigInteger> HeroToMeditationQueryAsync(BigInteger returnValue1, BlockParameter blockParameter = null)
		{
			var heroToMeditationFunction = new HeroToMeditationFunction();
			heroToMeditationFunction.ReturnValue1 = returnValue1;

			return ContractHandler.QueryAsync<HeroToMeditationFunction, BigInteger>(heroToMeditationFunction, blockParameter);
		}

		public Task<string> InitializeRequestAsync(InitializeFunction initializeFunction)
		{
			return ContractHandler.SendRequestAsync(initializeFunction);
		}

		public Task<TransactionReceipt> InitializeRequestAndWaitForReceiptAsync(InitializeFunction initializeFunction, CancellationTokenSource cancellationToken = null)
		{
			return ContractHandler.SendRequestAndWaitForReceiptAsync(initializeFunction, cancellationToken);
		}

		public Task<string> InitializeRequestAsync(string heroCoreAddress, string statScienceAddress, string powerTokenAddress)
		{
			var initializeFunction = new InitializeFunction();
			initializeFunction.HeroCoreAddress = heroCoreAddress;
			initializeFunction.StatScienceAddress = statScienceAddress;
			initializeFunction.PowerTokenAddress = powerTokenAddress;

			return ContractHandler.SendRequestAsync(initializeFunction);
		}

		public Task<TransactionReceipt> InitializeRequestAndWaitForReceiptAsync(string heroCoreAddress, string statScienceAddress, string powerTokenAddress, CancellationTokenSource cancellationToken = null)
		{
			var initializeFunction = new InitializeFunction();
			initializeFunction.HeroCoreAddress = heroCoreAddress;
			initializeFunction.StatScienceAddress = statScienceAddress;
			initializeFunction.PowerTokenAddress = powerTokenAddress;

			return ContractHandler.SendRequestAndWaitForReceiptAsync(initializeFunction, cancellationToken);
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

		public Task<ProfileActiveMeditationsOutputDTO> ProfileActiveMeditationsQueryAsync(ProfileActiveMeditationsFunction profileActiveMeditationsFunction, BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryDeserializingToObjectAsync<ProfileActiveMeditationsFunction, ProfileActiveMeditationsOutputDTO>(profileActiveMeditationsFunction, blockParameter);
		}

		public Task<ProfileActiveMeditationsOutputDTO> ProfileActiveMeditationsQueryAsync(string returnValue1, BigInteger returnValue2, BlockParameter blockParameter = null)
		{
			var profileActiveMeditationsFunction = new ProfileActiveMeditationsFunction();
			profileActiveMeditationsFunction.ReturnValue1 = returnValue1;
			profileActiveMeditationsFunction.ReturnValue2 = returnValue2;

			return ContractHandler.QueryDeserializingToObjectAsync<ProfileActiveMeditationsFunction, ProfileActiveMeditationsOutputDTO>(profileActiveMeditationsFunction, blockParameter);
		}

		public Task<string> RemoveAttunementCrystalRequestAsync(RemoveAttunementCrystalFunction removeAttunementCrystalFunction)
		{
			return ContractHandler.SendRequestAsync(removeAttunementCrystalFunction);
		}

		public Task<TransactionReceipt> RemoveAttunementCrystalRequestAndWaitForReceiptAsync(RemoveAttunementCrystalFunction removeAttunementCrystalFunction, CancellationTokenSource cancellationToken = null)
		{
			return ContractHandler.SendRequestAndWaitForReceiptAsync(removeAttunementCrystalFunction, cancellationToken);
		}

		public Task<string> RemoveAttunementCrystalRequestAsync(string address)
		{
			var removeAttunementCrystalFunction = new RemoveAttunementCrystalFunction();
			removeAttunementCrystalFunction.Address = address;

			return ContractHandler.SendRequestAsync(removeAttunementCrystalFunction);
		}

		public Task<TransactionReceipt> RemoveAttunementCrystalRequestAndWaitForReceiptAsync(string address, CancellationTokenSource cancellationToken = null)
		{
			var removeAttunementCrystalFunction = new RemoveAttunementCrystalFunction();
			removeAttunementCrystalFunction.Address = address;

			return ContractHandler.SendRequestAndWaitForReceiptAsync(removeAttunementCrystalFunction, cancellationToken);
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

		public Task<string> RunesQueryAsync(RunesFunction runesFunction, BlockParameter blockParameter = null)
		{
			return ContractHandler.QueryAsync<RunesFunction, string>(runesFunction, blockParameter);
		}


		public Task<string> RunesQueryAsync(BigInteger returnValue1, BlockParameter blockParameter = null)
		{
			var runesFunction = new RunesFunction();
			runesFunction.ReturnValue1 = returnValue1;

			return ContractHandler.QueryAsync<RunesFunction, string>(runesFunction, blockParameter);
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

		public Task<string> SetFlagStorageRequestAsync(SetFlagStorageFunction setFlagStorageFunction)
		{
			return ContractHandler.SendRequestAsync(setFlagStorageFunction);
		}

		public Task<TransactionReceipt> SetFlagStorageRequestAndWaitForReceiptAsync(SetFlagStorageFunction setFlagStorageFunction, CancellationTokenSource cancellationToken = null)
		{
			return ContractHandler.SendRequestAndWaitForReceiptAsync(setFlagStorageFunction, cancellationToken);
		}

		public Task<string> SetFlagStorageRequestAsync(string flagStorageAddress)
		{
			var setFlagStorageFunction = new SetFlagStorageFunction();
			setFlagStorageFunction.FlagStorageAddress = flagStorageAddress;

			return ContractHandler.SendRequestAsync(setFlagStorageFunction);
		}

		public Task<TransactionReceipt> SetFlagStorageRequestAndWaitForReceiptAsync(string flagStorageAddress, CancellationTokenSource cancellationToken = null)
		{
			var setFlagStorageFunction = new SetFlagStorageFunction();
			setFlagStorageFunction.FlagStorageAddress = flagStorageAddress;

			return ContractHandler.SendRequestAndWaitForReceiptAsync(setFlagStorageFunction, cancellationToken);
		}

		public Task<string> SetRuneRequestAsync(SetRuneFunction setRuneFunction)
		{
			return ContractHandler.SendRequestAsync(setRuneFunction);
		}

		public Task<TransactionReceipt> SetRuneRequestAndWaitForReceiptAsync(SetRuneFunction setRuneFunction, CancellationTokenSource cancellationToken = null)
		{
			return ContractHandler.SendRequestAndWaitForReceiptAsync(setRuneFunction, cancellationToken);
		}

		public Task<string> SetRuneRequestAsync(byte index, string address)
		{
			var setRuneFunction = new SetRuneFunction();
			setRuneFunction.Index = index;
			setRuneFunction.Address = address;

			return ContractHandler.SendRequestAsync(setRuneFunction);
		}

		public Task<TransactionReceipt> SetRuneRequestAndWaitForReceiptAsync(byte index, string address, CancellationTokenSource cancellationToken = null)
		{
			var setRuneFunction = new SetRuneFunction();
			setRuneFunction.Index = index;
			setRuneFunction.Address = address;

			return ContractHandler.SendRequestAndWaitForReceiptAsync(setRuneFunction, cancellationToken);
		}

		public Task<string> SetStatScienceAddressRequestAsync(SetStatScienceAddressFunction setStatScienceAddressFunction)
		{
			return ContractHandler.SendRequestAsync(setStatScienceAddressFunction);
		}

		public Task<TransactionReceipt> SetStatScienceAddressRequestAndWaitForReceiptAsync(SetStatScienceAddressFunction setStatScienceAddressFunction, CancellationTokenSource cancellationToken = null)
		{
			return ContractHandler.SendRequestAndWaitForReceiptAsync(setStatScienceAddressFunction, cancellationToken);
		}

		public Task<string> SetStatScienceAddressRequestAsync(string statScienceAddress)
		{
			var setStatScienceAddressFunction = new SetStatScienceAddressFunction();
			setStatScienceAddressFunction.StatScienceAddress = statScienceAddress;

			return ContractHandler.SendRequestAsync(setStatScienceAddressFunction);
		}

		public Task<TransactionReceipt> SetStatScienceAddressRequestAndWaitForReceiptAsync(string statScienceAddress, CancellationTokenSource cancellationToken = null)
		{
			var setStatScienceAddressFunction = new SetStatScienceAddressFunction();
			setStatScienceAddressFunction.StatScienceAddress = statScienceAddress;

			return ContractHandler.SendRequestAndWaitForReceiptAsync(setStatScienceAddressFunction, cancellationToken);
		}

		public Task<string> StartMeditationRequestAsync(StartMeditationFunction startMeditationFunction)
		{
			return ContractHandler.SendRequestAsync(startMeditationFunction);
		}

		public Task<TransactionReceipt> StartMeditationRequestAndWaitForReceiptAsync(StartMeditationFunction startMeditationFunction, CancellationTokenSource cancellationToken = null)
		{
			return ContractHandler.SendRequestAndWaitForReceiptAsync(startMeditationFunction, cancellationToken);
		}

		public Task<string> StartMeditationRequestAsync(BigInteger heroId, byte primaryStat, byte secondaryStat, byte tertiaryStat, string attunementCrystal)
		{
			var startMeditationFunction = new StartMeditationFunction();
			startMeditationFunction.HeroId = heroId;
			startMeditationFunction.PrimaryStat = primaryStat;
			startMeditationFunction.SecondaryStat = secondaryStat;
			startMeditationFunction.TertiaryStat = tertiaryStat;
			startMeditationFunction.AttunementCrystal = attunementCrystal;

			return ContractHandler.SendRequestAsync(startMeditationFunction);
		}

		public Task<TransactionReceipt> StartMeditationRequestAndWaitForReceiptAsync(BigInteger heroId, byte primaryStat, byte secondaryStat, byte tertiaryStat, string attunementCrystal, CancellationTokenSource cancellationToken = null)
		{
			var startMeditationFunction = new StartMeditationFunction();
			startMeditationFunction.HeroId = heroId;
			startMeditationFunction.PrimaryStat = primaryStat;
			startMeditationFunction.SecondaryStat = secondaryStat;
			startMeditationFunction.TertiaryStat = tertiaryStat;
			startMeditationFunction.AttunementCrystal = attunementCrystal;

			return ContractHandler.SendRequestAndWaitForReceiptAsync(startMeditationFunction, cancellationToken);
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
	}
}
