using _123Movers.DataEntities;
using _123Movers.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace _123Movers.BusinessEntities
{
    public partial class BusinessLayer
    {
        public static bool AddCompanyAreaDestinationZipCodes(int? companyId, int? serviceId, int? areaCode, string zipCodes)
        {
            return DataLayer.AddCompanyAreaDestinationZipCodes(companyId, serviceId, areaCode, zipCodes);
        }
        public static bool DeleteCompanyAreaDestinationZipCodes(int? companyId, int? serviceId, int? areaCode, string zipCodes)
        {
            return DataLayer.DeleteCompanyAreaDestinationZipCodes(companyId, serviceId, areaCode, zipCodes);
        }

        public static List<List<string>> GetAvailableDestinationZipCodes(int? companyId, int? serviceId, int? areaCode, int? destAreaCode = null)
        {
            return DataLayer.GetAvailableDestinationZipCodes(companyId, serviceId, areaCode, destAreaCode);
        }

        public static DataTable GetCompanyAreasDestinationZipCodes(int? companyId, int? serviceId, int? areaCode)
        {
            return DataLayer.GetCompanyAreasDestinationZipCodes(companyId, serviceId, areaCode);
        }

        public static DestinationZipModel GetCompanyDestinationServiceAreaCodes(int? companyId, int? serviceId)
        {
            return DataLayer.GetCompanyDestinationServiceAreaCodes(companyId, serviceId);
        }
    }
}