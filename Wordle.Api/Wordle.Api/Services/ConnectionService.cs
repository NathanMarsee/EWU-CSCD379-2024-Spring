using Microsoft.EntityFrameworkCore;
using Wordle.Api.Dtos;
using Wordle.Api.Models;

namespace Wordle.Api.Services
{
    public class ConnectionService
    {
        private readonly List<ConnectionGroup> connections = ConnectionList();
        private static object _lock = new();

        public WordleDbContext Db { get; set; }

        public ConnectionService(WordleDbContext db)
        {
            Db = db;
        }

        public List<ConnectionGroup> GetRandomConnections()
        {
            var numberOfConnections = Db.ConnectionGroups.CountAsync();

            Random random = new();

            //return await Db.ConnectionGroups.Skip(randomIndex).FirstAsync();
            return Db.ConnectionGroups.OrderBy(x => random.Next()).Take(4).ToList();
        }

        public async Task<ConnectionsOfTheDay> GetConnectionsOfTheDay(DateOnly date)
        {
            ConnectionsOfTheDay? connectionsOfTheDay = await Db.ConnectionsOfTheDays
                .Include(connectionsOfTheDay => connectionsOfTheDay.Connections)
                .FirstOrDefaultAsync(connectionsOfTheDay => connectionsOfTheDay.Date == date);

            if (connectionsOfTheDay is null)
            {
                lock (_lock)
                {
                    connectionsOfTheDay = Db.ConnectionsOfTheDays
                        .Include(connectionsOfTheDay => connectionsOfTheDay.Connections)
                        .FirstOrDefault(connectionsOfTheDay => connectionsOfTheDay.Date == date);
                    if (connectionsOfTheDay is null)
                    {
                        /*var randomConnectionsTask = GetRandomConnections();
                        randomConnectionsTask.Wait();*/
                        var randomConnections = GetRandomConnections();
                        connectionsOfTheDay = new()
                        {
                            Connections = randomConnections,
                            Date = date,
                        };

                        Db.ConnectionsOfTheDays.Add(connectionsOfTheDay);
                        Db.SaveChanges();
                    }
                }
            }

            return connectionsOfTheDay!;
        }

        public async Task<ConnectionListDto> GetConnectionList(string query, int page, int pageSize)
        {

            var queryResult =
                Db.ConnectionGroups.
                Select(connection => new ConnectionDto() { Description = connection.Description, Items = connection.Items.ToList() })
                .Where(connectionDto => connectionDto.Description.StartsWith(query) || connectionDto.Items.Contains(query))
                .OrderBy(connectionDto => connectionDto.Description);

            var count = await queryResult.CountAsync();
            var results = await
                queryResult.Skip((page - 1) * pageSize)
                .Take(pageSize).ToListAsync();


            return new ConnectionListDto() { Count = count, Connections = ToWordList(results) };
        }

        public List<string> ToWordList(List<ConnectionDto> connectionDtos)
        {
            var list = new List<string>();
            foreach (var connectionDto in connectionDtos)
            { 
                list.Add(connectionDto.Description);
                foreach (var item in connectionDto.Items)
                {
                    list.Add(item);
                }
            }
            return list;
        }

        #region ConnectionList

        public static List<ConnectionGroup> ConnectionList()
        {
            return
            [
                new ConnectionGroup() { Description = "ocean phenomena", Items = ["current", "drift", "tide", "wave"] },
                new ConnectionGroup() { Description = "don't delay", Items = ["hurry", "now", "pronto", "stat"] },
                new ConnectionGroup() { Description = "dumbbell exercises", Items = ["curl", " fly", "press", "row"] },
                new ConnectionGroup() { Description = "philosopher homophones", Items = ["lock", "marks", "pane", "rustle"] },
                new ConnectionGroup() { Description = "conformists", Items = ["followers", "sheep", "puppets", "lemmings"] },
                new ConnectionGroup() { Description = "company ownership", Items = ["equity", "options", "shares", "stock"] },
                new ConnectionGroup() { Description = "U.S. cities", Items = ["billings", "buffalo", "mobile", "phoenix"] },
                new ConnectionGroup() { Description = "what 'digs' might mean", Items = ["apartment", "insults", "likes", "shovels"] },
                new ConnectionGroup() { Description = "remove, as body hair", Items = ["laser", "pluck", "thread", "wax"] },
                new ConnectionGroup() { Description = "things made of cells", Items = ["honeycomb", "organism", "solar panel", "spreadsheet"] },
                new ConnectionGroup() { Description = "twist around", Items = ["coil", "spool", "twine", "wrap"] },
                new ConnectionGroup() { Description = "b-_ _ _", Items = ["ball", "movie", "school", "vitamin"] }
            ];
        }

        #endregion ConnectionList
    }
}