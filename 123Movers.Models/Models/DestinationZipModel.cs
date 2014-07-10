using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace _123Movers.Models
{
    public class DestinationZipModel
    {
        [Display(Name = "Company ID")]
        public int? CompanyId { get; set; }

        [Display(Name = "Service ID")]
        public int? ServiceId { get; set; }

        [Display(Name = "Area Code")]
        public int? AreaCode { get; set; }

        [Display(Name = "ZipCode")]
        public int? ZipCode { get; set; }

        public IEnumerable<DestinationZipModel> DestinationAreaCodes { get; set; }
        public IEnumerable<DestinationZipModel> DestinationZipCodes { get; set; }
        public CompanyModel _companyInfo { get; set; }
    }
}