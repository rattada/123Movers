using System.Collections.Generic;

namespace _123Movers.Models
{
    public class AreaCodeModel 
    {
        public int? CompanyId { get; set; }
		public int?	ServiceId { get; set; }
		public int?	AreaCode { get; set; }
        public int IsForceSelect { get; set; }
        public int IsDestinationAreaCode { get; set; }
        public int IsDestinationZipCode { get; set; }
        public int IsMoveDistanceSelect { get; set; }
        public int IsMoveWeightSelect { get; set; }
        public int IsOriginZipCode { get; set; }
        public int IsSpecificOriginDestinationAreacode { get; set; }
        public int IsSpecificOriginDestinationState { get; set; }

        public CompanyModel CompanyInfo { get; set; }
        public List<AreaCodeModel> AreaCodes { get; set; }
    }
}