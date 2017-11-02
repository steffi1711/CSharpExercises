using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vocabulary.Core.Entities
{
    public class Trial: EntityObject
    {
        public string User { get; set; }
        public DateTime SubmittedAt { get; set; }
        public int TotalWords { get; set; }
        public int CorrectWords { get; set; }
        public int Mistakes { get; set; }
    }
}
