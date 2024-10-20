using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using TvMaze.Core.Aggregates;
using TvMaze.Infrastructure.Database.Repository;

namespace TvMaze.IntegrationTests;

public class NetworkRepositoryTests : TvMazeFixture
{
    [Fact]
    public async Task AddAsync_Works()
    {
        var repository = new NetworkRepository(_context);
        var network = CreateTestNetwork(1);

        await repository.AddAsync(network);
        await repository.SaveAsync();

        var insertedNetwork = await _context.Networks.FirstOrDefaultAsync(x => x.Id == 1);
        insertedNetwork.Should().NotBeNull();
    }

    [Fact]
    public async Task AddRangeAsync_Works()
    {
        var repository = new NetworkRepository(_context);
        List<Network> networks =
        [
            CreateTestNetwork(2),
            CreateTestNetwork(3),
            CreateTestNetwork(4),
            CreateTestNetwork(5),
            CreateTestNetwork(6)
        ];

        await repository.AddRangeAsync(networks);
        await repository.SaveAsync();

        _context.Networks.FirstOrDefault(x => x.Id == 2)
            .Should().NotBeNull();
        _context.Networks.FirstOrDefault(x => x.Id == 3)
            .Should().NotBeNull();
        _context.Networks.FirstOrDefault(x => x.Id == 4)
            .Should().NotBeNull();
        _context.Networks.FirstOrDefault(x => x.Id == 5)
            .Should().NotBeNull();
        _context.Networks.FirstOrDefault(x => x.Id == 6)
            .Should().NotBeNull();
    }

    [Fact]
    public async Task Exists_Works()
    {
        var repository = new NetworkRepository(_context);
        await _context.Networks.AddAsync(CreateTestNetwork(7));
        await _context.SaveChangesAsync();

        repository.Exists(7).Should().BeTrue();
    }

    private Network CreateTestNetwork(int id)
        => new()
        {
            Id = id,
            Name = "CBS",
            Country = new Country
            {
                Name = "United States",
                Code = "US",
                Timezone = "America/New_York"
            },
            OfficialSite = "https://www.cbs.com/"
        };
}
