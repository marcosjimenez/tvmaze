using AutoMapper;
using TvMaze.Core.Aggregates;
using TvMaze.Core.Aggregates.TvMaze;

namespace TvMaze.Application.Mappers;

public class TvMazeShowProfile : Profile
{
    public TvMazeShowProfile()
    {
        CreateMap<TvMazeCountry, Country>();
        CreateMap<TvMazeExternals, Externals>();
        CreateMap<TvMazeImage, Image>();
        CreateMap<TvMazeNetwork, Network>();

        CreateMap<TvMazeShow, Show>()
            .ForMember(x => x.SelfLink,
                a => a.MapFrom(o => o.Links.Self.Href))
            .ForMember(x => x.PreviousEpisodeLink,
                a => a.MapFrom(o => o.Links.PreviousEpisode.Href))
            .ForMember(x => x.PreviousEpisodeName,
                a => a.MapFrom(o => o.Links.PreviousEpisode.Name))
            .ForMember(x => x.ScheduleDays,
                a => a.MapFrom(o => o.Schedule.Days))
            .ForMember(x => x.ScheduleTime,
                a => a.MapFrom(o => o.Schedule.Time))
            .ForMember(x => x.RatingAverage,
                a => a.MapFrom(o => o.Rating.Average));

        CreateMap<TvMazeShow, Show>()
            .ForMember(x => x.NetworkId, a => a.MapFrom(x => x.Network!.Id))
            .ForMember(x => x.PreviousEpisodeLink, a => a.MapFrom(x => x.Links!.PreviousEpisode!.Href))
            .ForMember(x => x.PreviousEpisodeName, a => a.MapFrom(x => x.Links!.PreviousEpisode!.Name))
            .ForMember(x => x.SelfLink, a => a.MapFrom(x => x.Links!.Self!.Href))
            .ForMember(x => x.ScheduleDays, a => a.MapFrom(x => x.Schedule!.Days))
            .ForMember(x => x.ScheduleTime, a => a.MapFrom(x => x.Schedule!.Time))
            .ForMember(x => x.RatingAverage, a => a.MapFrom(x => x.Rating!.Average));
    }
}
