using FluentAssertions;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;
using TvMaze.Application.Dtos;
using TvMaze.FunctionalTests.Infrastructure;

namespace TvMaze.FunctionalTests
{
    [Collection("Shows")]
    public class ShowEndpointsTests(TvMazeWebApplicationFactory factory) : IClassFixture<TvMazeWebApplicationFactory>
    {

        private readonly HttpClient _client = factory.CreateClient();

        [Fact]
        public async Task ShowsLists_Returns_OK()
        {
            var data = await _client.GetFromJsonAsync<List<ShowDto>>("api/shows");

            data.Should().NotBeNullOrEmpty();
            data?.Count.Should().Be(240); // Total items on sample data file
        }

        [Fact]
        public async Task Import_Returns_OK()
        {
            var config = new ConfigurationBuilder().AddJsonFile("testsettings.json").Build();
            _client.DefaultRequestHeaders.Add("ApiKey", config["ApiKey"]);

            var importAction = await _client.PostAsJsonAsync("api/import/shows", string.Empty);

            importAction.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task Import_Returns_Unauthorized_Without_ApiKey()
        {
            _client.DefaultRequestHeaders.Remove("ApiKey");
            var importAction = await _client.PostAsJsonAsync<string>("api/import/shows", string.Empty);
            importAction.StatusCode.Should().Be(System.Net.HttpStatusCode.Unauthorized);
        }

    }
}