using _123Movers.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using _123Movers.Entity;
using System.Data.Entity;

namespace _123Movers.DataEntities
{
    public partial class DataLayer
    {
        public static List<List<string>> GetCompanyAreasCodes(int? companyId, int? serviceId)
        {
            using (var dbCon = ConnectToDb())
            {
                _cmd = new SqlCommand
                    {
                        Connection = dbCon,
                        CommandType = CommandType.StoredProcedure,
                        CommandText = Constants.SP_GET_COMPANY_DEST_AREACODE
                    };

                _cmd.Parameters.AddWithValue("companyID", companyId);
                _cmd.Parameters.AddWithValue("serviceID", serviceId.IfServiceNullLocal());

                var ds = new DataSet();

                var da = new SqlDataAdapter(_cmd);
                da.Fill(ds);

                return ConfigValues.DataSetToList(ds);
            }
        }

        public static bool AddCompanyDestAreaCodes(int? companyId, int? serviceId, string areaCodes)
        {
            var dAreaCodes = areaCodes.Split(',');
            using (var db = new MoversDBEntities())
            {
                foreach (var areaCode in dAreaCodes)
                {
                    db.CompanyDestinationAreaCodesZipCodes.Add(new tbl_companyDestinationAreaCodesZipCodes {companyID = (int)companyId, serviceID = (int)serviceId, destinationAreaCode = areaCode, destinationZipCode = null, stampDate = DateTime.UtcNow});
                }
                db.SaveChanges();
            }
            return true;
        }

        public static bool DeleteCompanyDestAreaCodes(int? companyId, int? serviceId, string areaCodes)
        {

            var dAeaCodes = areaCodes.Split(',');
            using (var db = new MoversDBEntities())
            {
                foreach (var areaCode in dAeaCodes)
                {
                    var destAreaCode = db.CompanyDestinationAreaCodesZipCodes.FirstOrDefault(da => da.companyID == companyId && da.serviceID == serviceId && da.destinationAreaCode == areaCode && da.destinationZipCode == null);
                    if (destAreaCode != null)
                    {
                        db.CompanyDestinationAreaCodesZipCodes.Remove(destAreaCode);
                    }
                }
                db.SaveChanges();
            }
            return true;
        }

        public static bool Turn_ON_OFF_CompanyDestAreaCodes(int? companyId, int? serviceId, string areaCodes)
        {
            var dAreaCodes = areaCodes.Split(',');
            using (var db = new MoversDBEntities())
            {
                var allAreaCodes = db.CompanyAreacode.Where(a => a.companyID == companyId && a.serviceID == serviceId).ToList();
                foreach (var areacode in allAreaCodes)
                {
                    areacode.isDestinationAreaCode = 0;
                    db.Entry(areacode).State = EntityState.Modified;
                }
                db.SaveChanges();
                
                foreach (var dAreaCode in dAreaCodes.Select(areaCode => allAreaCodes.First(a => a.areaCode == Convert.ToInt16(areaCode))))
                {
                    dAreaCode.isDestinationAreaCode = 1;
                    db.Entry(dAreaCode).State = EntityState.Modified;
                }
                db.SaveChanges();
            }
            return true;
        }
    }
}