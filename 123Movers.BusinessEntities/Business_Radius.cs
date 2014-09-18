using _123Movers.DataEntities;
using System.Collections.Generic;

namespace _123Movers.BusinessEntities
{
    public partial class BusinessLayer
    {
        public static void AddZipCodesByRadius(int? companyId, int? serviceId, int zipcode, decimal radius, string category)
        {
            DataLayer.AddZipCodesByRadius(companyId,serviceId,zipcode,radius,category);
        }
        public static List<List<string>> GetZipCodesByRadius(int? companyId, int? serviceId, int zipcode, decimal radius, string category)
        {
            return DataLayer.GetZipCodesByRadius(companyId, serviceId, zipcode, radius, category);
        }
    }
}