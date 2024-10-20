using Microsoft.EntityFrameworkCore;
using TvMaze.Infrastructure.Database;

namespace TvMaze.IntegrationTests;

public abstract class TvMazeFixture
{
    protected readonly TvMazeDbContext _context;

    protected TvMazeFixture()
    {
        var serviceProvider = new ServiceCollection()
        .AddEntityFrameworkInMemoryDatabase()
        .BuildServiceProvider();

        var builder = new DbContextOptionsBuilder<TvMazeDbContext>();
        builder.UseInMemoryDatabase("shows")
               .UseInternalServiceProvider(serviceProvider);

        var options = builder.Options;
        _context = new TvMazeDbContext(options);
    }
}
