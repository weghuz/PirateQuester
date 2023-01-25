using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using System.Numerics;

namespace DFKContracts.QuestCore.ContractDefinition
{


	public partial class QuestCoreDeployment : QuestCoreDeploymentBase
	{
		public QuestCoreDeployment() : base(BYTECODE) { }
		public QuestCoreDeployment(string byteCode) : base(byteCode) { }
	}

	public class QuestCoreDeploymentBase : ContractDeploymentMessage
	{
		public static string BYTECODE = "";
		public QuestCoreDeploymentBase() : base(BYTECODE) { }
		public QuestCoreDeploymentBase(string byteCode) : base(byteCode) { }

	}

	public partial class CancelerRoleFunction : CancelerRoleFunctionBase { }

	[Function("CANCELER_ROLE", "bytes32")]
	public class CancelerRoleFunctionBase : FunctionMessage
	{

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

	public partial class ActivateQuestFunction : ActivateQuestFunctionBase { }

	[Function("activateQuest")]
	public class ActivateQuestFunctionBase : FunctionMessage
	{
		[Parameter("address", "_questAddress", 1)]
		public virtual string QuestAddress { get; set; }
	}

	public partial class ActiveAccountQuestsFunction : ActiveAccountQuestsFunctionBase { }

	[Function("activeAccountQuests", typeof(ActiveAccountQuestsOutputDTO))]
	public class ActiveAccountQuestsFunctionBase : FunctionMessage
	{
		[Parameter("address", "", 1)]
		public virtual string ReturnValue1 { get; set; }
		[Parameter("address", "", 2)]
		public virtual string ReturnValue2 { get; set; }
		[Parameter("uint256", "", 3)]
		public virtual BigInteger ReturnValue3 { get; set; }
	}

	public partial class CancelQuestFunction : CancelQuestFunctionBase { }

	[Function("cancelQuest")]
	public class CancelQuestFunctionBase : FunctionMessage
	{
		[Parameter("uint256", "_heroId", 1)]
		public virtual BigInteger HeroId { get; set; }
	}

	public partial class ClearActiveQuestsFunction : ClearActiveQuestsFunctionBase { }

	[Function("clearActiveQuests")]
	public class ClearActiveQuestsFunctionBase : FunctionMessage
	{
		[Parameter("address", "_questAddress", 1)]
		public virtual string QuestAddress { get; set; }
	}

	public partial class ClearActiveQuestsAndHeroesFunction : ClearActiveQuestsAndHeroesFunctionBase { }

	[Function("clearActiveQuestsAndHeroes")]
	public class ClearActiveQuestsAndHeroesFunctionBase : FunctionMessage
	{

	}

	public partial class ClearActiveQuestsAndHeroesWithOffsetFunction : ClearActiveQuestsAndHeroesWithOffsetFunctionBase { }

	[Function("clearActiveQuestsAndHeroesWithOffset")]
	public class ClearActiveQuestsAndHeroesWithOffsetFunctionBase : FunctionMessage
	{
		[Parameter("uint256", "_offset", 1)]
		public virtual BigInteger Offset { get; set; }
		[Parameter("uint256", "_amount", 2)]
		public virtual BigInteger Amount { get; set; }
	}

	public partial class CompleteQuestFunction : CompleteQuestFunctionBase { }

	[Function("completeQuest")]
	public class CompleteQuestFunctionBase : FunctionMessage
	{
		[Parameter("uint256", "_heroId", 1)]
		public virtual BigInteger HeroId { get; set; }
	}

	public partial class GetAccountActiveQuestsFunction : GetAccountActiveQuestsFunctionBase { }

	[Function("getAccountActiveQuests", typeof(GetAccountActiveQuestsOutputDTO))]
	public class GetAccountActiveQuestsFunctionBase : FunctionMessage
	{
		[Parameter("address", "_account", 1)]
		public virtual string Account { get; set; }
	}

	public partial class GetCurrentStaminaFunction : GetCurrentStaminaFunctionBase { }

	[Function("getCurrentStamina", "uint256")]
	public class GetCurrentStaminaFunctionBase : FunctionMessage
	{
		[Parameter("uint256", "_heroId", 1)]
		public virtual BigInteger HeroId { get; set; }
	}

