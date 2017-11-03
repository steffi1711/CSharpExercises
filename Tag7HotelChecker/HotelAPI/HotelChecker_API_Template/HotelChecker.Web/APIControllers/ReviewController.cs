using HotelChecker.Core.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelChecker.Web.APIControllers
{
    [Route("api/[controller]")]
    public class ReviewController: Controller
    {
        IUnitOfWork uow;
        public ReviewController(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        [Route("GetByHotelId/{hotelId}")]
        public IActionResult GetReviewsByHotelId(int hotelId)
        {
            return Ok(uow.HotelReviewRepository.Get(r => r.Hotel.Id == hotelId, orderBy: ord => ord.OrderBy(r => r.Id)));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var review = uow.HotelReviewRepository.GetById(id);
            if (review == null)
                return NotFound();

            uow.HotelReviewRepository.Delete(id);
            uow.Save();
            return NoContent();
        }
    }
}
