using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _123Movers.Models
{
    public class DestinationAreaCodeModel
    {
        public CompanyModel _companyInfo { get; set; }
        [Display(Name = "Service")]
        public int? ServiceId { get; set; }
    }
}