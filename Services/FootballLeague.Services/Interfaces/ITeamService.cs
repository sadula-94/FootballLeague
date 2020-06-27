using FootballLeague.Services.Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FootballLeague.Services.Interfaces
{
    public interface ITeamService
    {
        Task<TeamServiceModel> GetTeamAsync(int id);

        Task<ICollection<TeamServiceModel>> GetTeamsAsync();

        Task<TeamDetailsServiceModel> GetTeamDetailsAsync(int id);

        Task CreateAsync(TeamCreateServiceModel team);

        Task EditAsync(TeamServiceModel team);

        Task DeleteAsync(int id);

    }
}
