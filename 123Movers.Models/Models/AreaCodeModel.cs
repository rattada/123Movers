using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _123Movers.Models
{
    public class AreaCodeModel 
    {
        public int? companyID { get; set; }
		public int?	serviceID { get; set; }
		public int?	areaCode { get; set; }
        public int isForceSelect { get; set; }
        public int isDestinationAreaCode { get; set; }
        public int isDestinationZipCode { get; set; }
        public int isMoveDistanceSelect { get; set; }
        public int isMoveWeightSelect { get; set; }
        public int isOriginZipCode { get; set; }
        public int isSpecificOriginDestinationAreacode { get; set; }
        public int isSpecificOriginDestinationState { get; set; }

        public CompanyModel _companyInfo { get; set; }
        public List<AreaCodeModel> _areaCodes { get; set; }
    }
}