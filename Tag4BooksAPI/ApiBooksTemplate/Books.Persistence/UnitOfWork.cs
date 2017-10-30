using Books.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Books.Core.Entities;
using Utils;

namespace Books.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        const string FILENAME = "books.csv";

        private readonly ApplicationDbContext _context = new ApplicationDbContext();
        private bool _disposed;

        /// <summary>
        ///     Konkrete Repositories. Keine Ableitung erforderlich
        /// </summary>
        private GenericRepository<Publisher> _publisherRepository;
        private GenericRepository<Book> _bookRepository;

        public IGenericRepository<Publisher> PublisherRepository
        {
            get
            {
                if (_publisherRepository == null)
                    _publisherRepository = new GenericRepository<Publisher>(_context);
                return _publisherRepository;
            }
        }

        public IGenericRepository<Book> BookRepository
        {
            get
            {
                if (_bookRepository == null)
                    _bookRepository = new GenericRepository<Book>(_context);
                return _bookRepository;
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

        public void FillDatabaseFromCsv()
        {
            DeleteDatabase();

            string[][] csvFile = FILENAME.ReadStringMatrixFromCsv(true);

            var publishers = csvFile.GroupBy(line => line[2]).Select(grp =>
                new Publisher()
                {
                    Name = grp.Key
                }).ToList();

            var books = csvFile.GroupBy(line => line[3]).Select(grp =>
                new Book()
                {
                    Title=grp.First()[1],
                    Authors=grp.First()[0],
                    Publisher=publishers.First(p=>p.Name==grp.First()[2]),
                    Isbn= grp.First()[3]
                }).ToList();

            _context.Books.AddRange(books);
            _context.SaveChanges();
        }

        public Publisher GetPublisherWithMostBooks()
        {
            return _context.Authors.OrderByDescending(a => a.Books.Count()).FirstOrDefault();
        }
    }
}
