using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace _123Movers.DataEntities
{
    [Table("tbl_companyBudget")]
    public class Budget
    {
        public int tid { get; set; }

        public int companyID { get; set; }

        public int serviceID { get; set; }

        public int areaCode { get; set; }

        public int moveWeightID { get; set; }

        public decimal totalBudget { get; set; }

        public decimal remainingBudget { get; set; }

        public int lastQuoteID { get; set; }

        public decimal lastQuotePrice { get; set; }

        public DateTime stampDate { get; set; }

        public DateTime lastModified { get; set; }

        public string budgetAction { get; set; }

        public string budgetNote { get; set; }

        public bool IsRecurring { get; set; }

        public bool IsRequireNoticeToCharge { get; set; }

        public bool IsOneTimeRenew { get; set; }

        public int MinDaysToCharge { get; set; }

        public string AgreementNumber { get; set; }

        //public CompanyModel _companyInfo { get; set; }

    }
}