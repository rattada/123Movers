using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _123Movers.Models
{
    public class LeadLimitModel
    {

        [Display(Name = "Company ID")]
        public int? CompanyId { get; set; }

        [Display(Name = "Service ID")]
        public int? ServiceId { get; set; }

        [Display(Name = "Move Weight ID")]
        public int? MoveWeightID { get; set; }

        [RegularExpression(RegexPattern.NUMERIC, ErrorMessage = "Total Lead Limit must be a numerical value.")]
        [Display(Name = "Total Lead Limit")]
        public int? TotalLeadLimit { get; set; }

        [RegularExpression(RegexPattern.NUMERIC, ErrorMessage = "Daily Lead Limit must be a numerical value.")]
        [Display(Name = "Daily Lead Limit")]
        public int? DailyLeadLimit { get; set; }

        [RegularExpression(RegexPattern.NUMERIC, ErrorMessage = "Monthly Lead Limit must be a numerical value.")]
        [Display(Name = "Monthly Lead Limit")]
        public int? MonthlyLeadLimit { get; set; }

        [RegularExpression(RegexPattern.NUMERIC, ErrorMessage = "Lead Counter must be a numerical value.")]
        [Display(Name = "Lead Counter")]
        public int? LeadCounter { get; set; }

        [RegularExpression(RegexPattern.NUMERIC, ErrorMessage = "Lead Frequency must be a numerical value.")]
        [Display(Name = "Lead Frequency")]
        public int? LeadFrequency { get; set; }

        [Display(Name = "Is Total Lead Limit")]
        public bool IsTotalLeadLimit { get; set; }

        [Display(Name = "Is Daily Lead Limit")]
        public bool IsDailyLeadLimit { get; set; }

        [Display(Name = "Is Monthly Lead Limit")]
        public bool IsMonthlyLeadLimit { get; set; }

        [Display(Name = "Price")]
        public decimal? Price { get; set; }

        [Required]
        [Display(Name = "Services")]
        public string Services { get; set; }

    }
}