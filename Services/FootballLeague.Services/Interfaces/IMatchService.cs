using FootballLeague.Services.Models.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FootballLeague.Services.Interfaces
{
    public interface IMatchService
    {
        Task<ICollection<MatchServiceModel>> GetMatchesAsync(DateTime day);

        Task<MatchServiceModel> GetMatchDetailsAsync(int id);

        Task CreateAsync(MatchCreateServiceModel match);

        Task EditAsync(MatchServiceModel match);

        Task DeleteAsync(int id);
    }
}
