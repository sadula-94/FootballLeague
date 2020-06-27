using FootballLeague.Data;
using FootballLeague.Data.Models;
using FootballLeague.Services.Interfaces;
using FootballLeague.Services.Models.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeague.Services
{
    public class TeamService : ITeamService
    {
        private readonly ApplicationDbContext _dbContext;

        public TeamService(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task CreateAsync(TeamCreateServiceModel team)
        {
            var t = new Team() { Name = team.Name };
            this._dbContext
                .Teams
                .Add(t);
            this._dbContext.SaveChanges();

            var fixture = new Fixture
            {
                TeamId = t.Id,
                Team = t,
                Points = 0,
                Drawn = 0,
                Win = 0,
                Loss = 0,
                PlayedMatches = 0
            };

            this._dbContext
                .Fixtures
                .Add(fixture);
            this._dbContext.SaveChanges();

            t.Fixture = fixture;
            t.FixtureId = fixture.Id;
            
            await this._dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            this._dbContext
                .Teams
                .Remove(new Team { Id = id});
            await this._dbContext.SaveChangesAsync();
        }

        public async Task EditAsync(TeamServiceModel team)
        {
            var t = await this._dbContext
                            .Teams
                            .Where(t => t.Id == team.Id)
                            .FirstOrDefaultAsync();
            t.Name = team.Name;
            
            await this._dbContext.SaveChangesAsync();
        }

        public async Task<TeamServiceModel> GetTeamAsync(int id)
        {
            var team = await this._dbContext
                            .Teams
                            .Where(t => t.Id == id)
                            .Select(t => new TeamServiceModel { Id = t.Id, Name = t.Name })
                            .FirstOrDefaultAsync();

            return team;
        }

        public async Task<TeamDetailsServiceModel> GetTeamDetailsAsync(int id)
        {
            var team = await this._dbContext
                 .Teams
                 .Where(t => t.Id == id)
                 .Select(t => new TeamDetailsServiceModel
                 {
                     Id = t.Id,
                     Name = t.Name,
                     Matches = this._dbContext
                                             .Matches
                                             .Where(m => m.HomeTeamId == t.Id || m.AwayTeamId == t.Id)
                                             .Select(m => new MatchServiceModel
                                             {
                                                 Id = m.Id,
                                                 HomeTeam = m.HomeTeam.Name,
                                                 AwayTeam = m.AwayTeam.Name,
                                                 MatchDay = m.MatchDay,
                                                 HomeGoals = m.HomeGoals,
                                                 AwayGoals = m.AwayGoals
                                             })
                                             .ToList(),

                 })
                 .FirstOrDefaultAsync();

            return team;
        }
        
        public async Task<ICollection<TeamServiceModel>> GetTeamsAsync()
        {
            var teams = await this._dbContext
                                .Teams
                                .Select(t => new TeamServiceModel
                                {
                                    Id = t.Id,
                                    Name = t.Name
                                })
                                .ToListAsync();

            return teams;
        }
    
    }
}
