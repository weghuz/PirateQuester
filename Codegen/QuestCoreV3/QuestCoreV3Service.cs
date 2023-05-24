using Nethereum.Contracts.ContractHandlers;
using Nethereum.RPC.Eth.DTOs;
using PirateQuester.QuestCoreV3.ContractDefinition;
using System.Numerics;

namespace PirateQuester.QuestCoreV3
{
    public partial class QuestCoreV3Service
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, QuestCoreV3Deployment questCoreV3Deployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<QuestCoreV3Deployment>().SendRequestAndWaitForReceiptAsync(questCoreV3Deployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, QuestCoreV3Deployment questCoreV3Deployment)
        {
            return web3.Eth.GetContractDeploymentHandler<QuestCoreV3Deployment>().SendRequestAsync(questCoreV3Deployment);
        }

        public static async Task<QuestCoreV3Service> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, QuestCoreV3Deployment questCoreV3Deployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, questCoreV3Deployment, cancellationTokenSource);
            return new QuestCoreV3Service(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.Web3 Web3 { get; }

        public ContractHandler ContractHandler { get; }

        public QuestCoreV3Service(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<string> ClearActiveQuestsRequestAsync(ClearActiveQuestsFunction clearActiveQuestsFunction)
        {
            return ContractHandler.SendRequestAsync(clearActiveQuestsFunction);
        }

        public Task<TransactionReceipt> ClearActiveQuestsRequestAndWaitForReceiptAsync(ClearActiveQuestsFunction clearActiveQuestsFunction, CancellationTokenSource cancellationToken = null)
        {
            return ContractHandler.SendRequestAndWaitForReceiptAsync(clearActiveQuestsFunction, cancellationToken);
        }

        public Task<string> ClearActiveQuestsRequestAsync(BigInteger questInstanceId)
        {
            var clearActiveQuestsFunction = new ClearActiveQuestsFunction();
            clearActiveQuestsFunction.QuestInstanceId = questInstanceId;

            return ContractHandler.SendRequestAsync(clearActiveQuestsFunction);
        }

        public Task<TransactionReceipt> ClearActiveQuestsRequestAndWaitForReceiptAsync(BigInteger questInstanceId, CancellationTokenSource cancellationToken = null)
        {
            var clearActiveQuestsFunction = new ClearActiveQuestsFunction();
            clearActiveQuestsFunction.QuestInstanceId = questInstanceId;

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

        public Task<List<BigInteger>> GetQuestInstanceIdsQueryAsync(GetQuestInstanceIdsFunction getQuestInstanceIdsFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetQuestInstanceIdsFunction, List<BigInteger>>(getQuestInstanceIdsFunction, blockParameter);
        }


        public Task<List<BigInteger>> GetQuestInstanceIdsQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetQuestInstanceIdsFunction, List<BigInteger>>(null, blockParameter);
        }

        public Task<BigInteger> HeroToQuestQueryAsync(HeroToQuestFunction heroToQuestFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<HeroToQuestFunction, BigInteger>(heroToQuestFunction, blockParameter);
        }


        public Task<BigInteger> HeroToQuestQueryAsync(BigInteger heroId, BlockParameter blockParameter = null)
        {
            var heroToQuestFunction = new HeroToQuestFunction();
            heroToQuestFunction.HeroId = heroId;

            return ContractHandler.QueryAsync<HeroToQuestFunction, BigInteger>(heroToQuestFunction, blockParameter);
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

        public Task<string> MultiStartQuestRequestAsync(List<List<BigInteger>> heroIds, List<BigInteger> questInstanceId, List<byte> attempts, List<byte> level, List<byte> type)
        {
            var multiStartQuestFunction = new MultiStartQuestFunction();
            multiStartQuestFunction.HeroIds = heroIds;
            multiStartQuestFunction.QuestInstanceId = questInstanceId;
            multiStartQuestFunction.Attempts = attempts;
            multiStartQuestFunction.Level = level;
            multiStartQuestFunction.Type = type;

            return ContractHandler.SendRequestAsync(multiStartQuestFunction);
        }

        public Task<TransactionReceipt> MultiStartQuestRequestAndWaitForReceiptAsync(List<List<BigInteger>> heroIds, List<BigInteger> questInstanceId, List<byte> attempts, List<byte> level, List<byte> type, CancellationTokenSource cancellationToken = null)
        {
            var multiStartQuestFunction = new MultiStartQuestFunction();
            multiStartQuestFunction.HeroIds = heroIds;
            multiStartQuestFunction.QuestInstanceId = questInstanceId;
            multiStartQuestFunction.Attempts = attempts;
            multiStartQuestFunction.Level = level;
            multiStartQuestFunction.Type = type;

            return ContractHandler.SendRequestAndWaitForReceiptAsync(multiStartQuestFunction, cancellationToken);
        }

        public Task<BigInteger> QuestCounterQueryAsync(QuestCounterFunction questCounterFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<QuestCounterFunction, BigInteger>(questCounterFunction, blockParameter);
        }


        public Task<BigInteger> QuestCounterQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<QuestCounterFunction, BigInteger>(null, blockParameter);
        }

        public Task<QuestsOutputDTO> QuestsQueryAsync(QuestsFunction questsFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<QuestsFunction, QuestsOutputDTO>(questsFunction, blockParameter);
        }

        public Task<QuestsOutputDTO> QuestsQueryAsync(BigInteger id, BlockParameter blockParameter = null)
        {
            var questsFunction = new QuestsFunction();
            questsFunction.Id = id;

            return ContractHandler.QueryDeserializingToObjectAsync<QuestsFunction, QuestsOutputDTO>(questsFunction, blockParameter);
        }

        public Task<string> StartQuestRequestAsync(StartQuestFunction startQuestFunction)
        {
            return ContractHandler.SendRequestAsync(startQuestFunction);
        }

        public Task<TransactionReceipt> StartQuestRequestAndWaitForReceiptAsync(StartQuestFunction startQuestFunction, CancellationTokenSource cancellationToken = null)
        {
            return ContractHandler.SendRequestAndWaitForReceiptAsync(startQuestFunction, cancellationToken);
        }

        public Task<string> StartQuestRequestAsync(List<BigInteger> heroIds, BigInteger questInstanceId, byte attempts, byte level, byte type)
        {
            var startQuestFunction = new StartQuestFunction();
            startQuestFunction.HeroIds = heroIds;
            startQuestFunction.QuestInstanceId = questInstanceId;
            startQuestFunction.Attempts = attempts;
            startQuestFunction.Level = level;
            startQuestFunction.Type = type;

            return ContractHandler.SendRequestAsync(startQuestFunction);
        }

        public Task<TransactionReceipt> StartQuestRequestAndWaitForReceiptAsync(List<BigInteger> heroIds, BigInteger questInstanceId, byte attempts, byte level, byte type, CancellationTokenSource cancellationToken = null)
        {
            var startQuestFunction = new StartQuestFunction();
            startQuestFunction.HeroIds = heroIds;
            startQuestFunction.QuestInstanceId = questInstanceId;
            startQuestFunction.Attempts = attempts;
            startQuestFunction.Level = level;
            startQuestFunction.Type = type;

            return ContractHandler.SendRequestAndWaitForReceiptAsync(startQuestFunction, cancellationToken);
        }
    }
}
