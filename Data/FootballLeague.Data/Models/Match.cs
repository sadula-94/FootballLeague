using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootballLeague.Data.Models
{
    public class Match
    {
        public int Id { get; set; }

        public int? HomeTeamId { get; set; }

        public int? AwayTeamId { get; set; }

        [ForeignKey("HomeTeamId")]
        public Team HomeTeam { get; set; }

        [ForeignKey("AwayTeamId")]
        public Team AwayTeam { get; set; }

        public uint HomeGoals { get; set; }

        public uint AwayGoals { get; set; }

        [Required]
        public DateTime MatchDay { get; set; }

    }
}
