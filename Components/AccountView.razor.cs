using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Nethereum.Web3;
using Newtonsoft.Json;
using PirateQuester.Pages;
using PirateQuester.Services;
using PirateQuester.Utils;
using Utils;

namespace PirateQuester.Components
{
    public partial class AccountView
	{
		[Inject]
		public AccountManager Acc { get; set; }
		[Inject]
		public IJSInProcessRuntime JS { get; set; }
		[Inject]
		public NavigationManager Nav { get; set; }
		[Inject]
		public BotService Bots { get; set; }
		[Parameter]
		public List<DFKAccount> Accounts { get; set; }
		public bool ShowPrivateKey { get; set; } = false;
		public bool ShowPrivateKeyDialog { get; set; } = false;
		public bool ShowDeleteAccountDialog { get; set; } = false;
		public bool ShowRenameAccountDialog { get; set; } = false;
		public bool ShowBuyPQTDialog { get; set; } = false;
		private bool showConfirmBuyPQTDialog = false;
		public bool ShowConfirmBuyPQTDialog { get { return showConfirmBuyPQTDialog; } set { showConfirmBuyPQTDialog = value; BoughtPQT = false; } }
        public bool IsBuying { get; set; }
        public bool	BoughtPQT { get; set; }
        public string Password { get; set; } = "";
		public string NewAccountName { get; set; } = "";
        public string ErrorMessage { get; set; }
        public decimal PQTPrice { get; set; }
		public int BuyAmount { get; set; } = 1;
        public AccountUpdaterService AccountUpdater { get; set; }
        protected override async void OnInitialized()
		{
			PQTPrice = Math.Round(Web3.Convert.FromWei(await Accounts[0].PQT.PriceQueryAsync()), 2);
		}
		
        private void CheckPasswordDialog()
		{
			if (CheckPassword(Accounts[0].Name, Password))
			{
				ShowPrivateKey = true;
			}
			Password = "";
		}
        private void RenameAccount()
        {
            Acc.AccountNames = Acc.AccountNames.Where(accName => accName != Accounts[0].Name).ToList();
			Acc.AccountNames.Add(NewAccountName);
			var scryptEncodedAccount = JS.Invoke<string>("localStorage.getItem", Accounts[0].Name);
            JS.InvokeVoid("localStorage.setItem", NewAccountName, scryptEncodedAccount);
            JS.InvokeVoid("localStorage.setItem", "AccountNames", JsonConvert.SerializeObject(Acc.AccountNames));
			JS.InvokeVoid("localStorage.removeItem", Accounts[0].Name);
            JS.InvokeVoid("alert", "Account renamed from " + Accounts[0].Name + " to " + NewAccountName);
			foreach(DFKAccount acc in Accounts)
			{
                acc.Name = NewAccountName;
            }
			ShowRenameAccountDialog = false;
        }
		
        private void CheckPasswordDeleteAccountDialog()
		{
			if (CheckPassword(Accounts[0].Name, Password))
			{
				JS.InvokeVoid("localStorage.removeItem", Accounts[0].Name);
				Acc.AccountNames.Remove(Accounts[0].Name);
				JS.InvokeVoid("localStorage.setItem", "AccountNames", JsonConvert.SerializeObject(Acc.AccountNames));
				JS.InvokeVoid("alert", "Account deleted from local storage. Log out to reflect changes.");
				ShowDeleteAccountDialog = false;
			}
			Password = "";
		}
		
		public bool CheckPassword(string accountName, string password)
		{
			try
			{
				string json = JS.Invoke<string>("localStorage.getItem", accountName);
				Encrypt.GetAccount(password, json);
				return true;
			}
			catch
			{
				JS.InvokeVoid("alert", $"Incorrect Password.");
			}
			return false;
		}

		public async void BuyPirateQuesterToken()
		{
			ErrorMessage = null;
			IsBuying = true;
			string response = await Transaction.BuyPirateQuesterToken(Accounts[0], BuyAmount, Bots.Settings.MaxGasFeeGwei, Bots.Settings.CancelTxnDelay);
			IsBuying = false;
			if (response.Contains("failed"))
			{
				ErrorMessage = "The transaction failed, make sure you have enough AVAX to pay for gas on top of the price!";
			}
			else
			{
				BoughtPQT = true;
				await Accounts[0].InitializeAccount();
			}
		}
	}
}
