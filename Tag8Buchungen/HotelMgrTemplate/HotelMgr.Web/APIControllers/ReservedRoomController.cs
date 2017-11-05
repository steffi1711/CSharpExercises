using HotelMgr.Core.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelMgr.Web.APIControllers
{
    [Route("api/[controller]")]
    public class ReservedRoomController: Controller
    {
        public IUnitOfWork uow;

        public ReservedRoomController(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var hotels = uow.ReservationRoomRepository.Get();
            return Ok(hotels);
        }
        
    }
}
