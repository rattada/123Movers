using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _123Movers.Models
{
    public class GeographyModel
    {
        [Display(Name = "Company ID")]
        public int? CompanyId { get; set; }

        [Display(Name = "Service")]
        public int? ServiceId { get; set; }

        [Display(Name = "Area Code")]
        public int? AreaCode { get; set; }

        [Display(Name = "ZipCode")]
        public int? ZipCode { get; set; }

        public IEnumerable<GeographyModel> OriginAreaCodes { get; set; }
        public IEnumerable<GeographyModel> OriginZipCodes { get; set; }
        public CompanyModel _companyInfo { get; set; }
    }
}