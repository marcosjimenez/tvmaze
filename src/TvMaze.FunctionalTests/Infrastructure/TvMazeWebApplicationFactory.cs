using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TvMaze.Application.Mappers;
using TvMaze.FunctionalTests.Data;
using TvMaze.Infrastructure.Database;

namespace TvMaze.FunctionalTests.Infrastructure;

public class TvMazeWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.UseEnvironment("Test");
        var host = builder.Build();
        host.Start();

        var serviceProvider = host.Services;
        using (var scope = serviceProvider.CreateScope())
        {
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<TvMazeDbContext>();
            var mapper = scopedServices.GetRequiredService<IMapper>();

            var logger = scopedServices
                .GetRequiredService<ILogger<TvMazeWebApplicationFactory>>();

            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            try
            {
                FakeData.GenerateTvMazeDataFromFile(db, mapper);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error loading Fake Data");
            }
        }

        return host;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder
            .ConfigureServices(services =>
            {
                // Remove original dbContext to use InMemory
                RemoveService(services, typeof(DbContextOptions<TvMazeDbContext>));
                // Remove original Mapper to avoid assembly load errors
                RemoveService(services, typeof(IMapper));

                services.AddDbContext<TvMazeDbContext>(options =>
                {
                    options.UseInMemoryDatabase("Shows");
                });

                var mapperConfiguration = new MapperConfiguration(c =>
                {
                    c.AddProfile(new ShowProfile());
                });
                var mapper = mapperConfiguration.CreateMapper();
                services.AddSingleton(mapper);
                services.AddSingleton(new LinksResolver());
                services.AddSingleton(new RatingResolver());
                services.AddSingleton(new ScheduleResolver());
            });
    }

    private static void RemoveService(IServiceCollection services, Type typeToRemove)
    {
        var contextService = services.SingleOrDefault(x => x.ServiceType == typeToRemove);
        if (contextService is not null)
            services.Remove(contextService);
    }
}
