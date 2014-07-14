using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _123Movers.Models
{
    public class MoveDistanceModel
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

        public decimal? MaxMoveDistance { get; set; }

        [Display(Name = "Move Weight Seq")]
  
        public decimal? MoveWeightSeq { get; set; }

        [Required]
        [Display(Name = "Services")]
        public string Services { get; set; }

        public CompanyModel _companyInfo { get; set; }
    }
}