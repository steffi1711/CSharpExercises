using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CdMgr.Core.Contracts;
using CdMgr.Core.Entities;
using CdMgr.Web.ViewModels;

namespace CdMgr.Web.Controllers
{

    public class HomeController : Controller
    {
        IUnitOfWork _unitOfWork;


        public HomeController(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        public IActionResult Index()
        {
            CdOverviewViewModel model = new CdOverviewViewModel();
            model.Cds = _unitOfWork.CdRepository.Get(orderBy: ord => ord.OrderBy(c => c.Title)).ToList();
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(CdOverviewViewModel model)
        {
            if (!String.IsNullOrEmpty(model.FilterText))
            {
                model.Cds = _unitOfWork.CdRepository.Get(filter: cd => cd.Title.ToUpper().StartsWith(model.FilterText.ToUpper()), orderBy: ord => ord.OrderBy(c => c.Title)).ToList();
            }
            else
            {
                model.Cds = _unitOfWork.CdRepository.Get(orderBy: ord => ord.OrderBy(c => c.Title)).ToList();
            }
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            EditCdViewModel model = new EditCdViewModel();
            model.Cd = _unitOfWork.CdRepository.GetById(id);

            if (model.Cd == null)
                return NotFound();

            return View(model);
        }

       [HttpPost]
       public IActionResult Edit(EditCdViewModel model)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.CdRepository.Update(model.Cd);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(model);
        }

       
        public IActionResult Error()
        {
            return View();
        }
    }
}
