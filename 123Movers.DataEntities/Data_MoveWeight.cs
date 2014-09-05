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
                MoveWeightModel wModel = new MoveWeightModel
                {
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

        //public static bool SaveMoveWeight(MoveWeightModel model)
        //{

        //    using (MoversDBEntities db = new MoversDBEntities())
        //    {
        //        var moveWeight = db.tbl_companyMoveWeight.Where(d => d.companyID == model.CompanyId && d.serviceID == model.ServiceId).FirstOrDefault();
        //        if (moveWeight != null)
        //        {
        //            moveWeight.minMoveWeight = Convert.ToInt32(model.MinMoveWeightSeq);
        //            moveWeight.maxMoveWeight = Convert.ToInt32(model.MaxMoveWeightSeq);

        //            db.ObjectStateManager.ChangeObjectState(moveWeight, EntityState.Modified);
        //        }
        //        else
        //        {
        //            tbl_companyMoveWeight mw = new tbl_companyMoveWeight
        //            {
        //                companyID = Convert.ToInt32(model.CompanyId),
        //                serviceID = Convert.ToInt32(model.ServiceId),
        //                minMoveWeight = Convert.ToInt32(model.MinMoveWeightSeq),
        //                maxMoveWeight = Convert.ToInt32(model.MaxMoveWeightSeq),
        //                stampDate = DateTime.UtcNow
        //            };
        //            db.tbl_companyMoveWeight.AddObject(mw);

        //        }

        //        var areas = db.tbl_companyAreacode.Where(a => a.companyID == model.CompanyId && a.serviceID == model.ServiceId).ToList();

        //        foreach (var area in areas)
        //        {
        //            area.isMoveWeightSelect = 1;
        //            db.ObjectStateManager.ChangeObjectState(area, EntityState.Modified);
        //        }
        //        db.SaveChanges();
        //    }
        //    return true;
        //}

        public static bool SaveMoveWeight(MoveWeightModel model)
        {
            bool result = false;

            using (MoversDBEntities db = new MoversDBEntities())
            {
                var moveWeight =  db.tbl_companyMoveWeight.Where(d => d.companyID == model.CompanyId && d.serviceID == model.ServiceId).FirstOrDefault();
                if (moveWeight != null)
                {
                    moveWeight.minMoveWeight = Convert.ToInt32(model.MinMoveWeightSeq);
                    moveWeight.maxMoveWeight = Convert.ToInt32(model.MaxMoveWeightSeq);

                    db.Entry(moveWeight).State = EntityState.Modified;
                }
                else
                {
                    tbl_companyMoveWeight mw = new tbl_companyMoveWeight
                    {
                        companyID = Convert.ToInt32(model.CompanyId),
                        serviceID = Convert.ToInt32(model.ServiceId),
                        minMoveWeight = Convert.ToInt32(model.MinMoveWeightSeq),
                        maxMoveWeight = Convert.ToInt32(model.MaxMoveWeightSeq),
                        stampDate = DateTime.UtcNow
                    };
                    db.tbl_companyMoveWeight.Add(mw);

                }

                var areas = db.tbl_companyAreacode.Where(a => a.companyID == model.CompanyId && a.serviceID == model.ServiceId).ToList();

                if (areas.Count != 0 && areas != null)
                {
                    foreach (var area in areas)
                    {
                        area.isMoveWeightSelect = 1;
                        //db.ObjectStateManager.ChangeObjectState(area, EntityState.Modified);
                        db.Entry(area).State = EntityState.Modified;
                    }
                    db.SaveChanges();
                    result = true;
                }
            }
            return result;
        }

    }
}