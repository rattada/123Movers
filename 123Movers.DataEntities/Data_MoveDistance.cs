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
        public static bool SaveMoveDistance(MoveDistanceModel model)
        {
            using (MoversDBEntities db = new MoversDBEntities())
            {
                var distance = db.tbl_companyMoveDistance.Where(d => d.companyID == model.CompanyId && d.serviceID == model.ServiceId).FirstOrDefault();
                if (distance != null)
                {
                    distance.minMoveDistance = model.MinMoveDistance;
                    distance.maxMoveDistance = model.MaxMoveDistance;

                    db.ObjectStateManager.ChangeObjectState(distance, EntityState.Modified);
                }
                else
                {
                    tbl_companyMoveDistance d = new tbl_companyMoveDistance { 
                        companyID = Convert.ToInt32(model.CompanyId),
                        serviceID = Convert.ToInt32(model.ServiceId),
                        minMoveDistance = model.MinMoveDistance,
                        maxMoveDistance = model.MaxMoveDistance
                    };
                    db.tbl_companyMoveDistance.AddObject(d);

                }

                var areas = db.tbl_companyAreacode.Where(a => a.companyID == model.CompanyId && a.serviceID == model.ServiceId).ToList();

                foreach (var area in areas)
                {
                    area.isMoveDistanceSelect = 1;
                    db.ObjectStateManager.ChangeObjectState(area, EntityState.Modified);
                }
                db.SaveChanges();
            }

            //int i = 0;
            //using (SqlConnection dbCon = ConnectToDb())
            //{
            //    _cmd = new SqlCommand();
            //    _cmd.Connection = dbCon;
            //    _cmd.CommandType = System.Data.CommandType.StoredProcedure;
            //    _cmd.CommandText = Constants.SP_ADD_COMPANY_MOVEDISTANCE;



            //    SqlParameter paramCompanyId = new SqlParameter("companyID", model.CompanyId);
            //    SqlParameter paramServiceID = new SqlParameter("serviceID", model.ServiceId);
            //    SqlParameter paramMinWeight = new SqlParameter("minMoveWeight", model.MinMoveDistance);
            //    SqlParameter paramMaxWeight = new SqlParameter("maxMoveWeight", model.MaxMoveDistance);


            //    _cmd.Parameters.Add(paramCompanyId);
            //    _cmd.Parameters.Add(paramServiceID);
            //    _cmd.Parameters.Add(paramMinWeight);
            //    _cmd.Parameters.Add(paramMaxWeight);



            //    i = _cmd.ExecuteNonQuery();


            //}
            return true;
        }


        //public static DataTable GetCompanyMoveDistance1(int? companyId, int? serviceId)
        //{
        //    using (SqlConnection dbCon = ConnectToDb())
        //    {
        //        _cmd = new SqlCommand();
        //        _cmd.Connection = dbCon;
        //        _cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //        _cmd.CommandText = Constants.SP_GET_COMPANY_MOVEDISTANCE;

        //        SqlParameter paramCompanyId = new SqlParameter("companyID", companyId);
        //        SqlParameter paramService = new SqlParameter("serviceID", serviceId);

        //        _cmd.Parameters.Add(paramCompanyId);
        //        _cmd.Parameters.Add(paramService);

        //        DataTable dtResults = new DataTable();

        //        SqlDataReader drResults = _cmd.ExecuteReader();
        //        dtResults.Load(drResults);

        //        return dtResults;

        //    }
        //}
        public static MoveDistanceModel GetCompanyMoveDistance(int? companyId, int? serviceId)
        {

            tbl_companyMoveDistance distance;
            MoveDistanceModel dModel = new MoveDistanceModel();

            using (MoversDBEntities db = new MoversDBEntities())
            {
                if (serviceId == null)
                {
                    distance = db.tbl_companyMoveDistance.Where(d => d.companyID == companyId).OrderByDescending(d => d.serviceID).FirstOrDefault();
                }
                else
                {
                    distance = db.tbl_companyMoveDistance.Where(d => d.companyID == companyId && d.serviceID == serviceId).FirstOrDefault();
                }
            }
            if (distance != null)
            {
                dModel.ServiceId = distance.serviceID;
                dModel.MinMoveDistance = distance.minMoveDistance;
                dModel.MaxMoveDistance = distance.maxMoveDistance;
            }
            return dModel;

        }
    }
}