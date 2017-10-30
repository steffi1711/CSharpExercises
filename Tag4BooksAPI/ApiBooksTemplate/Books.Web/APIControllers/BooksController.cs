using Books.Core.Contracts;
using Books.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Web.APIControllers
{
    [Route("api/[controller]")]
    public class BooksController : Controller 
    {
        private IUnitOfWork uow;
        public BooksController(IUnitOfWork unitOfWork)
        {
            this.uow = unitOfWork;
        }
       
        [HttpGet]
        public IActionResult Get()
        {
            var books = uow.BookRepository.Get();
            return Ok(books);
        }


        [HttpGet("{id}", Name = "GetById")]
        public IActionResult GetById(int id)
        {
            var books = uow.BookRepository.GetById(id);
            if (books == null)
            {
                return NotFound();
            }
            return Ok(books);
        }


        [HttpPost]
        public IActionResult Post(Book book)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            uow.BookRepository.Insert(book);
            uow.Save();
            return new CreatedAtRouteResult("GetById", new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Book book)
        {
            if (book == null || book.Id != id)
            {
                return BadRequest();
            }
            var updBook = uow.BookRepository.GetById(id);
            if (updBook == null)
            {
                return NotFound();
            }
            uow.BookRepository.Update(book);
            uow.Save();
            return new NoContentResult();

        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var updBook = uow.BookRepository.GetById(id);
            if (updBook == null)
            {
                return NotFound();
            }
            uow.BookRepository.Delete(id);
            uow.Save();
            return new NoContentResult();
        }
    }
}