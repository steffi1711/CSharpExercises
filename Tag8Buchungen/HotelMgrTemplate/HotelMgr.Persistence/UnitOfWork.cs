using HotelMgr.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelMgr.Core.Entities;
using System.IO;
using Utils;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace HotelMgr.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();
        private bool _disposed;

        /// <summary>
        ///     Konkrete Repositories. Keine Ableitung erforderlich
        /// </summary>
        private GenericRepository<Country> _countryRepository;
        private GenericRepository<Guest> _guestRepository;
        private GenericRepository<Room> _roomRepository;
        private GenericRepository<ReservationRoom> _reservationRoomRepository;
        public IGenericRepository<Country> CountryRepository
        {
            get
            {
                if (_countryRepository == null)
                    _countryRepository = new GenericRepository<Country>(_context);
                return _countryRepository;
            }
        }

        public IGenericRepository<Guest> PupilRepository
        {
            get
            {
                if (_guestRepository == null)
                    _guestRepository = new GenericRepository<Guest>(_context);
                return _guestRepository;
            }
        }

        public IGenericRepository<Room> RoomRepository
        {
            get
            {
                if (_roomRepository == null)
                    _roomRepository = new GenericRepository<Room>(_context);
                return _roomRepository;
            }
        }

        public IGenericRepository<ReservationRoom> ReservationRoomRepository
        {
            get
            {
                if (_reservationRoomRepository == null)
                    _reservationRoomRepository = new GenericRepository<ReservationRoom>(_context);
                return _reservationRoomRepository;
            }
        }

        public IGenericRepository<Guest> GuestRepository
        {
            get
            {
                if (_guestRepository == null)
                    _guestRepository = new GenericRepository<Guest>(_context);
                return _guestRepository;
            }
        }

    
        public UnitOfWork(string connectionString)
        {
            _context = new ApplicationDbContext(connectionString);
        }

        public UnitOfWork() : this("name=DefaultConnection")
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

    

        public void ImportCSV(string fileName)
        {
            string actLine;
            string[] actCols;
            Guest actGuest;
            Country actCountry;
            Room actRoom;
            ReservationRoom actResRoom = null;

            _context.Database.ExecuteSqlCommand("delete from ReservationRooms");
            _context.Database.ExecuteSqlCommand("delete from Guests");

            using (StreamReader sr = new StreamReader(fileName, Encoding.Default))
            {
                sr.ReadLine(); //Header überlesen
                while (!sr.EndOfStream )
                {
                    actLine = sr.ReadLine();

                    //Strichpunkte zwischen Anführungsstrichen gegen Bindestriche austauschen
                    actLine = Regex.Replace(actLine, "\"(.*?)\"",
                        s => s.ToString().Replace(";", "-"));

                    actCols = actLine.Split(';');

                    actCols[7] = actCols[7].Replace("\"", ""); //Anführungsstriche entfernen 
                    actCols[7] = actCols[7].Replace("Towels", "Handtücher");
                    actCols[7] = actCols[7].Replace("Bed-linen", "Bettwäsche");

                    if (actCols[0] == "Steuersatz")
                        break; //Die Steuersätze am Ende des Files werden nicht behandelt --> vorher Abbruch

                    if ((actCols[0]!="") || (actCols[1]!=""))  //Neue Reservierungszeile (Gast-Name, etc.)
                    {
                        //Prüfen ob diese Buchungsnummer bereits vorhanden
                        string actBookingNr = actCols[6];
                        actResRoom = _context.ReservationRoom.FirstOrDefault(r => r.BookingNr == actBookingNr);
                        if (actResRoom == null)
                        {
                            actResRoom = new ReservationRoom();
                            actResRoom.BookingNr = actCols[6];
                        }

                        int actCustNr = int.Parse(actCols[14]);
                        actGuest = _context.Guests.FirstOrDefault(g => g.CustomerNr == actCustNr);
                        if (actGuest==null)
                        {
                            actGuest = new Guest();
                            actGuest.CustomerNr = int.Parse(actCols[14]);
                            actGuest.FirstName = actCols[0];
                            actGuest.LastName = actCols[1];
                            actGuest.Address = actCols[15];
                            actGuest.ZipCode = actCols[16];
                            actGuest.City = actCols[17];

                            string actCountryString = actCols[18];
                            actCountry = _context.Countries.Where(c=>c.CountryName== actCountryString).FirstOrDefault();
                            if (actCountry == null)
                            {
                                actCountry = new Country() { CountryName = actCols[18] };
                                _context.Countries.Add(actCountry);
                            }
                            actGuest.Country = actCountry;
                           

                            _context.Guests.Add(actGuest);

                            _context.SaveChanges();
                        }
                        actResRoom.Guest = actGuest;
                    }
                    else if ((actCols[7].StartsWith("Kategorie")) || (actCols[7].StartsWith("Category")))
                    {
                        
                        if (actResRoom == null)
                            throw new Exception("Kategoriedatensatz ohne vorherige Reservierungskopfzeile");

                        actCols[7] = actCols[7].Replace(", ", ",");
                        string[] cols = actCols[7].Split(',');

                        

                        int idxFromColumn;
                        if (((cols[1].Contains("Verpflegung")) || (cols[1].Contains("Catering"))))
                            idxFromColumn = 2;
                        else
                            idxFromColumn = 1;

                        cols[0]=cols[0].Replace("Kategorie: ", "");
                        cols[0] = cols[0].Replace("Category: ", "");
                        cols[0] = cols[0].Replace("Apartment with balcony", "Apartment mit Balkon");
                        cols[0] = cols[0].Replace("Superior Apartment with balcony", "Superior Apartment mit Balkon");
                        cols[idxFromColumn+3] = cols[idxFromColumn + 3].Replace("Child", "Kind");

                        string actRoomName = cols[0];
                        actRoom = _context.Rooms.FirstOrDefault(r => r.RoomName.ToUpper() == actRoomName);
                        if (actRoom == null)
                            throw new Exception("Importierter Raum " + actRoomName + " nicht in Datenbank gefunden!");

                        actResRoom.Room = actRoom;
                        actResRoom.From = Convert.ToDateTime(cols[idxFromColumn].Split(' ').Last());
                        actResRoom.To = Convert.ToDateTime(cols[idxFromColumn + 1].Split(' ').Last());
                        actResRoom.NumOfAdults = Convert.ToInt32(cols[idxFromColumn + 2].Split(' ').First());
                        actResRoom.CostsRoom = Convert.ToDouble(actCols[11]);
                        if (((actCols[7].IndexOf("Endreinigung") > -1) || (actCols[7].IndexOf("Final cleaning") > -1)))
                        {
                            actResRoom.FinalCleaning = 1;
                            actResRoom.CostsFinalCleaning = 45;
                        }

                        if (cols[idxFromColumn + 3].Split(' ').Last().StartsWith("Kind"))
                        {
                            actResRoom.NumOfChilds = Convert.ToInt32(cols[idxFromColumn + 3].Split(' ').First());
                            actResRoom.AgeOfChilds = cols[idxFromColumn + 4].Split(' ').Last();
                        }

                        _context.ReservationRoom.Add(actResRoom);
                        _context.SaveChanges();
                    }
                    else if (actCols[7].StartsWith("Handtücher"))
                    {
                        actResRoom.Towels += (int)Convert.ToDouble(actCols[11]) / 6;
                        actResRoom.CostsTowels += (int)Convert.ToDouble(actCols[11]);

                        _context.SaveChanges();
                    }
                    else if (actCols[7].StartsWith("Bettwäsche"))
                    {

                        actResRoom.BedLinen += (int)Convert.ToDouble(actCols[11]) / 9;
                        actResRoom.CostsBedLinen += (int)Convert.ToDouble(actCols[11]);

                        _context.SaveChanges();
                    }
                }
                
            }
        }

        
    }
}
