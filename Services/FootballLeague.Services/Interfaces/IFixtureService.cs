using FootballLeague.Services.Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FootballLeague.Services.Interfaces
{
    public interface IFixtureService
    {
        Task<ICollection<FixtureServiceModel>> Show();
    }
}
