using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using BankAccount.Core.Contracts;

namespace BankAccount.Core.Entities
{

        public class EntityObject : IEntityObject
        {
            #region IEnityObject Members

            [Key]
            public int Id { get; set; }

            [Timestamp]
            public byte[] Timestamp
            {
                get;
                set;
            }

            #endregion
        }
}
