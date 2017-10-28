using MovieMgr.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieMgr.Core.Contracts
{
    public interface IUnitOfWork: IDisposable
    {
        IGenericRepository<Movie> MovieRepository { get; }
        IGenericRepository<Category> CategoryRepository { get; }

        void Save();

        void DeleteDatabase();
    }
}
