using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CdMgr.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace CdMgr.Web.ViewModels
{
    public class CdOverviewViewModel 
    {
        public List<Cd> Cds { get; set; }

        [Display(Name = "Filter-Text")]
        public string FilterText { get; set; }
    }
}
