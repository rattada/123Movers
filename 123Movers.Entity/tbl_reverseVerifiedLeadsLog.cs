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
    
    public partial class tbl_reverseVerifiedLeadsLog
    {
        public int tid { get; set; }
        public Nullable<int> verID { get; set; }
        public string formType { get; set; }
        public string cityFrom { get; set; }
        public string cityTo { get; set; }
        public string contactPref { get; set; }
        public string currentZip { get; set; }
        public string zipTo { get; set; }
        public string email { get; set; }
        public string firstName { get; set; }
        public string hphone { get; set; }
        public string lastName { get; set; }
        public string moveDate { get; set; }
        public string moveWeight { get; set; }
        public Nullable<int> serviceID { get; set; }
        public string stateFrom { get; set; }
        public string stateTo { get; set; }
        public string wPhone { get; set; }
        public string httpReferer { get; set; }
        public string remoteHost { get; set; }
        public string keyword { get; set; }
        public string se { get; set; }
        public Nullable<int> postRequestID { get; set; }
        public Nullable<decimal> price { get; set; }
        public int isInValidEmail { get; set; }
        public int isInValidPhone { get; set; }
        public string verificationReason { get; set; }
        public Nullable<int> isVerifiedByPhone { get; set; }
        public Nullable<int> isVerifiedByEmail { get; set; }
        public string notes { get; set; }
        public Nullable<System.DateTime> dateSubmitted { get; set; }
        public Nullable<int> isInValid { get; set; }
        public string loggedBy { get; set; }
        public System.DateTime logDate { get; set; }
    }
}
