using System.ComponentModel.DataAnnotations;

namespace Wordle.Api.Models;
public class ConnectionGame
{
    public int GameId { get; set; }

    public int Attempts { get; set; }

    public bool IsWin {  get; set; }

    public DateTime DateAttempted { get; set; } = DateTime.UtcNow.AddHours(-7);

    [Required]
    public int? ConnectionsOfTheDayId { get; set; }
    public WordOfTheDay? ConnectionsOfTheDay { get; set; }

    public string? PlayerName { get; set; }

    public List<ConnectionGroup>? Connections { get; set; }
}
