using MovieMgr.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieMgr.Persistence
{
    public class ApplicationDbContext: DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Category> categories { get; set; }

        public ApplicationDbContext() : base("name=DefaultConnection")
        {

        }

        public ApplicationDbContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {

        }
    }
}
