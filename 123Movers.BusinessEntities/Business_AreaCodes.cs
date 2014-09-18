using _123Movers.DataEntities;
using System.Collections.Generic;
using System.Data;

namespace _123Movers.BusinessEntities
{
    public partial class BusinessLayer
    {

        public static List<List<string>> GetAvailableAreas(int? companyId, int? serviceId)
        {
            return DataLayer.GetAvailableAreas(companyId, serviceId);
        }
        public static DataTable GetCompanyAreasWithPrices(int? companyId, int? serviceId)
        {
            return DataLayer.GetCompanyAreasWithPrices(companyId, serviceId);
        }
        public static void AddCompanyAreaCodes(int? companyId, int? serviceId, string areaCodes)
        {
            DataLayer.AddCompanyAreaCodes(companyId, serviceId, areaCodes);
        }
        public static void DeleteCompanyAreaCodes(int? companyId, int? serviceId, string areaCodes)
        {
            DataLayer.DeleteCompanyAreaCodes(companyId, serviceId, areaCodes);
        }

        public static bool AddCompanyPricePerLead(int? companyId, int? serviceId, string areaCodes, int? moveWeightId)
        {
            return DataLayer.AddCompanyPricePerLead(companyId, serviceId, areaCodes, moveWeightId);
        }
    }
}