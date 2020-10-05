using DAL;
using Models;
//using Repositories.UnitOfWork;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FootballLeague.Controllers
{
    public class TeamsController : Controller
    {
        // GET: Teams
        //private IUnitOfWork _unitOfWork;
        private ITeamsService _teamsService;
        public TeamsController( ITeamsService teamsService)
        {
            //_unitOfWork = unitOfWork;
            _teamsService = teamsService;
        }
        public ActionResult Index()
        {
            var model = _teamsService.GetAll();
            return View(model);
        }
        public ActionResult AddTeam()
        {
            var model = new Team();
            return View(model);
        }
        public ActionResult EditTeam(int id)
        {
            var model = _teamsService.GetByID(id);
            return View("AddTeam", model);
        }
        [HttpPost]
        public ActionResult AddTeamToDB(string name)
        {
            _teamsService.AddNewTeamToDB(name);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult UpdateTeam(int id,string name)
        {
            _teamsService.EditTeam(id, name);
            return RedirectToAction("Index");
        }
        
        public ActionResult DeleteTeam(int id)
        {
            _teamsService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}