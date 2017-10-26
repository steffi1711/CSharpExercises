using BankAccount.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount.Core.Contracts
{
    public interface IUnitOfWork: IDisposable
    {
        
        /// <summary>
        /// Standard Repositories 
        /// </summary>
        IGenericRepository<Account> AccountRepository { get; }
        IGenericRepository<Transaction> TransactionRepository { get; }

        /// <summary>
        /// Erweiterte Repositories
        /// </summary>
        //IAddressRepository AddressRepository { get; }

        void Save();

        void DeleteDatabase();

        void MigrateDatabase();

        void FillDb();

    }
}
