using Vocabulary.Core.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vocabulary.Core.Entities
{
    public class Word: EntityObject
    {
        public string LeftWord { get; set; }
        public string RightWord { get; set; }
    }
}
