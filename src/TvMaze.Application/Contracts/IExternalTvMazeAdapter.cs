using TvMaze.Core.Aggregates.TvMaze;

namespace TvMaze.Application.Contracts;

public interface IExternalTvMazeAdapter
{
    Task<IEnumerable<TvMazeShow>> ImportAsync(string baseUrl, CancellationToken cancellationToken);
}
