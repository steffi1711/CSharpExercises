using HotelMgr.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelMgr.Core.Contracts
{
    public interface IUnitOfWork: IDisposable
    {
        IGenericRepository<Room> RoomRepository { get; }
        IGenericRepository<Guest> GuestRepository { get; }

        IGenericRepository<ReservationRoom> ReservationRoomRepository { get; }

        IGenericRepository<Country> CountryRepository { get; }

        void Save();

        void DeleteDatabase();

        void ImportCSV(string fileName);

    }
}
