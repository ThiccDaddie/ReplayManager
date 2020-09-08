using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using ThiccDaddie.ReplayManager.Client.Services;

namespace ThiccDaddie.ReplayManager.Client
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebAssemblyHostBuilder.CreateDefault(args);
			builder.RootComponents.Add<App>("app");

			builder
				.Services
				.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) })
				.AddSingleton(sp => new ReplayService(sp.GetService<NavigationManager>()));

			await builder.Build().RunAsync();
		}
	}
}
