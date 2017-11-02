using Vocabulary.Core.Contracts;
using Vocabulary.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Vocabulary.Core
{
    public class VocabularyController
    {
        const string FILENAME = "words.csv";
        IUnitOfWork _unitOfWork;
        public VocabularyController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public void FillDatabaseFromCsv()
        {
            string[][] csvWords = FILENAME.ReadStringMatrixFromCsv(true,'\t');

            List<Word> words = csvWords.Select(line =>
              new Word()
              {
                  LeftWord = line[0],
                  RightWord = line[1]
                  
              }).ToList();

            _unitOfWork.DeleteDatabase();
            _unitOfWork.WordRepository.InsertMany(words);
            _unitOfWork.Save();
        }
    }
}
