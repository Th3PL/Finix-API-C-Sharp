using Business;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("finix/[controller]")]
    public class RankingController : ControllerBase
    {
        private readonly IRankingService _rankingService;

        public RankingController(IRankingService rankingService)
        {
            _rankingService = rankingService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var ranking = _rankingService.GerarRanking();
            return ranking.Count == 0 ? NoContent() : Ok(ranking);
        }
    }
}
