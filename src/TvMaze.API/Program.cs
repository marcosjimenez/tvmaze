using TvMaze.API;
using TvMaze.Application;
using TvMaze.Infrastructure;
using TvMaze.Infrastructure.Database;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddApiSecurity(builder.Configuration["ApiKey"] ?? String.Empty)
    .AddInfrastructureServices(builder.Configuration)
    .AddApplicationServices();

var app = builder.Build();

// Ensure DB
using var scope = app.Services.CreateScope();
var dbcontext = scope.ServiceProvider.GetRequiredService<TvMazeDbContext>();
dbcontext.Database.EnsureCreated();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

public partial class Program { } // For tests