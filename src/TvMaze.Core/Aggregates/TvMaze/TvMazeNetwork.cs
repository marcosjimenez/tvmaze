namespace TvMaze.Core.Aggregates.TvMaze;

public class TvMazeNetwork
{
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public TvMazeCountry? Country { get; set; }
    public string OfficialSite { get; set; } = String.Empty;
}
