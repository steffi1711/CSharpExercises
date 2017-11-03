using HotelChecker.Core.Contracts;
using HotelChecker.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelChecker.Web.APIControllers
{
    [Route("api/[controller]")]
    public class HotelController: Controller
    {
        public IUnitOfWork uow;

        public HotelController(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        [HttpPost]
        public IActionResult Get()
        {
            var hotels = uow.HotelRepository.Get();
            return Ok(hotels);

        }

        [HttpGet, Route("{id}", Name = "GetHotelById")]
        public IActionResult GetById(int id)
        {
            var item = uow.HotelRepository.GetById(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        public IActionResult PostHotel([FromBody] Hotel newHotel)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            uow.HotelRepository.Insert(newHotel);
            uow.Save();
            return new CreatedAtRouteResult("GetHotelById", new { id = newHotel.Id }, newHotel);
        }
    }
}
