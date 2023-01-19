using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PirateQuester.Utils;
using System.Text.Json.Serialization;
using System.Text.Json;
using PirateQuester.Services;
using Microsoft.AspNetCore.Components.Forms;
using PirateQuester.Bot;

namespace PirateQuester.Pages;

public partial class Options
{
    [Inject]
    public IJSInProcessRuntime JS { get; set; }
    [Inject]
    public AccountSettings AccSettings { get; set; }
    [Inject]
    public BotService Bots { get; set; }

    public string UploadedBotSettings { get; set; }
    void ClearProblematicStorage()
    {
        JS.InvokeVoidAsync("localStorage.removeItem", "DFKBotSettings", "");
        JS.InvokeVoidAsync("localStorage.removeItem", "gridRunningQuestsGrid", "");
        JS.InvokeVoidAsync("localStorage.removeItem", "gridQuestRewardsGrid", "");
        JS.InvokeVoidAsync("localStorage.removeItem", "gridControlCenterHeroGrid", "");
        JS.InvokeVoidAsync("localStorage.removeItem", "ChainSettings", "");
    }

    void ExportBotSettings()
    {
        JS.Invoke<string>("download", $"PirateQuesterDFKBotSettings-{DateTime.Now:yyyy-MM-ddThh:mm}", JsonSerializer.Serialize(Bots.Settings, new JsonSerializerOptions()
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
        }));
    }

    private async Task OnInputFileChange(InputFileChangeEventArgs e)
    {

        foreach (var file in e.GetMultipleFiles(1))
        {
            try
            {
                var fileContent = new StreamContent(file.OpenReadStream(file.Size));
                UploadedBotSettings = await fileContent.ReadAsStringAsync();
                Console.WriteLine(UploadedBotSettings);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
    void ImportBotSettings()
    {
        if(UploadedBotSettings is null)
        {
            JS.InvokeVoid("alert", "No file selected");
            return;
        }
        if(JS.Invoke<bool>("confirm", "Are you sure you want to import bot settings? This will overwrite your current settings."))
        {
            try
            {
                Bots.ImportBotSettings(UploadedBotSettings);
                Bots.SaveSettings();
                JS.InvokeVoid("alert", "Bot settings imported successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                JS.InvokeVoid("alert", "Invalid bot settings file");
            }
        }
    }

    void ClearTableCache()
    {
        JS.InvokeVoid("localStorage.removeItem", "gridQuestRewardsGrid", "");
        JS.InvokeVoid("localStorage.removeItem", "gridControlCenterHeroGrid", "");
    }
    
    void ClearLocalStorage()
    {
        if(JS.Invoke<bool>("confirm", "This will delete ALL your local accounts. Are you sure?"))
        {
            JS.InvokeVoid("localStorage.clear");
        }
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
}