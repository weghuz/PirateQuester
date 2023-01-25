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

		public static string FormatThousndK(int num)
		{
			if (num >= 100000)
				return FormatThousndK(num / 1000) + "K";

			if (num >= 10000)
				return (num / 1000D).ToString("0.#") + "K";

			return num.ToString("#,0");
		}
	}
}
