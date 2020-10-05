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
    public class TeamsService : ITeamsService
    {
        IUnitOfWork _unitOfWork;
        public TeamsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public bool AddNewTeamToDB(string name)
        {
            try
            {
                _unitOfWork.TeamRepository.Insert(new Team() { Name = name });
                _unitOfWork.Save();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool EditTeam(int id, string name)
        {
            try
            {
                var team = _unitOfWork.TeamRepository.GetByID(id);
                UpdateTeamName(ref team, name);
                _unitOfWork.TeamRepository.Update(team);
                _unitOfWork.Save();
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
        private void UpdateTeamName(ref Team team, string name)
        {
            team.Name = name;
        }
        public void ChangeName(int id, string name)
        {
            throw new NotImplementedException();
        }

        public List<Team> GetAll()
        {
            return _unitOfWork.TeamRepository.Get(null, teams => teams.OrderByDescending(x => x.Points)).ToList();
        }

        public Team GetByID(int id)
        {
            return _unitOfWork.TeamRepository.GetByID(id);
        }

        public void AddNew()
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            try
            {
                var team = _unitOfWork.TeamRepository.GetByIDWithMatches(id);
                if (team.HomeMatches.Any())
                {
                    var homeMatches = _unitOfWork.MatchRepository.Get(x => x.HomeTeamId == id).Select(x => x.Id).ToList();
                    foreach (var item in homeMatches)
                    {
                        _unitOfWork.MatchRepository.Delete(item);
                    }
                }
                if (team.AwayMatches.Any())
                {
                    var awayMatches = _unitOfWork.MatchRepository.Get(x => x.AwayTeamId == id).Select(x=>x.Id).ToList();
                    foreach (var item in awayMatches)
                    {
                        _unitOfWork.MatchRepository.Delete(item);
                    }
                }
                //_unitOfWork.Save();
                _unitOfWork.TeamRepository.Delete(team.Id);
                _unitOfWork.Save();
                ReCalculateTeamsPoints();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ReCalculateTeamsPoints() 
        {
            var teams = _unitOfWork.TeamRepository.GetAllWithMatches();
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
                points += homeDraws* 1;
                points += awayWins * 3;
                points += awayDraws * 1;
                team.Points = points;
                team.Wins = homeWins + awayWins;
                team.Draws = homeDraws + awayDraws;
                team.Loses = homeLoses + awayLoses;
                _unitOfWork.TeamRepository.Update(team);
            }
            _unitOfWork.Save();
        }
        private int HomeMatchesWinsCalc(Team team)
        {
           return team.HomeMatches.Where(x => x.HomeTeamGoalsScored > x.AwayTeamGoalsScored).Count();
        }
        private int HomeMatchesDrawCalc(Team team)
        {
            return team.HomeMatches.Where(x => x.HomeTeamGoalsScored == x.AwayTeamGoalsScored).Count();
        }
        private int HomeMatchesLostCalc(Team team)
        {
            return team.HomeMatches.Where(x => x.HomeTeamGoalsScored < x.AwayTeamGoalsScored).Count();
        }
        private int AwayMatchesWinsCalc(Team team)
        {
            return team.AwayMatches.Where(x => x.AwayTeamGoalsScored > x.HomeTeamGoalsScored).Count();
        }
        private int AwayMatchesDrawCalc(Team team)
        {
            return team.AwayMatches.Where(x => x.AwayTeamGoalsScored == x.HomeTeamGoalsScored).Count();
        }

        private int AwayMatchesLostCalc(Team team)
        {
            return team.AwayMatches.Where(x => x.AwayTeamGoalsScored < x.HomeTeamGoalsScored).Count();
        }

        public List<TeamsComboBoxViewModel> GetTeamsViewModel()
        {
            var teams = _unitOfWork.TeamRepository.Get().ToList();
            List<TeamsComboBoxViewModel> listOfTeams = new List<TeamsComboBoxViewModel>();
            for (int i = 0; i < teams.Count; i++)
            {
                Team team = teams[i];
                listOfTeams.Add(new TeamsComboBoxViewModel(team.Id, team.Name));
            };
            return listOfTeams;
        }
    }
}
