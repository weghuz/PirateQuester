using DFK;
using Nethereum.Web3.Accounts;

namespace Utils
{
	public class DFKAccount
	{
        public Account Account { get; set; }
        public string Name { get; set; }
        public List<Hero> Heroes { get; set; }
    }
}