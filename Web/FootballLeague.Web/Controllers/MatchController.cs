using FootballLeague.Services.Interfaces;
using FootballLeague.Services.Models.Models;
using FootballLeague.Web.ViewModels.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FootballLeague.Web.Controllers
{
    public class MatchController : Controller
    {
        private readonly IMatchService _matchService;

        public MatchController(IMatchService matchService)
        {
            this._matchService = matchService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> List(MatchDayViewModel matchDayViewModel)
        {
            var matches = await this._matchService
                                .GetMatchesAsync(matchDayViewModel.MatchDay);
            ICollection<MatchViewModel> matchesViewModel = new List<MatchViewModel>();

            foreach (var match in matches)
            {
                matchesViewModel.Add(new MatchViewModel
                {
                    Id = match.Id,
                    HomeTeam = match.HomeTeam,
                    AwayTeam = match.AwayTeam,
                    HomeGoals = match.HomeGoals,
                    AwayGoals = match.AwayGoals,
                    MatchDay = match.MatchDay
                });
            }

            return View(matchesViewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Create(MatchCreateViewModel matchViewModel)
        {
            bool isValid = matchViewModel.AwayTeamId == matchViewModel.HomeTeamId ? false : true;

            if (ModelState.IsValid && isValid)
            { 
                await this._matchService.CreateAsync(new MatchCreateServiceModel
                {
                    HomeTeamId = matchViewModel.HomeTeamId,
                    AwayTeamId = matchViewModel.AwayTeamId,
                    MatchDay = matchViewModel.MatchDay
                });
                
                return RedirectToAction("Index");
            }

            return View(matchViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var match = await this._matchService
                            .GetMatchDetailsAsync(id);
            MatchViewModel matchViewModel = new MatchViewModel();

            if (match != null)
            {
                matchViewModel.Id = match.Id;
                matchViewModel.HomeTeam = match.HomeTeam;
                matchViewModel.AwayTeam = match.AwayTeam;
                matchViewModel.HomeGoals = match.HomeGoals;
                matchViewModel.AwayGoals = match.AwayGoals;
                matchViewModel.MatchDay = match.MatchDay;
            }
            
            return View(matchViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MatchViewModel matchViewModel)
        {
            if (matchViewModel.MatchDay > DateTime.UtcNow)
            {
                return RedirectToAction("Error");
            }

            if (ModelState.IsValid)
            {
                await this._matchService
                 .EditAsync(new MatchServiceModel
                 {
                     Id = matchViewModel.Id,
                     HomeTeam = matchViewModel.HomeTeam,
                     AwayTeam = matchViewModel.AwayTeam,
                     HomeGoals = matchViewModel.HomeGoals,
                     AwayGoals = matchViewModel.AwayGoals
                 });

                return RedirectToAction("Index");
            }

            return View(matchViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var match = await this._matchService
                            .GetMatchDetailsAsync(id);
            MatchViewModel matchViewModel = new MatchViewModel()
            {
                Id = match.Id,
                HomeTeam = match.HomeTeam,
                AwayTeam = match.AwayTeam,
                HomeGoals = match.HomeGoals,
                AwayGoals = match.AwayGoals,
                MatchDay = match.MatchDay
            };


            return View(matchViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(MatchViewModel matchViewModel)
        {
            if (matchViewModel.MatchDay > DateTime.UtcNow)
            {
                return RedirectToAction("Error");
            }

            await this._matchService
                .DeleteAsync(matchViewModel.Id);
          
            return RedirectToAction("Index");
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
