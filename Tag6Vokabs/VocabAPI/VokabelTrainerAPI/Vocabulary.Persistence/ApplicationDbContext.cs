using Vocabulary.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vocabulary.Persistence
{
    public class ApplicationDbContext: DbContext
    {
        public DbSet<Trial> Movies { get; set; }
        public DbSet<Word> categories { get; set; }

        public ApplicationDbContext() : base("name=DefaultConnection")
        {

        }

        public ApplicationDbContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {

        }
    }
}
