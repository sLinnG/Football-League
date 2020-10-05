using DAL;
using Models;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FootballLeague.Controllers
{
    public class MatchesController : Controller
    {
        private IMatchesService _matchesService;
        private ITeamsService _teamsService;
        const string sameTeamError = "You cant use the same team for home and away side!";
        const string negativeGoalsError = "Goals should never be a negative number!";
        public MatchesController(IMatchesService matchesService, ITeamsService teamsService)
        {
            _matchesService = matchesService;
            _teamsService = teamsService;
        }
        // GET: Matches
        public ActionResult Index()
        {
            var model = _matchesService.GetAll();

            return View(model);
        }
        public ActionResult AddMatch()
        {
            var teams = _teamsService.GetTeamsViewModel();
            var model = _matchesService.CreateNewModel(teams);
            return View(model);
        }
        public ActionResult EditMatch(int id)
        {
            var teams = _teamsService.GetTeamsViewModel();
            var model = _matchesService.GetViewModelByID(id, teams);
            return View("AddMatch", model);
        }
        [HttpPost]
        public ActionResult AddMatchToDB(int homeTeamId, int awayTeamId, int homeTeamGoalsScored, int awayTeamGoalsScored)
        {
            ValidateData(homeTeamId, awayTeamId, homeTeamGoalsScored, awayTeamGoalsScored);
            if (ModelState.IsValid)
            {
                _matchesService.AddNewMatchToDB(homeTeamId, awayTeamId, homeTeamGoalsScored, awayTeamGoalsScored);
                _teamsService.ReCalculateTeamsPoints();
                return RedirectToAction("Index");
            }
            else
            {
                var teams = _teamsService.GetTeamsViewModel();
                var model = _matchesService.CreateNewModel(teams);
                return View("AddMatch", model);
            }
            
        }
        [HttpPost]
        public ActionResult UpdateMatch(int id, int homeTeamId, int awayTeamId, int homeTeamGoalsScored, int awayTeamGoalsScored)
        {
            ValidateData(homeTeamId, awayTeamId, homeTeamGoalsScored, awayTeamGoalsScored);
            if (ModelState.IsValid)
            {
                _matchesService.EditMatch(id, homeTeamId, awayTeamId, homeTeamGoalsScored, awayTeamGoalsScored);
                _teamsService.ReCalculateTeamsPoints();
                return RedirectToAction("Index");
            }
            else
            {
                var teams = _teamsService.GetTeamsViewModel();
                var model = _matchesService.CreateNewModel(teams, id, homeTeamId, awayTeamId, homeTeamGoalsScored, awayTeamGoalsScored);
                return View("AddMatch", model);
            }
        }
        private void ValidateData(int homeTeamId, int awayTeamId, int homeTeamGoals, int awayTeamGoals)
        {
            if (homeTeamId == awayTeamId)
            {
                ModelState.AddModelError("AwayTeamId", sameTeamError);
            }
            if (awayTeamGoals < 0)
            {
                ModelState.AddModelError("AwayTeamGoalsScored", negativeGoalsError);
            }
            if (homeTeamGoals < 0)
            {
                ModelState.AddModelError("HomeTeamGoalsScored", negativeGoalsError);
            }
        }
        //private List<TeamsComboBoxViewModel> GetTeams() 
        //{
        //    return _teamsService.GetTeamsViewModel();
        //}
        public ActionResult DeleteMatch(int id)
        {
            _matchesService.Delete(id);
            _teamsService.ReCalculateTeamsPoints();
            return RedirectToAction("Index");
        }
    }
}