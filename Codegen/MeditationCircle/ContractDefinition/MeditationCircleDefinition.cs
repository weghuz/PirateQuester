using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using System.Numerics;

namespace DFKContracts.MeditationCircle.ContractDefinition
{


	public partial class MeditationCircleDeployment : MeditationCircleDeploymentBase
	{
		public MeditationCircleDeployment() : base(BYTECODE) { }
		public MeditationCircleDeployment(string byteCode) : base(byteCode) { }
	}

	public class MeditationCircleDeploymentBase : ContractDeploymentMessage
	{
		public static string BYTECODE = "";
		public MeditationCircleDeploymentBase() : base(BYTECODE) { }
		public MeditationCircleDeploymentBase(string byteCode) : base(byteCode) { }

	}

	public partial class DefaultAdminRoleFunction : DefaultAdminRoleFunctionBase { }

	[Function("DEFAULT_ADMIN_ROLE", "bytes32")]
	public class DefaultAdminRoleFunctionBase : FunctionMessage
	{

	}

	public partial class ModeratorRoleFunction : ModeratorRoleFunctionBase { }

	[Function("MODERATOR_ROLE", "bytes32")]
	public class ModeratorRoleFunctionBase : FunctionMessage
	{

	}

	public partial class GetRequiredRunesFunction : GetRequiredRunesFunctionBase { }

	[Function("_getRequiredRunes", "uint16[10]")]
	public class GetRequiredRunesFunctionBase : FunctionMessage
	{
		[Parameter("uint16", "_level", 1)]
		public virtual ushort Level { get; set; }
	}

	public partial class ActiveAttunementCrystalsFunction : ActiveAttunementCrystalsFunctionBase { }

	[Function("activeAttunementCrystals", "bool")]
	public class ActiveAttunementCrystalsFunctionBase : FunctionMessage
	{
		[Parameter("address", "", 1)]
		public virtual string ReturnValue1 { get; set; }
	}

	public partial class AddAttunementCrystalFunction : AddAttunementCrystalFunctionBase { }

	[Function("addAttunementCrystal")]
	public class AddAttunementCrystalFunctionBase : FunctionMessage
	{
		[Parameter("address", "_address", 1)]
		public virtual string Address { get; set; }
	}

	public partial class AdminRemoveFunction : AdminRemoveFunctionBase { }

	[Function("adminRemove")]
	public class AdminRemoveFunctionBase : FunctionMessage
	{
		[Parameter("uint256", "_heroId", 1)]
		public virtual BigInteger HeroId { get; set; }
	}

	public partial class CompleteMeditationFunction : CompleteMeditationFunctionBase { }

	[Function("completeMeditation")]
	public class CompleteMeditationFunctionBase : FunctionMessage
	{
		[Parameter("uint256", "_heroId", 1)]
		public virtual BigInteger HeroId { get; set; }
	}

	public partial class FeeAddressesFunction : FeeAddressesFunctionBase { }

	[Function("feeAddresses", "address")]
	public class FeeAddressesFunctionBase : FunctionMessage
	{
		[Parameter("uint256", "", 1)]
		public virtual BigInteger ReturnValue1 { get; set; }
	}

	public partial class FeePercentsFunction : FeePercentsFunctionBase { }

	[Function("feePercents", "uint256")]
	public class FeePercentsFunctionBase : FunctionMessage
	{
		[Parameter("uint256", "", 1)]
		public virtual BigInteger ReturnValue1 { get; set; }
	}

	public partial class GetActiveMeditationsFunction : GetActiveMeditationsFunctionBase { }

	[Function("getActiveMeditations", typeof(GetActiveMeditationsOutputDTO))]
	public class GetActiveMeditationsFunctionBase : FunctionMessage
	{
		[Parameter("address", "_address", 1)]
		public virtual string Address { get; set; }
	}

	public partial class GetHeroMeditationFunction : GetHeroMeditationFunctionBase { }

