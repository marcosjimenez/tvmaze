using System.ComponentModel.DataAnnotations.Schema;

namespace TvMaze.Core.Aggregates;

public class Image
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Medium { get; set; } = String.Empty;
    public string Original { get; set; } = String.Empty;
}
