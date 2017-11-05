using HotelMgr.Core.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelMgr.Web.APIControllers
{
    [Route("api/[controller]")]
    public class HotelController:Controller
    {
        public IUnitOfWork uow; 

        public HotelController(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var hotels = uow.RoomRepository.Get(orderBy: n => n.OrderBy(r => r.RoomNumber));
            return Ok(hotels);
        }
        [HttpGet, Route("{id}", Name = "GetById")]    
        public IActionResult GetById(int id)
        {
            var item = uow.RoomRepository.GetById(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }
       
    }
}
