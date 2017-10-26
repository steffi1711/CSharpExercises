using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using BankAccount.Core.Contracts;
using BankAccount.Persistence;
using BankAccount.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Utils;

namespace BankAccount.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();
        private bool _disposed;


        /// <summary>
        ///     Konkrete Standard-Repositories. Keine Ableitung nötig
        /// </summary>
        public IGenericRepository<Account> AccountRepository { get; }
        public IGenericRepository<Transaction> TransactionRepository { get; }

        /// <summary>
        ///     Konkrete Repositories. Mit Ableitung nötig
        /// </summary>
        //  public IAddressRepository AddressRepository { get; }

       

        public UnitOfWork() 
        {
            _context = new ApplicationDbContext();

            AccountRepository = new GenericRepository<Account>(_context);
            TransactionRepository = new GenericRepository<Transaction>(_context);

            //Bsp.: Konkretes Repository mit Überschreibung
            //AddressRepository = new AddressRepository(_context);
        }

        /// <summary>
        ///     Repository-übergreifendes Speichern der Änderungen
        /// </summary>
        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void DeleteDatabase()
        {
            _context.Database.EnsureDeleted();
        }

        public void MigrateDatabase()
        {
            try
            {
                _context.Database.Migrate();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void FillDb()
        {
            this.DeleteDatabase();
            this.MigrateDatabase();

            List<Account> accounts;
            List<Transaction> transactions;


            string[] lines = File.ReadAllLines("transactions.csv".GetFullNameInApplicationTree());
            string[][] csvFile = new string[lines.Length - 1][];
            for (int i = 1; i < lines.Length; i++)
            {
                csvFile[i - 1] = lines[i].Split(';');
            }

            accounts = csvFile.GroupBy(line => line[1]).Select(grp =>
                new Account()
                {
                    Iban = grp.Key,
                    Balance = grp.Sum(y => Double.Parse(y[2]))
                }).ToList();


            transactions = csvFile.Select(line =>
                new Transaction()
                {
                    Date = Convert.ToDateTime(line[0]),
                    Account = accounts.SingleOrDefault(x => x.Iban.Equals(line[1])),
                    Amount = Convert.ToDouble(line[2])
                }).ToList();

            foreach (var item in accounts)
            {
                _context.Accounts.Add(item);
            }

            foreach (var item in transactions)
            {
                _context.Transactions.Add(item);
            }
            this.Save();
        }

    }
}
