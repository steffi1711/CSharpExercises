using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HotelMgr.Core.Contracts;
using HotelMgr.Web.ViewModels;

namespace HotelMgr.Web.Controllers
{
    public class HomeController : Controller
    {
        IUnitOfWork uow;

        public HomeController(IUnitOfWork uow)
        {
            this.uow = uow;
        }
        public IActionResult Index(string filter="")
        {
            var model = new ListGuestViewModel(this.uow);
            model.LoadGuests(filter);
            return View(model);
        }

 
        public IActionResult Error()
        {
            return View();
        }
    }
}
