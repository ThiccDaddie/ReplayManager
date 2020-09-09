using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ThiccDaddie.ReplayManager.Server.Services;

namespace ThiccDaddie.ReplayManager.Server.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class SettingsController : ControllerBase
	{
		private readonly IConfiguration _configuration;
		private readonly OptionsService _optionsService;
		private readonly ReplayService _replayService;

		public SettingsController(IConfiguration configuration, OptionsService optionsService, ReplayService replayService)
		{
			_configuration = configuration;
			_optionsService = optionsService;
			_replayService = replayService;
		}

		[HttpGet("PrimaryReplaysDirectory")]
		public string GetPrimaryReplaysDirectory()
		{
			return _optionsService.ReplayManagerOptions.PrimaryReplaysDirectory;
		}

		[HttpPost("PrimaryReplaysDirectory")]
		public ActionResult PostPrimaryReplaysDirectory([FromBody] string newValue)
		{
			_optionsService.PrimaryReplaysDirectory = newValue;
			_optionsService.IsInitialised = false;
			_replayService.CheckStatus();

			return Ok();
		}
	}
}
