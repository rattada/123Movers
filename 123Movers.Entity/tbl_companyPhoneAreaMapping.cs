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
    
    public partial class tbl_companyPhoneAreaMapping
    {
        public int tid { get; set; }
        public int companyID { get; set; }
        public int serviceID { get; set; }
        public short areaCode { get; set; }
        public System.DateTime stampDate { get; set; }
    
        public virtual tbl_companyAreacode tbl_companyAreacode { get; set; }
    }
}
