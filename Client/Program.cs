using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using PirateQuester.Bot;
using PirateQuester.PirateQuesterToken;
using PirateQuester.Services;
using PirateQuester.Utils;
using Syncfusion.Blazor;

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
			builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
			builder.Services.AddSingleton(serviceProvider => (IJSInProcessRuntime)serviceProvider.GetRequiredService<IJSRuntime>());
			builder.Services.AddSingleton(serviceProvider => (IJSUnmarshalledRuntime)serviceProvider.GetRequiredService<IJSRuntime>());
			builder.Services.AddSingleton<AccountManager>();
			builder.Services.AddSingleton<DFKBot>();
			builder.Services.AddSingleton<BotService>();
			builder.Services.AddSingleton<AccountSettings>();
            builder.Services.AddSingleton<HeroPricingService>();
            //use static files

            builder.Services.AddSyncfusionBlazor();
			Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("ODg0ODY4QDMyMzAyZTM0MmUzMGR5d2tJMFdZSDNjcEJqSTBXQi9VcFlIVURYdGNPei9DQWhvcHArdWFJbjQ9");
			await builder.Build().RunAsync();
		}
	}
}