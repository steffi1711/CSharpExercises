using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vocabulary.Core.Contracts;
using Vocabulary.Core.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Vocabulary.Web.Controllers
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
            return View();
        }

      
        
       
    }
}
