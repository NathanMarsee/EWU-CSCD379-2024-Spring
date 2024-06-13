using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Wordle.Api.Models;
    [Table("ConnectionsOfTheDay")]
    public class ConnectionsOfTheDay
    {
        [Required]
        public int ConnectionsOfTheDayId { get; set; }
        /*[Required]
        public int ConnectionsId { get; set; }*/
        public List<ConnectionGroup>? Connections { get; set; }
        
        public DateOnly Date { get; set; }

        public ICollection<Game> Games { get; set; } = [];
    }
