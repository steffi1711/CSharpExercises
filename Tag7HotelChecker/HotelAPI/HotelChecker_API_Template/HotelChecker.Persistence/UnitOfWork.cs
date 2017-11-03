using HotelChecker.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelChecker.Core.Entities;
using System.IO;
using Utils;
using Newtonsoft.Json.Linq;

namespace HotelChecker.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();
        private bool _disposed;

        /// <summary>
        ///     Konkrete Repositories. Keine Ableitung erforderlich
        /// </summary>
        private GenericRepository<Hotel> _hotelRepository;
        private GenericRepository<HotelReview> _hotelReviewRepository;
        private GenericRepository<Country> _countryRepository;
        public IGenericRepository<Hotel> HotelRepository
        {
            get
            {
                if (_hotelRepository == null)
                    _hotelRepository = new GenericRepository<Hotel>(_context);
                return _hotelRepository;
            }
        }

        public IGenericRepository<HotelReview> HotelReviewRepository
        {
            get
            {
                if (_hotelReviewRepository == null)
                    _hotelReviewRepository = new GenericRepository<HotelReview>(_context);
                return _hotelReviewRepository;
            }
        }

        public IGenericRepository<Country> CountryRepository
        {
            get
            {
                if (_countryRepository == null)
                    _countryRepository = new GenericRepository<Country>(_context);
                return _countryRepository;
            }
        }

        public UnitOfWork(string connectionString)
        {
            _context = new ApplicationDbContext(connectionString);
        }

        public UnitOfWork(): this("name=DefaultConnection")
        {

        }

        /// <summary>
        ///     Repository-übergreifendes Speichern der Änderungen
        /// </summary>
        public void Save()
        {
             _context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void DeleteDatabase()
        {
            _context.Database.Delete();
        }

        public void FillDbFromJson()
        {
            DeleteDatabase();
            string hotelReviewJsonStr = File.ReadAllText("hotelreviews.json".GetFullNameInApplicationTree(),Encoding.Default);
            string hotelJsonStr = File.ReadAllText("hotels.json".GetFullNameInApplicationTree(), Encoding.Default);

            JObject hotelJson = JObject.Parse(hotelJsonStr);
            JObject hotelReviewJson = JObject.Parse(hotelReviewJsonStr);

            //Country[] countries = hotelJson["hotels"].GroupBy(json => json["country"]).Select(grp =>
            //    new Country()
            //    {
            //        CountryName = grp.Key.ToString()
            //    }).ToArray();

            Hotel[] hotels = hotelJson["hotels"].Select(json =>
              new Hotel()
              {
                  Name = json["name"].ToString(),
                  Country=json["country"].ToString(),
                  Id=Int32.Parse(json["id"].ToString()),
                  Stars=Int32.Parse(json["stars"].ToString())
              }).ToArray();

            HotelReview[] reviews = hotelReviewJson["hotelreviews"].Select(json =>
              new HotelReview()
              {
                 Comment=json["comment"].ToString(),
                 Hotel=hotels.First(h=>h.Id==Int32.Parse(json["hotel_id"].ToString())),
                 Points= Int32.Parse(json["points"].ToString())
              }).ToArray();

          //  _context.Countries.AddRange(countries);
            _context.Hotels.AddRange(hotels);
            _context.HotelReviews.AddRange(reviews);

            _context.SaveChanges();
        }
    }
}
