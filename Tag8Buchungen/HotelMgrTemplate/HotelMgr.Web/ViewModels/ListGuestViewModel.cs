using HotelMgr.Core.Contracts;
using HotelMgr.Core.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelMgr.Web.ViewModels
{
    public class ListGuestViewModel
    {
        const string filterString = "alle";
        public string Filter { get; set; }
        public List<Guest> Guests { get; set; }
        public SelectList Countries { get; set; }


        IUnitOfWork uow;
        public ListGuestViewModel(IUnitOfWork uow)
        {
            this.uow = uow;
            var stringList = uow.CountryRepository.Get().Select(c => c.CountryName).ToList();
            stringList.Add(filterString);
            stringList.OrderBy(l => l);
            this.Countries = new SelectList(stringList);
        }

        internal void LoadGuests(string filter="")
        {
              List<Guest> guests;
              if(string.IsNullOrEmpty(filter) || filter == filterString)
                {
                    guests = this.uow.GuestRepository.Get(orderBy: g => g.OrderBy(c => c.LastName).ThenBy(c => c.FirstName), includeProperties: "country").ToList();

                }
            else
            {
                guests = this.uow.GuestRepository.Get(filter: (c => c.Country.CountryName == filter), orderBy: g => g.OrderBy(c => c.LastName).ThenBy(c => c.FirstName), includeProperties: "country").ToList();
            }
            this.Guests = guests;
        }
    }
}
