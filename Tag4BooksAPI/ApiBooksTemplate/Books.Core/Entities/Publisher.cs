using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Core.Entities
{
    public class Publisher : EntityObject
    {
        [Required, MaxLength(100)]
        public string Name { get; set; }

        public ICollection<Book> Books { get; set; }

        public Publisher()
        {
            Books = new List<Book>();
        }

        public override string ToString()
        {
            return $"Herausgeber {Name} hat {Books.Count} Bücher";
        }
    }
}