	public partial class GetHeroQuestFunction : GetHeroQuestFunctionBase { }

	[Function("getHeroQuest", typeof(GetHeroQuestOutputDTO))]
	public class GetHeroQuestFunctionBase : FunctionMessage
	{
		[Parameter("uint256", "heroId", 1)]
		public virtual BigInteger HeroId { get; set; }
	}

	public partial class GetQuestContractsFunction : GetQuestContractsFunctionBase { }

	[Function("getQuestContracts", "address[]")]
	public class GetQuestContractsFunctionBase : FunctionMessage
	{

	}

	public partial class GetRoleAdminFunction : GetRoleAdminFunctionBase { }

	[Function("getRoleAdmin", "bytes32")]
	public class GetRoleAdminFunctionBase : FunctionMessage
	{
		[Parameter("bytes32", "role", 1)]
		public virtual byte[] Role { get; set; }
	}

	public partial class GetRoleMemberFunction : GetRoleMemberFunctionBase { }

	[Function("getRoleMember", "address")]
	public class GetRoleMemberFunctionBase : FunctionMessage
	{
		[Parameter("bytes32", "role", 1)]
		public virtual byte[] Role { get; set; }
		[Parameter("uint256", "index", 2)]
		public virtual BigInteger Index { get; set; }
	}

	public partial class GetRoleMemberCountFunction : GetRoleMemberCountFunctionBase { }

	[Function("getRoleMemberCount", "uint256")]
	public class GetRoleMemberCountFunctionBase : FunctionMessage
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

	public partial class HeroCoreFunction : HeroCoreFunctionBase { }

	[Function("heroCore", "address")]
	public class HeroCoreFunctionBase : FunctionMessage
	{

	}

	public partial class HeroToQuestFunction : HeroToQuestFunctionBase { }

	[Function("heroToQuest", "uint256")]
	public class HeroToQuestFunctionBase : FunctionMessage
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
	}

	public partial class IsQuestFunction : IsQuestFunctionBase { }

	[Function("isQuest", "bool")]
	public class IsQuestFunctionBase : FunctionMessage
	{
		[Parameter("address", "", 1)]
		public virtual string ReturnValue1 { get; set; }
	}

	public partial class MultiCompleteQuestFunction : MultiCompleteQuestFunctionBase { }

	[Function("multiCompleteQuest")]
	public class MultiCompleteQuestFunctionBase : FunctionMessage
	{
		[Parameter("uint256[]", "_heroIds", 1)]
		public virtual List<BigInteger> HeroIds { get; set; }
	}

	public partial class MultiStartQuestFunction : MultiStartQuestFunctionBase { }

	[Function("multiStartQuest")]
	public class MultiStartQuestFunctionBase : FunctionMessage
	{
		[Parameter("address[]", "_questAddress", 1)]
		public virtual List<string> QuestAddress { get; set; }
		[Parameter("uint256[][]", "_heroIds", 2)]
		public virtual List<List<BigInteger>> HeroIds { get; set; }
		[Parameter("uint8[]", "_attempts", 3)]
		public virtual List<byte> Attempts { get; set; }
		[Parameter("uint8[]", "_level", 4)]
		public virtual List<byte> Level { get; set; }
	}

	public partial class PausedFunction : PausedFunctionBase { }

	[Function("paused", "bool")]
	public class PausedFunctionBase : FunctionMessage
	{

	}

	public partial class QuestCounterFunction : QuestCounterFunctionBase { }

	[Function("questCounter", "uint256")]
	public class QuestCounterFunctionBase : FunctionMessage
	{

	}

	public partial class QuestRewarderFunction : QuestRewarderFunctionBase { }

	[Function("questRewarder", "address")]
	public class QuestRewarderFunctionBase : FunctionMessage
	{

	}

	public partial class QuestsFunction : QuestsFunctionBase { }

	[Function("quests", typeof(QuestsOutputDTO))]
	public class QuestsFunctionBase : FunctionMessage
	{
		[Parameter("uint256", "", 1)]
		public virtual BigInteger ReturnValue1 { get; set; }
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

	public partial class SetQuestRewarderFunction : SetQuestRewarderFunctionBase { }

