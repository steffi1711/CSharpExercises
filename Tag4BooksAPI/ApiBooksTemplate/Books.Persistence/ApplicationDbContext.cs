using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Books.Core.Entities;

namespace Books.Persistence
{
    public class ApplicationDbContext : DbContext
    {

        public DbSet<Book> Books { get; set; }
        public DbSet<Publisher> Authors { get; set; }

        /// <summary>
        /// Connectionstring kommt entweder über den Namen (Verweis auf 
        /// XML-Configdatei) oder direkt als voller ConnectionString
        /// </summary>
        /// <param name="nameOrConnectionString"></param>
        public ApplicationDbContext(string nameOrConnectionString)
                    : base(nameOrConnectionString)
        {
        }

        public ApplicationDbContext() : base("name=DefaultConnection")
        {

        }

    }
}
