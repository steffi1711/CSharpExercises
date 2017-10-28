using MovieMgr.Core;
using MovieMgr.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleFillDb
{
    class Program
    {
        static void Main(string[] args)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                MovieController ctrl = new MovieController(uow);
                ctrl.FillDatabaseFromCsv();

                var movies = uow.MovieRepository.Get();
                foreach (var movie in movies)
                {
                    Console.WriteLine(movie.Title);
                }

            }
        }
    }
}
