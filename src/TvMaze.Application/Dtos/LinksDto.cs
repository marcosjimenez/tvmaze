namespace TvMaze.Application.Dtos;

public class LinksDto
{
    public SelfReferenceDto? Self { get; set; }
    public PreviousEpisodeDto? PreviousEpisode { get; set; }

    public LinksDto()
    {

    }
}
