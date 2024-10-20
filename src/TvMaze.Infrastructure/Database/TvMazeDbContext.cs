using Microsoft.EntityFrameworkCore;
using TvMaze.Core.Aggregates;

namespace TvMaze.Infrastructure.Database;

public class TvMazeDbContext(DbContextOptions<TvMazeDbContext> options) : DbContext(options)
{
    public DbSet<Show> Shows { get; set; }
    public DbSet<Network> Networks { get; set; }

    // TODO: Add more DbSets when needed for Images, Externals, Countries, etc. 
}
