// <copyright file="OptionsWriter.cs" company="Josh">
// Copyright (c) Josh. All rights reserved.
// </copyright>

using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using ReplayManager.Models;

namespace ReplayManager.Services
{
	public class OptionsWriter : IOptionsWriter
	{
		public async Task WriteOptionsAsync(ReplayManagerOptions options)
		{
#if DEBUG
			string filePath = "appsettings.Development.json";
#else
			string filePath = "appsettings.json";

#endif
			JObject settingsJson = JObject.Parse(File.ReadAllText(filePath));
			settingsJson[ReplayManagerOptions.ReplayManager] = JObject.FromObject(options);
			await File.WriteAllTextAsync(filePath, settingsJson.ToString());
		}
	}
}
