using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using PirateQuester.Utils;
using Radzen;
using Utils;

namespace PirateQuester.Shared;

public partial class MainLayout : LayoutComponentBase
{

    [Inject]
    private AccountManager Acc { get; set; }
	[Inject]
    private NavigationManager Nav { get; set; }
	[Inject]
    private IJSInProcessRuntime JS { get; set; }
    [Inject]
    public DialogService Dialog { get; set; }
    private string NotePad { get; set; }
	private bool SidebarExpanded { get; set; }
    public decimal Balance { get; set; }

    protected override void OnInitialized()
	{
		NotePad = JS.Invoke<string>("localStorage.getItem", "notePad");
		//Don't know why but I have to toggle the sidebar expand twice to actually have it not expanded by default.
		LoadDarkMode();
		if (SidebarExpanded)
		{
			SidebarExpanded = false;
		}
		Transaction.TransactionAdded += UpdateUI;
		DFKAccount.UpdatedAccount += UpdateUI;
    }

	public void UpdateUI()
	{
		Balance = 0;
		foreach(DFKAccount acc in Acc.Accounts)
		{
			Balance += acc.Balance;
		}
		StateHasChanged();
	}

	void SaveNotepad()
	{
		JS.InvokeVoid("localStorage.setItem", new string[] { "notePad", NotePad });
	}

	List<string> GetLoggedOutAccountNames()
	{
		return Acc.AccountNames.Where(acc => Acc.Accounts.Select(acc => acc.Name).Any(key => key == acc) is false).ToList();
	}
	void LoadDarkMode()
	{
		string darkMode = JS.Invoke<string>("localStorage.getItem", "darkMode");
		if (bool.TryParse(darkMode, out bool darkModeBool))
		{
			if (darkModeBool)
			{
				JS.Invoke<string>("SetStylesheet", "_content/Radzen.Blazor/css/dark.css");
			}
			else
			{
				JS.Invoke<string>("SetStylesheet", "_content/Radzen.Blazor/css/standard.css");
			}
		}
		else
		{
			JS.Invoke<string>("SetStylesheet", "_content/Radzen.Blazor/css/dark.css");
		}
	}
}