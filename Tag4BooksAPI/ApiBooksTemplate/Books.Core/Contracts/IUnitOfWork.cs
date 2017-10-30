using Books.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Core.Contracts
{
    public interface IUnitOfWork: IDisposable
    {
        IGenericRepository<Publisher> PublisherRepository { get; }
        IGenericRepository<Book> BookRepository { get; }

        void Save();

        void DeleteDatabase();

        void FillDatabaseFromCsv();

        Publisher GetPublisherWithMostBooks();
       
    }
}
