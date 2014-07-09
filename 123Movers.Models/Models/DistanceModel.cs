using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _123Movers.Models
{
    public class DistanceModel
    {
        [Display(Name = "Company ID")]
        public int? CompanyId { get; set; }

        [Display(Name = "Service")]
        public int? ServiceId { get; set; }

        [Display(Name = "Move Weight")]
        public string MoveWeight { get; set; }

        [Display(Name = "Min Move Distance")]
        public decimal? MinMoveDistance { get; set; }

        [Display(Name = "Max Move Distance")]
        //[RegularExpression(RegexPattern.Decimal, ErrorMessage = "Min Move Weight must be a decimal.")]
        public decimal? MaxMoveDistance { get; set; }

        [Display(Name = "Move Weight Seq")]
      //  [RegularExpression(RegexPattern.Decimal, ErrorMessage = "Max Move Weight must be a decimal.")]
        public decimal? MoveWeightSeq { get; set; }

        [Required]
        [Display(Name = "Services")]
        public string Services { get; set; }
    }
}