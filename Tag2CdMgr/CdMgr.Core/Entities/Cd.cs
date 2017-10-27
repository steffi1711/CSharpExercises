using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace CdMgr.Core.Entities
{
    public class Cd: EntityObject
    {
        [Display(Name = "Titel"),Required]
        public string Title {get;set;}
        public string Artist {get;set;}
        public string Country {get;set;}
        public Company Company {get;set;}
        public double Price {get;set;}
        public int Year {get;set;}

        public override string ToString()
        {
            return String.Format("{0,-25} {1,-15} {2,-10} {3,-10} {4,6} {5,5}", Title, Artist, Country, Company==null?"":Company.CompanyName, Price, Year);
        }
    }
}
