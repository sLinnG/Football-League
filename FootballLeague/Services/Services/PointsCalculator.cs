namespace Services.Services
{
    using System.Linq;
    using Interfaces;
    using Models.DBModels;

    public class PointsCalculator : IPointsCalculator
    {
        private readonly IRepository repository;

        public PointsCalculator(IRepository repository)
        {
            this.repository = repository;
        }

        public void CalculatePoints()
        {
            var teams = repository.GetAll<Team>(t => t.AwayMatches, t => t.HomeMatches);
            foreach (var team in teams)
            {
                int points = 0;
                int homeWins = HomeMatchesWinsCalc(team);
                int homeDraws = HomeMatchesDrawCalc(team);
                int homeLoses = HomeMatchesLostCalc(team);
                int awayWins = AwayMatchesWinsCalc(team);
                int awayDraws = AwayMatchesDrawCalc(team);
                int awayLoses = AwayMatchesLostCalc(team);

                points += homeWins * 3;
                points += homeDraws * 1;
                points += awayWins * 3;
                points += awayDraws * 1;

                team.Points = points;
                team.Wins = homeWins + awayWins;
                team.Draws = homeDraws + awayDraws;
                team.Loses = homeLoses + awayLoses;

                repository.Update(team);
            }
            repository.Save();
        }

        private int HomeMatchesWinsCalc(Team team)
            => team.HomeMatches.Where(x => x.HomeTeamGoalsScored > x.AwayTeamGoalsScored).Count();

        private int HomeMatchesDrawCalc(Team team)
            => team.HomeMatches.Where(x => x.HomeTeamGoalsScored == x.AwayTeamGoalsScored).Count();

        private int HomeMatchesLostCalc(Team team)
            => team.HomeMatches.Where(x => x.HomeTeamGoalsScored < x.AwayTeamGoalsScored).Count();

        private int AwayMatchesWinsCalc(Team team)
            => team.AwayMatches.Where(x => x.AwayTeamGoalsScored > x.HomeTeamGoalsScored).Count();

        private int AwayMatchesDrawCalc(Team team)
            => team.AwayMatches.Where(x => x.AwayTeamGoalsScored == x.HomeTeamGoalsScored).Count();

        private int AwayMatchesLostCalc(Team team)
            => team.AwayMatches.Where(x => x.AwayTeamGoalsScored < x.HomeTeamGoalsScored).Count();
    }
}