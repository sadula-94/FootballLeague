using System.ComponentModel.DataAnnotations;

namespace FootballLeague.Data.Models
{
    public class Fixture
    {
        public int Id { get; set; }

        [Required]
        public uint Points { get; set; }

        public uint Win { get; set; }

        public uint Drawn { get; set; }

        public uint Loss { get; set; }

        public uint PlayedMatches { get; set; }

        [Required]
        public Team Team { get; set; }

        public int TeamId { get; set; }

    }
}
