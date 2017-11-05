using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMgr.Core.Entities
{
    public class Guest: EntityObject
    {
        [Display(Name = "Knd. Nr.")]
        public int CustomerNr { get; set; }
        [Display(Name="Vorname")]
        public string FirstName { get; set; }
        [Display(Name = "Nachname")]
        public string LastName { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public Country Country { get; set; }
    }
}