	[Function("getHeroMeditation", typeof(GetHeroMeditationOutputDTO))]
	public class GetHeroMeditationFunctionBase : FunctionMessage
	{
		[Parameter("uint256", "_heroId", 1)]
		public virtual BigInteger HeroId { get; set; }
	}

	public partial class GetMeditationFunction : GetMeditationFunctionBase { }

	[Function("getMeditation", typeof(GetMeditationOutputDTO))]
	public class GetMeditationFunctionBase : FunctionMessage
	{
		[Parameter("uint256", "_id", 1)]
		public virtual BigInteger Id { get; set; }
	}

	public partial class GetRoleAdminFunction : GetRoleAdminFunctionBase { }

	[Function("getRoleAdmin", "bytes32")]
	public class GetRoleAdminFunctionBase : FunctionMessage
	{
		[Parameter("bytes32", "role", 1)]
		public virtual byte[] Role { get; set; }
	}

	public partial class GrantRoleFunction : GrantRoleFunctionBase { }

	[Function("grantRole")]
	public class GrantRoleFunctionBase : FunctionMessage
	{
		[Parameter("bytes32", "role", 1)]
		public virtual byte[] Role { get; set; }
		[Parameter("address", "account", 2)]
		public virtual string Account { get; set; }
	}

	public partial class HasRoleFunction : HasRoleFunctionBase { }

	[Function("hasRole", "bool")]
	public class HasRoleFunctionBase : FunctionMessage
	{
		[Parameter("bytes32", "role", 1)]
		public virtual byte[] Role { get; set; }
		[Parameter("address", "account", 2)]
		public virtual string Account { get; set; }
	}

	public partial class HeroToMeditationFunction : HeroToMeditationFunctionBase { }

	[Function("heroToMeditation", "uint256")]
	public class HeroToMeditationFunctionBase : FunctionMessage
	{
		[Parameter("uint256", "", 1)]
		public virtual BigInteger ReturnValue1 { get; set; }
	}

	public partial class InitializeFunction : InitializeFunctionBase { }

	[Function("initialize")]
	public class InitializeFunctionBase : FunctionMessage
	{
		[Parameter("address", "_heroCoreAddress", 1)]
		public virtual string HeroCoreAddress { get; set; }
		[Parameter("address", "_statScienceAddress", 2)]
		public virtual string StatScienceAddress { get; set; }
		[Parameter("address", "_powerTokenAddress", 3)]
		public virtual string PowerTokenAddress { get; set; }
	}

	public partial class PauseFunction : PauseFunctionBase { }

	[Function("pause")]
	public class PauseFunctionBase : FunctionMessage
	{

	}

	public partial class PausedFunction : PausedFunctionBase { }

	[Function("paused", "bool")]
	public class PausedFunctionBase : FunctionMessage
	{

	}

	public partial class PowerTokenFunction : PowerTokenFunctionBase { }

	[Function("powerToken", "address")]
	public class PowerTokenFunctionBase : FunctionMessage
	{

	}

	public partial class ProfileActiveMeditationsFunction : ProfileActiveMeditationsFunctionBase { }

	[Function("profileActiveMeditations", typeof(ProfileActiveMeditationsOutputDTO))]
	public class ProfileActiveMeditationsFunctionBase : FunctionMessage
	{
		[Parameter("address", "", 1)]
		public virtual string ReturnValue1 { get; set; }
		[Parameter("uint256", "", 2)]
		public virtual BigInteger ReturnValue2 { get; set; }
	}

	public partial class RemoveAttunementCrystalFunction : RemoveAttunementCrystalFunctionBase { }

	[Function("removeAttunementCrystal")]
	public class RemoveAttunementCrystalFunctionBase : FunctionMessage
	{
		[Parameter("address", "_address", 1)]
		public virtual string Address { get; set; }
	}

