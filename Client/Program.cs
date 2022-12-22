using DFKContracts.HeroCore;
using DFKContracts.QuestCore;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using Nethereum.Web3;
using PirateQuester.Bot;
using PirateQuester.Services;
using PirateQuester.Utils;
using Radzen;
using DFKContracts.MeditationCircle;
using Nethereum.JsonRpc.WebSocketClient;
using Syncfusion.Blazor;
using PirateQuester.PirateQuesterToken;

namespace PirateQuester
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Web3 w3Service = new Web3("https://subnets.avax.network/defi-kingdoms/dfk-chain/rpc");
            w3Service.Eth.TransactionManager.UseLegacyAsDefault = true;
			WebSocketClient w3Socket = new WebSocketClient("wss://subnets.avax.network/defi-kingdoms/dfk-chain/ws");
            
			var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");
			builder.Services.AddSingleton(w3Service);
            builder.Services.AddSingleton(w3Socket);
			builder.Services.AddSingleton(new HeroCoreService(w3Service, "0xEb9B61B145D6489Be575D3603F4a704810e143dF"));
			builder.Services.AddSingleton(new QuestCoreService(w3Service, "0xE9AbfBC143d7cef74b5b793ec5907fa62ca53154"));
            builder.Services.AddSingleton(new MeditationCircleService(w3Service, "0xD507b6b299d9FC835a0Df92f718920D13fA49B47"));
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