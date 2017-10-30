using Books.Core;
using Books.Persistence;
using System;
using System.Linq;

namespace Books.ConsoleFillDb
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Import der Bücher in die Datenbank");
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                unitOfWork.FillDatabaseFromCsv();
                var books = unitOfWork.BookRepository.Get();

                Console.WriteLine($"  Es wurden {books.Count()} Bücher eingelesen!");
                var publisher = unitOfWork.PublisherRepository.Get();
                Console.WriteLine($"  Es wurden {publisher.Count()} Verlage eingelesen!");

                Console.Write("Beenden mit Eingabetaste ...");
                Console.ReadLine();
            }
        }
    }
}
