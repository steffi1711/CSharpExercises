using MovieMgr.Core.Contracts;
using MovieMgr.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieMgr.Web.ViewModels
{
    public class CategoryListViewModel
    {
        public List<Category> Categories { get; set; }

        public Category NewCat { get; set; }

        public CategoryListViewModel()
        {
            NewCat = new Category();
        }
        internal void ListCategories(IUnitOfWork uow)
        {
            Categories = uow.CategoryRepository.Get(orderBy: ord => ord.OrderBy(c => c.CategoryName)).ToList();
        }


    }
}
