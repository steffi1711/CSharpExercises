using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vocabulary.Core.Contracts;
using Vocabulary.Core.Entities;

namespace Vocabulary.API.Controllers
{
    [Route("api/[controller]")]
    public class WordController
    {
        IUnitOfWork _unitOfWork;
        public WordController(IUnitOfWork uow)
        {
            this._unitOfWork = uow;
        }
        [HttpGet]
        public IEnumerable<Word> Get()
        {
            return _unitOfWork.WordRepository.Get();
        }
    }
}
