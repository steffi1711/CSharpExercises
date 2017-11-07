using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bakery.Model
{
    public class OrderItems
    {
        public int Id { get; set; }
        [ForeignKey("Order_Id"), Required]
        public Order Order { get; set; }
        public int Order_Id { get; set; }
        [ForeignKey("Product_Id"), Required]
        public Product Product { get; set; }
        public int Product_Id { get; set; }
        public int Amount { get; set; }
    }
}
