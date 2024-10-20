namespace TvMaze.Core.Aggregates.TvMaze;

public class TvMazeLinks
{
    public TvMazeSelfReference Self { get; set; } = default!;
    public TvMazePreviousEpisode PreviousEpisode { get; set; } = default!;

    public TvMazeLinks()
    {

    }
}
