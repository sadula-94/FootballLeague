using System.ComponentModel;

namespace FootballLeague.Web.ViewModels.Models
{
    public class FixtureViewModel
    {
        public string Team { get; set; }

        public uint Points { get; set; }

        public uint Win { get; set; }

        public uint Drawn { get; set; }

        public uint Loss { get; set; }

        [DisplayName("Played matches")]
        public uint PlayedMatches { get; set; }
    }
}
