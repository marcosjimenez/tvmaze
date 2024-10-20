using AutoMapper;
using System.Text;
using System.Text.Json;
using TvMaze.Application.Dtos;
using TvMaze.Core.Aggregates;
using TvMaze.Infrastructure.Database;

namespace TvMaze.FunctionalTests.Data
{
    public static class FakeData
    {

        private const string FakeDataFile = @".\Data\SampleData.json";
        private static readonly JsonSerializerOptions jsonSerializerOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public static void GenerateTvMazeDataFromFile(TvMazeDbContext context, IMapper mapper)
        {
            ArgumentNullException.ThrowIfNull(context);

            if (!File.Exists(FakeDataFile))
                throw new FileNotFoundException($"Cannot open data file {FakeDataFile}");

            var json = File.ReadAllText(FakeDataFile, Encoding.UTF8);
            var showsDto = JsonSerializer.Deserialize<IEnumerable<ShowDto>>(json, jsonSerializerOptions);

            var showsEntities = mapper.Map<IEnumerable<Show>>(showsDto);

            var networks = showsEntities
                .Where(x => x.Network is not null)
                .Select(x => x.Network)
                .GroupBy(x => x?.Id)
                .Select(grp => grp.First())
                .ToList();
            if (networks is not null)
            {
                foreach (var network in networks)
                {
                    if (network is not null)
                        context.Networks.Add(network);
                }
                context.SaveChanges();
            }

            foreach (var item in showsEntities)
            {
                item.Network = null;
                context.Shows.Add(item);
            }

            context.SaveChanges();
        }
    }
}
