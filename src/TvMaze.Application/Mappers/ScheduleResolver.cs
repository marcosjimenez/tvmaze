using AutoMapper;
using TvMaze.Application.Dtos;
using TvMaze.Core.Aggregates;

namespace TvMaze.Application.Mappers;

public class ScheduleResolver : IValueResolver<Show, ShowDto, ScheduleDto?>
{
    public ScheduleDto Resolve(Show source, ShowDto destination, ScheduleDto? destMember, ResolutionContext context)
        => new()
        {
            Days = source.ScheduleDays ?? [],
            Time = source.ScheduleTime ?? String.Empty
        };
}
