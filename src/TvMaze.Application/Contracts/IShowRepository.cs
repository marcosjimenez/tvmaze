using TvMaze.Core.Aggregates;

namespace TvMaze.Application.Contracts;

public interface IShowRepository
{
    Task<Show?> GetByIdAsync(int id);
    Task<List<Show>> GetAllShowsAsync();
    Task AddAsync(Show show);
    Task AddRangeAsync(IEnumerable<Show> shows);
    bool Exists(int Id);
    Task<int> SaveAsync();
}
