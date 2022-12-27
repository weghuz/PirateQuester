using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PirateQuester.Utils;

namespace PirateQuester.Pages;

public partial class Options
{
    [Inject]
    public IJSInProcessRuntime JS { get; set; }
    [Inject]
    public AccountSettings AccSettings { get; set; }
    void ClearProblematicStorage()
    {
        JS.InvokeVoidAsync("localStorage.removeItem", "DFKBotSettings", "");
        JS.InvokeVoidAsync("localStorage.removeItem", "gridRunningQuestsGrid", "");
        JS.InvokeVoidAsync("localStorage.removeItem", "gridQuestRewardsGrid", "");
        JS.InvokeVoidAsync("localStorage.removeItem", "gridControlCenterHeroGrid", "");
        JS.InvokeVoidAsync("localStorage.removeItem", "ChainSettings", "");
        JS.InvokeVoid("location.reload");
    }
    
    void ClearLocalStorage()
    {
        if(JS.Invoke<bool>("confirm", "This will delete ALL your local accounts. Are you sure?"))
        {
            JS.InvokeVoid("localStorage.clear");
        }
        JS.InvokeVoid("location.reload");
    }
    void ToggleDarkMode()
    {
        Console.WriteLine("Toggling dark mode");
        string darkMode = JS.Invoke<string>("localStorage.getItem", "darkMode");
        if (bool.TryParse(darkMode, out bool darkModeBool))
        {
            Console.WriteLine("Toggled");
            JS.InvokeVoid("localStorage.setItem", "darkMode", !darkModeBool);
        }
        else
        {
            Console.WriteLine("Set to false");
            JS.InvokeVoid("localStorage.setItem", "darkMode", false);
        }
        Console.WriteLine(darkMode);
        LoadDarkMode();
    }
    void LoadDarkMode()
    {
        string darkMode = JS.Invoke<string>("localStorage.getItem", "darkMode");
        if (bool.TryParse(darkMode, out bool darkModeBool))
        {
            if (darkModeBool)
			{
				JS.Invoke<string>("SetStylesheet", "_content/Radzen.Blazor/css/dark.css");
				JS.Invoke<string>("SetSyncfusionStylesheet", "_content/Syncfusion.Blazor/Styles/bootstrap5-dark.css");
			}
            else
            {
                JS.Invoke<string>("SetStylesheet", "_content/Radzen.Blazor/css/standard.css");
				JS.Invoke<string>("SetSyncfusionStylesheet", "_content/Syncfusion.Blazor/Styles/bootstrap5.css");
			}
        }
        else
        {
            JS.Invoke<string>("SetStylesheet", "_content/Radzen.Blazor/css/dark.css");
        }
    }
}