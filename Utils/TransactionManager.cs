using DFKContracts.HeroCore;
using DFKContracts.QuestCore.ContractDefinition;
using Nethereum.RPC.Eth.DTOs;
using PirateQuester.DFK.Contracts;
using DFK;
using PirateQuester.Utils;
using BigInteger = System.Numerics.BigInteger;
namespace Utils;

public class TransactionManager
{
	private readonly HeroCoreService _heroCore;

	public TransactionManager(HeroCoreService heroCore)
	{
		_heroCore = heroCore;
	}


	public async Task<TransactionReceipt> CompleteQuest(DFKAccount account, List<Hero> selectedHeroes)
	{
		Hero hero = selectedHeroes.FirstOrDefault();
		if(hero is null)
		{
			throw new NullReferenceException();
		}
		var receipt = await account.Quest.CompleteQuestRequestAndWaitForReceiptAsync(new BigInteger(long.Parse(hero.id)));
		Console.WriteLine(receipt.BlockNumber);

		return receipt;
	}

	public async Task<TransactionReceipt> StartQuest(DFKAccount account, List<Hero> selectedHeroes, QuestType questType, int attempts)
	{
		var quest = ContractDefinitions.GetQuestContract(questType);
		TransactionReceipt receipt;
		bool isApproved = await _heroCore.IsApprovedForAllQueryAsync(account.Account.Address, quest.Address);
		Console.WriteLine($"Is approved: {isApproved}");
		
		if (isApproved is false)
		{
			var approveReceipt = await account.Hero.SetApprovalForAllRequestAndWaitForReceiptAsync(quest.Address, true);
			Console.WriteLine($"Set Approved for {quest.Name}: {approveReceipt.BlockNumber}");
		}
		receipt = await account.Quest.StartQuestRequestAndWaitForReceiptAsync(new StartQuestFunction()
		{
			HeroIds = selectedHeroes.Select(hero => hero.id).ToList().ConvertAll(id => new BigInteger(long.Parse(id))),
			QuestAddress = quest.Address,
			Attempts = (byte)attempts,
			Level = (byte)quest.Level
		});
		return receipt;
	}
}