using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BankAccount.Core.Entities
{
    public class Account : EntityObject
    {
        public String Iban { get; set; }
        public double Balance { get; set; }

        
        
        /// <summary>
        /// Statische Methode zur Prüfung eines als String übergebenen Iban´s
        /// </summary>
        /// <param name="iban">zu prüfende Iban</param>
        /// <returns> wenn Iban korrekt, sonst false</returns>
        public static bool CheckIBAN(String iban)
        {
            string convertIban = "";
            string check = iban.Substring(4);
            check = check + iban.Substring(0, 4);
            check.ToUpper();

            foreach(char item in check)
            {
                if(Char.IsLetter(item))
                {
                    convertIban += item - 55;
                }
                else
                {
                    convertIban += item;
                }
            }

            int checksum = Convert.ToInt32(convertIban.Substring(0,1));
           

            for(int i = 1; i < convertIban.Length; i++)
            {
                int j = Convert.ToInt32(convertIban.Substring(i,1));
                checksum = checksum * 10;
                checksum = checksum + j;
                checksum = checksum % 97;


            }

            if(checksum == 1)
            {
                return true;
            }

            return false;
            
        }
    }
}
