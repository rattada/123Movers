using _123Movers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using _123Movers.Entity;
using System.Data.Entity;

namespace _123Movers.DataEntities
{
    public partial class DataLayer
    {
        public static List<MoveWeightModel> GetMoveSizeLookup()
        {
            List<tbl_MoveSizelookup_V2> moveSizeLookups;
            using (var db = new MoversDBEntities())
            {
                moveSizeLookups = db.MoveSizelookup_V2.ToList();
            }

            return moveSizeLookups.Select(m => new MoveWeightModel
                {
                    MoveWeight = m.moveweight,
                    MoveWeightSeq = m.moveWeightSeq
                }).ToList();
        }

        public static MoveWeightModel GetMoveWeights(int? companyId, int? serviceId)
        {
            tbl_companyMoveWeight moveWeight;
            var wModel = new MoveWeightModel();

            using (var db = new MoversDBEntities())
            {
                moveWeight = serviceId == null ? db.CompanyMoveWeight.Where(d => d.companyID == companyId).OrderByDescending(d => d.serviceID).FirstOrDefault() : db.CompanyMoveWeight.Where(d => d.companyID == companyId && d.serviceID == serviceId).FirstOrDefault();
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
            bool result = false;

            using (var db = new MoversDBEntities())
            {
                var moveWeight = db.CompanyMoveWeight.FirstOrDefault(d => d.companyID == model.CompanyId && d.serviceID == model.ServiceId);
                if (moveWeight != null)
                {
                    moveWeight.minMoveWeight = Convert.ToInt32(model.MinMoveWeightSeq);
                    moveWeight.maxMoveWeight = Convert.ToInt32(model.MaxMoveWeightSeq);

                    db.Entry(moveWeight).State = EntityState.Modified;
                }
                else
                {
                    var mw = new tbl_companyMoveWeight
                    {
                        companyID = Convert.ToInt32(model.CompanyId),
                        serviceID = Convert.ToInt32(model.ServiceId),
                        minMoveWeight = Convert.ToInt32(model.MinMoveWeightSeq),
                        maxMoveWeight = Convert.ToInt32(model.MaxMoveWeightSeq),
                        stampDate = DateTime.UtcNow
                    };
                    db.CompanyMoveWeight.Add(mw);
                }

                var areas = db.CompanyAreacode.Where(a => a.companyID == model.CompanyId && a.serviceID == model.ServiceId).ToList();

                if (areas.Count != 0)
                {
                    foreach (var area in areas)
                    {
                        area.isMoveWeightSelect = 1;
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