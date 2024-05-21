﻿using Microsoft.EntityFrameworkCore;
using Wordle.Api.Dtos;
using Wordle.Api.Models;

namespace Wordle.Api.Services;
public class GameService(WordleDbContext db)
{
    public WordleDbContext Db { get; set; } = db;


    public async Task<Game> PostGameResult(GameDto gameDto)
    {
        // Get todays date
        var today = DateOnly.FromDateTime(DateTime.UtcNow);

        // Get all the words that match our game word and load their WOTDs
        var word = Db.Words
        .Include(word => word.WordsOfTheDays)
            .Where(word => word.Text == gameDto.Word)
            .First();

        // Create a new game object to save to the DB
        Game game = new()
        {
            Attempts = gameDto.Attempts,
            IsWin = gameDto.IsWin,
            // Attempt to find the WOTD that best matches todays date
            WordOfTheDay = word.WordsOfTheDays
                .OrderByDescending(wotd => wotd.Date)
                .FirstOrDefault(wotd => wotd.Date < today),
            Word = word
        };

        Db.Games.Add(game);
        await Db.SaveChangesAsync();
        return game;
    }

    public async Task<GameStatsDto> GetGameStats(Game game)
    {
        var gamesForWord = Db.Games.Where(g => g.WordId == game.WordId);

        GameStatsDto stats = new()
        {
            Word = game.Word!.Text,
            AverageGuesses = await gamesForWord.AverageAsync(g => g.Attempts),
            TotalTimesPlayed = await gamesForWord.CountAsync(),
            TotalWins = await gamesForWord.CountAsync(g => g.IsWin)
        };

        return stats;
    }

    public IQueryable<AllWordStats> StatsForAllWords()
    {
        IQueryable<AllWordStats> result = Db.Games
            .Include(g => g.Word)
            .GroupBy(g => g.Word!.Text)
            .Select(g => new AllWordStats()
            {
                Word = g.Key,
                AverageGuesses = g.Average(x => x.Attempts)
            });

        return result;
    }

    public async Task<List<GameStatsDto>> GetGames(DateTime date, int num)
    {
        List<GameStatsDto> gameStatsList = [];

        for (int i = 0; i < num; i++)
        {
            DateOnly dateOnly = DateOnly.FromDateTime(date.AddDays(-i));

            WordOfTheDay? word = await Db.WordsOfTheDays
                .Include(wotd => wotd.Games)
                .FirstOrDefaultAsync(wotd => wotd.Date == dateOnly);

            GameStatsDto stats = new()
            {
                Date = dateOnly,
                AverageGuesses = word?.Games.Average(g => g.Attempts) ?? 0,
                AverageTime = word?.Games.Average(w => w.Time) ?? 0,
                TotalTimesPlayed = word?.Games.Count ?? 0,
                TotalWins = word?.Games.Count(g => g.IsWin) ?? 0
            };

            gameStatsList.Add(stats);
        }

        return gameStatsList;
    }


    public class AllWordStats()
    {
        public required string Word { get; set; }

        public double AverageGuesses { get; set; }
    }
}