	public partial class RenounceRoleFunction : RenounceRoleFunctionBase { }

	[Function("renounceRole")]
	public class RenounceRoleFunctionBase : FunctionMessage
	{
		[Parameter("bytes32", "role", 1)]
		public virtual byte[] Role { get; set; }
		[Parameter("address", "account", 2)]
		public virtual string Account { get; set; }
	}

	public partial class RevokeRoleFunction : RevokeRoleFunctionBase { }

	[Function("revokeRole")]
	public class RevokeRoleFunctionBase : FunctionMessage
	{
		[Parameter("bytes32", "role", 1)]
		public virtual byte[] Role { get; set; }
		[Parameter("address", "account", 2)]
		public virtual string Account { get; set; }
	}

	public partial class RunesFunction : RunesFunctionBase { }

	[Function("runes", "address")]
	public class RunesFunctionBase : FunctionMessage
	{
		[Parameter("uint256", "", 1)]
		public virtual BigInteger ReturnValue1 { get; set; }
	}

	public partial class SetFeesFunction : SetFeesFunctionBase { }

	[Function("setFees")]
	public class SetFeesFunctionBase : FunctionMessage
	{
		[Parameter("address[]", "_feeAddresses", 1)]
		public virtual List<string> FeeAddresses { get; set; }
		[Parameter("uint256[]", "_feePercents", 2)]
		public virtual List<BigInteger> FeePercents { get; set; }
	}

	public partial class SetFlagStorageFunction : SetFlagStorageFunctionBase { }

	[Function("setFlagStorage")]
	public class SetFlagStorageFunctionBase : FunctionMessage
	{
		[Parameter("address", "_flagStorageAddress", 1)]
		public virtual string FlagStorageAddress { get; set; }
	}

	public partial class SetRuneFunction : SetRuneFunctionBase { }

	[Function("setRune")]
	public class SetRuneFunctionBase : FunctionMessage
	{
		[Parameter("uint8", "_index", 1)]
		public virtual byte Index { get; set; }
		[Parameter("address", "_address", 2)]
		public virtual string Address { get; set; }
	}

	public partial class SetStatScienceAddressFunction : SetStatScienceAddressFunctionBase { }

	[Function("setStatScienceAddress")]
	public class SetStatScienceAddressFunctionBase : FunctionMessage
	{
		[Parameter("address", "_statScienceAddress", 1)]
		public virtual string StatScienceAddress { get; set; }
	}

	public partial class StartMeditationFunction : StartMeditationFunctionBase { }

	[Function("startMeditation")]
	public class StartMeditationFunctionBase : FunctionMessage
	{
		[Parameter("uint256", "_heroId", 1)]
		public virtual BigInteger HeroId { get; set; }
		[Parameter("uint8", "_primaryStat", 2)]
		public virtual byte PrimaryStat { get; set; }
		[Parameter("uint8", "_secondaryStat", 3)]
		public virtual byte SecondaryStat { get; set; }
		[Parameter("uint8", "_tertiaryStat", 4)]
		public virtual byte TertiaryStat { get; set; }
		[Parameter("address", "_attunementCrystal", 5)]
		public virtual string AttunementCrystal { get; set; }
	}

	public partial class SupportsInterfaceFunction : SupportsInterfaceFunctionBase { }

	[Function("supportsInterface", "bool")]
	public class SupportsInterfaceFunctionBase : FunctionMessage
	{
		[Parameter("bytes4", "interfaceId", 1)]
		public virtual byte[] InterfaceId { get; set; }
	}

	public partial class UnpauseFunction : UnpauseFunctionBase { }

	[Function("unpause")]
	public class UnpauseFunctionBase : FunctionMessage
	{

	}

	public partial class AttunementCrystalAddedEventDTO : AttunementCrystalAddedEventDTOBase { }

