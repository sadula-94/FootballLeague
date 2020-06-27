namespace FootballLeague.Services.Models.Models
{
    public class FixtureServiceModel
    {
        public string Team { get; set; }
       
        public uint Points { get; set; }

        public uint Win { get; set; }

        public uint Drawn { get; set; }

        public uint Loss { get; set; }

        public uint PlayedMatches { get; set; }

    }
}