	[Function("setQuestRewarder")]
	public class SetQuestRewarderFunctionBase : FunctionMessage
	{
		[Parameter("address", "_questRewarder", 1)]
		public virtual string QuestRewarder { get; set; }
	}

	public partial class SetTimePerStaminaFunction : SetTimePerStaminaFunctionBase { }

	[Function("setTimePerStamina")]
	public class SetTimePerStaminaFunctionBase : FunctionMessage
	{
		[Parameter("uint256", "_timePerStamina", 1)]
		public virtual BigInteger TimePerStamina { get; set; }
	}

	public partial class StartQuestFunction : StartQuestFunctionBase { }

	[Function("startQuest")]
	public class StartQuestFunctionBase : FunctionMessage
	{
		[Parameter("uint256[]", "_heroIds", 1)]
		public virtual List<BigInteger> HeroIds { get; set; }
		[Parameter("address", "_questAddress", 2)]
		public virtual string QuestAddress { get; set; }
		[Parameter("uint8", "_attempts", 3)]
		public virtual byte Attempts { get; set; }
		[Parameter("uint8", "_level", 4)]
		public virtual byte Level { get; set; }
	}

	public partial class SupportsInterfaceFunction : SupportsInterfaceFunctionBase { }

	[Function("supportsInterface", "bool")]
	public class SupportsInterfaceFunctionBase : FunctionMessage
	{
		[Parameter("bytes4", "interfaceId", 1)]
		public virtual byte[] InterfaceId { get; set; }
	}

	public partial class TimePerStaminaFunction : TimePerStaminaFunctionBase { }

	[Function("timePerStamina", "uint256")]
	public class TimePerStaminaFunctionBase : FunctionMessage
	{

	}

	public partial class InitializedEventDTO : InitializedEventDTOBase { }

	[Event("Initialized")]
	public class InitializedEventDTOBase : IEventDTO
	{
		[Parameter("uint8", "version", 1, false)]
		public virtual byte Version { get; set; }
	}

	public partial class PausedEventDTO : PausedEventDTOBase { }

	[Event("Paused")]
	public class PausedEventDTOBase : IEventDTO
	{
		[Parameter("address", "account", 1, false)]
		public virtual string Account { get; set; }
	}

	public partial class QuestCanceledEventDTO : QuestCanceledEventDTOBase { }

	[Event("QuestCanceled")]
	public class QuestCanceledEventDTOBase : IEventDTO
	{
		[Parameter("uint256", "questId", 1, true)]
		public virtual BigInteger QuestId { get; set; }
		[Parameter("address", "player", 2, true)]
		public virtual string Player { get; set; }
		[Parameter("uint256", "heroId", 3, true)]
		public virtual BigInteger HeroId { get; set; }
		[Parameter("tuple", "quest", 4, false)]
		public virtual Quest Quest { get; set; }
	}

	public partial class QuestCompletedEventDTO : QuestCompletedEventDTOBase { }

	[Event("QuestCompleted")]
	public class QuestCompletedEventDTOBase : IEventDTO
	{
		[Parameter("uint256", "questId", 1, true)]
		public virtual BigInteger QuestId { get; set; }
		[Parameter("address", "player", 2, true)]
		public virtual string Player { get; set; }
		[Parameter("uint256", "heroId", 3, true)]
		public virtual BigInteger HeroId { get; set; }
		[Parameter("tuple", "quest", 4, false)]
		public virtual Quest Quest { get; set; }
	}

	public partial class QuestRewardEventDTO : QuestRewardEventDTOBase { }

	[Event("QuestReward")]
	public class QuestRewardEventDTOBase : IEventDTO
	{
		[Parameter("uint256", "questId", 1, true)]
		public virtual BigInteger QuestId { get; set; }
		[Parameter("address", "player", 2, true)]
		public virtual string Player { get; set; }
		[Parameter("uint256", "heroId", 3, false)]
		public virtual BigInteger HeroId { get; set; }
		[Parameter("address", "rewardItem", 4, false)]
		public virtual string RewardItem { get; set; }
		[Parameter("uint256", "itemQuantity", 5, false)]
		public virtual BigInteger ItemQuantity { get; set; }
	}

	public partial class QuestSkillUpEventDTO : QuestSkillUpEventDTOBase { }

