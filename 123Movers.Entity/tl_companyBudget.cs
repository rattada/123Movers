//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace _123Movers.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class tl_companyBudget
    {
        public int tid { get; set; }
        public Nullable<int> companyID { get; set; }
        public Nullable<int> serviceID { get; set; }
        public string areaCode { get; set; }
        public Nullable<int> moveWeightID { get; set; }
        public Nullable<decimal> totalBudget { get; set; }
        public Nullable<decimal> remainingBudget { get; set; }
        public Nullable<int> lastQuoteID { get; set; }
        public Nullable<decimal> lastQuotePrice { get; set; }
        public Nullable<System.DateTime> lastModified { get; set; }
        public Nullable<System.DateTime> stampDate { get; set; }
        public System.DateTime logDate { get; set; }
        public string action { get; set; }
        public string budgetAction { get; set; }
        public string budgetNote { get; set; }
        public Nullable<bool> isRecurring { get; set; }
        public Nullable<bool> isRequireNoticeToCharge { get; set; }
        public Nullable<bool> isOneTimeRenew { get; set; }
        public Nullable<int> minDaysToCharge { get; set; }
        public string agreementNumber { get; set; }
        public string budgetInsertionID_auto { get; set; }
    }
}
