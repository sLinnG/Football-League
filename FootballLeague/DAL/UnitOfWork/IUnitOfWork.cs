using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        TeamRepository TeamRepository { get; }
        GenericRepository<Match> MatchRepository { get; }
        void Save();
        void Dispose();
    }
}
