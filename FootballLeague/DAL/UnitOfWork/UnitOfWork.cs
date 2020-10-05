using DAL;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private FootballLeagueEntities context = new FootballLeagueEntities();
        private TeamRepository teamRepository;
        private GenericRepository<Match> matchRepository;

        public TeamRepository TeamRepository
        {
            get
            {

                if (this.teamRepository == null)
                {
                    this.teamRepository = new TeamRepository(context);
                }
                return teamRepository;
            }
        }

        public GenericRepository<Match> MatchRepository
        {
            get
            {

                if (this.matchRepository == null)
                {
                    this.matchRepository = new GenericRepository<Match>(context);
                }
                return matchRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
