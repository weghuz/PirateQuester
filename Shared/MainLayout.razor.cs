using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using Newtonsoft.Json;
using PirateQuester.Utils;
using Syncfusion.Blazor.RichTextEditor;
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
	private string notePad;
    private string NotePad { get { return notePad; } set { notePad = value; SaveNotepad(); } }
	private bool SidebarExpanded { get; set; }
    public decimal Balance { get; set; }
    public ElementReference BodyElement { get; set; }
    private bool stickySidebar;
    public bool StickySidebar { get { return stickySidebar; } set { stickySidebar = value; JS.InvokeVoid("localStorage.setItem", "StickySidebar", value); } }
    public bool ExpandNotepad { get; set; }
    public bool ShowTransactionsWindow { get; set; }

    protected override void OnInitialized()
	{
		string sticky = JS.Invoke<string>("localStorage.getItem", "StickySidebar");
        if(sticky is not null)
		{
			StickySidebar = JsonConvert.DeserializeObject<bool>(sticky);
			
		}
		string notepad = JS.Invoke<string>("localStorage.getItem", "notePad");
		if(notepad is not null)
		{
			NotePad = JsonConvert.DeserializeObject<string>(notepad);
		}

		LoadDarkMode();
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
		JS.InvokeVoid("localStorage.setItem", "notePad", JsonConvert.SerializeObject(NotePad) );
	}
	
	void LoadDarkMode()
	{
		string darkMode = JS.Invoke<string>("localStorage.getItem", "darkMode");
		if (bool.TryParse(darkMode, out bool darkModeBool))
		{
			if (darkModeBool)
            {
                JS.Invoke<string>("SetSyncfusionStylesheet", Constants.DARK_THEME);
                JS.Invoke<string>("SetStylesheet", "css/app-dark.css");
            }
			else
            {
                JS.Invoke<string>("SetSyncfusionStylesheet", Constants.THEME);
                JS.Invoke<string>("SetStylesheet", "css/app.css");
            }
		}
		else
        {
            JS.Invoke<string>("SetSyncfusionStylesheet", Constants.DARK_THEME);
            JS.Invoke<string>("SetStylesheet", "css/app-dark.css");
        }
	}
	
	public void ToggleSidebar()
	{
		if(!StickySidebar)
        {
            SidebarExpanded = !SidebarExpanded;
        }
	}

	public void HideSidebar()
    {
        if (!StickySidebar)
        {
            SidebarExpanded = false;
        }
    }

    public void Navigate(string navigateTo)
    {
        ToggleSidebar();
        Nav.NavigateTo(navigateTo);
    }
	
    public void NavigateExternal(string navigateTo)
    {
        JS.InvokeVoid("open", navigateTo, "_blank");
    }
}