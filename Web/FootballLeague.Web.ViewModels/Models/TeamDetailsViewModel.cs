using System.Collections.Generic;

namespace FootballLeague.Web.ViewModels.Models
{
    public class TeamDetailsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<MatchViewModel> Matches { get; set; }

    }
}