	[Event("AttunementCrystalAdded")]
	public class AttunementCrystalAddedEventDTOBase : IEventDTO
	{
		[Parameter("address", "atunementItemAddress", 1, false)]
		public virtual string AtunementItemAddress { get; set; }
	}

	public partial class InitializedEventDTO : InitializedEventDTOBase { }

	[Event("Initialized")]
	public class InitializedEventDTOBase : IEventDTO
	{
		[Parameter("uint8", "version", 1, false)]
		public virtual byte Version { get; set; }
	}

	public partial class LevelUpEventDTO : LevelUpEventDTOBase { }

	[Event("LevelUp")]
	public class LevelUpEventDTOBase : IEventDTO
	{
		[Parameter("address", "player", 1, true)]
		public virtual string Player { get; set; }
		[Parameter("uint256", "heroId", 2, true)]
		public virtual BigInteger HeroId { get; set; }
		[Parameter("tuple", "hero", 3, false)]
		public virtual Hero Hero { get; set; }
		[Parameter("tuple", "oldHero", 4, false)]
		public virtual Hero OldHero { get; set; }
	}

	public partial class MeditationBegunEventDTO : MeditationBegunEventDTOBase { }

	[Event("MeditationBegun")]
	public class MeditationBegunEventDTOBase : IEventDTO
	{
		[Parameter("address", "player", 1, true)]
		public virtual string Player { get; set; }
		[Parameter("uint256", "heroId", 2, true)]
		public virtual BigInteger HeroId { get; set; }
		[Parameter("uint256", "meditationId", 3, false)]
		public virtual BigInteger MeditationId { get; set; }
		[Parameter("uint8", "primaryStat", 4, false)]
		public virtual byte PrimaryStat { get; set; }
		[Parameter("uint8", "secondaryStat", 5, false)]
		public virtual byte SecondaryStat { get; set; }
		[Parameter("uint8", "tertiaryStat", 6, false)]
		public virtual byte TertiaryStat { get; set; }
		[Parameter("address", "attunementCrystal", 7, false)]
		public virtual string AttunementCrystal { get; set; }
	}

	public partial class MeditationCompletedEventDTO : MeditationCompletedEventDTOBase { }

	[Event("MeditationCompleted")]
	public class MeditationCompletedEventDTOBase : IEventDTO
	{
		[Parameter("address", "player", 1, true)]
		public virtual string Player { get; set; }
		[Parameter("uint256", "heroId", 2, true)]
		public virtual BigInteger HeroId { get; set; }
		[Parameter("uint256", "meditationId", 3, false)]
		public virtual BigInteger MeditationId { get; set; }
	}

	public partial class PausedEventDTO : PausedEventDTOBase { }

	[Event("Paused")]
	public class PausedEventDTOBase : IEventDTO
	{
		[Parameter("address", "account", 1, false)]
		public virtual string Account { get; set; }
	}

	public partial class RoleAdminChangedEventDTO : RoleAdminChangedEventDTOBase { }

	[Event("RoleAdminChanged")]
	public class RoleAdminChangedEventDTOBase : IEventDTO
	{
		[Parameter("bytes32", "role", 1, true)]
		public virtual byte[] Role { get; set; }
		[Parameter("bytes32", "previousAdminRole", 2, true)]
		public virtual byte[] PreviousAdminRole { get; set; }
		[Parameter("bytes32", "newAdminRole", 3, true)]
		public virtual byte[] NewAdminRole { get; set; }
	}

	public partial class RoleGrantedEventDTO : RoleGrantedEventDTOBase { }

	[Event("RoleGranted")]
	public class RoleGrantedEventDTOBase : IEventDTO
	{
		[Parameter("bytes32", "role", 1, true)]
		public virtual byte[] Role { get; set; }
		[Parameter("address", "account", 2, true)]
		public virtual string Account { get; set; }
		[Parameter("address", "sender", 3, true)]
		public virtual string Sender { get; set; }
	}

