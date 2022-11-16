using DFK;
using Microsoft.JSInterop;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using Newtonsoft.Json;
using PirateQuester.ViewModels;
using Utils;

namespace PirateQuester.Utils;

public class AccountManager
{
    public List<DFKAccount> Accounts { get; set; } = new();
    private readonly IJSInProcessRuntime _js;
    public List<string> AccountNames { get; set; }
    public AccountManager(IJSInProcessRuntime js)
    {
        _js = js;
        AccountNames = GetAccountNames();
    }

    public async Task<bool> Login(LoginViewModel model)
    {
        try
        {
            foreach (string name in model.SelectedAccounts)
            {
                string json = _js.Invoke<string>("localStorage.getItem", name);
                DFKAccount account = new(name, Encrypt.GetAccount(model.Password, json));

				Accounts.Add(account);
                await account.LoadHeroes();

			}
            return true;
        }
        catch (Exception e)
        {
            _js.InvokeVoid("alert", $"{e.Message}.");
        }
        return false;
    }

    public async Task Create(AccountViewModel model)
    {
        if (model.Password.Length < 8 || model.RecheckPassword.Length < 8)
        {
            _js.InvokeVoid("alert", "Password needs to have 8 or more characters.");
            return;
        }
        if (model.Password != model.RecheckPassword)
        {
            _js.InvokeVoid("alert", "The passwords don't match.");
            return;
        }
        if (!model.GenerateAccount && model.PrivateKey is not null && model.PrivateKey.Length != 66)
        {
            _js.InvokeVoid("alert", "The private key is in and incorrect format.");
            return;
        }
        if (model.Name == "")
        {
            _js.InvokeVoid("alert", "Accounts need to have names.");
            return;
        }
        if (AccountNames.Any(acc => acc == model.Name))
        {
            _js.InvokeVoid("alert", "You already have an account with that name!");
            return;
        }
        AccountNames.Add(model.Name);
        _js.Invoke<string>("localStorage.setItem", new string[] { "AccountNames", JsonConvert.SerializeObject(AccountNames) });
        string json;
        if (model.GenerateAccount)
        {
            json = Encrypt.GenerateAccount(model.Password);
            _js.InvokeVoid("localStorage.setItem", new string[] { model.Name, json });
        }
        else
        {
            json = Encrypt.CreateAccount(model.PrivateKey, model.Password);
            _js.InvokeVoid("localStorage.setItem", new string[] { model.Name, json });
        }
        DFKAccount account = new(model.Name, Encrypt.GetAccount(model.Password, json));
		Accounts.Add(account);
		await account.LoadHeroes();
		return;
    }

    public List<string> GetAccountNames()
    {
        string accNames = _js.Invoke<string>("localStorage.getItem", "AccountNames");
        if (accNames is null)
        {
            return new();
        }
        else
        {
            return JsonConvert.DeserializeObject<List<string>>(accNames);
        }
    }
}