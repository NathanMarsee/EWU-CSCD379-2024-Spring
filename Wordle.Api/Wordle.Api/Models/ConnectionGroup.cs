using System.ComponentModel.DataAnnotations;

namespace Wordle.Api.Models;
public class ConnectionGroup
{
    public int WordId { get; set; }

    [Required]
    public required string Description { get; set; }

    public string[] Items { get; set; } = new string[4];

    public ICollection<Game> Games { get; set; } = [];
    public ICollection<ConnectionsOfTheDay> ConnectionsOfTheDays { get; set; } = [];
}
