using MovieMgr.Core.Contracts;
using MovieMgr.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace MovieMgr.Core
{
    public class MovieController
    {
        const string FILENAME = "movies.csv";
        IUnitOfWork _unitOfWork;
        public MovieController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public void FillDatabaseFromCsv()
        {
            string[][] csvMovies = FILENAME.ReadStringMatrixFromCsv(true);

            List<Category> categories = csvMovies.GroupBy(line => line[2]).Select(grp => new Category() { CategoryName = grp.Key }).ToList();
            List<Movie> movies = csvMovies.Select(line =>
              new Movie()
              {
                  Category=categories.Single(cat=>cat.CategoryName==line[2]),
                  Duration=int.Parse(line[3]),
                  Title=line[0],
                  Year=int.Parse(line[1]),
              }).ToList();

            _unitOfWork.DeleteDatabase();
            _unitOfWork.CategoryRepository.InsertMany(categories);
            _unitOfWork.MovieRepository.InsertMany(movies);
            _unitOfWork.Save();
        }
    }
}