	[Event("QuestSkillUp")]
	public class QuestSkillUpEventDTOBase : IEventDTO
	{
		[Parameter("uint256", "questId", 1, true)]
		public virtual BigInteger QuestId { get; set; }
		[Parameter("address", "player", 2, true)]
		public virtual string Player { get; set; }
		[Parameter("uint256", "heroId", 3, false)]
		public virtual BigInteger HeroId { get; set; }
		[Parameter("uint8", "profession", 4, false)]
		public virtual byte Profession { get; set; }
		[Parameter("uint16", "skillUp", 5, false)]
		public virtual ushort SkillUp { get; set; }
	}

	public partial class QuestStaminaSpentEventDTO : QuestStaminaSpentEventDTOBase { }

	[Event("QuestStaminaSpent")]
	public class QuestStaminaSpentEventDTOBase : IEventDTO
	{
		[Parameter("uint256", "questId", 1, true)]
		public virtual BigInteger QuestId { get; set; }
		[Parameter("address", "player", 2, true)]
		public virtual string Player { get; set; }
		[Parameter("uint256", "heroId", 3, false)]
		public virtual BigInteger HeroId { get; set; }
		[Parameter("uint256", "staminaFullAt", 4, false)]
		public virtual BigInteger StaminaFullAt { get; set; }
		[Parameter("uint256", "staminaSpent", 5, false)]
		public virtual BigInteger StaminaSpent { get; set; }
	}

	public partial class QuestStartedEventDTO : QuestStartedEventDTOBase { }

	[Event("QuestStarted")]
	public class QuestStartedEventDTOBase : IEventDTO
	{
		[Parameter("uint256", "questId", 1, true)]
		public virtual BigInteger QuestId { get; set; }
		[Parameter("address", "player", 2, true)]
		public virtual string Player { get; set; }
		[Parameter("uint256", "heroId", 3, true)]
		public virtual BigInteger HeroId { get; set; }
		[Parameter("tuple", "quest", 4, false)]
		public virtual Quest Quest { get; set; }
		[Parameter("uint256", "startAtTime", 5, false)]
		public virtual BigInteger StartAtTime { get; set; }
		[Parameter("uint256", "completeAtTime", 6, false)]
		public virtual BigInteger CompleteAtTime { get; set; }
	}

	public partial class QuestXPEventDTO : QuestXPEventDTOBase { }

	[Event("QuestXP")]
	public class QuestXPEventDTOBase : IEventDTO
	{
		[Parameter("uint256", "questId", 1, true)]
		public virtual BigInteger QuestId { get; set; }
		[Parameter("address", "player", 2, true)]
		public virtual string Player { get; set; }
		[Parameter("uint256", "heroId", 3, false)]
		public virtual BigInteger HeroId { get; set; }
		[Parameter("uint64", "xpEarned", 4, false)]
		public virtual ulong XpEarned { get; set; }
	}

	public partial class RewardMintedEventDTO : RewardMintedEventDTOBase { }

	[Event("RewardMinted")]
	public class RewardMintedEventDTOBase : IEventDTO
	{
		[Parameter("uint256", "questId", 1, true)]
		public virtual BigInteger QuestId { get; set; }
		[Parameter("address", "player", 2, true)]
		public virtual string Player { get; set; }
		[Parameter("uint256", "heroId", 3, false)]
		public virtual BigInteger HeroId { get; set; }
		[Parameter("address", "reward", 4, true)]
		public virtual string Reward { get; set; }
		[Parameter("uint256", "amount", 5, false)]
		public virtual BigInteger Amount { get; set; }
		[Parameter("uint256", "data", 6, false)]
		public virtual BigInteger Data { get; set; }
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

	public partial class TrainingAttemptDoneEventDTO : TrainingAttemptDoneEventDTOBase { }

	[Event("TrainingAttemptDone")]
	public class TrainingAttemptDoneEventDTOBase : IEventDTO
	{
		[Parameter("bool", "success", 1, false)]
		public virtual bool Success { get; set; }
		[Parameter("uint256", "attempt", 2, false)]
		public virtual BigInteger Attempt { get; set; }
		[Parameter("uint256", "heroId", 3, true)]
		public virtual BigInteger HeroId { get; set; }
	}

	public partial class TrainingSuccessRatioEventDTO : TrainingSuccessRatioEventDTOBase { }

