using TvMaze.Application.Contracts;
using TvMaze.Core.Aggregates;

namespace TvMaze.Infrastructure.Database.Repository;

public class NetworkRepository(TvMazeDbContext dbContext) : INetworkRepository, IDisposable
{
    private readonly TvMazeDbContext _dbContext = dbContext ??
        throw new ArgumentNullException(nameof(dbContext));

    private bool disposed = false;

    public async Task AddAsync(Network network)
        => await _dbContext.Networks.AddAsync(network);

    public async Task AddRangeAsync(IEnumerable<Network> networks)
        => await _dbContext.Networks.AddRangeAsync(networks);

    public bool Exists(int Id)
        => _dbContext.Networks.Any(x => x.Id == Id);
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
