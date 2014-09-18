using System.Collections.Generic;
using _123Movers.DataEntities;

namespace _123Movers.BusinessEntities
{
    public partial class BusinessLayer
    {
        public static List<List<string>> GetCompanyAreasCodes(int? companyId, int? serviceId)
        {
            return DataLayer.GetCompanyAreasCodes(companyId, serviceId);
        }

        public static bool Turn_ON_OFF_CompanyDestAreaCodes(int? companyId, int? serviceId, string areaCodes)
        {
            return DataLayer.Turn_ON_OFF_CompanyDestAreaCodes(companyId, serviceId, areaCodes);
        }

        public static bool DeleteCompanyDestAreaCodes(int? companyId, int? serviceId, string areaCodes)
        {
            return DataLayer.DeleteCompanyDestAreaCodes(companyId, serviceId, areaCodes);
        }

        public static bool AddCompanyDestAreaCodes(int? companyId, int? serviceId, string areaCodes)
        {
            return DataLayer.AddCompanyDestAreaCodes(companyId, serviceId, areaCodes);
        }

    }
}