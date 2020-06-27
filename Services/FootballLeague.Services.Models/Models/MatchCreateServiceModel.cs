using System;

namespace FootballLeague.Services.Models.Models
{
    public class MatchCreateServiceModel
    {
        public int HomeTeamId { get; set; }

        public int AwayTeamId { get; set; }

        public DateTime MatchDay { get; set; }
    }
}
