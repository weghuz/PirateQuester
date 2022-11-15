using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace PirateQuester.Pages;

public partial class Options
{
    [Inject]
    public IJSInProcessRuntime JS { get; set; }
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