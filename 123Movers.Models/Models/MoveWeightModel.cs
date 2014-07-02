using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _123Movers.Models
{
    public class MoveWeightModel
    {
        [Display(Name = "Company ID")]
        public int? CompanyId { get; set; }

        [Display(Name = "Service ID")]
        public int? ServiceId { get; set; }

        [Display(Name = "Move Weight")]
        public string MoveWeight { get; set; }

        [Display(Name = "Min Move Weight")]
        public string MinMoveWeight { get; set; }

        [Display(Name = "Max Move Weight")]
        public string MaxMoveWeight { get; set; }

        [Display(Name = "Move Weight Seq")]
        public int? MoveWeightSeq { get; set; }

        [Required]
        [Display(Name = "Services")]
        public string Services { get; set; }

    }
}