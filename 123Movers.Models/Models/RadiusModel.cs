using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace _123Movers.Models
{
    public class RadiusModel
    {
            public CompanyModel CompanyInfo { get; set; }
            [Display(Name = "Service")]
            public int? ServiceId { get; set; }
    }
}