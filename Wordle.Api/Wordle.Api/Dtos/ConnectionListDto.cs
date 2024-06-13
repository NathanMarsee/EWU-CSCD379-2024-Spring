namespace Wordle.Api.Dtos
{
    public class ConnectionListDto
    {
        public int Count { get; set; }
        public List<string> Connections { get; set; } = [];
    }
}
