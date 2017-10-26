using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BankAccount.Core.Entities
{
    public partial class Transaction: EntityObject
    {
        public DateTime Date { get; set; }
        public double Amount { get; set; }
    
        [ForeignKey("Account_id")]
        public Account Account { get; set; }

        public int Account_id { get; set; }
    }
}
