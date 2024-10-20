using Microsoft.AspNetCore.Authorization;

namespace TvMaze.API.Authorization;

public class ApiKeyRequirement : IAuthorizationRequirement
{
    public string ApiKey { get; set; } = String.Empty;
}
