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