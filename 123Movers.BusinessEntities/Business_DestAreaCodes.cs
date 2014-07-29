using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _123Movers.DataEntities;

namespace _123Movers.BusinessEntities
{
    public partial class BusinessLayer
    {
        public static List<List<string>> GetCompanyAreasCodes(int? companyId, int? serviceId)
        {
          return  DataLayer.GetCompanyAreasCodes(companyId, serviceId);
        }

        public static bool AddCompanyDestAreaCodes(int? companyId, int? serviceId, string areaCodes)
        {
            return DataLayer.AddCompanyDestAreaCodes(companyId, serviceId, areaCodes);
        }

        public static bool DeleteCompanyDestAreaCodes(int? companyId, int? serviceId, string areaCodes)
        {
            return DataLayer.DeleteCompanyDestAreaCodes(companyId, serviceId, areaCodes);
        }

    }
}