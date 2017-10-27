using CdMgr.Core.Contracts;
using CdMgr.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Utils;

namespace CdMgr.Core
{
    public class CdController
    {
        const string XML_CD_FILENAME = "cds.xml";
        const string XML_COMPANY_FILENAME = "companies.xml";
        IUnitOfWork _unitOfWork;
        public CdController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public void ImportDataFromXMLFiles()
        {
            List<Company> comps;
            List<Cd> cds;

            _unitOfWork.DeleteDatabase();

            XDocument compDoc=XDocument.Load(XML_COMPANY_FILENAME.GetFullNameInApplicationTree());

            comps = compDoc.Root.Elements("COMPANY").Select(xmlComp => new Company()
            {
                CompanyName = xmlComp.Element("NAME").Value,
                Director = (string)xmlComp.Element("DIRECTOR"),
                Founded = (int)xmlComp.Element("FOUNDED")
            }).ToList();

            XDocument cdDoc = XDocument.Load(XML_CD_FILENAME.GetFullNameInApplicationTree());

            cds = cdDoc.Root.Elements("CD").Select(xmlComp => new Cd()
            {
                Title = xmlComp.Element("TITLE").Value,
                Artist = (string)xmlComp.Element("ARTIST"),
                Country=(string)xmlComp.Element("COUNTRY"),
                Year=(int)xmlComp.Element("YEAR"),
                Price=(double)xmlComp.Element("PRICE"),
                Company=comps.FirstOrDefault(c=>c.CompanyName== (string)xmlComp.Element("COMPANY"))
            }).ToList();

            _unitOfWork.CdRepository.InsertMany(cds);
            _unitOfWork.CompanyRepository.InsertMany(comps);




            _unitOfWork.Save();
        }
    }
}
