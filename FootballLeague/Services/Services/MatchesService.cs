using DAL;
using DAL.UnitOfWork;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class MatchesService : IMatchesService
    {
        IUnitOfWork _unitOfWork;
        public MatchesService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool AddNewMatchToDB(int homeTeamId, int awayTeamId, int homeTeamGoalsScored, int AwayTeamGoalsScored)
        {
            try
            {
                _unitOfWork.MatchRepository.Insert(new Match() { HomeTeamId= homeTeamId, AwayTeamId = awayTeamId,
                    HomeTeamGoalsScored = homeTeamGoalsScored, AwayTeamGoalsScored = AwayTeamGoalsScored});
                _unitOfWork.Save();
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
                _unitOfWork.MatchRepository.Delete(id);
                _unitOfWork.Save();
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
                var match = _unitOfWork.MatchRepository.GetByID(id);
                UpdateMatch(ref match, id, homeTeamId, awayTeamId, homeTeamGoalsScored, awayTeamGoalsScored);
                _unitOfWork.MatchRepository.Update(match);
                _unitOfWork.Save();
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
        {
            return new MatchViewModel(listOfTeams);
        }
        public List<Match> GetAll()
        {
            return _unitOfWork.MatchRepository.Get().ToList();
        }

        public Match GetByID(int id)
        {
            return _unitOfWork.MatchRepository.GetByID(id);
        }
        public MatchViewModel GetViewModelByID(int id, List<TeamsComboBoxViewModel> listOfTeams)
        {
            var match = _unitOfWork.MatchRepository.GetByID(id);
            return new MatchViewModel(listOfTeams,match.HomeTeamId, match.AwayTeamId, 
                match.HomeTeamGoalsScored, match.AwayTeamGoalsScored, id);
        }

        public MatchViewModel CreateNewModel(List<TeamsComboBoxViewModel> listOfTeams, int homeTeamId, int awayTeamId, int homeTeamGoalsScored, int awayTeamGoalsScored)
        {
            return new MatchViewModel(listOfTeams, homeTeamId, awayTeamId, homeTeamGoalsScored, awayTeamGoalsScored);
        }
        public MatchViewModel CreateNewModel(List<TeamsComboBoxViewModel> listOfTeams,int id, int homeTeamId, int awayTeamId, int homeTeamGoalsScored, int awayTeamGoalsScored)
        {
            return new MatchViewModel(listOfTeams, homeTeamId, awayTeamId, homeTeamGoalsScored, awayTeamGoalsScored, id);
        }
    }
}
