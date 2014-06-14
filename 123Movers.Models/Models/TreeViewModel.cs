using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _123Movers.Models
{
    public class TreeViewModel
    {

        public TreeViewModel()
        {
            ChildNode = new List<TreeViewModel>();
        }

        public string AreaCode { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
        public string NodeName
        {
            get { return GetNodeName(); }
        }
        public IList<TreeViewModel> ChildNode { get; set; }

        public string GetNodeName()
        {
           // string temp = ChildNode.Count > 0 ? "    This employee manages " + ChildNode.Count : "    This employee is working without westing time in managing.";
            return this.AreaCode + "  " + this.State; //+ temp;
        }

    }
}