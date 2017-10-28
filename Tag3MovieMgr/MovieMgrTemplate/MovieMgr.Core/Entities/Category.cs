using MovieMgr.Core.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieMgr.Core.Entities
{
    public class Category: EntityObject
    { 
        public String CategoryName { get; set; }
    }
}
