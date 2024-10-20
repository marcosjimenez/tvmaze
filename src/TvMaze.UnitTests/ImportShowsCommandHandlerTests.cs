using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using TvMaze.Application.Configuration;
using TvMaze.Application.Contracts;
using TvMaze.Application.Mappers;
using TvMaze.Application.Shows.Commands.ImportShows;
using TvMaze.Core.Aggregates.TvMaze;

namespace TvMaze.UnitTests;

public class ImportShowsCommandHandlerTests
{
    [Fact]
    public async Task Handle_Works()
    {

        Mock<IServiceScopeFactory> serviceScopeFactory = new();
        Mock<IServiceProvider> serviceProvider = new();
        Mock<IServiceScope> serviceScope = new();
        Mock<IOptions<ExternalApiConfiguration>> externalApiConfiguration = new();
        Mock<ILogger<ImportShowsCommandHandler>> logger = new();
        Mock<IShowRepository> showRepository = new();
        Mock<INetworkRepository> networkRepository = new();
        Mock<IExternalTvMazeAdapter> externalApiConnector = new();
        var myProfile = new ShowProfile();
        var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
        IMapper mapper = new Mapper(mapperConfiguration);

        serviceScopeFactory.Setup(x => x.CreateScope())
            .Returns(serviceScope.Object);
        serviceScope.Setup(x => x.ServiceProvider)
            .Returns(serviceProvider.Object);
        serviceProvider.Setup(x => x.GetService(typeof(ILogger<ImportShowsCommandHandler>)))
            .Returns(logger.Object);
        serviceProvider.Setup(x => x.GetService(typeof(IShowRepository)))
            .Returns(showRepository.Object);
        serviceProvider.Setup(x => x.GetService(typeof(INetworkRepository)))
            .Returns(networkRepository.Object);
        serviceProvider.Setup(x => x.GetService(typeof(IExternalTvMazeAdapter)))
            .Returns(externalApiConnector.Object);
        serviceProvider.Setup(x => x.GetService(typeof(IMapper)))
            .Returns(mapper);

        externalApiConfiguration.SetupGet(x => x.Value)
            .Returns(new ExternalApiConfiguration
            {
                BaseUrl = "http://localhost"
            });

        externalApiConnector.Setup(x => x.ImportAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .Returns(Task.Run(() => GetAllShowsDtoList()));


        var handler = new ImportShowsCommandHandler(serviceScopeFactory.Object, externalApiConfiguration.Object);
        var response = await handler.Handle(new ImportShowsCommand(), new CancellationToken());

        response.Should().Be(false); // returns IsCompleted.
    }

    private static IEnumerable<TvMazeShow> GetAllShowsDtoList()
        => [
            CreateTestShowDto(1),
            CreateTestShowDto(2),
            CreateTestShowDto(3),
            CreateTestShowDto(4),
            CreateTestShowDto(5)
        ];

    private static TvMazeShow CreateTestShowDto(int id)
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
           Schedule = new TvMazeSchedule
           {
               Days = ["Thursday"],
               Time = "22:00",
           },
           Rating = new TvMazeRating
           {
               Average = 6.5,

           },
           Weight = 99,
           Network = new TvMazeNetwork
           {
               Id = id,
               Name = "CBS",
               Country = new TvMazeCountry
               {
                   Name = "United States",
                   Code = "US",
                   Timezone = "America/New_York"
               },
               OfficialSite = "https://www.cbs.com/"
           },
           Externals = new TvMazeExternals
           {
               TvRage = 25988,
               TheTvdb = 264492,
               Imdb = "tt1553656"
           },
           Image = new TvMazeImage
           {
               Medium = "https://static.tvmaze.com/uploads/images/medium_portrait/81/202627.jpg",
               Original = "https://static.tvmaze.com/uploads/images/original_untouched/81/202627.jpg"
           },
           Summary = "<p><b>Under the Dome</b> is the story of a small town that is suddenly and inexplicably sealed off from the rest of the world by an enormous transparent dome. The town's inhabitants must deal with surviving the post-apocalyptic conditions while searching for answers about the dome, where it came from and if and when it will go away.</p>",
           Updated = 1704794065,
           Links = new TvMazeLinks
           {
               Self = new TvMazeSelfReference
               {
                   Href = "https://api.tvmaze.com/shows/1"
               },
               PreviousEpisode = new TvMazePreviousEpisode
               {
                   Href = "https://api.tvmaze.com/episodes/185054",
                   Name = "The Enemy Within",
               }
           }
       };
}
