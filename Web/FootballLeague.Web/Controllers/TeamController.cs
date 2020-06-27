using FootballLeague.Services.Interfaces;
using FootballLeague.Services.Models.Models;
using FootballLeague.Web.ViewModels.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeague.Web.Controllers
{
    public class TeamController : Controller
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            this._teamService = teamService;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            ICollection<TeamViewModel> teamViewModels = new List<TeamViewModel>();
            try
            {
                var teams = await this._teamService.GetTeamsAsync();
                foreach (var team in teams)
                {
                    teamViewModels.Add(new TeamViewModel { Id = team.Id, Name = team.Name });
                }
            }
            catch (System.Exception) 
            {
                return RedirectToAction("Error");
            }
            
            return this.View(teamViewModels);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            TeamDetailsServiceModel team = null;
            try
            {
                team = await this._teamService.GetTeamDetailsAsync(id);
            }
            catch (System.Exception)
            {
                return RedirectToAction("Error");
            }

            TeamDetailsViewModel teamViewModel = new TeamDetailsViewModel() 
                { 
                    Id = team.Id, 
                    Name = team.Name,
                    Matches = team.Matches
                                    .Select(m => new MatchViewModel 
                                    { 
                                        Id = m.Id,
                                        HomeTeam = m.HomeTeam,
                                        AwayTeam = m.AwayTeam,
                                        MatchDay = m.MatchDay,
                                        HomeGoals = m.HomeGoals,
                                        AwayGoals = m.AwayGoals
                                    })
                                    .ToList(),
                }; 

            return this.View(teamViewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TeamViewModel teamInputModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await this._teamService
                                .CreateAsync(new TeamCreateServiceModel { Name = teamInputModel.Name });
                }
                catch (System.Exception)
                {
                    return RedirectToAction("Error");
                }
             

                return RedirectToAction("List");
            }

            return View(teamInputModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var team = await this._teamService.GetTeamAsync(id);
            TeamViewModel teamViewModel = new TeamViewModel() 
            { 
                    Id = team.Id, 
                    Name = team.Name 
            };

            return View(teamViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TeamViewModel teamViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await this._teamService.EditAsync(new TeamServiceModel { Id = teamViewModel.Id, Name = teamViewModel.Name });
                }
                catch (System.Exception)
                {
                    return RedirectToAction("Error");
                }

                return RedirectToAction("List");
            }

            return View(teamViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var team = await this._teamService.GetTeamAsync(id);
            TeamViewModel teamViewModel = new TeamViewModel()
            {
                Id = team.Id,
                Name = team.Name
            };

            return View(teamViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(TeamViewModel inputModel)
        {
            try
            {
                await this._teamService
              .DeleteAsync(inputModel.Id);

                return RedirectToAction("List");
            }
            catch (System.Exception)
            {
                return RedirectToAction("Error");
            }
        }

        [HttpGet]
        public IActionResult Error()
        {
            return View();
        }
    }
}
