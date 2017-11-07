using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bakery.Model
{
    public class Order
    {
        public int Id { get; set; }
        [Required]
        public string OrderNr { get; set; }
        public DateTime Date { get; set; }
        [ForeignKey("Customer_Id"), Required]
        public Customer Customer { get; set; }
        public int Customer_Id { get; set; }
        public List<OrderItems> OrderItems { get; set; }
    }
}