	public partial class RoleRevokedEventDTO : RoleRevokedEventDTOBase { }

	[Event("RoleRevoked")]
	public class RoleRevokedEventDTOBase : IEventDTO
	{
		[Parameter("bytes32", "role", 1, true)]
		public virtual byte[] Role { get; set; }
		[Parameter("address", "account", 2, true)]
		public virtual string Account { get; set; }
		[Parameter("address", "sender", 3, true)]
		public virtual string Sender { get; set; }
	}

	public partial class StatUpEventDTO : StatUpEventDTOBase { }

	[Event("StatUp")]
	public class StatUpEventDTOBase : IEventDTO
	{
		[Parameter("address", "player", 1, true)]
		public virtual string Player { get; set; }
		[Parameter("uint256", "heroId", 2, true)]
		public virtual BigInteger HeroId { get; set; }
		[Parameter("uint256", "stat", 3, false)]
		public virtual BigInteger Stat { get; set; }
		[Parameter("uint8", "increase", 4, false)]
		public virtual byte Increase { get; set; }
		[Parameter("uint8", "updateType", 5, false)]
		public virtual byte UpdateType { get; set; }
	}

	public partial class UnpausedEventDTO : UnpausedEventDTOBase { }

	[Event("Unpaused")]
	public class UnpausedEventDTOBase : IEventDTO
	{
		[Parameter("address", "account", 1, false)]
		public virtual string Account { get; set; }
	}

	public partial class DefaultAdminRoleOutputDTO : DefaultAdminRoleOutputDTOBase { }

	[FunctionOutput]
	public class DefaultAdminRoleOutputDTOBase : IFunctionOutputDTO
	{
		[Parameter("bytes32", "", 1)]
		public virtual byte[] ReturnValue1 { get; set; }
	}

	public partial class ModeratorRoleOutputDTO : ModeratorRoleOutputDTOBase { }

	[FunctionOutput]
	public class ModeratorRoleOutputDTOBase : IFunctionOutputDTO
	{
		[Parameter("bytes32", "", 1)]
		public virtual byte[] ReturnValue1 { get; set; }
	}

	public partial class GetRequiredRunesOutputDTO : GetRequiredRunesOutputDTOBase { }

	[FunctionOutput]
	public class GetRequiredRunesOutputDTOBase : IFunctionOutputDTO
	{
		[Parameter("uint16[10]", "", 1)]
		public virtual List<ushort> ReturnValue1 { get; set; }
	}

	public partial class ActiveAttunementCrystalsOutputDTO : ActiveAttunementCrystalsOutputDTOBase { }

	[FunctionOutput]
	public class ActiveAttunementCrystalsOutputDTOBase : IFunctionOutputDTO
	{
		[Parameter("bool", "", 1)]
		public virtual bool ReturnValue1 { get; set; }
	}







	public partial class FeeAddressesOutputDTO : FeeAddressesOutputDTOBase { }

	[FunctionOutput]
	public class FeeAddressesOutputDTOBase : IFunctionOutputDTO
	{
		[Parameter("address", "", 1)]
		public virtual string ReturnValue1 { get; set; }
	}

	public partial class FeePercentsOutputDTO : FeePercentsOutputDTOBase { }

	[FunctionOutput]
	public class FeePercentsOutputDTOBase : IFunctionOutputDTO
	{
		[Parameter("uint256", "", 1)]
		public virtual BigInteger ReturnValue1 { get; set; }
	}

	public partial class GetActiveMeditationsOutputDTO : GetActiveMeditationsOutputDTOBase { }

	[FunctionOutput]
	public class GetActiveMeditationsOutputDTOBase : IFunctionOutputDTO
	{
		[Parameter("tuple[]", "", 1)]
		public virtual List<Meditation> ReturnValue1 { get; set; }
	}

	public partial class GetHeroMeditationOutputDTO : GetHeroMeditationOutputDTOBase { }

