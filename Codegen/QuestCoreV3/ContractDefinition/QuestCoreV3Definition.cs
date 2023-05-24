using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using System.Numerics;

namespace PirateQuester.QuestCoreV3.ContractDefinition
{


    public partial class QuestCoreV3Deployment : QuestCoreV3DeploymentBase
    {
        public QuestCoreV3Deployment() : base(BYTECODE) { }
        public QuestCoreV3Deployment(string byteCode) : base(byteCode) { }
    }

    public class QuestCoreV3DeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "";
        public QuestCoreV3DeploymentBase() : base(BYTECODE) { }
        public QuestCoreV3DeploymentBase(string byteCode) : base(byteCode) { }

    }

    public partial class ClearActiveQuestsFunction : ClearActiveQuestsFunctionBase { }

    [Function("clearActiveQuests")]
    public class ClearActiveQuestsFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "_questInstanceId", 1)]
        public virtual BigInteger QuestInstanceId { get; set; }
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

    public partial class CancelQuestFunction : CancelQuestFunctionBase { }

    [Function("cancelQuest")]
    public class CancelQuestFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "_heroId", 1)]
        public virtual BigInteger HeroId { get; set; }
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

    public partial class GetQuestInstanceIdsFunction : GetQuestInstanceIdsFunctionBase { }

    [Function("getQuestInstanceIds", "uint256[]")]
    public class GetQuestInstanceIdsFunctionBase : FunctionMessage
    {

    }

    public partial class HeroToQuestFunction : HeroToQuestFunctionBase { }

    [Function("heroToQuest", "uint256")]
    public class HeroToQuestFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "_heroId", 1)]
        public virtual BigInteger HeroId { get; set; }
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
        [Parameter("uint256[][]", "_heroIds", 1)]
        public virtual List<List<BigInteger>> HeroIds { get; set; }
        [Parameter("uint256[]", "_questInstanceId", 2)]
        public virtual List<BigInteger> QuestInstanceId { get; set; }
        [Parameter("uint8[]", "_attempts", 3)]
        public virtual List<byte> Attempts { get; set; }
        [Parameter("uint8[]", "_level", 4)]
        public virtual List<byte> Level { get; set; }
        [Parameter("uint8[]", "_type", 5)]
        public virtual List<byte> Type { get; set; }
    }

    public partial class QuestCounterFunction : QuestCounterFunctionBase { }

    [Function("questCounter", "uint256")]
    public class QuestCounterFunctionBase : FunctionMessage
    {

    }

    public partial class QuestsFunction : QuestsFunctionBase { }

    [Function("quests", typeof(QuestsOutputDTO))]
    public class QuestsFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "_id", 1)]
        public virtual BigInteger Id { get; set; }
    }

    public partial class StartQuestFunction : StartQuestFunctionBase { }

    [Function("startQuest")]
    public class StartQuestFunctionBase : FunctionMessage
    {
        [Parameter("uint256[]", "_heroIds", 1)]
        public virtual List<BigInteger> HeroIds { get; set; }
        [Parameter("uint256", "_questInstanceId", 2)]
        public virtual BigInteger QuestInstanceId { get; set; }
        [Parameter("uint8", "_attempts", 3)]
        public virtual byte Attempts { get; set; }
        [Parameter("uint8", "_level", 4)]
        public virtual byte Level { get; set; }
        [Parameter("uint8", "_type", 5)]
        public virtual byte Type { get; set; }
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

    public partial class QuickStudyEventDTO : QuickStudyEventDTOBase { }

    [Event("QuickStudy")]
    public class QuickStudyEventDTOBase : IEventDTO
    {
        [Parameter("uint256", "questId", 1, true)]
        public virtual BigInteger QuestId { get; set; }
        [Parameter("address", "player", 2, true)]
        public virtual string Player { get; set; }
        [Parameter("uint256", "heroId", 3, false)]
        public virtual BigInteger HeroId { get; set; }
        [Parameter("uint256", "xpBefore", 4, false)]
        public virtual BigInteger XpBefore { get; set; }
        [Parameter("uint256", "xpAfter", 5, false)]
        public virtual BigInteger XpAfter { get; set; }
        [Parameter("uint256", "percentage", 6, false)]
        public virtual BigInteger Percentage { get; set; }
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

    public partial class TokenBonusAwardedEventDTO : TokenBonusAwardedEventDTOBase { }

    [Event("TokenBonusAwarded")]
    public class TokenBonusAwardedEventDTOBase : IEventDTO
    {
        [Parameter("uint256", "questId", 1, true)]
        public virtual BigInteger QuestId { get; set; }
        [Parameter("address", "player", 2, true)]
        public virtual string Player { get; set; }
        [Parameter("uint256", "heroId", 3, false)]
        public virtual BigInteger HeroId { get; set; }
        [Parameter("uint256", "amount", 4, false)]
        public virtual BigInteger Amount { get; set; }
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

    public partial class GetQuestInstanceIdsOutputDTO : GetQuestInstanceIdsOutputDTOBase { }

    [FunctionOutput]
    public class GetQuestInstanceIdsOutputDTOBase : IFunctionOutputDTO
    {
        [Parameter("uint256[]", "", 1)]
        public virtual List<BigInteger> ReturnValue1 { get; set; }
    }

    public partial class HeroToQuestOutputDTO : HeroToQuestOutputDTOBase { }

    [FunctionOutput]
    public class HeroToQuestOutputDTOBase : IFunctionOutputDTO
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }





    public partial class QuestCounterOutputDTO : QuestCounterOutputDTOBase { }

    [FunctionOutput]
    public class QuestCounterOutputDTOBase : IFunctionOutputDTO
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class QuestsOutputDTO : QuestsOutputDTOBase { }

    [FunctionOutput]
    public class QuestsOutputDTOBase : IFunctionOutputDTO
    {
        [Parameter("tuple", "", 1)]
        public virtual Quest ReturnValue1 { get; set; }
    }


}
