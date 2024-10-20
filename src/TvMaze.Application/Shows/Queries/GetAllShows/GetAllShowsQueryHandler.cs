using AutoMapper;
using MediatR;
using TvMaze.Application.Contracts;
using TvMaze.Application.Dtos;

namespace TvMaze.Application.Shows.Queries.GetAllShows;

public class GetAllShowsQueryHandler(IShowRepository showRepository, IMapper mapper)
    : IRequestHandler<GetAllShowsQuery, IEnumerable<ShowDto>>
{
    private readonly IShowRepository _showRepository = showRepository ??
          throw new ArgumentNullException(nameof(showRepository));
    private readonly IMapper _mapper = mapper ??
        throw new ArgumentNullException(nameof(mapper));

    public async Task<IEnumerable<ShowDto>> Handle(GetAllShowsQuery request, CancellationToken cancellationToken)
    {
        var response = new List<ShowDto>();
        var shows = await _showRepository.GetAllShowsAsync();
        if (shows is not null)
        {
            response = _mapper.Map<List<ShowDto>>(shows);
        }
        return response;
    }
}
