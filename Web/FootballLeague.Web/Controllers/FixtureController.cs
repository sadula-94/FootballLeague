using FootballLeague.Services.Interfaces;
using FootballLeague.Web.ViewModels.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FootballLeague.Web.Controllers
{
    public class FixtureController : Controller
    {
        private readonly IFixtureService _fixtureService;

        public FixtureController(IFixtureService fixtureService)
        {
            this._fixtureService = fixtureService;
        }

        public async Task<IActionResult> Index()
        {
            var fixture = await this._fixtureService.Show();
            ICollection<FixtureViewModel> fixtureViewModel = new List<FixtureViewModel>();
            
            foreach (var team in fixture)
            {
                fixtureViewModel.Add(new FixtureViewModel
                {
                    Team = team.Team,
                    Points = team.Points,
                    Drawn = team.Drawn,
                    Win = team.Win,
                    Loss = team.Loss,
                    PlayedMatches = team.PlayedMatches
                });
            }

            return View(fixtureViewModel);
        }
    }
}
