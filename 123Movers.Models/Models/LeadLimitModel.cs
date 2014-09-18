using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace _123Movers.Models
{
    public class LeadLimitModel
    {

        [Display(Name = "Company ID")]
        public int? CompanyId { get; set; }

        [Display(Name = "Service")]
        public int? ServiceId { get; set; }


        [RegularExpression(RegexPattern.NUMERIC, ErrorMessage = "Total Lead Limit must be a numerical value.")]
        [Display(Name = "Total Lead Limit")]
        public int? TotalLeadLimit { get; set; }

        [RegularExpression(RegexPattern.NUMERIC, ErrorMessage = "Daily Lead Limit must be a numerical value.")]
        [Display(Name = "Daily Lead Limit")]
        public int? DailyLeadLimit { get; set; }

        [RegularExpression(RegexPattern.NUMERIC, ErrorMessage = "Monthly Lead Limit must be a numerical value.")]
        [Display(Name = "Monthly Lead Limit")]
        public int? MonthlyLeadLimit { get; set; }


        [Required]
        [RegularExpression(RegexPattern.NUMERIC, ErrorMessage = "Lead Frequency must be a numerical value.")]
        [Display(Name = "Lead Frequency")]
        public int? LeadFrequency { get; set; }

        [Display(Name = "chkTotal Lead Limit")]
        public bool IsTotalLeadLimit { get; set; }

        [Display(Name = "chkDaily Lead Limit")]
        public bool IsDailyLeadLimit { get; set; }

        [Display(Name = "chkMonthly Lead Limit")]
        public bool IsMonthlyLeadLimit { get; set; }


        [Display(Name = "Services")]
        public string Services { get; set; }

        [Display(Name = "Area Codes")]
        public string AreaCodes { get; set; }

        public IEnumerable<LeadLimitModel> LeadLimitData { get; set; }
        public CompanyModel CompanyInfo { get; set; }

    }
}