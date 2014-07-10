using _123Movers.DataEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace _123Movers.BusinessEntities
{
    public partial class BusinessLayer
    {
        public static DataTable GetReports(int? companyId, int? serviceId)
        {
            return DataLayer.GetReports(companyId, serviceId);
        }
    }
}