using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bakery.Model
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string ProductNr { get; set; }
        [MaxLength(20)]
        public string Name { get; set; }
        public double Price { get; set; }
    }
}
