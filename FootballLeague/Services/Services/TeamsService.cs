namespace Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces;
    using Models;
    using Models.DBModels;

    public class TeamsService : ITeamsService
    {
        private readonly IRepository repository;

        public TeamsService(IRepository unitOfWork)
        {
            repository = unitOfWork;
        }

        public bool AddNewTeamToDB(string name)
        {
            try
            {
                repository.Insert(new Team() { Name = name });
                repository.Save();
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
                var team = repository.GetByID<Team>(id);
                UpdateTeamName(ref team, name);
                repository.Update(team);
                repository.Save();
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

        public List<Team> GetAll()
            => repository.Get<Team>(null, teams => teams.OrderByDescending(x => x.Points)).ToList();

        public Team GetByID(int id)
            => repository.GetByID<Team>(id);

        public bool Delete(int id)
        {
            try
            {
                var team = repository.GetByID<Team>(id, t => t.AwayMatches, t => t.HomeMatches);
                if (team.HomeMatches.Any())
                {
                    var homeMatches = repository.Get<Match>(x => x.HomeTeamId == id).Select(x => x.Id);
                    foreach (var item in homeMatches)
                    {
                        repository.Delete<Match>(item);
                    }
                }
                if (team.AwayMatches.Any())
                {
                    var awayMatches = repository.Get<Match>(x => x.AwayTeamId == id).Select(x => x.Id);
                    foreach (var item in awayMatches)
                    {
                        repository.Delete<Match>(item);
                    }
                }

                repository.Delete<Team>(team.Id);
                repository.Save();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TeamsComboBoxViewModel> GetTeamsViewModel()
        {
            var teams = repository.Get<Team>();
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
