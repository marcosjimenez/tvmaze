using AutoMapper;
using TvMaze.Application.Dtos;
using TvMaze.Core.Aggregates;

namespace TvMaze.Application.Mappers;

public class LinksResolver : IValueResolver<Show, ShowDto, LinksDto?>
{
    public LinksDto Resolve(Show source, ShowDto destination, LinksDto? destMember, ResolutionContext context)
        => new()
        {
            Self = new SelfReferenceDto
            {
                Href = source.SelfLink ?? String.Empty
            },
            PreviousEpisode = new PreviousEpisodeDto
            {
                Href = source.PreviousEpisodeLink ?? String.Empty,
                Name = source.PreviousEpisodeName ?? String.Empty
            }
        };
}
