using Vocabulary.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vocabulary.Core.Entities;

namespace Vocabulary.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();
        private bool _disposed;

        /// <summary>
        ///     Konkrete Repositories. Keine Ableitung erforderlich
        /// </summary>
        private GenericRepository<Trial> _trialRepository;
        private GenericRepository<Word> _wordRepository;

        public IGenericRepository<Trial> TrialRepository
        {
            get
            {
                if (_trialRepository == null)
                    _trialRepository = new GenericRepository<Trial>(_context);
                return _trialRepository;
            }
        }

        public IGenericRepository<Word> WordRepository
        {
            get
            {
                if (_wordRepository == null)
                    _wordRepository = new GenericRepository<Word>(_context);
                return _wordRepository;
            }
        }

        public UnitOfWork(string connectionString)
        {
            _context = new ApplicationDbContext(connectionString);
        }

        public UnitOfWork(): this("name=DefaultConnection")
        {

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
            _context.Database.Delete();
        }


    }
}
