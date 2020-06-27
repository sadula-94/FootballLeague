using System;
using System.ComponentModel;

namespace FootballLeague.Web.ViewModels.Models
{
    public class MatchViewModel
    {
        public int Id { get; set; }

        [DisplayName("Home team")]
        public string HomeTeam { get; set; }


        [DisplayName("Away team")]
        public string AwayTeam { get; set; }

        public uint HomeGoals { get; set; }

        public uint AwayGoals { get; set; }

        public string Score 
        { 
            get 
            {
                return $"{HomeGoals} - {AwayGoals}";
            }
        }

        [DisplayName("Match date time")]
        public DateTime MatchDay { get; set; }
    }
}
