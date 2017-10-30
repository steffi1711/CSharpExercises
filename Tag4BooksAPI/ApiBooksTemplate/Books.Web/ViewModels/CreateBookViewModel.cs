using Books.Core.Contracts;
using Books.Core.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Web.ViewModels
{
    public class CreateBookViewModel
    {
        public Book Book { get; set; } = new Book();
        public SelectList PublisherList { get; set; }

        public void LoadSelectList (IUnitOfWork uow)
        {
            PublisherList = new SelectList(uow.PublisherRepository.Get(orderBy: ord => ord.OrderBy(p => p.Name)), "Id", "Name");               
        }
    }
}
