using MovieMgr.Core.Contracts;
using MovieMgr.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieMgr.Web.ViewModels
{
    public class CategoryDetailsViewModel
    {
        public List<Movie> Movies { get; set; }

        public Category Category { get; set; }

        internal void ListMovies(int catId, IUnitOfWork uow)
        {
            Category = uow.CategoryRepository.GetById(catId);
            Movies = uow.MovieRepository.Get(c => c.Category_Id == catId, orderBy: m => m.OrderBy(ord => ord.Title)).ToList();
        }
    }
}
