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
    
    public partial class tbl_availabilityListArchives
    {
        public Nullable<int> serviceID { get; set; }
        public string service { get; set; }
        public string longstate { get; set; }
        public string areacode { get; set; }
        public Nullable<int> compCount { get; set; }
        public Nullable<decimal> numOfLeadsLast30Days { get; set; }
        public Nullable<int> excLeads { get; set; }
        public Nullable<int> leads { get; set; }
        public Nullable<decimal> monthlyFeeOfThisAreaCode { get; set; }
        public Nullable<int> numCompLeadSent { get; set; }
        public Nullable<decimal> avgLeadsTheCompanyReceive { get; set; }
        public Nullable<decimal> avgPriceOfLeadPerCompany { get; set; }
        public Nullable<decimal> desiredMaxAvgPricePerLeadPerCompany { get; set; }
        public Nullable<bool> isFlagged { get; set; }
        public Nullable<System.DateTime> dateSubmitted { get; set; }
        public System.DateTime stampDate { get; set; }
    }
}
