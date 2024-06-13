using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wordle.Api.Dtos;
using Wordle.Api.Identity;
using Wordle.Api.Services;

namespace Wordle.Api.Controllers;
[ApiController]
[Route("[controller]")]
public class ConnectionController(ConnectionService connectionService) : ControllerBase
{
    [HttpGet("RandomConnections")]
    public async Task<ConnectionListDto> GetRandomConnections(int count)
    {
        var randomConnections = await connectionService.GetRandomConnections(count);
        List<string> connections = [];
        foreach(var connection in randomConnections) 
        {
            connections.Add(connection.Description);
            foreach(string item in connection.Items)
            {
                connections.Add(item);
            }
        }
        return new ConnectionListDto { Count = count, Connections = connections };
    }

    /// <summary>
    /// Get the word of the day.
    /// </summary>
    /// <param name="offsetInHours">Timezone offset in hours. Default to PST</param>
    /// <returns></returns>
    [HttpGet("ConnectionsOfTheDay")]
    public async Task<ConnectionListDto> GetConnectionsOfTheDay(double offsetInHours = -7.0)
    {
        DateOnly today = DateOnly.FromDateTime(DateTime.UtcNow.AddHours(offsetInHours));
        return await connectionService.GetConnectionsOfTheDay(today);
    }

    [HttpGet("ConnectionsList/")]
    public async Task<ConnectionListDto> GetConnectionList(string query = "", int page = 1, int pageSize = 10)
    {
        return await connectionService.GetConnectionList(query, page, pageSize);
    }
}
