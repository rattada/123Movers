using _123Movers.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using _123MoversEntity;

namespace _123Movers.DataEntities
{
    public partial class DataLayer
    {
        public static bool AddCompanyLeadLimit(LeadLimitModel leadlimit)
        {
            int i = 0;
            using (SqlConnection dbCon = ConnectToDb())
            {
                _cmd = new SqlCommand();
                _cmd.Connection = dbCon;
                _cmd.CommandType = System.Data.CommandType.StoredProcedure;
                _cmd.CommandText = Constants.SP_ADD_COMPANY_LEADLIMIT;

          
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
                SqlParameter paramService = new SqlParameter("serviceID", leadlimit.ServiceId.ToString().IntNullOrEmpty());
                SqlParameter paramAreaCode = new SqlParameter("areaCode", leadlimit.AreaCodes.IntNullOrEmpty());
                SqlParameter paramisDailyLeadLimit = new SqlParameter("isDailyLeadLimit", leadlimit.IsDailyLeadLimit);
                SqlParameter paramisMonthlyLeadLimit = new SqlParameter("isMonthlyLeadLimit", leadlimit.IsMonthlyLeadLimit);
                SqlParameter paramisTotalLeadLimit = new SqlParameter("isTotalLeadLimit", leadlimit.IsTotalLeadLimit);
                SqlParameter paramdailyLeadLimit = new SqlParameter("dailyLeadLimit", leadlimit.DailyLeadLimit);
                SqlParameter parammontlyLeadLimit = new SqlParameter("montlyLeadLimit", leadlimit.MonthlyLeadLimit);
                SqlParameter paramtotalLeadLimit = new SqlParameter("totalLeadLimit", leadlimit.TotalLeadLimit);
                SqlParameter paramleadFrq = new SqlParameter("leadFrq", leadlimit.LeadFrequency);


                _cmd.Parameters.Add(paramCompanyId);
                _cmd.Parameters.Add(paramService);
                _cmd.Parameters.Add(paramAreaCode);
                _cmd.Parameters.Add(paramisDailyLeadLimit);
                _cmd.Parameters.Add(paramisMonthlyLeadLimit);
                _cmd.Parameters.Add(paramisTotalLeadLimit);
                _cmd.Parameters.Add(paramdailyLeadLimit);
                _cmd.Parameters.Add(parammontlyLeadLimit);
                _cmd.Parameters.Add(paramtotalLeadLimit);
                _cmd.Parameters.Add(paramleadFrq);


                i = _cmd.ExecuteNonQuery();


            }
            return true;
        }

        public static LeadLimitModel GetCompanyLeadLimit(int? companyId, int? serviceId)
        {

            //List<tbl_companyLeadLimit> leadlimits;
            //using (MoversDBEntities db = new MoversDBEntities())
            //{
            //    if (serviceId == null)
            //    {
            //        leadlimits = db.tbl_companyLeadLimit.Where(l => l.companyID == companyId).ToList();
            //    }
            //    else {
            //        leadlimits = db.tbl_companyLeadLimit.Where(l => l.companyID == companyId && l.serviceID == serviceId).ToList();
            //    }
            //}

            //List<LeadLimitModel> leads = new List<LeadLimitModel>();
            //foreach (var lead in leadlimits)
            //{
            //    LeadLimitModel l = new LeadLimitModel {
            //        AreaCodes = lead.areaCode,
            //        ServiceId = lead.serviceID,
            //        LeadFrequency = lead.leadFrequency,
            //        IsDailyLeadLimit = Convert.ToBoolean(lead.isDailyLeadLimit),
            //        DailyLeadLimit = lead.dailyLeadLimit,
            //        IsMonthlyLeadLimit =Convert.ToBoolean(lead.isMonthlyLeadLimit),
            //        MonthlyLeadLimit = lead.monthlyLeadLimit,
            //        IsTotalLeadLimit = Convert.ToBoolean(lead.isTotalLeadLimit),
            //        TotalLeadLimit = lead.totalLeadLimit
            //    };

            //    leads.Add(l);
            //}



            LeadLimitModel ldModel = new LeadLimitModel();

            List<LeadLimitModel> leadLimitData = new List<LeadLimitModel>();


            using (SqlConnection dbCon = ConnectToDb())
            {
                _cmd = new SqlCommand();
                _cmd.Connection = dbCon;
                _cmd.CommandType = System.Data.CommandType.StoredProcedure;
                _cmd.CommandText = Constants.SP_GET_COMPANY_LEADLIMIT;

                SqlParameter paramCompanyId = new SqlParameter("companyID", companyId);
                SqlParameter paramService = new SqlParameter("serviceID", serviceId);

                _cmd.Parameters.Add(paramCompanyId);
                _cmd.Parameters.Add(paramService);

                DataTable dtResults = new DataTable();

                SqlDataReader drResults = _cmd.ExecuteReader();
                dtResults.Load(drResults);
                List<List<string>> lst = new List<List<string>>();

                if (dtResults.Rows.Count > 0)
                {

                    foreach (DataRow row in dtResults.Rows)
                    {

                        LeadLimitModel obj = new LeadLimitModel()
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