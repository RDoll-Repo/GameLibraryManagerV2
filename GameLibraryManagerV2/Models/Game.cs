
namespace GameLibraryManagerV2.Models
{
    public class Game
    {
        public Guid Id { get; set; }
        public String Title { get; set; }
        public Status Status { get; set; }
        public Console Console { get; set; }
        public Format Format { get; set; }
        public Genre Genre { get; set; }
        public int AverageLength { get; set; }
        public int YearOfRelease { get; set; }
        public DateTime LoggedAt { get; set; } = DateTime.UtcNow;
        public DateTime? CompletedAt { get; set; }
        public String? Notes { get; set; }
    }
}