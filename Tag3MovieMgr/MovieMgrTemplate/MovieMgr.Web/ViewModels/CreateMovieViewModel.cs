using Microsoft.AspNetCore.Mvc.Rendering;
using MovieMgr.Core.Contracts;
using MovieMgr.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieMgr.Web.ViewModels
{
    public class CreateMovieViewModel
    {
        public Movie NewMovie { get; set; }
        public SelectList Categories { get; set; }
        public CreateMovieViewModel()
        {
            NewMovie = new Movie();
        }

        public void ListCategories(IUnitOfWork uow)
        {
            var catlist = uow.CategoryRepository.Get(orderBy: ord => ord.OrderBy(c => c.CategoryName)).ToList();
            Categories = new SelectList(catlist, "Id", "CategoryName");
        }
    }
}
