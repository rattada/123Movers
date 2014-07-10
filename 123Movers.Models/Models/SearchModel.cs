using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _123Movers.Models
{
    public class SearchModel
    {

        // [Required]
        [Display(Name = "Company ID")]
       // [MaxLength(4, ErrorMessage = "The Name field must be less than 4 characters.")]
        public int? CompanyId { get; set; }

        //[Required]
        [Display(Name = "Name")]
        public string CompanyName { get; set; }

        [Display(Name = "Sales Person")]
        public string ContactPerson { get; set; }

        //[Required]
        [Display(Name = "AX #")]
        public string AX { get; set; }

        //[Required]
        [Display(Name = "Insertion Order ID")]
        public string InsertionOrderId { get; set; }

        //[Required]
        //[Display(Name = "Area Codes")]
        //public string AreaCodes { get; set; }

        //[Required]
        [Display(Name = "Agreement")]
        public string AgreementNumber { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }

        //[Display(Name = "Display Name")]
        //public string DisplayName { get; set; }

        //[Display(Name = "Company Handle")]
        //public string CompanyHandle { get; set; }

        [Display(Name = "Suspended")]
        public string Suspended { get; set; }


        public IEnumerable<BudgetModel> budget { get; set; }

        public IEnumerable<SearchModel> Companies { get; set; }

    }
}