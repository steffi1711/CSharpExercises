using HotelChecker.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelChecker.Core.Contracts
{
    public interface IUnitOfWork: IDisposable
    {
        IGenericRepository<Hotel> HotelRepository { get; }
        IGenericRepository<HotelReview> HotelReviewRepository { get; }

        IGenericRepository<Country> CountryRepository { get; }

        void Save();

        void DeleteDatabase();

        void FillDbFromJson();
    }
}
