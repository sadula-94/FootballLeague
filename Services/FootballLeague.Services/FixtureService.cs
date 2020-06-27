using FootballLeague.Data;
using FootballLeague.Services.Interfaces;
using FootballLeague.Services.Models.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeague.Services
{
    public class FixtureService : IFixtureService
    {
        private readonly ApplicationDbContext _dbContext; 

        public FixtureService(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<ICollection<FixtureServiceModel>> Show()
        {
            var fixture = await this._dbContext
                               .Fixtures
                               .Select(f => new FixtureServiceModel
                               {
                                   Team = f.Team.Name,
                                   Points = f.Points,
                                   Drawn = f.Drawn,
                                   Win = f.Win,
                                   Loss = f.Loss,
                                   PlayedMatches = f.PlayedMatches
                               })
                               .OrderByDescending(f => f.Points)
                               .ToListAsync();

            return fixture;
        }
    }
}
