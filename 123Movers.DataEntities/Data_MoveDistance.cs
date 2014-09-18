using _123Movers.Models;
using System;
using System.Linq;
using _123Movers.Entity;
using System.Data.Entity;

namespace _123Movers.DataEntities
{
    public partial class DataLayer
    {
        public static bool SaveMoveDistance(MoveDistanceModel model)
        {
            var result = false;
            using (var db = new MoversDBEntities())
            {
                var distance = db.CompanyMoveDistance.FirstOrDefault(d => d.companyID == model.CompanyId && d.serviceID == model.ServiceId);
                if (distance != null)
                {
                    distance.minMoveDistance = model.MinMoveDistance;
                    distance.maxMoveDistance = model.MaxMoveDistance;

                    db.Entry(distance).State = EntityState.Modified;
                }
                else
                {
                    var md = new tbl_companyMoveDistance
                    {
                        companyID = Convert.ToInt32(model.CompanyId),
                        serviceID = Convert.ToInt32(model.ServiceId),
                        minMoveDistance = model.MinMoveDistance,
                        maxMoveDistance = model.MaxMoveDistance,
                        stampDate = DateTime.UtcNow
                    };
                    db.CompanyMoveDistance.Add(md);
                }
                var areas = db.CompanyAreacode.Where(a => a.companyID == model.CompanyId && a.serviceID == model.ServiceId).ToList();

                if (areas.Count != 0)
                {
                    foreach (var area in areas)
                    {
                        area.isMoveDistanceSelect = 1;
                        db.Entry(area).State = EntityState.Modified;
                    }
                    db.SaveChanges();
                    result = true;
                }
                return result;
            }
        }


        public static MoveDistanceModel GetCompanyMoveDistance(int? companyId, int? serviceId)
        {
            tbl_companyMoveDistance distance;
            var dModel = new MoveDistanceModel();

            using (var db = new MoversDBEntities())
            {
                distance = serviceId == null ? db.CompanyMoveDistance.Where(d => d.companyID == companyId).OrderByDescending(d => d.serviceID).FirstOrDefault() : db.CompanyMoveDistance.FirstOrDefault(d => d.companyID == companyId && d.serviceID == serviceId);
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