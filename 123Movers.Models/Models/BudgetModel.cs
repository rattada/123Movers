using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _123Movers.Models
{
    public class BudgetModel
    {

        public int? tId { get; set; }

        [Display(Name = "Company ID")]
        public int? CompanyId { get; set; }

        [Display(Name = "Min Days To Charge")]
        public int? MinDaysToCharge { get; set; }

        [Required]
        [Display(Name = "Service")]
        public int? ServiceId { get; set; }

        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        [Display(Name = "Sales Person")]
        public string ContactPerson { get; set; }

        [Display(Name = "Suspended")]
        public string Suspended { get; set; }

        [Required]
        [Display(Name = "Budget Amount")]
        public decimal? TotalBudget { get; set; }

        [Display(Name = "Remaining Budget")]
        public decimal? RemainingBudget { get; set; }

        [Display(Name = "Uncharged Amount")]
        public decimal? UnchargedAmount { get; set; }

        [Display(Name = "Budget Action")]
        public string BudgetAction { get; set; }

        [Display(Name = "Start Date")]
        public DateTime? StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }

        [Display(Name = "Recurring")]
        public bool? IsRecurring { get; set; }

        [Display(Name = "Require Notice To Chagrge")]
        public bool? IsRequireNoticeToCharge { get; set; }

        public bool IsOneTimeRenew { get; set; }

        //[Display(Name = "Expire")]
        //public bool Expire { get; set; }

        [Display(Name = "Agreement Number")]
        public string AgreementNumber { get; set; }

        [Display(Name = "AX #")]
        public string AX { get; set; }

        [Display(Name = "Area Codes")]
        public string AreaCodes { get; set; }

        [Display(Name = "Insertion Order ID")]
        public string InsertionOrderId { get; set; }


        [Required]
        [Display(Name = "Terms")]
        public string TermType { get; set; }

        public string Type { get; set; }

        public CompanyModel _companyInfo { get; set; }

        public List<BudgetModel> _currentBudgets { get; set; }

        public List<BudgetModel> _pastBudgets { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            
            return null;
            //if (validationContext.Items.Count() > 0)
            //{

            //    //Match coupon data elements with the same name, same coupon type, different id and same site id.
            //    var duplicateData = ((ICmsEntities)validationContext.Items["Context"]).Coupons.Where(
            //        c => c.Name.ToLower() == this.Name.ToLower() && c.Id != this.Id
            //            && c.CouponType == this.CouponType && c.SiteId == this.SiteId && !c.IsDeleted);

            //    if (duplicateData.Count() > 0)
            //    {
            //        yield return
            //            new ValidationResult("The coupon already exists for this site and type.");
            //    }
            //}
        }

    }
}