using Microsoft.EntityFrameworkCore;
using TvMaze.Application.Contracts;
using TvMaze.Core.Aggregates;

namespace TvMaze.Infrastructure.Database.Repository;

public class ShowRepository(TvMazeDbContext dbContext) : IShowRepository, IDisposable
{
    private readonly TvMazeDbContext _dbContext = dbContext ??
        throw new ArgumentNullException(nameof(dbContext));

    private bool disposed = false;


    public async Task<Show?> GetByIdAsync(int id)
        => await _dbContext.Shows.FirstOrDefaultAsync(x => x.Id == id);

    public async Task<List<Show>> GetAllShowsAsync()
    => await _dbContext.Shows
            .Include(x => x.Network)
                .ThenInclude(x => x!.Country)
            .Include(x => x.Externals)
            .Include(x => x.Image)
            .ToListAsync();

    public async Task AddAsync(Show show)
        => await _dbContext.Shows.AddAsync(show);

    public async Task AddRangeAsync(IEnumerable<Show> shows)
        => await _dbContext.Shows.AddRangeAsync(shows);

    public bool Exists(int Id)
        => _dbContext.Shows.Any(x => x.Id == Id);

    public async Task<int> SaveAsync()
        => await _dbContext.SaveChangesAsync();

    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }
        this.disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}