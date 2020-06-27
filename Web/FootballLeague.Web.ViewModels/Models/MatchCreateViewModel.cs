using System;
using System.ComponentModel.DataAnnotations;

namespace FootballLeague.Web.ViewModels.Models
{
    public class MatchCreateViewModel
    {
        [Required]
        public int HomeTeamId { get; set; }

        [Required]
        public int AwayTeamId { get; set; }

        [Required]
        public DateTime MatchDay { get; set; }
    }
}
