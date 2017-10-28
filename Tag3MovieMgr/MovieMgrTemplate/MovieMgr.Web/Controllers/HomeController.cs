using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieMgr.Core.Contracts;
using MovieMgr.Core.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieMgr.Web.ViewModels;

namespace MovieMgr.Web.Controllers
{
    public class HomeController : Controller
    {
        public IUnitOfWork uow;

        public HomeController(IUnitOfWork uow)
        {
            this.uow = uow;
        }
        public IActionResult Index()
        {
            CategoryListViewModel model = new CategoryListViewModel();
            model.ListCategories(uow);
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(CategoryListViewModel model)
        {
            if (uow.CategoryRepository.Get(c => c.CategoryName.ToUpper() == model.NewCat.CategoryName).Count() > 0)
            {
                ModelState.AddModelError("NewCat.CategoryName", "Diese Kategorie existiert bereits!");
            }
            if (ModelState.IsValid)
            {
                uow.CategoryRepository.Insert(model.NewCat);
                uow.Save();
                return RedirectToAction("Index");
            }

            model.ListCategories(uow);
            return View("Index", model);
        }

        public IActionResult Details(int catId)
        {
            CategoryDetailsViewModel model = new CategoryDetailsViewModel();
            model.ListMovies(catId, uow);
            if (model.Category == null)
                return NotFound();

            return View(model);
        }

        public IActionResult CreateMovie(int catId)
        {
            CreateMovieViewModel model = new CreateMovieViewModel();
            model.NewMovie.Category_Id = catId;
            model.ListCategories(uow);
            return View(model);
        }

        [HttpPost]
        public IActionResult CreateMovie(CreateMovieViewModel model)
        {
            if(ModelState.IsValid)
            {
                uow.MovieRepository.Insert(model.NewMovie);
                uow.Save();
                return RedirectToAction("Details", new { catId = model.NewMovie.Category_Id });
            }
            return View(model);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
