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



        public static OriginZipCodeModel GetCompanyServiceAreaCodes(int? companyId, int? serviceId)
        {
            return DataLayer.GetCompanyServiceAreaCodes(companyId, serviceId);
        }
        public static DataTable GetAvailableZipCodes(int? companyId, int? serviceId, int? areaCode)
        {
            return DataLayer.GetAvailableZipCodes(companyId, serviceId, areaCode);
        }
        public static DataTable GetCompanyAreasZipCodes(int? companyId, int? serviceId, int? areaCode)
        {
            return DataLayer.GetCompanyAreasZipCodes(companyId, serviceId, areaCode);
        }
        public static bool AddCompanyAreaZipCodes(int? companyId, int? serviceId, int? areaCode, string zipCodes)
        {
            return DataLayer.AddCompanyAreaZipCodes(companyId, serviceId, areaCode, zipCodes);
        }
        public static bool DeleteCompanyAreaZipCodes(int? companyId, int? serviceId, int? areaCode, string zipCodes)
        {
            return DataLayer.DeleteCompanyAreaZipCodes(companyId, serviceId, areaCode, zipCodes);
        }
        //public static bool AddCompanyZipCodesPerAreaCodes(int? companyId, int serviceId, string areaCodes, int IsOrigin)
        //{
        //    return DataLayer.AddCompanyZipCodesPerAreaCodes(companyId, serviceId, areaCodes, IsOrigin);
        //}
    }
}