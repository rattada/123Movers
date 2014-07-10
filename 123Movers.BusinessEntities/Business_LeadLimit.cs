using _123Movers.DataEntities;
using _123Movers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _123Movers.BusinessEntities
{
    public partial class BusinessLayer
    {
        public static bool AddCompanyLeadLimit(LeadLimitModel leadlimit)
        {
            return DataLayer.AddCompanyLeadLimit(leadlimit);
        }

        public static LeadLimitModel GetCompanyLeadLimit(int? companyId, int? serviceId)
        {
            return DataLayer.GetCompanyLeadLimit(companyId, serviceId);
        }
    }
}