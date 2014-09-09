using _123Movers.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using _123Movers.Entity;
using System.Data.Entity;

namespace _123Movers.DataEntities
{
    public partial class DataLayer
    {
        public static List<List<string>> GetCompanyAreasCodes(int? companyId, int? serviceId)
        {
            using (SqlConnection dbCon = ConnectToDb())
            {
                _cmd = new SqlCommand();
                _cmd.Connection = dbCon;
                _cmd.CommandType = System.Data.CommandType.StoredProcedure;
                _cmd.CommandText = Constants.SP_GET_COMPANY_DEST_AREACODE;

                SqlParameter paramCompanyId = new SqlParameter("companyID", companyId);
                SqlParameter paramService = new SqlParameter("serviceID", serviceId.IfServiceNullLocal());

                _cmd.Parameters.Add(paramCompanyId);
                _cmd.Parameters.Add(paramService);

                DataSet ds = new DataSet();

                SqlDataAdapter da = new SqlDataAdapter(_cmd);
                da.Fill(ds);

                return ConfigValues.DataSetToList(ds);
            }
        }

        public static bool AddCompanyDestAreaCodes(int? companyId, int? serviceId, string areaCodes)
        {
            var _areaCodes = areaCodes.Split(',');
            using (MoversDBEntities db = new MoversDBEntities())
            {
                foreach (string areaCode in _areaCodes)
                {
                    db.CompanyDestinationAreaCodesZipCodes.Add(new tbl_companyDestinationAreaCodesZipCodes {companyID = (int)companyId, serviceID = (int)serviceId, destinationAreaCode = areaCode, destinationZipCode = null, stampDate = DateTime.UtcNow});
                }
                db.SaveChanges();
            }
            return true;
        }

        public static bool DeleteCompanyDestAreaCodes(int? companyId, int? serviceId, string areaCodes)
        {

            var _areaCodes = areaCodes.Split(',');
            using (MoversDBEntities db = new MoversDBEntities())
            {
                foreach (string areaCode in _areaCodes)
                {
                    var _destAreaCode = db.CompanyDestinationAreaCodesZipCodes.FirstOrDefault(da => da.companyID == companyId && da.serviceID == serviceId && da.destinationAreaCode == areaCode && da.destinationZipCode == null);
                    if (_destAreaCode != null)
                    {
                        db.CompanyDestinationAreaCodesZipCodes.Remove(_destAreaCode);
                    }
                }
                db.SaveChanges();
            }
            return true;
        }

        public static bool Turn_ON_OFF_CompanyDestAreaCodes(int? companyId, int? serviceId, string areaCodes)
        {
            var _areaCodes = areaCodes.Split(',');
            using (MoversDBEntities db = new MoversDBEntities())
            {
                var _allAreaCodes = db.CompanyAreacode.Where(a => a.companyID == companyId && a.serviceID == serviceId).ToList();
                foreach (var areacode in _allAreaCodes)
                {
                    areacode.isDestinationAreaCode = 0;
                    db.Entry(areacode).State = EntityState.Modified;
                }
                db.SaveChanges();
                
                foreach (string areaCode in _areaCodes)
                {
                    var _areaCode = _allAreaCodes.First(a => a.areaCode == Convert.ToInt16(areaCode));

                    _areaCode.isDestinationAreaCode = 1;
                    db.Entry(_areaCode).State = EntityState.Modified;
                }
                db.SaveChanges();
            }
            return true;
        }
    }
}