	[FunctionOutput]
	public class GetHeroMeditationOutputDTOBase : IFunctionOutputDTO
	{
		[Parameter("tuple", "", 1)]
		public virtual Meditation ReturnValue1 { get; set; }
	}

	public partial class GetMeditationOutputDTO : GetMeditationOutputDTOBase { }

	[FunctionOutput]
	public class GetMeditationOutputDTOBase : IFunctionOutputDTO
	{
		[Parameter("tuple", "", 1)]
		public virtual Meditation ReturnValue1 { get; set; }
	}

	public partial class GetRoleAdminOutputDTO : GetRoleAdminOutputDTOBase { }

	[FunctionOutput]
	public class GetRoleAdminOutputDTOBase : IFunctionOutputDTO
	{
		[Parameter("bytes32", "", 1)]
		public virtual byte[] ReturnValue1 { get; set; }
	}



	public partial class HasRoleOutputDTO : HasRoleOutputDTOBase { }

	[FunctionOutput]
	public class HasRoleOutputDTOBase : IFunctionOutputDTO
	{
		[Parameter("bool", "", 1)]
		public virtual bool ReturnValue1 { get; set; }
	}

	public partial class HeroToMeditationOutputDTO : HeroToMeditationOutputDTOBase { }

	[FunctionOutput]
	public class HeroToMeditationOutputDTOBase : IFunctionOutputDTO
	{
		[Parameter("uint256", "", 1)]
		public virtual BigInteger ReturnValue1 { get; set; }
	}





	public partial class PausedOutputDTO : PausedOutputDTOBase { }

	[FunctionOutput]
	public class PausedOutputDTOBase : IFunctionOutputDTO
	{
		[Parameter("bool", "", 1)]
		public virtual bool ReturnValue1 { get; set; }
	}

	public partial class PowerTokenOutputDTO : PowerTokenOutputDTOBase { }

	[FunctionOutput]
	public class PowerTokenOutputDTOBase : IFunctionOutputDTO
	{
		[Parameter("address", "", 1)]
		public virtual string ReturnValue1 { get; set; }
	}

	public partial class ProfileActiveMeditationsOutputDTO : ProfileActiveMeditationsOutputDTOBase { }

	[FunctionOutput]
	public class ProfileActiveMeditationsOutputDTOBase : IFunctionOutputDTO
	{
		[Parameter("uint256", "id", 1)]
		public virtual BigInteger Id { get; set; }
		[Parameter("address", "player", 2)]
		public virtual string Player { get; set; }
		[Parameter("uint256", "heroId", 3)]
		public virtual BigInteger HeroId { get; set; }
		[Parameter("uint8", "primaryStat", 4)]
		public virtual byte PrimaryStat { get; set; }
		[Parameter("uint8", "secondaryStat", 5)]
		public virtual byte SecondaryStat { get; set; }
		[Parameter("uint8", "tertiaryStat", 6)]
		public virtual byte TertiaryStat { get; set; }
		[Parameter("address", "attunementCrystal", 7)]
		public virtual string AttunementCrystal { get; set; }
		[Parameter("uint256", "startBlock", 8)]
		public virtual BigInteger StartBlock { get; set; }
		[Parameter("uint8", "status", 9)]
		public virtual byte Status { get; set; }
	}







	public partial class RunesOutputDTO : RunesOutputDTOBase { }

	[FunctionOutput]
	public class RunesOutputDTOBase : IFunctionOutputDTO
	{
		[Parameter("address", "", 1)]
		public virtual string ReturnValue1 { get; set; }
	}











	public partial class SupportsInterfaceOutputDTO : SupportsInterfaceOutputDTOBase { }

	[FunctionOutput]
	public class SupportsInterfaceOutputDTOBase : IFunctionOutputDTO
	{
		[Parameter("bool", "", 1)]
		public virtual bool ReturnValue1 { get; set; }
	}


}
