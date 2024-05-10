﻿using Microsoft.AspNetCore.Mvc;
using Wordle.Api.Models;
using Wordle.Api.Services;

namespace Wordle.Api.Controllers;
[ApiController]
[Route("[controller]")]
public class ScoreController(LeaderboardService leaderboardService) : ControllerBase
{
    [HttpGet("Leaderboard")]
    public async Task<List<Score>> GetTopTen()
    {
        return await leaderboardService.GetTopTenScores();
    }
    [HttpPost("UpdateScore")]
    public async Task<bool> UpdateScore(NewScore score)
    {
        return await leaderboardService.UpdateScore(score);
    }
}
