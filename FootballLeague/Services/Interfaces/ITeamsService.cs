using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Threading.Tasks;
using Models;

namespace Services
{
    public interface ITeamsService
    {
        void ChangeName(int id, string name);
        void AddNew();
        List<Team> GetAll();
        Team GetByID(int id);
        bool AddNewTeamToDB(string name);
        bool EditTeam(int id, string name);
        bool Delete(int id);
        void ReCalculateTeamsPoints();
        List<TeamsComboBoxViewModel> GetTeamsViewModel(); 
    }
}
