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
    
    public partial class tl_company_deletelistings_log
    {
        public int tid { get; set; }
        public int companyID { get; set; }
        public byte isAdvertisement { get; set; }
        public int serviceID { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public string areacode { get; set; }
        public string country { get; set; }
        public System.DateTime dateLog { get; set; }
    }
}
