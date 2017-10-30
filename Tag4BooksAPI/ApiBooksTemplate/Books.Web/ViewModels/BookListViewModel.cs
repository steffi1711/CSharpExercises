using Books.Core.Contracts;
using Books.Core.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Web.ViewModels
{
    public class BookListViewModel
    {
        public Book[] Books { get; set; }
        public SelectList PublisherList { get; set; }

        public int SelectedPub_id { get; set; }

        public void LoadData(IUnitOfWork uow)
        {
            if (SelectedPub_id>0)
              Books=uow.BookRepository.Get(b => b.Publisher.Id == SelectedPub_id,orderBy: ord => ord.OrderBy(book => book.Title), includeProperties:  "Publisher");
            else
              Books = uow.BookRepository.Get(orderBy: ord => ord.OrderBy(book => book.Title), includeProperties: "Publisher");

            var tempList = uow.PublisherRepository.Get(orderBy: ord => ord.OrderBy(p => p.Name)).ToList();
            tempList.Insert(0, new Publisher()
            {
                Id = 0,
                Name = "< Alle anzeigen >"
            });
            PublisherList = new SelectList(tempList,"Id", "Name");
        }
    }
}
