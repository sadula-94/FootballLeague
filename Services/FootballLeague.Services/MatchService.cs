using FootballLeague.Data;
using FootballLeague.Data.Models;
using FootballLeague.Services.Interfaces;
using FootballLeague.Services.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeague.Services
{
    public class MatchService : IMatchService
    {
        private readonly ApplicationDbContext _dbContext;

        public MatchService(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task CreateAsync(MatchCreateServiceModel match)
        {
            this._dbContext
                .Matches
                .Add(new Match
                {
                    HomeTeamId = match.HomeTeamId,
                    HomeTeam = this._dbContext
                                    .Teams
                                    .Where(t => t.Id == match.HomeTeamId)
                                    .FirstOrDefault(),
                    AwayTeamId = match.AwayTeamId,
                    AwayTeam = this._dbContext
                                    .Teams
                                    .Where(t => t.Id == match.AwayTeamId)
                                    .FirstOrDefault(),
                    MatchDay = match.MatchDay
                });

            await this._dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            this._dbContext
                .Matches
                .Remove(new Match { Id = id });
            await this._dbContext.SaveChangesAsync();
        }

        public async Task EditAsync(MatchServiceModel match)
        {
            var m = await this._dbContext
                        .Matches
                        .Where(m => m.Id == match.Id)
                        .FirstOrDefaultAsync();
            
            m.HomeGoals = match.HomeGoals;
            m.AwayGoals = match.AwayGoals;

            var homeTeam = await this._dbContext
                                     .Fixtures
                                     .Where(f => f.TeamId == m.HomeTeamId)
                                     .FirstOrDefaultAsync();

            var awayTeam = await this._dbContext
                                     .Fixtures
                                     .Where(f => f.TeamId == m.AwayTeamId)
                                     .FirstOrDefaultAsync();
            homeTeam.PlayedMatches++;
            awayTeam.PlayedMatches++;

            if (match.HomeGoals > match.AwayGoals)
            {
                homeTeam.Points += 3;
                homeTeam.Win += 1;
                awayTeam.Loss += 1;
            }
            else if (match.HomeGoals < match.AwayGoals)
            {
                awayTeam.Points += 3;
                awayTeam.Win += 1;
                homeTeam.Loss += 1;
            }
            else
            {
                awayTeam.Points += 1;
                homeTeam.Points += 1;
                awayTeam.Drawn += 1;
                homeTeam.Drawn += 1;
            }

            await this._dbContext.SaveChangesAsync();
        }

        public async Task<MatchServiceModel> GetMatchDetailsAsync(int id)
        {
            var match = await this._dbContext
                                    .Matches
                                    .Where(m => m.Id == id)
                                    .Select(m => new MatchServiceModel
                                    {
                                        AwayTeam = m.AwayTeam.Name,
                                        HomeTeam = m.HomeTeam.Name,
                                        MatchDay = m.MatchDay,
                                        HomeGoals = m.HomeGoals,
                                        AwayGoals = m.AwayGoals
                                    })
                                    .FirstOrDefaultAsync();

            return match;
        }

        public async Task<ICollection<MatchServiceModel>> GetMatchesAsync(DateTime day)
        {
            var matches = await this._dbContext
                .Matches
                .Where(m => m.MatchDay.Day == day.Day )
                .Select(m => new MatchServiceModel
                {
                    Id = m.Id,
                    AwayTeam = m.AwayTeam.Name,
                    HomeTeam = m.HomeTeam.Name,
                    MatchDay = m.MatchDay,
                    HomeGoals = m.HomeGoals,
                    AwayGoals = m.AwayGoals

                })
                .ToListAsync();

            return matches;
        }
    }
}
