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

        public static List<List<string>> GetAvailableAreas(int? companyId, int? serviceId)
        {
            return DataLayer.GetAvailableAreas(companyId, serviceId);
        }
        public static DataTable GetCompanyAreasWithPrices(int? companyId, int? serviceId)
        {
            return DataLayer.GetCompanyAreasWithPrices(companyId, serviceId);
        }
        public static void AddCompanyAdByArea(int? companyId, int? serviceId, int areaCode)
        {
            DataLayer.AddCompanyAdByArea(companyId, serviceId, areaCode);
        }
        public static void DeleteCompanyAdByArea(int? companyId, int? serviceId, int areaCode)
        {
            DataLayer.DeleteCompanyAdByArea(companyId, serviceId, areaCode);
        }

        public static bool AddCompanyPricePerLead(int? companyId, int? serviceId, string areaCodes, int? moveWeightID)
        {
            return DataLayer.AddCompanyPricePerLead(companyId, serviceId, areaCodes, moveWeightID);
        }
    }
}