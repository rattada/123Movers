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

        [Display(Name = "Tid")]
        public int Tid { get; set; }
       // [Required]
        [Display(Name = "Company ID")]
        public int? CompanyId { get; set; }

        [Display(Name = "Min Days To Charge")]
        public int? MinDaysToCharge { get; set; }

        [Required]
        [Display(Name = "Service ID")]
        public int? ServiceId { get; set; }

        //[Required]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        //[Required]
        [Display(Name = "Display Name")]
        public string DisplayName { get; set; }

        [Display(Name = "Sales Person")]
        public string ContactPerson { get; set; }

        [Required]
        [Display(Name = "Company Handle")]
        public string CompanyHandle { get; set; }

        [Display(Name = "Suspended")]
        public string Suspended { get; set; }


        [Required]
       // [DataType(DataType.Currency)]
        //[DisplayFormat(DataFormatString = "{0:c}", ApplyFormatInEditMode = true)]
       // [HtmlProperties(CssClass = "currency")]
        //[RegularExpression(@"^\d+.\d{0,2}$")]
        //[Range(0, 9999999999999999.99)]
        [Display(Name = "Budget Amount")]
        public decimal? TotalBudget { get; set; }

       // [Required]
        [Display(Name = "Remaining Budget")]
        //[DataType(DataType.Currency)]
       // [DisplayFormat(DataFormatString = "{0:c}", ApplyFormatInEditMode = true)]
        public decimal? RemainingBudget { get; set; }

        [Required]
        [Display(Name = "Uncharged Amount")]
        //[DataType(DataType.Currency)]
        //[DisplayFormat(DataFormatString = "{0:c}", ApplyFormatInEditMode = true)]
        public decimal? UnchargedAmount { get; set; }

        //[Required]
        [Display(Name = "Budget Action")]

        public string BudgetAction { get; set; }

        [Required]
        [Display(Name = "Start Date")]
       // [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}",  ApplyFormatInEditMode = true)]
        public DateTime? StartDate { get; set; }

        [Display(Name = "End Date")]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }

        //[Required]
        [Display(Name = "Active")]
        public bool IsActive { get; set; }


        //[Required]
        [Display(Name = "Recurring")]
        public bool IsRecurring { get; set; }

        //[Required]
        [Display(Name = "Require Notice To Chagrge")]
        public bool IsRequireNoticeToCharge { get; set; }

        [Display(Name = "Expire")]
        public bool Expire { get; set; }

        [Required]
        [Display(Name = "Terms")]
        public string Terms { get; set; }

        [Required]
        [Display(Name = "Services")]
        public string Services { get; set; }

       // [Required]
        [Display(Name = "Agreement Number")]
        public string AgreementNumber { get; set; }

         [Display(Name = "AX #")]
        public string AX { get; set; }

         //[Required]
         [Display(Name = "Area Codes")]
         public string AreaCodes { get; set; }

         //[Required]
         [Display(Name = "Insertion Order ID")]
         public string InsertionOrderId { get; set; }

         public string Type { get; set; }

        [Required]
         public string TermType { get; set; }
    }
}