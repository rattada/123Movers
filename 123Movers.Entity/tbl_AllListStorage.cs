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
    
    public partial class tbl_AllListStorage
    {
        public int listID { get; set; }
        public string listName { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
        public string zip_4 { get; set; }
        public Nullable<System.DateTime> dateEntered { get; set; }
        public Nullable<int> serviceID { get; set; }
        public string phone { get; set; }
        public Nullable<int> isActive { get; set; }
        public string listHandle { get; set; }
        public string uniqueID { get; set; }
        public bool isPub { get; set; }
    }
}
