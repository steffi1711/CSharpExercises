using System;
using System.Collections.Generic;
using System.Linq;
using Utils;

namespace F1Logic
{
    public class Controller
    {
        IEnumerable<Team> _teams;
        IEnumerable<Driver> _drivers;
        IEnumerable<Result> _results;
        IEnumerable<Race> _races;

        public IEnumerable<Team> Teams
        {
            get { return _teams; }
        }

        public IEnumerable<Driver> Drivers
        {
            get { return _drivers; }
        }

        public IEnumerable<Result> Results
        {
            get { return _results; }
        }

        public IEnumerable<Race> Races
        {
            get { return _races; }
        }


        /// <summary>
        /// Alle Ergebnisse der Formel1-Rennen in die Collections einlesen
        /// </summary>
        public void ReadDataFromCsvFiles()
        {
            string[][] raceData = "Races.csv".ReadStringMatrixFromCsv(false);
            _races = raceData.Select((line, idx) => new Race()
            {
                Country = line[0],
                City = line[1],
                Date = DateTime.Parse(line[2]),
                Number = idx + 1,
            }).ToList();

            string[][] resultData = "Results.csv".ReadStringMatrixFromCsv(true);
            _teams = resultData.GroupBy(line => line[4]).Select(grp => new Team()
            {
                Name = grp.Key.Split('-').First(),
                Engine = grp.Key.Split('-').Last(), // grp.Key.Contains('-') ? grp.Key.Split('-')[1] : String.Empty,
            }).ToList();

            _drivers = resultData.GroupBy(line => new
            {
                Name = line[3],
                StartNr = line[2],
                TeamName = line[4].Split('-').First()
            }).Select(grp => new Driver()
            {
                FirstName = grp.Key.Name.Split(' ')[0],
                LastName = grp.Key.Name.Split(' ')[1],
                StartNumber = Convert.ToInt32(grp.Key.StartNr),
                Team = _teams.Where(t => t.Name == grp.Key.TeamName).First()
            }).ToList();

            _results = resultData.GroupBy(line => new
            {
                FirstName = line[3].Split(' ')[0],
                LastName = line[3].Split(' ')[1],
                RaceNr = line[0],
                PositionNr = line[1]
            }).Select(grp => new Result()
            {
                Driver = _drivers.Where(d => d.FirstName == grp.Key.FirstName && d.LastName == grp.Key.LastName).First(),
                Race = _races.Where(r => r.Number == Convert.ToInt32(grp.Key.RaceNr)).First(),
                Position = Convert.ToInt32(grp.Key.PositionNr)
            }
            ).ToList();
        }

        /// <summary>
        /// Berechnet aus den Ergebnissen den aktuellen WM-Stand
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ResultLine> GetDriverWmTable()
        {
            return _results.GroupBy(r => r.Driver).OrderByDescending(grp => grp.Sum(r => r.Points)).Select((grp, idx) => new ResultLine
            {
                Name = grp.Key.LastName,
                Points = grp.Sum(r => r.Points),
                Position = idx + 1
            }).ToList();
        }

        /// <summary>
        /// Teamtabelle ermitteln
        /// </summary>
        /// <returns>Team-WM-Stand</returns>
        public IEnumerable<ResultLine> GetTeamWmTable()
        {
            return _results.GroupBy(r => r.Driver.Team).OrderByDescending(grp => grp.Sum(r => r.Points)).Select((grp, idx) => new ResultLine
            {
                Name = grp.Key.Name,
                Points = grp.Sum(r => r.Points),
                Position = idx + 1
            }).ToList();
        }

    }

}
