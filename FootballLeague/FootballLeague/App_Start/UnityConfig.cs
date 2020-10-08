namespace FootballLeague
{
    using System.Configuration;
    using System.Web.Mvc;
    using DAL;
    using Services;
    using Services.Interfaces;
    using Services.Services;
    using Unity;
    using Unity.Mvc5;

    public static class UnityConfig
    {
        public static void RegisterComponents(ConnectionStringSettingsCollection connectionStrings)
        {
            var container = new UnityContainer();

            var connectionStringSettings = connectionStrings[nameof(FootballLeagueDbContext)];
            var dbInstance = new FootballLeagueDbContext(connectionStringSettings.ConnectionString);

            container.RegisterInstance(dbInstance)
                    .RegisterSingleton<IRepository, FootballLeagueRepository>()
                    .RegisterSingleton<IPointsCalculator, PointsCalculator>()
                    .RegisterSingleton<ITeamsService, TeamsService>()
                    .RegisterSingleton<IMatchesService, MatchesService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}