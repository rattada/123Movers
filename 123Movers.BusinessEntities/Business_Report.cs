using _123Movers.DataEntities;
using System.Data;

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