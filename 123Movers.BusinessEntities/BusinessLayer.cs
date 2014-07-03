using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _123Movers.Models;
using _123Movers.DataEntities;
using System.Data;

namespace _123Movers.BusinessEntities
{
    public static class BusinessLayer
    {
        //public static void RegisterUser(RegisterModel model)
        //{
        //    DataLayer.RegisterUser(model);
        //}
        //public static bool Login(LoginModel login)
        //{
        //    return DataLayer.Login(login);
        //}

        public static bool SaveBudget(BudgetModel budget)
        {
            budget.RemainingBudget = budget.TotalBudget;
            return DataLayer.SaveBudget(budget);
        }
        public static IEnumerable<SearchModel> SearchCompany(SearchModel search)
        {
            //if (search.CompanyId)
            //{
            //    search.CompanyId = search.CompanyId.Trim();
            //}
            if (!string.IsNullOrEmpty(search.CompanyName))
            {
                search.CompanyName = search.CompanyName.Trim();
            }

            if (!string.IsNullOrEmpty(search.AX))
            {
                search.AX = search.AX.Trim();
            }
             if (!string.IsNullOrEmpty(search.InsertionOrderId))
            {
                search.InsertionOrderId = search.InsertionOrderId.Trim();
            }
            return DataLayer.SearchCompany(search);
        }

        public static List<BudgetModel> GetBudget(string companyid)
        {

            return DataLayer.GetBudget(companyid);
        }
        public static DataTable GetServies()
        {
            return DataLayer.GetServices();
        }
        public static DataTable GetAvailableAreas(int? companyId, int? serviceId)
        {
            return DataLayer.GetAvailableAreas(companyId, serviceId);
        }
        public static DataTable GetCompanyAdByArea(int? companyId, int? serviceId)
        {
            return DataLayer.GetCompanyAdByArea(companyId, serviceId);
        }
        public static void AddCompanyAdByArea(int? companyId, int? serviceId, int areaCode)
        {
            DataLayer.AddCompanyAdByArea(companyId, serviceId, areaCode);
        }
        public static void DeleteCompanyAdByArea(int? companyId, int? serviceId, int areaCode)
        {
            DataLayer.DeleteCompanyAdByArea(companyId, serviceId, areaCode);
        }
        //public static bool AddCompanyPricePerLead(int? companyId, int? serviceId, int? areaCode, decimal? price, int? moveWeightID)
        //{
        //    return  DataLayer.AddCompanyPricePerLead(companyId, serviceId, areaCode, price, moveWeightID);
        //}

        public static bool AddCompanyPricePerLead(int? companyId, int? serviceId, string areaCodes, int? moveWeightID)
        {
            return DataLayer.AddCompanyPricePerLead(companyId, serviceId, areaCodes, moveWeightID);
        }
        public static bool AddCompanyLeadLimit(LeadLimitModel leadlimit)
        {
            return DataLayer.AddCompanyLeadLimit(leadlimit);
        }

        public static LeadLimitModel GetCompanyLeadLimit(int? companyId, int? serviceId)
        {
            return DataLayer.GetCompanyLeadLimit(companyId, serviceId);
        }
        public static DataTable GetCompanyPricePerLead(int? companyId, int? serviceId)
        {
            return DataLayer.GetCompanyPricePerLead(companyId, serviceId);
        }
        public static bool AddCompanyZipCodesPerAreaCodes(int companyId, int serviceId, string areaCodes, int IsOrigin)
        {
            return DataLayer.AddCompanyZipCodesPerAreaCodes(companyId, serviceId, areaCodes, IsOrigin);
        }
        public static GeographyModel GetCompanyServiceAreaCodes(int? companyId, int? serviceId)
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

        public static bool SaveMoveDistance(DistanceModel model)
        {
            return DataLayer.SaveMoveDistance(model);
        }
        public static DataTable GetCompanyMoveDistance(int? companyId, int? serviceId)
        {
            return DataLayer.GetCompanyMoveDistance(companyId, serviceId);
        }
        public static DataSet GetMoveWeights(int? companyId, int? serviceId)
        {
            return DataLayer.GetMoveWeights(companyId, serviceId);
        }
        public static string ToJSON(this object o)
        {
            var oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return oSerializer.Serialize(o);
        }

        public static bool SaveMoveWeight(MoveWeightModel model)
        {
            return DataLayer.SaveMoveWeight(model);
        }







    }
}
