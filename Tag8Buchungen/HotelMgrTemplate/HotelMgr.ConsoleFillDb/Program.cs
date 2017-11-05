using HotelMgr.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace HotelMgr.ConsoleFillDb
{
    class Program
    {
        static void Main(string[] args)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                uow.ImportCSV("rechnungen.csv".GetFullNameInApplicationTree());

                var roomReservations = uow.ReservationRoomRepository.Get(includeProperties:"Guest,Guest.Country,Room");

                foreach (var rres in roomReservations)
                {         
                    Console.WriteLine($"{rres.From.ToShortDateString(),10} {rres.To.ToShortDateString(),10} {rres.Guest.LastName,-15} {rres.Room.RoomNumber}");
                }
            }
            Console.WriteLine("Taste drücken...");
            Console.ReadKey();
        }
    }
}
