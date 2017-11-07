using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bakery.Model
{
    /// <summary>
    /// Wird als Rückgabewert für die Methode GetInvoices() benötigt (nicht in Datenbank abzubilden)
    /// </summary>
    public class SalesStatistic
    {
        public string FirstName;
        public string LastName;
        public double TotalPrice; 
    }
}
