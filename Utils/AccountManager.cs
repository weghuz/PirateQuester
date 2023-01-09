using DFK;
using Microsoft.JSInterop;
using Nethereum.Model;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using Newtonsoft.Json;
using PirateQuester.Bot;
using PirateQuester.Pages;
using PirateQuester.Services;
using PirateQuester.ViewModels;
using System.Runtime;
using Utils;

namespace PirateQuester.Utils;

public class AccountManager
{
    public List<DFKAccount> Accounts { get; set; } = new();
    private readonly IJSInProcessRuntime _js;
    public List<string> AccountNames { get; set; }

	public static AccountSettings AccSettings;
	public AccountManager(IJSInProcessRuntime js, AccountSettings accountSettings)
    {
        AccSettings = accountSettings;
		_js = js;
        AccountNames = GetAccountNames();
    }
    
	public async Task<bool> Login(LoginViewModel model, DFKBotSettings settings)
    {
        string loggingInAccountName = "";
        try
        {
            int count = model.SelectedAccounts.Count;
            List<string> refList = new(model.SelectedAccounts);
			foreach(string accountName in refList)
            {
                loggingInAccountName = accountName;

				string json = _js.Invoke<string>("localStorage.getItem", accountName);
                foreach (Chain.Chain chain in AccSettings.ChainSettings.Where(cs => cs.Enabled && cs.Name != "Avalanche"))
                {
                    DFKAccount account = new(accountName, Encrypt.GetAccount(model.Password, json), chain, AccSettings.ChainSettings.First(cs => cs.Name == "Avalanche"), settings);
                    Accounts.Add(account);
                    await account.InitializeAccount();
                }
                model.SelectedAccounts.Remove(accountName);

			}
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            _js.InvokeVoid("alert", $"Incorrect Password for account {loggingInAccountName}.");
        }
        return false;
    }

    public async Task Create(AccountViewModel model, DFKBotSettings settings)
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
        if(model.PrivateKey is not null && model.PrivateKey.Length == 64)
        {
            model.PrivateKey = $"0x{model.PrivateKey}";
        }
        if (!model.GenerateAccount && model.PrivateKey is not null && (model.PrivateKey.Length != 66))
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
        foreach (Chain.Chain chain in AccSettings.ChainSettings.Where(cs => cs.Enabled && cs.Name != "Avalanche"))
        {
            DFKAccount account = new(model.Name, Encrypt.GetAccount(model.Password, json), chain, AccSettings.ChainSettings.FirstOrDefault(cs => cs.Name == "Avalanche"), settings);
            Accounts.Add(account);
            await account.InitializeAccount();
        }
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