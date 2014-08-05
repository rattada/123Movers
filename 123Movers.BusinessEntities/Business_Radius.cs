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
        public static bool AddZipCodesByRadius(int? companyId, int? serviceId, int zipcode, decimal radius, string category, string type)
        {
            return DataLayer.AddZipCodesByRadius(companyId,serviceId,zipcode,radius,category,type);
        }
        public static List<List<string>> GetZipCodesByRadius(int? companyId, int? serviceId, int zipcode, decimal radius, string category)
        {
            return DataLayer.GetZipCodesByRadius(companyId, serviceId, zipcode, radius, category);
        }

    }
}