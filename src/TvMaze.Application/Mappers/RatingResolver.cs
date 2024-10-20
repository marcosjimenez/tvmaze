using AutoMapper;
using TvMaze.Application.Dtos;
using TvMaze.Core.Aggregates;

namespace TvMaze.Application.Mappers;

public class RatingResolver : IValueResolver<Show, ShowDto, RatingDto?>
{
    public RatingDto Resolve(Show source, ShowDto destination, RatingDto? destMember, ResolutionContext context)
        => new()
        {
            Average = source.RatingAverage
        };
}
