using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieMgr.Core.Entities
{
    public class Movie: EntityObject
    {
       
        public string Title { get; set; }
        [ForeignKey("Category_Id")]
        public Category Category { get; set; }
       
        public int Category_Id { get; set; }
        public int Duration { get; set; } //in Minuten
        public int Year { get; set; }
    }
}
