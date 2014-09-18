using System.ComponentModel.DataAnnotations;

namespace _123Movers.Models
{
    public class SpecificOriginAreaCode
    {
        public CompanyModel CompanyInfo { get; set; }
        [Display(Name = "Service")]
        public int? ServiceId { get; set; }
    }
}