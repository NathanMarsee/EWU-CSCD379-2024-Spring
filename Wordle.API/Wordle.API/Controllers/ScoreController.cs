﻿using Microsoft.AspNetCore.Mvc;
using Wordle.Api.Services;

namespace Wordle.Api.Controllers;
[ApiController]
[Route("[controller]")]
public class ScoreController(LeaderboardService leaderboardService) : ControllerBase
{
    [HttpGet("Leaderboard")]
    public async Task<string[]> GetTopTen()
    {
        return await leaderboardService.GetTopTenScores();
    }
    public async Task<bool> PostScore(string playerName, int attempts, int time)
    {
        return await leaderboardService.UpdateScore(playerName, attempts, time);
    }
}