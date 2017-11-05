using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMgr.Core.Entities
{
    public class Country: EntityObject
    {
        [Display(Name = "Land")]
        public String CountryName { get; set; }
    }
}
