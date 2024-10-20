using MediatR;
using TvMaze.Application.Dtos;

namespace TvMaze.Application.Shows.Queries.GetAllShows;

public class GetAllShowsQuery : IRequest<IEnumerable<ShowDto>>
{
}
