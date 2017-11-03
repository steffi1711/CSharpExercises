using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelChecker.Core.Entities
{
    public class Hotel: EntityObject
    {
        public string Name { get; set; }
        
        public string Country { get; set; } //Für Angular Beispiel --> Country zunächst nur als String
       // public Country Country { get; set; }
        public int Stars { get; set; }
    }
}
