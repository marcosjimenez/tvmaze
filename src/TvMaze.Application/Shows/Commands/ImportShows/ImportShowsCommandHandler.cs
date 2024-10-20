using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TvMaze.Application.Configuration;
using TvMaze.Application.Contracts;
using TvMaze.Core.Aggregates;

namespace TvMaze.Application.Shows.Commands.ImportShows;

public class ImportShowsCommandHandler(IServiceScopeFactory serviceScopeFactory, IOptions<ExternalApiConfiguration> externalApiConfiguration)
    : IRequestHandler<ImportShowsCommand, bool>
{

    private readonly IServiceScopeFactory _serviceScopeFactory = serviceScopeFactory ??
        throw new ArgumentNullException(nameof(serviceScopeFactory));
    private readonly ExternalApiConfiguration _externalApiConfiguration = externalApiConfiguration?.Value ??
        throw new ArgumentNullException(nameof(externalApiConfiguration));

    public Task<bool> Handle(ImportShowsCommand request, CancellationToken cancellationToken)
    {
        var task = Task.Run(() => ExecuteAsync(_externalApiConfiguration.BaseUrl, cancellationToken), cancellationToken);
        return task.IsCompleted ? task : Task.FromResult(false);
    }

    private async Task<bool> ExecuteAsync(string url, CancellationToken cancellationToken)
    {
        using var scope = _serviceScopeFactory.CreateScope();

        var logger = scope.ServiceProvider.GetRequiredService<ILogger<ImportShowsCommandHandler>>();
        var showRepository = scope.ServiceProvider.GetRequiredService<IShowRepository>();
        var networkRepository = scope.ServiceProvider.GetRequiredService<INetworkRepository>();
        var externalApiConnector = scope.ServiceProvider.GetRequiredService<IExternalTvMazeAdapter>();
        var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();

        try
        {
            var data = await externalApiConnector.ImportAsync(url, cancellationToken);
            var entities = mapper.Map<IEnumerable<Show>>(data);

            // Add networks first to avoid duplicates
            var networks = entities
                .Where(x => x.Network is not null)
                .Select(x => x.Network)
                .GroupBy(x => x?.Id)
                .Select(grp => grp.First())
                .ToList();
            if (networks is not null)
            {
                foreach (var network in networks)
                {
                    if (network is not null && !networkRepository.Exists(network.Id))
                        await networkRepository.AddAsync(network);
                }
                var networkCount = await networkRepository.SaveAsync();
                logger.LogInformation("{networkCount} networks imported.", networkCount);
            }

            foreach (var item in entities)
            {
                if (!showRepository.Exists(item.Id))
                {
                    item.Network = null;
                    await showRepository.AddAsync(item);
                }
            }
            var showCount = await showRepository.SaveAsync();
            logger.LogInformation("{showCount} items imported.", showCount);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error importing data");
            throw;
        }

        return true;
    }
}
