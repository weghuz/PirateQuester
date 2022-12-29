using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using Nethereum.Web3;
using PirateQuester.Bot;
using PirateQuester.Services;
using PirateQuester.Utils;
using Radzen;
using Nethereum.JsonRpc.WebSocketClient;
using Syncfusion.Blazor;
using PirateQuester.PirateQuesterToken;

namespace PirateQuester
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
			var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");
            builder.Services.AddSingleton(new PirateQuesterTokenService(new(), "0xAC2b4Ffe04AB34e43e780Dad5C8DEac47B7db775"));
            builder.Services.AddScoped<DialogService>();
			builder.Services.AddScoped<TooltipService>();
			builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddSingleton(serviceProvider => (IJSInProcessRuntime)serviceProvider.GetRequiredService<IJSRuntime>());
            builder.Services.AddSingleton(serviceProvider => (IJSUnmarshalledRuntime)serviceProvider.GetRequiredService<IJSRuntime>());
            builder.Services.AddSingleton<AccountManager>();
            builder.Services.AddSingleton<DFKBot>();
            builder.Services.AddSingleton<BotService>();
            builder.Services.AddSingleton<AccountSettings>();
            builder.Services.AddSyncfusionBlazor();
            
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NzcwNjIwQDMyMzAyZTMzMmUzMFdwWFM4cnp3NHN0eVlQODFEMkhyRUFuNG16Sm1LTVJzRGxFY3d4SC9ZZlE9");
            await builder.Build().RunAsync();
        }
    }
}