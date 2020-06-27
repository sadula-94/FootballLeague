using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FootballLeague.Data.Models
{
    public class Team
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public ICollection<Match> HomeMatches { get; set; }

        public ICollection<Match> AwayMatches { get; set; }

        public int? FixtureId { get; set; }

        public Fixture Fixture { get; set; }

    }
}
