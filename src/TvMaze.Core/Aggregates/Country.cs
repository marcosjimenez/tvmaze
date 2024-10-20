using System.ComponentModel.DataAnnotations.Schema;

namespace TvMaze.Core.Aggregates;

public class Country
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public string Code { get; set; } = String.Empty;
    public string Timezone { get; set; } = String.Empty;
}
