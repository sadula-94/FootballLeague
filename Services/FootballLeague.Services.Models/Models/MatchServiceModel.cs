using System;

namespace FootballLeague.Services.Models.Models
{
    public class MatchServiceModel
    {
        public int Id { get; set; }

        public string HomeTeam { get; set; }

        public string AwayTeam { get; set; }

        public uint HomeGoals { get; set; }

        public uint AwayGoals { get; set; }

        public DateTime MatchDay { get; set; }
    }
}
