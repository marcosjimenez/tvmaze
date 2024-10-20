using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using TvMaze.Core.Aggregates;
using TvMaze.Infrastructure.Database.Repository;

namespace TvMaze.IntegrationTests;

public class ShowRepositoryTests : TvMazeFixture
{

    [Fact]
    public async Task AddAsync_Works()
    {
        var repository = new ShowRepository(_context);
        var show = CreateTestShow(1);

        await repository.AddAsync(show);
        await repository.SaveAsync();

        var insertedShow = await _context.Shows.FirstOrDefaultAsync(x => x.Id == 1);
        insertedShow.Should().NotBeNull();
    }

    [Fact]
    public async Task GetAllShowsAsync_Works()
    {
        var repository = new ShowRepository(_context);
        List<Show> shows =
        [
            CreateTestShow(2),
            CreateTestShow(3),
            CreateTestShow(4),
            CreateTestShow(5),
            CreateTestShow(6)
        ];

        await _context.Shows.AddRangeAsync(shows);
        await _context.SaveChangesAsync();

        var insertedShows = await repository.GetAllShowsAsync();
        insertedShows.Count.Should().Be(5);
    }

    [Fact]
    public async Task AddRangeAsync_Works()
    {
        var repository = new ShowRepository(_context);
        List<Show> shows =
        [
            CreateTestShow(7),
            CreateTestShow(8),
            CreateTestShow(9),
            CreateTestShow(10),
            CreateTestShow(11)
        ];

        await repository.AddRangeAsync(shows);
        await repository.SaveAsync();

        _context.Shows.FirstOrDefault(x => x.Id == 7)
            .Should().NotBeNull();
        _context.Shows.FirstOrDefault(x => x.Id == 8)
            .Should().NotBeNull();
        _context.Shows.FirstOrDefault(x => x.Id == 9)
            .Should().NotBeNull();
        _context.Shows.FirstOrDefault(x => x.Id == 10)
            .Should().NotBeNull();
        _context.Shows.FirstOrDefault(x => x.Id == 11)
            .Should().NotBeNull();
    }

    [Fact]
    public async Task Exists_Works()
    {
        var repository = new ShowRepository(_context);
        await _context.Shows.AddAsync(CreateTestShow(12));
        await _context.SaveChangesAsync();

        repository.Exists(12).Should().BeTrue();
    }

    [Fact]
    public async Task GetByIdAsync_Works()
    {
        var repository = new ShowRepository(_context);

        await _context.Shows.AddAsync(CreateTestShow(13));
        await _context.SaveChangesAsync();

        var show = await repository.GetByIdAsync(13);
        show.Should().NotBeNull();
        show?.Id.Should().Be(13);
    }

    private Show CreateTestShow(int id)
        => new()
        {
            Id = id,
            Url = "https://www.tvmaze.com/shows/1/under-the-dome",
            Name = "Under the Dome",
            Type = "Scripted",
            Language = "English",
            Genres = ["Drama", "Science-Fiction", "Thriller"],
            Status = "Ended",
            Runtime = 60,
            AverageRuntime = 60,
            Premiered = "2013-06-24",
            Ended = "2015-09-10",
            OfficialSite = "http://www.cbs.com/shows/under-the-dome/",
            ScheduleDays = ["Thursday"],
            ScheduleTime = "22:00",
            RatingAverage = 6.5,
            Weight = 99,
            Network = new Network
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
            },
            Externals = new Externals
            {
                TvRage = 25988,
                TheTvdb = 264492,
                Imdb = "tt1553656"
            },
            Image = new Image
            {
                Medium = "https://static.tvmaze.com/uploads/images/medium_portrait/81/202627.jpg",
                Original = "https://static.tvmaze.com/uploads/images/original_untouched/81/202627.jpg"
            },
            Summary = "<p><b>Under the Dome</b> is the story of a small town that is suddenly and inexplicably sealed off from the rest of the world by an enormous transparent dome. The town's inhabitants must deal with surviving the post-apocalyptic conditions while searching for answers about the dome, where it came from and if and when it will go away.</p>",
            Updated = 1704794065,
            PreviousEpisodeLink = "https://api.tvmaze.com/episodes/185054",
            PreviousEpisodeName = "The Enemy Within",
            SelfLink = "https://api.tvmaze.com/shows/1"
        };
}