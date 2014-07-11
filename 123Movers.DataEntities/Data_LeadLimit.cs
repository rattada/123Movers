using _123Movers.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace _123Movers.DataEntities
{
    public partial class DataLayer
    {
        public static bool AddCompanyLeadLimit(LeadLimitModel leadlimit)
        {
            int i = 0;
            using (SqlConnection dbCon = ConnectToDb())
            {
                SqlCommand cmdAddCompanyAdByArea = new SqlCommand();
                cmdAddCompanyAdByArea.Connection = dbCon;
                cmdAddCompanyAdByArea.CommandType = System.Data.CommandType.StoredProcedure;
                cmdAddCompanyAdByArea.CommandText = "usp_AddCompanyLeadLimit";

                int? serviceId = null;
                int? areaCode = null;

                if (leadlimit.ServiceId != null)
                {
                    serviceId = Convert.ToInt32(leadlimit.ServiceId);
                }

                if (!string.IsNullOrWhiteSpace(leadlimit.AreaCodes))
                {
                    areaCode = Convert.ToInt32(leadlimit.AreaCodes);
                }
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

                SqlParameter paramCompanyId = new SqlParameter("companyID", leadlimit.CompanyId);
                SqlParameter paramService = new SqlParameter("serviceID", serviceId);
                SqlParameter paramAreaCode = new SqlParameter("areaCode", areaCode);
                //SqlParameter paramMoveWeight = new SqlParameter("moveWeightID", leadlimit.MoveWeightID);
                SqlParameter paramisDailyLeadLimit = new SqlParameter("isDailyLeadLimit", leadlimit.IsDailyLeadLimit);
                SqlParameter paramisMonthlyLeadLimit = new SqlParameter("isMonthlyLeadLimit", leadlimit.IsMonthlyLeadLimit);
                SqlParameter paramisTotalLeadLimit = new SqlParameter("isTotalLeadLimit", leadlimit.IsTotalLeadLimit);
                SqlParameter paramdailyLeadLimit = new SqlParameter("dailyLeadLimit", leadlimit.DailyLeadLimit);
                SqlParameter parammontlyLeadLimit = new SqlParameter("montlyLeadLimit", leadlimit.MonthlyLeadLimit);
                SqlParameter paramtotalLeadLimit = new SqlParameter("totalLeadLimit", leadlimit.TotalLeadLimit);
                // SqlParameter paramprice = new SqlParameter("price", leadlimit.Price);
                SqlParameter paramleadFrq = new SqlParameter("leadFrq", leadlimit.LeadFrequency);


                cmdAddCompanyAdByArea.Parameters.Add(paramCompanyId);
                cmdAddCompanyAdByArea.Parameters.Add(paramService);
                cmdAddCompanyAdByArea.Parameters.Add(paramAreaCode);
                // cmdAddCompanyAdByArea.Parameters.Add(paramMoveWeight);
                cmdAddCompanyAdByArea.Parameters.Add(paramisDailyLeadLimit);
                cmdAddCompanyAdByArea.Parameters.Add(paramisMonthlyLeadLimit);
                cmdAddCompanyAdByArea.Parameters.Add(paramisTotalLeadLimit);
                cmdAddCompanyAdByArea.Parameters.Add(paramdailyLeadLimit);
                cmdAddCompanyAdByArea.Parameters.Add(parammontlyLeadLimit);
                cmdAddCompanyAdByArea.Parameters.Add(paramtotalLeadLimit);
                //cmdAddCompanyAdByArea.Parameters.Add(paramprice);
                cmdAddCompanyAdByArea.Parameters.Add(paramleadFrq);


                i = cmdAddCompanyAdByArea.ExecuteNonQuery();


            }
            return true;
        }

        public static LeadLimitModel GetCompanyLeadLimit(int? companyId, int? serviceId)
        {
            LeadLimitModel ldModel = new LeadLimitModel();

            List<LeadLimitModel> leadLimitData = new List<LeadLimitModel>();


            using (SqlConnection dbCon = ConnectToDb())
            {
                SqlCommand cmdGetCompanyLeadLimit = new SqlCommand();
                cmdGetCompanyLeadLimit.Connection = dbCon;
                cmdGetCompanyLeadLimit.CommandType = System.Data.CommandType.StoredProcedure;
                cmdGetCompanyLeadLimit.CommandText = "usp_GetCompanyLeadLimit";

                SqlParameter paramCompanyId = new SqlParameter("companyID", companyId);
                SqlParameter paramService = new SqlParameter("serviceID", serviceId);

                cmdGetCompanyLeadLimit.Parameters.Add(paramCompanyId);
                cmdGetCompanyLeadLimit.Parameters.Add(paramService);

                DataTable dtResults = new DataTable();

                SqlDataReader drResults = cmdGetCompanyLeadLimit.ExecuteReader();
                dtResults.Load(drResults);

                if (dtResults.Rows.Count > 0)
                {

                    var Areacode = string.Empty;
                    var ServiceId = string.Empty;
                    foreach (DataRow row in dtResults.Rows)
                    {
                        if (String.IsNullOrEmpty(row["areaCode"].ToString()))
                        {
                            Areacode = null;

                        }
                        else
                        {
                            Areacode = row["areaCode"].ToString();
                        }
                        if (String.IsNullOrEmpty(row["serviceID"].ToString()))
                        {
                            ServiceId = null;

                        }
                        else
                        {
                            ServiceId = row["serviceID"].ToString();
                        }


                        LeadLimitModel obj = new LeadLimitModel()
                        {

                            AreaCodes = Areacode,
                            ServiceId = Convert.ToInt32(ServiceId),
                            LeadFrequency = Convert.ToInt32(row["leadFrequency"].ToString()),
                            IsDailyLeadLimit = Convert.ToBoolean(row["isDailyLeadLimit"]),
                            DailyLeadLimit = Convert.ToInt32(row["dailyLeadLimit"].ToString()),
                            IsMonthlyLeadLimit = Convert.ToBoolean(row["isMonthlyLeadLimit"]),
                            MonthlyLeadLimit = Convert.ToInt32(row["monthlyLeadLimit"].ToString()),
                            IsTotalLeadLimit = Convert.ToBoolean(row["isTotalLeadLimit"]),
                            TotalLeadLimit = Convert.ToInt32(row["totalLeadLimit"].ToString())



                        };
                        leadLimitData.Add(obj);

                    }
                    ldModel.LeadLimitData = leadLimitData;
                }


            }
            return ldModel;
        }
    }
}