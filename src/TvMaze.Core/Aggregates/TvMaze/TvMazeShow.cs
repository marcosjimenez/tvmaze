using System.Text.Json.Serialization;

namespace TvMaze.Core.Aggregates.TvMaze;

public class TvMazeShow
{
    public int Id { get; set; }
    public string Url { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Language { get; set; } = string.Empty;
    public List<string> Genres { get; set; } = [];
    public string Status { get; set; } = string.Empty;
    public int? Runtime { get; set; }
    public int AverageRuntime { get; set; }
    public string Premiered { get; set; } = string.Empty;
    public string Ended { get; set; } = string.Empty;
    public string OfficialSite { get; set; } = string.Empty;
    public TvMazeSchedule Schedule { get; set; } = default!;
    public TvMazeRating Rating { get; set; } = default!;
    public int Weight { get; set; }
    public TvMazeNetwork? Network { get; set; }
    public TvMazeExternals? Externals { get; set; }
    public TvMazeImage? Image { get; set; }
    public string Summary { get; set; } = string.Empty;
    public int Updated { get; set; }
    [JsonPropertyName("_links")]
    public TvMazeLinks Links { get; set; } = default!;
}
