using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace FootballLeague.Services.Models.Models
{
    public class TeamDetailsServiceModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
    
        public ICollection<MatchServiceModel> Matches { get; set; }

    }
}
