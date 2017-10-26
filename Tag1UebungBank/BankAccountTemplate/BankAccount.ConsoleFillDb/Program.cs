using BankAccount.Persistence;
using System;

namespace BankAccount.ConsoleFillDb
{
    class Program
    {
        static void Main(string[] args)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                uow.FillDb();
                var acc = uow.AccountRepository.Get();
                Console.WriteLine("Eingelesene Konten:");
                foreach (var item in acc)
                {
                    Console.WriteLine("Kto. Nr.:{0,-40} Stand: {1,10:f2} Eur", item.Iban, item.Balance);
                }
                Console.WriteLine("\n<Taste drücken>");
                Console.ReadKey();
            }
        }
    }
}
