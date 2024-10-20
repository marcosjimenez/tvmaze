using TvMaze.Core.Aggregates;

namespace TvMaze.Application.Contracts;

public interface INetworkRepository
{
    Task AddAsync(Network network);
    Task AddRangeAsync(IEnumerable<Network> networks);
    bool Exists(int Id);
    Task<int> SaveAsync();
}
