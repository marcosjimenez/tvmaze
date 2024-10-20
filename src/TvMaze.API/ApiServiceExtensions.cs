using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using TvMaze.API.Authorization;

namespace TvMaze.API;

public static class ApiServiceExtensions
{
    public static IServiceCollection AddApiSecurity(this IServiceCollection services, string ApiKey)
    {
        services.AddHttpContextAccessor();
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer();
        services.AddAuthorizationBuilder()
            .AddPolicy("ApiKeyPolicy", policy =>
            {
                policy.AddAuthenticationSchemes(new[] { JwtBearerDefaults.AuthenticationScheme });
                policy.Requirements.Add(new ApiKeyRequirement { ApiKey = ApiKey });
            });
        services.AddScoped<IAuthorizationHandler, ApiKeyHandler>();

        return services;
    }
}
