using HotelChecker.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelChecker.ConsoleFillDb
{
    class Program
    {
        static void Main(string[] args)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                uow.FillDbFromJson();

                var hotels = uow.HotelRepository.Get();

                foreach (var hotel in hotels)
                {
                    
                    Console.WriteLine(hotel.Id+" | "+hotel.Name+" | "+hotel.Country);
                }
            }
        }
    }
}
