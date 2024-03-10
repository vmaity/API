using HackerNewsAPI.API.Models;
using HackerNewsAPI.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HackerNewsAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HackerNewsController : ControllerBase
    {
        private readonly ILogger<HackerNewsController> _logger;
        private IHackerNewsService _hackerNewsService;
        public HackerNewsController(ILogger<HackerNewsController> logger, 
                     IHackerNewsService hackerNewsService)
        {
            _hackerNewsService = hackerNewsService;
            _logger = logger;
        }

        [HttpGet]
        [Route("/hackernews")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces(typeof(IEnumerable<StoryDto>))]
        public async Task<IEnumerable<StoryDto>> Get(int noOfStories)
        {
            bool disableCache = false;
            bool.TryParse(Request.Headers["DisableCache"], out disableCache);

            return await _hackerNewsService.GetBestOrderedStories(noOfStories,disableCache);
        }
    }
}
