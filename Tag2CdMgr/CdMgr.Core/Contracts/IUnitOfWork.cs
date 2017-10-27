using CdMgr.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CdMgr.Core.Contracts
{
    public interface IUnitOfWork: IDisposable
    {
        IGenericRepository<Cd> CdRepository { get; }
        IGenericRepository<Company> CompanyRepository { get; }

        void Save();

        void DeleteDatabase();
    }
}
