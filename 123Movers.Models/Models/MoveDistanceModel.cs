using System.ComponentModel.DataAnnotations;

namespace _123Movers.Models
{
    public class MoveDistanceModel
    {
        [Display(Name = "Company ID")]
        public int? CompanyId { get; set; }

        [Required]
        [Display(Name = "Service")]
        public int? ServiceId { get; set; }

        [Display(Name = "Move Weight")]
        public string MoveWeight { get; set; }

        [Display(Name = "Min Move Distance")]
        public double? MinMoveDistance { get; set; }

        [Display(Name = "Max Move Distance")]

        public double? MaxMoveDistance { get; set; }

        [Display(Name = "Move Weight Seq")]
        public decimal? MoveWeightSeq { get; set; }

        public CompanyModel CompanyInfo { get; set; }
    }
}