namespace Services
{
    using System;
    using System.Collections.Generic;
    using Interfaces;
    using Models;
    using Models.DBModels;

    public class MatchesService : IMatchesService
    {
        private readonly IRepository repository;

        public MatchesService(IRepository repository)
        {
            this.repository = repository;
        }

        public bool AddNewMatchToDB(int homeTeamId, int awayTeamId, int homeTeamGoalsScored, int AwayTeamGoalsScored)
        {
            try
            {
                repository.Insert(new Match()
                {
                    HomeTeamId = homeTeamId,
                    AwayTeamId = awayTeamId,
                    HomeTeamGoalsScored = homeTeamGoalsScored,
                    AwayTeamGoalsScored = AwayTeamGoalsScored
                });
                repository.Save();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                repository.Delete<Match>(id);
                repository.Save();
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool EditMatch(int id, int homeTeamId, int awayTeamId, int homeTeamGoalsScored, int awayTeamGoalsScored)
        {
            try
            {
                var match = repository.GetByID<Match>(id);

                UpdateMatch(ref match, id, homeTeamId, awayTeamId, homeTeamGoalsScored, awayTeamGoalsScored);
                repository.Update(match);

                repository.Save();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void UpdateMatch(ref Match match, int id, int homeTeamId, int awayTeamId, int homeTeamGoalsScored, int AwayTeamGoalsScored)
        {
            match.Id = id;
            match.HomeTeamId = homeTeamId;
            match.AwayTeamId = awayTeamId;
            match.HomeTeamGoalsScored = homeTeamGoalsScored;
            match.AwayTeamGoalsScored = AwayTeamGoalsScored;
        }

        public MatchViewModel CreateNewModel(List<TeamsComboBoxViewModel> listOfTeams)
            => new MatchViewModel(listOfTeams);


        public List<Match> GetAll()
            => repository.Get<Match>();


        public Match GetByID(int id)
            => repository.GetByID<Match>(id);


        public MatchViewModel GetViewModelByID(int id, List<TeamsComboBoxViewModel> listOfTeams)
        {
            var match = repository.GetByID<Match>(id);

            return new MatchViewModel(
                listOfTeams,
                match.HomeTeamId,
                match.AwayTeamId,
                match.HomeTeamGoalsScored,
                match.AwayTeamGoalsScored,
                id);
        }

        public MatchViewModel CreateNewModel(
            List<TeamsComboBoxViewModel> listOfTeams,
            int homeTeamId,
            int awayTeamId,
            int homeTeamGoalsScored,
            int awayTeamGoalsScored)
            => new MatchViewModel(
                listOfTeams,
                homeTeamId,
                awayTeamId,
                homeTeamGoalsScored,
                awayTeamGoalsScored);

        public MatchViewModel CreateNewModel(
            List<TeamsComboBoxViewModel> listOfTeams,
            int id,
            int homeTeamId,
            int awayTeamId,
            int homeTeamGoalsScored,
            int awayTeamGoalsScored)
            => new MatchViewModel(
                listOfTeams,
                homeTeamId,
                awayTeamId,
                homeTeamGoalsScored,
                awayTeamGoalsScored,
                id);
    }
}
