using CdMgr.Core;
using CdMgr.Core.Entities;
using CdMgr.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CdMgr.ConsoleFillDb
{
    class Program
    {
        static void Main(string[] args)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                CdController ctr = new CdController(uow);
                ctr.ImportDataFromXMLFiles();
               


                var res = uow.CdRepository.Get();
                foreach (var item in res)
                {
                    Console.WriteLine(item.Title);
                }
            }
        }
    }
}
