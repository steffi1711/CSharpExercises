using Books.Core.Contracts;
using Books.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Web.APIControllers
{
    [Route("api/[controller]")]
    public class PublisherController: Controller
    {
        private IUnitOfWork uow;

        public PublisherController(IUnitOfWork unitOfWork)
        {
            this.uow = unitOfWork;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var publisher = uow.PublisherRepository.Get();
            return Ok(publisher);
        }

        [HttpGet("{id}", Name = "GetById")]
        public IActionResult GetById(int id)
        {
            var publisher = uow.PublisherRepository.GetById(id);
            if (publisher == null)
            {
                return NotFound();
            }
            return Ok(publisher);
        }


        [HttpPost]
        public IActionResult Post(Publisher publisher)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            uow.PublisherRepository.Insert(publisher);
            uow.Save();
            return new CreatedAtRouteResult("GetById", new { id = publisher.Id }, publisher);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Publisher publisher)
        {
            if (publisher == null || publisher.Id != id)
            {
                return BadRequest();
            }
            var updPub = uow.PublisherRepository.GetById(id);
            if (updPub == null)
            {
                return NotFound();
            }
            uow.PublisherRepository.Update(publisher);
            uow.Save();
            return new NoContentResult();

        }

       
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var updPub = uow.PublisherRepository.GetById(id);
            if (updPub == null)
            {
                return NotFound();
            }
            uow.PublisherRepository.Delete(id);
            uow.Save();
            return new NoContentResult();
        }
    }
}
