using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface ITeamRepository : IDisposable
    {
        IEnumerable<Team> GetTeams();
        Team GetTeamByID(int id);
        void Insert(Team Team);
        void Delete(int studentID);
        void Update(Team student);
        void Save();
    }
}
