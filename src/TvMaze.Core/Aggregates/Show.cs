using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TvMaze.Core.Aggregates;

public class Show
{
    [Key]
    public int Id { get; set; }
    public string Url { get; set; } = String.Empty;
    public string Name { get; set; } = String.Empty;
    public string Type { get; set; } = String.Empty;
    public string Language { get; set; } = String.Empty;
    public virtual List<string> Genres { get; set; } = [];
    public string Status { get; set; } = String.Empty;
    public int Runtime { get; set; }
    public int AverageRuntime { get; set; }
    public string Premiered { get; set; } = String.Empty;
    public string? Ended { get; set; } = String.Empty;
    public string? OfficialSite { get; set; } = String.Empty;
    public string ScheduleTime { get; set; } = String.Empty;
    public virtual List<string> ScheduleDays { get; set; } = [];
    public double RatingAverage { get; set; }
    public int Weight { get; set; }
    public int? NetworkId { get; set; }
    [ForeignKey(nameof(NetworkId))]
    public virtual Network? Network { get; set; }
    public virtual Externals? Externals { get; set; }
    public virtual Image? Image { get; set; }
    public string Summary { get; set; } = String.Empty;
    public int Updated { get; set; }
    public string SelfLink { get; set; } = String.Empty;
    public string PreviousEpisodeLink { get; set; } = String.Empty;
    public string PreviousEpisodeName { get; set; } = String.Empty;
}
