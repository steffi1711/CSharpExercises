using HotelChecker.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelChecker.Persistence
{
    public class ApplicationDbContext: DbContext
    {
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<HotelReview> HotelReviews { get; set; }

       // public DbSet<Country> Countries { get; set; }

        public ApplicationDbContext() : base("name=DefaultConnection")
        {

        }

        public ApplicationDbContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {

        }
    }
}
