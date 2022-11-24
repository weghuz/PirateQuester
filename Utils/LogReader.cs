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
			var questRewardEvent = acc.Signer.Eth.GetEvent<RewardMintedEventDTO>("0x08D93Db24B783F8eBb68D7604bF358F5027330A6");
			var questEvents = await questRewardEvent
				.GetAllChangesAsync(questRewardEvent.CreateFilterInput(null,
					new[] { acc.Account.Address },
					LongToBlock(long.Parse(block.ToString()) - 2048),
					new BlockParameter(block.ToHexBigInteger())));
			return questEvents;
		}

		public static BlockParameter LongToBlock(long block)
		{
			return new BlockParameter(new HexBigInteger(new BigInteger(block)));
		}
    }
}
