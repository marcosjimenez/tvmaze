using AutoMapper;
using FluentAssertions;
using Moq;
using TvMaze.Application.Contracts;
using TvMaze.Application.Mappers;
using TvMaze.Application.Shows.Queries.GetAllShows;
using TvMaze.Core.Aggregates;

namespace TvMaze.UnitTests
{
    public class GetAllShowsQueryHandlerTests
    {
        [Fact]
        public async Task Handle_Works()
        {
            Mock<IShowRepository> showRepository = new Mock<IShowRepository>();

            showRepository.Setup(x => x.GetAllShowsAsync())
                .Returns(Task.Run(() => GetAllShows()));

            var myProfile = new ShowProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            IMapper mapper = new Mapper(configuration);

            var handler = new GetAllShowsQueryHandler(showRepository.Object, mapper);

            var response = await handler.Handle(new GetAllShowsQuery(), new CancellationToken());

            response.Should().NotBeNull();
            response.Count().Should().BeGreaterThan(0);
        }

        private static List<Show> GetAllShows()
            => [
                CreateTestShow(1),
                CreateTestShow(2),
                CreateTestShow(3),
                CreateTestShow(4),
                CreateTestShow(5)
            ];

        private static Show CreateTestShow(int id)
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
}