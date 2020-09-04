using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System.IO;
using ThiccDaddie.ReplayManager.Shared;

namespace ThiccDaddie.ReplayManager.Server.Services
{
	public class OptionsService
	{
		private readonly IConfiguration _configuration;

		private readonly JObject _configurationJson;

		public OptionsService(IConfiguration configuration)
		{
			_configuration = configuration;
			_configurationJson = JObject.Parse(File.ReadAllText("appsettings.json"));
		}

		private async void ConfigurationJsonPropertyChanged()
		{
			await File.WriteAllTextAsync("appsettings.json", _configurationJson.ToString());
		}

		private ReplayManagerOptions replayManagerOptions;

		public ReplayManagerOptions ReplayManagerOptions
		{
			get
			{
				if (replayManagerOptions == null)
				{
					replayManagerOptions = _configuration.GetSection(ReplayManagerOptions.ReplayManager).Get<ReplayManagerOptions>();
				}
				return replayManagerOptions;
			}
		}

		public string PrimaryReplaysDirectory
		{
			get
			{
				return ReplayManagerOptions.PrimaryReplaysDirectory;
			}
			set
			{
				if (string.IsNullOrWhiteSpace(value))
				{
					throw new System.ArgumentException($"'{nameof(value)}' cannot be null or whitespace", nameof(value));
				}

				replayManagerOptions.PrimaryReplaysDirectory = value;
				_configurationJson[ReplayManagerOptions.ReplayManager][nameof(PrimaryReplaysDirectory)] = value;
				ConfigurationJsonPropertyChanged();
			}
		}

		public bool IsInitialised
		{
			get
			{
				return ReplayManagerOptions.IsInitialised;
			}
			set
			{
				replayManagerOptions.IsInitialised = value;
				_configurationJson[ReplayManagerOptions.ReplayManager][nameof(IsInitialised)] = value;
				ConfigurationJsonPropertyChanged();
			}
		}
	}
}
