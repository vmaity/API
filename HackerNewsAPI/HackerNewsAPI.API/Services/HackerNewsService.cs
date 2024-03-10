using AutoMapper;
using HackerNewsAPI.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;

namespace HackerNewsAPI.API.Services
{
    public class HackerNewsService: IHackerNewsService
    {
        private IHackerNewsApi _hackerNewsApi;
        private IMapper _mapper;

        private ConcurrentDictionary<int, Story> _storyCache;
        public HackerNewsService([FromServices] IHackerNewsApi hackerNewsApi, IMapper mapper)
        {
            _hackerNewsApi = hackerNewsApi;
            _mapper = mapper;
            _storyCache = new ConcurrentDictionary<int, Story>();
        }

        public async Task<IEnumerable<StoryDto>> GetBestOrderedStories(int noOfStories,bool disableCache)
        {
            var outputStories = new List<StoryDto>();

            using (var client = new HttpClient())
            {
                var bestStories = await _hackerNewsApi.GetBestStories();
                var stories = new List<Story>();

                Array.Sort(bestStories);

                var tasks = bestStories.Select(async storyId =>
                {
                    Story story = await GetStoryFromCache(storyId, disableCache);
                    stories.Add(story);
                }).ToList();

                Task.WaitAll(tasks.ToArray());

                outputStories = _mapper.Map<List<StoryDto>>(stories);
            }

            return outputStories.OrderByDescending(s => s.Score).Take(noOfStories);
        }

        private async Task<Story> GetStoryFromCache(int storyId, bool disableCache)
        {
            Story story;
            if (disableCache == true)
            {
                story = await _hackerNewsApi.GetStoryById(storyId);
            }
            else
            {
                _storyCache.TryGetValue(storyId, out story);

                if (story == null)
                {
                    story = await _hackerNewsApi.GetStoryById(storyId);
                    _storyCache.TryAdd(storyId, story);
                }
            }

            return story;
        }
    }
}
