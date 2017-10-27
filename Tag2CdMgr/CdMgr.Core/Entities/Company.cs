using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CdMgr.Core.Entities
{
    public class Company: EntityObject
    {
        public string CompanyName { get; set; } 
        public string Director { get; set; }
        public int Founded { get; set; }
    }
}
