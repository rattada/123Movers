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
    
    public partial class tbl_reviewsReply
    {
        public int replyID { get; set; }
        public int reviewID { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string subject { get; set; }
        public string reply { get; set; }
        public System.DateTime replyDate { get; set; }
        public bool isActive { get; set; }
        public bool isPub { get; set; }
        public string IP { get; set; }
        public System.DateTime stampDate { get; set; }
    }
}
