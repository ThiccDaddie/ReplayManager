using Microsoft.AspNetCore.Mvc;
using ThiccDaddie.ReplayManager.Server.Services;

namespace ThiccDaddie.ReplayManager.Server.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class ReplaysController : ControllerBase
	{
		private readonly ReplayService _replayService;
		private readonly OptionsService _optionsService;

		public ReplaysController(ReplayService replayService, OptionsService optionsService)
		{
			_replayService = replayService;
			_optionsService = optionsService;
		}
	}
}
