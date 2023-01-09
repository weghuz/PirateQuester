using DFKContracts.QuestCore.ContractDefinition;
using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;
using System.Numerics;

namespace PirateQuester.Utils
{
	public class LogReader
	{
		public static async Task<List<EventLog<RewardMintedEventDTO>>> GetQuestRewardLogs(DFKAccount acc)
		{
			var block = await Functions.CurrentBlock(acc.Signer);
			int attempt = 0;
			bool retry = true;
			List<EventLog<RewardMintedEventDTO>> questEvents = new();
			while (retry)
			{
				try
				{
					var questRewardEvent = acc.Signer.Eth.GetEvent<RewardMintedEventDTO>(acc.Chain.QuestRewarder);
					questEvents = await questRewardEvent
						.GetAllChangesAsync(questRewardEvent.CreateFilterInput(null,
							new[] { acc.Account.Address },
							LongToBlock(long.Parse(block.ToString()) - 2048),
							new BlockParameter(block.ToHexBigInteger())));
					retry = false;
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
					if (attempt <= 5)
					{
						retry = true;
					}
					block -= 10;
					attempt++;
				}
			}
			return questEvents;
		}

		public static async Task<List<EventLog<QuestCompletedEventDTO>>> GetCompletedQuestsLogs(DFKAccount acc)
		{
			var block = await Functions.CurrentBlock(acc.Signer);
			int attempt = 0;
			bool retry = true;
			List<EventLog<QuestCompletedEventDTO>> questEvents = new();
			while (retry)
			{
				try
				{
					var completedRewardEvent = acc.Signer.Eth.GetEvent<QuestCompletedEventDTO>(acc.Chain.QuestAddress);
					questEvents = await completedRewardEvent
						.GetAllChangesAsync(completedRewardEvent.CreateFilterInput(null,
							new[] { acc.Account.Address },
							LongToBlock(long.Parse(block.ToString()) - 2048),
							new BlockParameter(block.ToHexBigInteger())));
					retry = false;
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
					if (attempt <= 5)
					{
						retry = true;
					}
					block -= 10;
					attempt++;
				}
			}
			return questEvents;
		}
		//Duno why this doesn't work.
		public static async Task<List<EventLog<QuestSkillUpEventDTO>>> GetSkillUpLogs(DFKAccount acc, BigInteger questId)
		{
			var block = await Functions.CurrentBlock(acc.Signer);
			int attempt = 0;
			bool retry = true;
			List<EventLog<QuestSkillUpEventDTO>> questEvents = new();
			while (retry)
			{
				try
				{
					var completedRewardEvent = acc.Signer.Eth.GetEvent<QuestSkillUpEventDTO>(acc.Chain.QuestAddress);
					questEvents = await completedRewardEvent
						.GetAllChangesAsync(completedRewardEvent.CreateFilterInput(null,
							new[] { questId.ToString() },
							LongToBlock(long.Parse(block.ToString()) - 2048),
							new BlockParameter(block.ToHexBigInteger())));
					retry = false;
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
					if (attempt <= 5)
					{
						retry = true;
					}
					block -= 10;
					attempt++;
				}
			}
			return questEvents;
		}

		public static BlockParameter LongToBlock(long block)
		{
			return new BlockParameter(new HexBigInteger(new BigInteger(block)));
		}
    }
}
