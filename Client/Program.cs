using DFKContracts.HeroCore;
using DFKContracts.QuestCore;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using Nethereum.Web3;
using PirateQuester.Bot;
using PirateQuester.Utils;
using Radzen;
using Utils;

namespace PirateQuester
{
    public class Program
    {
        public static async Task Main(string[] args)
        {

			Web3 w3Service = new Web3("https://subnets.avax.network/defi-kingdoms/dfk-chain/rpc");
            w3Service.TransactionManager.UseLegacyAsDefault = true;
			var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");
			builder.Services.AddSingleton(w3Service);
			builder.Services.AddSingleton(new HeroCoreService(w3Service, "0xEb9B61B145D6489Be575D3603F4a704810e143dF"));
			builder.Services.AddSingleton(new QuestCoreService(w3Service, "0xE9AbfBC143d7cef74b5b793ec5907fa62ca53154"));
			builder.Services.AddScoped<DialogService>();
			builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddSingleton(serviceProvider => (IJSInProcessRuntime)serviceProvider.GetRequiredService<IJSRuntime>());
            builder.Services.AddSingleton(serviceProvider => (IJSUnmarshalledRuntime)serviceProvider.GetRequiredService<IJSRuntime>());
            builder.Services.AddSingleton<AccountManager>();
            builder.Services.AddSingleton<DFKBot>();

            await builder.Build().RunAsync();
        }
    }
}