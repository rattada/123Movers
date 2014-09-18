using _123Movers.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace _123Movers.DataEntities
{
    public partial class DataLayer
    {
        public static bool AddCompanyLeadLimit(LeadLimitModel leadlimit)
        {
            using (SqlConnection dbCon = ConnectToDb())
            {
                _cmd = new SqlCommand
                    {
                        Connection = dbCon,
                        CommandType = CommandType.StoredProcedure,
                        CommandText = Constants.SP_ADD_COMPANY_LEADLIMIT
                    };

                if (leadlimit.DailyLeadLimit != null && leadlimit.DailyLeadLimit != 0)
                {
                    leadlimit.IsDailyLeadLimit = true;
                }
                else
                {
                    leadlimit.IsDailyLeadLimit = false;

                }
                if (leadlimit.MonthlyLeadLimit != null && leadlimit.MonthlyLeadLimit != 0)
                {
                    leadlimit.IsMonthlyLeadLimit = true;
                }
                else
                {
                    leadlimit.IsMonthlyLeadLimit = false;

                }
                if (leadlimit.TotalLeadLimit != null && leadlimit.TotalLeadLimit != 0)
                {
                    leadlimit.IsTotalLeadLimit = true;
                }
                else
                {
                    leadlimit.IsTotalLeadLimit = false;

                }

                _cmd.Parameters.AddWithValue("companyID", leadlimit.CompanyId);
                _cmd.Parameters.AddWithValue("serviceID", leadlimit.ServiceId.ToString().IntNullOrEmpty());
                _cmd.Parameters.AddWithValue("areaCode", leadlimit.AreaCodes.IntNullOrEmpty());
                _cmd.Parameters.AddWithValue("isDailyLeadLimit", leadlimit.IsDailyLeadLimit);
                _cmd.Parameters.AddWithValue("isMonthlyLeadLimit", leadlimit.IsMonthlyLeadLimit);
                _cmd.Parameters.AddWithValue("isTotalLeadLimit", leadlimit.IsTotalLeadLimit);
                _cmd.Parameters.AddWithValue("dailyLeadLimit", leadlimit.DailyLeadLimit);
                _cmd.Parameters.AddWithValue("montlyLeadLimit", leadlimit.MonthlyLeadLimit);
                _cmd.Parameters.AddWithValue("totalLeadLimit", leadlimit.TotalLeadLimit);
                _cmd.Parameters.AddWithValue("leadFrq", leadlimit.LeadFrequency);

               _cmd.ExecuteNonQuery();
            }
            return true;
        }

        public static LeadLimitModel GetCompanyLeadLimit(int? companyId, int? serviceId)
        {
            var ldModel = new LeadLimitModel();

            var leadLimitData = new List<LeadLimitModel>();

            using (var dbCon = ConnectToDb())
            {
                _cmd = new SqlCommand
                    {
                        Connection = dbCon,
                        CommandType = CommandType.StoredProcedure,
                        CommandText = Constants.SP_GET_COMPANY_LEADLIMIT
                    };

                _cmd.Parameters.AddWithValue("companyID", companyId);
                _cmd.Parameters.AddWithValue("serviceID", serviceId);

                var dtResults = new DataTable();

                var drResults = _cmd.ExecuteReader();
                dtResults.Load(drResults);
                var lst = new List<List<string>>();

                if (dtResults.Rows.Count > 0)
                {
                    leadLimitData.AddRange(from DataRow row in dtResults.Rows
                                           select new LeadLimitModel()
                                               {
                                                   AreaCodes = row["areaCode"].ToString().TrimNullOrEmpty(), 
                                                   ServiceId = row["serviceID"].ToString().IntNullOrEmpty(), 
                                                   LeadFrequency = row["leadFrequency"].ToString().IntNullOrEmpty(), 
                                                   IsDailyLeadLimit = row["isDailyLeadLimit"].ToString().BooleanNullOrEmpty(), 
                                                   DailyLeadLimit = row["dailyLeadLimit"].ToString().IntNullOrEmpty(), 
                                                   IsMonthlyLeadLimit = row["isMonthlyLeadLimit"].ToString().BooleanNullOrEmpty(), 
                                                   MonthlyLeadLimit = row["monthlyLeadLimit"].ToString().IntNullOrEmpty(), 
                                                   IsTotalLeadLimit = row["isTotalLeadLimit"].ToString().BooleanNullOrEmpty(), 
                                                   TotalLeadLimit = row["totalLeadLimit"].ToString().IntNullOrEmpty()
                                               });
                    ldModel.LeadLimitData = leadLimitData;
                }
            }
            return ldModel;
        }
    }
}