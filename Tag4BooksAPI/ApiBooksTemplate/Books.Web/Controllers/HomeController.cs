using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Books.Core.Contracts;
using Books.Core.Entities;
using Books.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Books.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public IActionResult Index(PublisherListModel model)
        {
            model.LoadData(_unitOfWork);
            return View(model);
        }

        public IActionResult BookList(BookListViewModel model)
        {
            model.LoadData(_unitOfWork);
            return View(model); 
        }
       
        public IActionResult CreateBook(int Publisher_Id)
        {
            CreateBookViewModel model = new CreateBookViewModel();
            model.Book.Publisher_Id = Publisher_Id;
            model.LoadSelectList(_unitOfWork);

            return View(model);
        }

        [HttpPost]
        public IActionResult CreateBook(CreateBookViewModel model)
        {
            if (!Book.CheckIsbn(model.Book.Isbn))
            {
                ModelState.AddModelError("Book.Isbn", "Ungültige ISBN-Nummer!");
            }
            if (!ModelState.IsValid)
            {
                model.LoadSelectList(_unitOfWork);
                return View(model);
            }
            _unitOfWork.BookRepository.Insert(model.Book);
            _unitOfWork.Save();
            return RedirectToAction("BookList", new { SelectedPub_id = model.Book.Publisher_Id });
        }

        public IActionResult DeleteBook(int id)
        {
            var book = _unitOfWork.BookRepository.GetById(id);
            if (book == null)
                return NotFound();
            _unitOfWork.BookRepository.Delete(id);
            _unitOfWork.Save();
            return RedirectToAction("BookList", new { SelectedPub_id = book.Publisher_Id });
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