	[Event("TrainingSuccessRatio")]
	public class TrainingSuccessRatioEventDTOBase : IEventDTO
	{
		[Parameter("uint256", "winCount", 1, false)]
		public virtual BigInteger WinCount { get; set; }
		[Parameter("uint256", "attempts", 2, false)]
		public virtual BigInteger Attempts { get; set; }
		[Parameter("uint256", "heroId", 3, true)]
		public virtual BigInteger HeroId { get; set; }
	}

	public partial class UnpausedEventDTO : UnpausedEventDTOBase { }

	[Event("Unpaused")]
	public class UnpausedEventDTOBase : IEventDTO
	{
		[Parameter("address", "account", 1, false)]
		public virtual string Account { get; set; }
	}

	public partial class CancelerRoleOutputDTO : CancelerRoleOutputDTOBase { }

	[FunctionOutput]
	public class CancelerRoleOutputDTOBase : IFunctionOutputDTO
	{
		[Parameter("bytes32", "", 1)]
		public virtual byte[] ReturnValue1 { get; set; }
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



	public partial class ActiveAccountQuestsOutputDTO : ActiveAccountQuestsOutputDTOBase { }

	[FunctionOutput]
	public class ActiveAccountQuestsOutputDTOBase : IFunctionOutputDTO
	{
		[Parameter("uint256", "id", 1)]
		public virtual BigInteger Id { get; set; }
		[Parameter("address", "questAddress", 2)]
		public virtual string QuestAddress { get; set; }
		[Parameter("uint8", "level", 3)]
		public virtual byte Level { get; set; }
		[Parameter("address", "player", 4)]
		public virtual string Player { get; set; }
		[Parameter("uint256", "startBlock", 5)]
		public virtual BigInteger StartBlock { get; set; }
		[Parameter("uint256", "startAtTime", 6)]
		public virtual BigInteger StartAtTime { get; set; }
		[Parameter("uint256", "completeAtTime", 7)]
		public virtual BigInteger CompleteAtTime { get; set; }
		[Parameter("uint8", "attempts", 8)]
		public virtual byte Attempts { get; set; }
		[Parameter("uint8", "status", 9)]
		public virtual byte Status { get; set; }
	}











	public partial class GetAccountActiveQuestsOutputDTO : GetAccountActiveQuestsOutputDTOBase { }

	[FunctionOutput]
	public class GetAccountActiveQuestsOutputDTOBase : IFunctionOutputDTO
	{
		[Parameter("tuple[]", "", 1)]
		public virtual List<Quest> ReturnValue1 { get; set; }
	}

	public partial class GetCurrentStaminaOutputDTO : GetCurrentStaminaOutputDTOBase { }

	[FunctionOutput]
	public class GetCurrentStaminaOutputDTOBase : IFunctionOutputDTO
	{
		[Parameter("uint256", "", 1)]
		public virtual BigInteger ReturnValue1 { get; set; }
	}

	public partial class GetHeroQuestOutputDTO : GetHeroQuestOutputDTOBase { }

	[FunctionOutput]
	public class GetHeroQuestOutputDTOBase : IFunctionOutputDTO
	{
		[Parameter("tuple", "", 1)]
		public virtual Quest ReturnValue1 { get; set; }
	}

	public partial class GetQuestContractsOutputDTO : GetQuestContractsOutputDTOBase { }

	[FunctionOutput]
	public class GetQuestContractsOutputDTOBase : IFunctionOutputDTO
	{
		[Parameter("address[]", "", 1)]
		public virtual List<string> ReturnValue1 { get; set; }
	}

	public partial class GetRoleAdminOutputDTO : GetRoleAdminOutputDTOBase { }

	[FunctionOutput]
	public class GetRoleAdminOutputDTOBase : IFunctionOutputDTO
	{
		[Parameter("bytes32", "", 1)]
		public virtual byte[] ReturnValue1 { get; set; }
	}

	public partial class GetRoleMemberOutputDTO : GetRoleMemberOutputDTOBase { }

	[FunctionOutput]
	public class GetRoleMemberOutputDTOBase : IFunctionOutputDTO
	{
		[Parameter("address", "", 1)]
		public virtual string ReturnValue1 { get; set; }
	}

