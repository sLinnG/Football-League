
namespace DAL
{
    using DAL;
    using Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public class TeamRepository : GenericRepository<Team>
    {
        public TeamRepository(FootballLeagueEntities context) : base(context)
        {
        }
        
        public  Team GetByIDWithMatches(int id)
        {
            return dbSet.Include(x => x.AwayMatches).Include(x => x.HomeMatches).First(x=> x.Id == id);
        }
        public List<Team> GetAllWithMatches()
        {
            return dbSet.Include(x => x.AwayMatches).Include(x => x.HomeMatches).ToList();
        }
    }
}
