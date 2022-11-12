using Microsoft.JSInterop;
using Nethereum.Web3.Accounts;
using Newtonsoft.Json;

namespace Utils;

public class AccountManager
{
    public Dictionary<string, Account> Accounts { get; set; } = new();
    public async Task<List<string>> GetAccountNames(IJSRuntime js)
    {
        string accNames = await js.InvokeAsync<string>("localStorage.getItem", "AccountNames");
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