namespace Services
{
    using System.Collections.Generic;
    using Models;
    using Models.DBModels;

    public interface ITeamsService
    {
        List<Team> GetAll();

        Team GetByID(int id);

        bool AddNewTeamToDB(string name);

        bool EditTeam(int id, string name);

        bool Delete(int id);

        List<TeamsComboBoxViewModel> GetTeamsViewModel();
    }
}
