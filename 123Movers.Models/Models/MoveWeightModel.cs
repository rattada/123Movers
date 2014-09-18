using System.ComponentModel.DataAnnotations;

namespace _123Movers.Models
{
    public class MoveWeightModel
    {
        [Display(Name = "Company ID")]
        public int? CompanyId { get; set; }

        [Display(Name = "Service")]
        public int? ServiceId { get; set; }

        [Display(Name = "Move Weight")]
        public string MoveWeight { get; set; }

        public int? MoveWeightSeq { get; set; }

        [Display(Name = "Min Move Weight")]
        public int? MinMoveWeightSeq { get; set; }

        [Display(Name = "Max Move Weight")]
        public int? MaxMoveWeightSeq { get; set; }

        public CompanyModel CompanyInfo { get; set; }
    }
}