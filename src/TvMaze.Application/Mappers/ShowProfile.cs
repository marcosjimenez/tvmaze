using AutoMapper;
using TvMaze.Application.Dtos;
using TvMaze.Core.Aggregates;

namespace TvMaze.Application.Mappers;

public class ShowProfile : Profile
{
    public ShowProfile()
    {
        CreateMap<Country, CountryDto>().ReverseMap();
        CreateMap<Externals, ExternalsDto>().ReverseMap();
        CreateMap<Image, ImageDto>().ReverseMap();
        CreateMap<Network, NetworkDto>().ReverseMap();

        CreateMap<Show, ShowDto>()
            .ForMember(x => x.Links,
                a => a.MapFrom<LinksResolver>())
            .ForMember(x => x.Schedule,
                a => a.MapFrom<ScheduleResolver>())
            .ForMember(x => x.Rating,
                a => a.MapFrom<RatingResolver>());

        CreateMap<ShowDto, Show>()
            .ForMember(x => x.NetworkId, a => a.MapFrom(x => x.Network!.Id))
            .ForMember(x => x.PreviousEpisodeLink, a => a.MapFrom(x => x.Links!.PreviousEpisode!.Href))
            .ForMember(x => x.PreviousEpisodeName, a => a.MapFrom(x => x.Links!.PreviousEpisode!.Name))
            .ForMember(x => x.SelfLink, a => a.MapFrom(x => x.Links!.Self!.Href))
            .ForMember(x => x.ScheduleDays, a => a.MapFrom(x => x.Schedule!.Days))
            .ForMember(x => x.ScheduleTime, a => a.MapFrom(x => x.Schedule!.Time))
            .ForMember(x => x.RatingAverage, a => a.MapFrom(x => x.Rating!.Average));
    }
}
