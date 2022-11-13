using DFK;
using Microsoft.JSInterop;
using Nethereum.Web3.Accounts;
using Newtonsoft.Json;
namespace Utils;

public class AccountManager
{
    public List<DFKAccount> Accounts { get; set; } = new();
    private readonly IJSInProcessRuntime _js;
    public AccountManager(IJSInProcessRuntime js)
    {
        _js = js;
    }
    public async Task AddAccount(string name, Account account)
    {
        Accounts.Add(new()
        {
            Name = name,
            Account = account,
            Heroes = (await LoadHeroes(account.Address)).ToList()
        });
    }

    private async Task<Hero[]> LoadHeroes(string owner)
    {
		Dictionary<HeroesArgument, string> args = new()
		{
			{ HeroesArgument.owner, owner }
		};
		string request = API.HeroesRequestBuilder(args);
        return await API.GetHeroes(request);
    }

    public async Task<List<string>> GetAccountNames()
    {
        string accNames = await _js.InvokeAsync<string>("localStorage.getItem", "AccountNames");
        if(accNames is null)
        {
            return new();
        }
        else
        {
            return JsonConvert.DeserializeObject<List<string>>(accNames);
        }
    }
}