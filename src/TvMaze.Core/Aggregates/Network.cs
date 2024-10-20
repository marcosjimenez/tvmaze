using System.ComponentModel.DataAnnotations;

namespace TvMaze.Core.Aggregates;

public class Network
{
    [Key]
    public int Id { get; set; }
    public string? Name { get; set; }
    public Country? Country { get; set; }
    public string? OfficialSite { get; set; }
}
