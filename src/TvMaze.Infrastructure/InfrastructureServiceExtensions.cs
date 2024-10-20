using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TvMaze.Application.Configuration;
using TvMaze.Application.Contracts;
using TvMaze.Infrastructure.Database;
using TvMaze.Infrastructure.Database.Repository;
using TvMaze.Infrastructure.External;

namespace TvMaze.Infrastructure;

public static class InfrastructureServiceExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ExternalApiConfiguration>(configuration.GetSection("ExternalApi"));
        var connectionString = configuration.GetConnectionString("ShowsConnectionString");
        services.AddDbContext<TvMazeDbContext>(options =>
            options.UseSqlite(connectionString));

        services
            .AddScoped<IExternalTvMazeAdapter, ExternalTvMazeAdapter>()
            .AddScoped<IShowRepository, ShowRepository>()
            .AddScoped<INetworkRepository, NetworkRepository>();

        return services;
    }
}