using Vocabulary.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vocabulary.Core.Contracts
{
    public interface IUnitOfWork: IDisposable
    {
        IGenericRepository<Trial> TrialRepository { get; }
        IGenericRepository<Word> WordRepository { get; }

        void Save();

        void DeleteDatabase();

        
      
       
    }
}
