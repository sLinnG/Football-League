namespace FootballLeague.Controllers
{
    using System.Web.Mvc;
    using Services;
    using Services.Interfaces;

    public class MatchesController : Controller
    {
        private const string SameTeamError = "You cant use the same team for home and away side!";
        private const string NegativeGoalsError = "Goals should never be a negative number!";

        private readonly IMatchesService matchesService;
        private readonly ITeamsService teamsService;
        private readonly IPointsCalculator pointsCalculator;

        public MatchesController(IMatchesService matchesService, ITeamsService teamsService, IPointsCalculator pointsCalculator)
        {
            this.matchesService = matchesService;
            this.teamsService = teamsService;
            this.pointsCalculator = pointsCalculator;
        }

        // GET: Matches
        public ActionResult Index()
        {
            var model = matchesService.GetAll();

            return View(model);
        }

        public ActionResult AddMatch()
        {
            var teams = teamsService.GetTeamsViewModel();
            var model = matchesService.CreateNewModel(teams);
            return View(model);
        }

        public ActionResult EditMatch(int id)
        {
            var teams = teamsService.GetTeamsViewModel();
            var model = matchesService.GetViewModelByID(id, teams);
            return View("AddMatch", model);
        }

        [HttpPost]
        public ActionResult AddMatchToDB(int homeTeamId, int awayTeamId, int homeTeamGoalsScored, int awayTeamGoalsScored)
        {
            ValidateData(homeTeamId, awayTeamId, homeTeamGoalsScored, awayTeamGoalsScored);
            if (ModelState.IsValid)
            {
                matchesService.AddNewMatchToDB(homeTeamId, awayTeamId, homeTeamGoalsScored, awayTeamGoalsScored);
                pointsCalculator.CalculatePoints();
                return RedirectToAction("Index");
            }
            else
            {
                var teams = teamsService.GetTeamsViewModel();
                var model = matchesService.CreateNewModel(teams);
                return View("AddMatch", model);
            }

        }

        [HttpPost]
        public ActionResult UpdateMatch(int id, int homeTeamId, int awayTeamId, int homeTeamGoalsScored, int awayTeamGoalsScored)
        {
            ValidateData(homeTeamId, awayTeamId, homeTeamGoalsScored, awayTeamGoalsScored);
            if (ModelState.IsValid)
            {
                matchesService.EditMatch(id, homeTeamId, awayTeamId, homeTeamGoalsScored, awayTeamGoalsScored);
                pointsCalculator.CalculatePoints();
                return RedirectToAction("Index");
            }
            else
            {
                var teams = teamsService.GetTeamsViewModel();
                var model = matchesService.CreateNewModel(teams, id, homeTeamId, awayTeamId, homeTeamGoalsScored, awayTeamGoalsScored);
                return View("AddMatch", model);
            }
        }

        private void ValidateData(int homeTeamId, int awayTeamId, int homeTeamGoals, int awayTeamGoals)
        {
            if (homeTeamId == awayTeamId)
            {
                ModelState.AddModelError("AwayTeamId", SameTeamError);
            }
            if (awayTeamGoals < 0)
            {
                ModelState.AddModelError("AwayTeamGoalsScored", NegativeGoalsError);
            }
            if (homeTeamGoals < 0)
            {
                ModelState.AddModelError("HomeTeamGoalsScored", NegativeGoalsError);
            }
        }

        public ActionResult DeleteMatch(int id)
        {
            matchesService.Delete(id);
            pointsCalculator.CalculatePoints();
            return RedirectToAction("Index");
        }
    }
}