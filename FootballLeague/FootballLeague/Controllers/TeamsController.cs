namespace FootballLeague.Controllers
{
    using System.Web.Mvc;
    using Models.DBModels;
    using Services;
    using Services.Interfaces;

    public class TeamsController : Controller
    {
        // GET: Teams
        private readonly ITeamsService teamsService;
        private readonly IPointsCalculator pointsCalculator;

        public TeamsController(ITeamsService teamsService, IPointsCalculator pointsCalculator)
        {
            this.teamsService = teamsService;
            this.pointsCalculator = pointsCalculator;
        }

        public ActionResult Index()
        {
            var model = teamsService.GetAll();
            return View(model);
        }

        public ActionResult AddTeam()
        {
            var model = new Team();
            return View(model);
        }

        public ActionResult EditTeam(int id)
        {
            var model = teamsService.GetByID(id);
            return View("AddTeam", model);
        }

        [HttpPost]
        public ActionResult AddTeamToDB(string name)
        {
            teamsService.AddNewTeamToDB(name);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult UpdateTeam(int id, string name)
        {
            teamsService.EditTeam(id, name);
            return RedirectToAction("Index");
        }

        public ActionResult DeleteTeam(int id)
        {
            teamsService.Delete(id);
            pointsCalculator.CalculatePoints();
            return RedirectToAction("Index");
        }
    }
}