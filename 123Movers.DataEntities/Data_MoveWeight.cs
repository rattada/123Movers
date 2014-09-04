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
        //public static DataSet GetMoveWeights(int? companyId, int? serviceId)
        //{
        //    using (SqlConnection dbCon = ConnectToDb())
        //    {
        //        _cmd = new SqlCommand();
        //        _cmd.Connection = dbCon;
        //        _cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //        _cmd.CommandText = Constants.SP_GET_MOVESIZE_LOOKUP;

        //        SqlParameter paramCompanyId = new SqlParameter("companyID", companyId);
        //        SqlParameter paramService = new SqlParameter("serviceID", serviceId);

        //        _cmd.Parameters.Add(paramCompanyId);
        //        _cmd.Parameters.Add(paramService);

        //        SqlDataAdapter da = new SqlDataAdapter(_cmd);

        //        DataSet ds = new DataSet();
        //        da.Fill(ds);

        //        return ds;

        //    }

        //}
        public static List<MoveWeightModel> GetMoveSizeLookup()
        {
            List<MoveWeightModel> _moveSizeModels = new List<MoveWeightModel>();
            List<tbl_MoveSizelookup_V2> _moveSizeLookups;
            using (MoversDBEntities db = new MoversDBEntities())
            {
               _moveSizeLookups = db.tbl_MoveSizelookup_V2.ToList();
            }

            foreach (var m in _moveSizeLookups)
            {
                MoveWeightModel wModel = new MoveWeightModel { 
                    MoveWeight = m.moveweight,
                    MoveWeightSeq = m.moveWeightSeq
                };
                _moveSizeModels.Add(wModel);
            }

            return _moveSizeModels;
        }

        public static MoveWeightModel GetMoveWeights(int? companyId, int? serviceId)
        {
            tbl_companyMoveWeight moveWeight;
            MoveWeightModel wModel = new MoveWeightModel();

            using (MoversDBEntities db = new MoversDBEntities())
            {
                if (serviceId == null)
                {
                    moveWeight = db.tbl_companyMoveWeight.Where(d => d.companyID == companyId).OrderByDescending(d => d.serviceID).FirstOrDefault();
                }
                else
                {
                    moveWeight = db.tbl_companyMoveWeight.Where(d => d.companyID == companyId && d.serviceID == serviceId).FirstOrDefault();
                }
            }
            if (moveWeight != null)
            {
                wModel.ServiceId = moveWeight.serviceID;
                wModel.MinMoveWeightSeq = moveWeight.minMoveWeight;
                wModel.MaxMoveWeightSeq = moveWeight.maxMoveWeight;
            }
            return wModel;
        }

        public static bool SaveMoveWeight(MoveWeightModel model)
        {

            using (MoversDBEntities db = new MoversDBEntities())
            {
                var moveWeight = db.tbl_companyMoveWeight.Where(d => d.companyID == model.CompanyId && d.serviceID == model.ServiceId).FirstOrDefault();
                if (moveWeight != null)
                {
                    moveWeight.minMoveWeight = Convert.ToInt32(model.MinMoveWeightSeq);
                    moveWeight.maxMoveWeight = Convert.ToInt32(model.MaxMoveWeightSeq);

                    db.ObjectStateManager.ChangeObjectState(moveWeight, EntityState.Modified);
                }
                else
                {
                    tbl_companyMoveWeight mw = new tbl_companyMoveWeight
                    {
                        companyID = Convert.ToInt32(model.CompanyId),
                        serviceID = Convert.ToInt32(model.ServiceId),
                        minMoveWeight = Convert.ToInt32(model.MinMoveWeightSeq),
                        maxMoveWeight = Convert.ToInt32(model.MaxMoveWeightSeq)
                    };
                    db.tbl_companyMoveWeight.AddObject(mw);

                }

                var areas = db.tbl_companyAreacode.Where(a => a.companyID == model.CompanyId && a.serviceID == model.ServiceId).ToList();

                foreach (var area in areas)
                {
                    area.isMoveWeightSelect = 1;
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
            //    _cmd.CommandText = Constants.SP_ADD_COMPANY_MOVEWEIGHT;

            //    SqlParameter paramCompanyId = new SqlParameter("companyID", model.CompanyId);
            //    SqlParameter paramService = new SqlParameter("serviceId", model.ServiceId);
            //    SqlParameter parammin = new SqlParameter("minMoveWeight", model.MinMoveWeightSeq);
            //    SqlParameter parammax = new SqlParameter("maxMoveWeight", model.MaxMoveWeightSeq);

            //    _cmd.Parameters.Add(paramCompanyId);
            //    _cmd.Parameters.Add(paramService);
            //    _cmd.Parameters.Add(parammin);
            //    _cmd.Parameters.Add(parammax);


            //    i = _cmd.ExecuteNonQuery();


            //}
            return true;
        }
    }
}