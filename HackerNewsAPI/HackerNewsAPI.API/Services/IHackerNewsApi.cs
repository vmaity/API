using HackerNewsAPI.API.Models;
using Refit;

namespace HackerNewsAPI.API.Services
{
    public interface IHackerNewsApi
    {
        [Get("/v0/beststories.json")]
        Task<int[]> GetBestStories();

        [Get("/v0/item/{id}.json")]
        Task<Story> GetStoryById(int id);

    }
}
