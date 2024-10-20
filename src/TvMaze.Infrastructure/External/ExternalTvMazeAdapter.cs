using System.Net.Http.Headers;
using System.Text.Json;
using TvMaze.Application.Contracts;
using TvMaze.Core.Aggregates.TvMaze;

namespace TvMaze.Infrastructure.External;

public class ExternalTvMazeAdapter : IExternalTvMazeAdapter
{

    public async Task<IEnumerable<TvMazeShow>> ImportAsync(string baseUrl, CancellationToken cancellationToken)
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri(baseUrl);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        HttpResponseMessage response = await client.GetAsync("shows", cancellationToken);
        if (!response.IsSuccessStatusCode)
            throw new Exception($"Error importing data from TvMaze. Status Code: {response.StatusCode}, Message: {response.ReasonPhrase}");

        var responseText = await response.Content.ReadAsStringAsync(cancellationToken);

        var data = JsonSerializer.Deserialize<IEnumerable<TvMazeShow>>(responseText, jsonReadOptions);
        return data ?? throw new Exception("No data received from external service.");
    }

    private static readonly JsonSerializerOptions jsonReadOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

}
