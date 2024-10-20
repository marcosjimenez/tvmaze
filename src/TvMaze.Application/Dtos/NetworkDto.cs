namespace TvMaze.Application.Dtos;

public class NetworkDto
{
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public CountryDto? Country { get; set; }
    public string OfficialSite { get; set; } = String.Empty;
}
