using System.ComponentModel.DataAnnotations.Schema;

namespace TvMaze.Core.Aggregates;

public class Externals
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int TvRage { get; set; }
    public int TheTvdb { get; set; }
    public string? Imdb { get; set; } = String.Empty;
}
