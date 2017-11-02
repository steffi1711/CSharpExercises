using Vocabulary.Core;
using Vocabulary.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleFillDb
{
    class Program
    {
        static void Main(string[] args)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                VocabularyController ctrl = new VocabularyController(uow);
                ctrl.FillDatabaseFromCsv();

                var words = uow.WordRepository.Get();
                foreach (var word in words)
                {
                    Console.WriteLine(word.LeftWord+" = "+word.RightWord);
                }

            }
        }
    }
}
