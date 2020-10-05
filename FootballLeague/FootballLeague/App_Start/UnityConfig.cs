using DAL.UnitOfWork;
using Services;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;


namespace FootballLeague
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IUnitOfWork, UnitOfWork>();
            container.RegisterType<ITeamsService, TeamsService>();
            container.RegisterType<IMatchesService, MatchesService>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            
        }
    }
}