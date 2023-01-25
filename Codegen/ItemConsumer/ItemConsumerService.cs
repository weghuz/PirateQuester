using Nethereum.Contracts.ContractHandlers;
using Nethereum.RPC.Eth.DTOs;
using PirateQuester.ItemConsumer.ContractDefinition;
using System.Numerics;

namespace PirateQuester.ItemConsumer
{
	public partial class ItemConsumerService
	{
		public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, ItemConsumerDeployment itemConsumerDeployment, CancellationTokenSource cancellationTokenSource = null)
		{
			return web3.Eth.GetContractDeploymentHandler<ItemConsumerDeployment>().SendRequestAndWaitForReceiptAsync(itemConsumerDeployment, cancellationTokenSource);
		}

		public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, ItemConsumerDeployment itemConsumerDeployment)
		{
			return web3.Eth.GetContractDeploymentHandler<ItemConsumerDeployment>().SendRequestAsync(itemConsumerDeployment);
		}

		public static async Task<ItemConsumerService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, ItemConsumerDeployment itemConsumerDeployment, CancellationTokenSource cancellationTokenSource = null)
		{
			var receipt = await DeployContractAndWaitForReceiptAsync(web3, itemConsumerDeployment, cancellationTokenSource);
			return new ItemConsumerService(web3, receipt.ContractAddress);
		}

		protected Nethereum.Web3.Web3 Web3 { get; }

		public ContractHandler ContractHandler { get; }

		public ItemConsumerService(Nethereum.Web3.Web3 web3, string contractAddress)
		{
			Web3 = web3;
			ContractHandler = web3.Eth.GetContractHandler(contractAddress);
		}

		public Task<string> ConsumeItemRequestAsync(ConsumeItemFunction consumeItemFunction)
		{
			return ContractHandler.SendRequestAsync(consumeItemFunction);
		}

		public Task<TransactionReceipt> ConsumeItemRequestAndWaitForReceiptAsync(ConsumeItemFunction consumeItemFunction, CancellationTokenSource cancellationToken = null)
		{
			return ContractHandler.SendRequestAndWaitForReceiptAsync(consumeItemFunction, cancellationToken);
		}

		public Task<string> ConsumeItemRequestAsync(string consumableAddress, BigInteger heroId)
		{
			var consumeItemFunction = new ConsumeItemFunction();
			consumeItemFunction.ConsumableAddress = consumableAddress;
			consumeItemFunction.HeroId = heroId;

			return ContractHandler.SendRequestAsync(consumeItemFunction);
		}

		public Task<TransactionReceipt> ConsumeItemRequestAndWaitForReceiptAsync(string consumableAddress, BigInteger heroId, CancellationTokenSource cancellationToken = null)
		{
			var consumeItemFunction = new ConsumeItemFunction();
			consumeItemFunction.ConsumableAddress = consumableAddress;
			consumeItemFunction.HeroId = heroId;

			return ContractHandler.SendRequestAndWaitForReceiptAsync(consumeItemFunction, cancellationToken);
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
	}
}
