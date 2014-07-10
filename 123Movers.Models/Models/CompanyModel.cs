﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _123Movers.Models
{
    public class CompanyModel
    {
        [Display(Name = "Company ID")]
        public int? CompanyId { get; set; }

        [Display(Name = "Name")]
        public string CompanyName { get; set; }

        [Display(Name = "Sales Person")]
        public string ContactPerson { get; set; }

        [Display(Name = "AX #")]
        public string AX { get; set; }

        //[Display(Name = "Agreement")]
        //public string AgreementNumber { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }

        [Display(Name = "Suspended")]
        public string Suspended { get; set; }


        public CompanyModel CurrentCompany
        {
            get
            {
                if (HttpContext.Current.Session["CurrentCompanyInfo"] == null)
                    return null;
                else
                    return (CompanyModel)HttpContext.Current.Session["CurrentCompanyInfo"];
            }

            set
            {
                HttpContext.Current.Session["CurrentCompanyInfo"] = value;
            }
        }

        //public CompanyModel CurrentCompany { get; set; }
    }
}