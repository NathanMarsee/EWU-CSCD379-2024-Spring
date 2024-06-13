using Wordle.Api.Models;
using Wordle.Api.Services;

namespace Wordle.Api;
public static class Seeder
{
    public static async Task Seed(WordleDbContext db)
    {
        if (!db.Words.Any())
        {
            foreach(var word in WordOfTheDayService.WordList())
            {
                db.Words.Add(new Word() { Text = word });
            }

            await db.SaveChangesAsync();
        }

        if (!db.ConnectionGroups.Any())
        {
            foreach (var connection in ConnectionService.ConnectionList())
            {
                db.ConnectionGroups.Add(new ConnectionGroup() { Description = connection.Description, Items = connection.Items });
            }

            await db.SaveChangesAsync();
        }
    }
}
