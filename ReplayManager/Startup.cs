// <copyright file="Startup.cs" company="Josh">
// Copyright (c) Josh. All rights reserved.
// </copyright>

using Fluxor;
using MudBlazor.Services;
using ReplayManager.Models;
using ReplayManager.Services;

namespace ReplayManager
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddRazorPages();
			services.AddServerSideBlazor();
			services.AddMudServices();
			services.Configure<ReplayManagerOptions>(Configuration.GetSection(
											ReplayManagerOptions.ReplayManager));
			services.AddSingleton<IOptionsWriter, OptionsWriter>();
			services.AddSingleton<IReplayLoadingService, ReplayLoadingService>();
			var currentAssembly = typeof(Startup).Assembly;
			services.AddFluxor(options => options
				.ScanAssemblies(currentAssembly)
				.UseReduxDevTools());
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
			}

			app.UseStaticFiles();

			app.UseRouting();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapBlazorHub();
				endpoints.MapFallbackToPage("/_Host");
			});
		}
	}
}
