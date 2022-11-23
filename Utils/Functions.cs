using Nethereum.Web3;
using System.Numerics;

namespace PirateQuester.Utils
{
	public static class Functions
	{
		public static async Task<BigInteger> CurrentBlock(Web3 w3)
		{
			return await w3.Eth.Blocks.GetBlockNumber.SendRequestAsync();
		}
		public static ulong BigIntToLong(BigInteger bigInt)
		{
			return ulong.Parse(bigInt.ToString());
		}

		public static ulong UnixTime()
		{
			return (ulong)DateTimeOffset.UtcNow.ToUnixTimeSeconds();
		}

		public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
		{
			// Unix timestamp is seconds past epoch
			DateTime dateTime = new(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
			dateTime = dateTime.AddSeconds(unixTimeStamp);
			return dateTime;
		}
	}
}
