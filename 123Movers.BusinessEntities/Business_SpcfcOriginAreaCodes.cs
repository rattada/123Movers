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
        public static void AddCompanySpcfcOriginAreaCodes(int? companyId, int? serviceId,int spcfcareacode ,string areaCodes)
        {
            DataLayer.AddCompanySpcfcOriginDestAreaCodes(companyId, serviceId, spcfcareacode, areaCodes);
        }
        public static void DeleteCompanySpcfcOriginDestAreaCodes(int? companyId, int? serviceId, int spcfcareacode, string areaCodes)
        {
            DataLayer.DeleteCompanySpcfcOriginDestAreaCodes(companyId, serviceId,spcfcareacode, areaCodes);
        }
        public static List<List<string>> GetAvailSpcfcOriginDestAreas(int? companyId, int? serviceId)
        {
            return DataLayer.GetAvailSpcfcOriginDestAreas(companyId, serviceId);
        }
        public static List<List<string>> GetCompanySpcfcOriginDestAreas(int? companyId, int? serviceId, int spcfcareacode)
        {
            return DataLayer.GetCompanySpcfcOriginDestAreas(companyId, serviceId, spcfcareacode);
        }
    }
}