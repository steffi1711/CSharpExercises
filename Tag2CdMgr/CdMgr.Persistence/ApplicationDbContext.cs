using CdMgr.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CdMgr.Persistence
{
    public class ApplicationDbContext: DbContext
    {
        public DbSet<Cd> Cds { get; set; }
        public DbSet<Company> Companies { get; set; }

        public ApplicationDbContext():base("name=DefaultConnection")
        {

        }

        public ApplicationDbContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {

        }

    }
}
