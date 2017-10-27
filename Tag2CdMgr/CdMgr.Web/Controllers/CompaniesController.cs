using CdMgr.Core.Contracts;
using CdMgr.Core.Entities;
using CdMgr.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CdMgr.Web.Controllers
{
    public class CompaniesController : Controller
    {
        IUnitOfWork _unitOfWork;


        public CompaniesController(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        public IActionResult Index()
        {
            CompanyOverviewViewModel model = new CompanyOverviewViewModel();
            model.Companies = _unitOfWork.CompanyRepository.Get(orderBy: ord => ord.OrderBy(c => c.CompanyName)).ToList();
            return View(model);
        }


        public IActionResult Add()
        {
            Company model = new Company();
            return View(model);
        }

        [HttpPost]
        public IActionResult Add(Company model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _unitOfWork.CompanyRepository.Insert(model);

            _unitOfWork.Save();
            return RedirectToAction("Index");

        }
    }
}
