using HackerNewsAPI.API.Models;

namespace HackerNewsAPI.API.Services
{
    public interface IHackerNewsService
    {
        Task<IEnumerable<StoryDto>> GetBestOrderedStories(int noOfStories,bool disableCache);
    }
}
