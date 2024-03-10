using AutoMapper;
using HackerNewsAPI.API.Models;

namespace HackerNewsAPI.API.Profiles
{
    public class HackerNewsProfile:Profile
    {
        public HackerNewsProfile()
        {
            CreateMap<Story,StoryDto>()
            .ForMember(dest => dest.Uri, opt => opt.MapFrom(src => src.Url))
            .ForMember(dest => dest.PostedBy, opt => opt.MapFrom(src => src.By))
            .ForMember(dest => dest.CommentCount, opt => opt.MapFrom(src => src.Descendants))
            .ForMember(dest => dest.Time, opt => opt.MapFrom(src => FormatDate(src.Time)));
        }
        private string FormatDate(long unixDate)
        {
            var dotNetDate = DateTimeOffset.FromUnixTimeSeconds(unixDate);
            return dotNetDate.LocalDateTime.ToString("yyyy-MM-ddTHH:mm:ssK");
        }
    }
}