	public partial class GetRoleMemberCountOutputDTO : GetRoleMemberCountOutputDTOBase { }

	[FunctionOutput]
	public class GetRoleMemberCountOutputDTOBase : IFunctionOutputDTO
	{
		[Parameter("uint256", "", 1)]
		public virtual BigInteger ReturnValue1 { get; set; }
	}



	public partial class HasRoleOutputDTO : HasRoleOutputDTOBase { }

	[FunctionOutput]
	public class HasRoleOutputDTOBase : IFunctionOutputDTO
	{
		[Parameter("bool", "", 1)]
		public virtual bool ReturnValue1 { get; set; }
	}

	public partial class HeroCoreOutputDTO : HeroCoreOutputDTOBase { }

	[FunctionOutput]
	public class HeroCoreOutputDTOBase : IFunctionOutputDTO
	{
		[Parameter("address", "", 1)]
		public virtual string ReturnValue1 { get; set; }
	}

	public partial class HeroToQuestOutputDTO : HeroToQuestOutputDTOBase { }

	[FunctionOutput]
	public class HeroToQuestOutputDTOBase : IFunctionOutputDTO
	{
		[Parameter("uint256", "", 1)]
		public virtual BigInteger ReturnValue1 { get; set; }
	}



	public partial class IsQuestOutputDTO : IsQuestOutputDTOBase { }

	[FunctionOutput]
	public class IsQuestOutputDTOBase : IFunctionOutputDTO
	{
		[Parameter("bool", "", 1)]
		public virtual bool ReturnValue1 { get; set; }
	}





	public partial class PausedOutputDTO : PausedOutputDTOBase { }

	[FunctionOutput]
	public class PausedOutputDTOBase : IFunctionOutputDTO
	{
		[Parameter("bool", "", 1)]
		public virtual bool ReturnValue1 { get; set; }
	}

	public partial class QuestCounterOutputDTO : QuestCounterOutputDTOBase { }

	[FunctionOutput]
	public class QuestCounterOutputDTOBase : IFunctionOutputDTO
	{
		[Parameter("uint256", "", 1)]
		public virtual BigInteger ReturnValue1 { get; set; }
	}

	public partial class QuestRewarderOutputDTO : QuestRewarderOutputDTOBase { }

	[FunctionOutput]
	public class QuestRewarderOutputDTOBase : IFunctionOutputDTO
	{
		[Parameter("address", "", 1)]
		public virtual string ReturnValue1 { get; set; }
	}

	public partial class QuestsOutputDTO : QuestsOutputDTOBase { }

	[FunctionOutput]
	public class QuestsOutputDTOBase : IFunctionOutputDTO
	{
		[Parameter("uint256", "id", 1)]
		public virtual BigInteger Id { get; set; }
		[Parameter("address", "questAddress", 2)]
		public virtual string QuestAddress { get; set; }
		[Parameter("uint8", "level", 3)]
		public virtual byte Level { get; set; }
		[Parameter("address", "player", 4)]
		public virtual string Player { get; set; }
		[Parameter("uint256", "startBlock", 5)]
		public virtual BigInteger StartBlock { get; set; }
		[Parameter("uint256", "startAtTime", 6)]
		public virtual BigInteger StartAtTime { get; set; }
		[Parameter("uint256", "completeAtTime", 7)]
		public virtual BigInteger CompleteAtTime { get; set; }
		[Parameter("uint8", "attempts", 8)]
		public virtual byte Attempts { get; set; }
		[Parameter("uint8", "status", 9)]
		public virtual byte Status { get; set; }
	}











	public partial class SupportsInterfaceOutputDTO : SupportsInterfaceOutputDTOBase { }

	[FunctionOutput]
	public class SupportsInterfaceOutputDTOBase : IFunctionOutputDTO
	{
		[Parameter("bool", "", 1)]
		public virtual bool ReturnValue1 { get; set; }
	}

	public partial class TimePerStaminaOutputDTO : TimePerStaminaOutputDTOBase { }

	[FunctionOutput]
	public class TimePerStaminaOutputDTOBase : IFunctionOutputDTO
	{
		[Parameter("uint256", "", 1)]
		public virtual BigInteger ReturnValue1 { get; set; }
	}
}
