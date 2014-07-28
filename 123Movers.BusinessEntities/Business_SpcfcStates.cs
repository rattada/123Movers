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
        public static void AddCompanySpcfcStates(int? companyId, int? serviceId, string originState, string destSates)
        {
            DataLayer.AddCompanySpcfcStates(companyId, serviceId, originState, destSates);
        }
        public static void DeleteCompanySpcfcStates(int? companyId, int? serviceId, string originState, string destSates)
        {
            DataLayer.DeleteCompanySpcfcStates(companyId, serviceId, originState, destSates);
        }
        public static DataTable GetAvailStates(int? companyId, int? serviceId)
        {
            return DataLayer.GetAvailStates(companyId, serviceId);
        }
        public static List<List<string>> GetCompanySpcfcStates(int? companyId, int? serviceId, string originState, bool destSates)
        {
            return DataLayer.GetCompanySpcfcStates(companyId, serviceId, originState, destSates);
        }
    }
}