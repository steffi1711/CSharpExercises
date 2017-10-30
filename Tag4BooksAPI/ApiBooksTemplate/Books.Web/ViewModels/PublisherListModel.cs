using Books.Core.Contracts;
using Books.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Web.ViewModels
{
    public class PublisherListModel
    {
        [Display(Name="Von")]
        public int? FilterFrom { get; set; }
        [Display(Name = "Bis")]
        public int? FilterTo { get; set; }

        public Publisher[] Publishers;

        public Publisher TopPublisher { get; set; }

        public void LoadData(IUnitOfWork uow)
        {
            
            Publishers = uow.PublisherRepository.Get(
                p=>(p.Books.Count>=(FilterFrom??int.MinValue)) && (p.Books.Count<=(FilterTo??int.MaxValue)),
                orderBy: ord => ord.OrderBy(p => p.Name),includeProperties:"Books");

            TopPublisher = uow.GetPublisherWithMostBooks();
        }
    }
}
