using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using Bakery.Model;

namespace Bakery.DataAccess
{
    public class BakeryContext: DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<OrderItems> OrderItems { get; set; }

        public BakeryContext(): base(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BakeryDB;
                Integrated Security=True")
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<BakeryContext>());
        }
    }
}
