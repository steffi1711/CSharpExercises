using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelChecker.Core.Entities
{
    public class HotelReview: EntityObject
    {
         public Hotel Hotel { get; set; }
         public int Points { get; set; }
        public string Comment { get; set; }
    }
}
