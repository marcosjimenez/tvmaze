using System.Text.Json.Serialization;

namespace TvMaze.Application.Dtos;

public class ShowDto
{
    public int Id { get; set; }
    public string Url { get; set; } = String.Empty;
    public string Name { get; set; } = String.Empty;
    public string Type { get; set; } = String.Empty;
    public string Language { get; set; } = String.Empty;
    public List<string> Genres { get; set; } = [];
    public string Status { get; set; } = String.Empty;
    public int? Runtime { get; set; }
    public int AverageRuntime { get; set; }
    public string Premiered { get; set; } = String.Empty;
    public string Ended { get; set; } = String.Empty;
    public string OfficialSite { get; set; } = String.Empty;
    public ScheduleDto? Schedule { get; set; }
    public RatingDto? Rating { get; set; }
    public int Weight { get; set; }
    public NetworkDto? Network { get; set; }
    public ExternalsDto? Externals { get; set; }
    public ImageDto? Image { get; set; }
    public string Summary { get; set; } = String.Empty;
    public int Updated { get; set; }
    [JsonPropertyName("_links")]
    public LinksDto? Links { get; set; }
}
