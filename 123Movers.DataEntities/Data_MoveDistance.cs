using _123Movers.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using _123Movers.DataEntities;
using _123Movers.Entity;
using System.Data.Entity;

namespace _123Movers.DataEntities
{
    public partial class DataLayer
    {
        //public static bool SaveMoveDistance(MoveDistanceModel model)
        //{
        //    using (MoversDBEntities db = new MoversDBEntities())
        //    {
        //        var distance = db.tbl_companyMoveDistance.Where(d => d.companyID == model.CompanyId && d.serviceID == model.ServiceId).FirstOrDefault();
        //        if (distance != null)
        //        {
        //            distance.minMoveDistance = model.MinMoveDistance;
        //            distance.maxMoveDistance = model.MaxMoveDistance;

        //            db.ObjectStateManager.ChangeObjectState(distance, EntityState.Modified);
        //        }
        //        else
        //        {
        //            tbl_companyMoveDistance d = new tbl_companyMoveDistance { 
        //                companyID = Convert.ToInt32(model.CompanyId),
        //                serviceID = Convert.ToInt32(model.ServiceId),
        //                minMoveDistance = model.MinMoveDistance,
        //                maxMoveDistance = model.MaxMoveDistance,
        //                stampDate = DateTime.UtcNow
        //            };
        //            db.tbl_companyMoveDistance.AddObject(d);

        //        }

        //        var areas = db.tbl_companyAreacode.Where(a => a.companyID == model.CompanyId && a.serviceID == model.ServiceId).ToList();

        //        foreach (var area in areas)
        //        {
        //            area.isMoveDistanceSelect = 1;
        //            db.ObjectStateManager.ChangeObjectState(area, EntityState.Modified);
        //        }
        //        db.SaveChanges();
        //    }

        //    return true;
        //}

        public static bool SaveMoveDistance(MoveDistanceModel model)
        {

            bool result = false;
            using (MoversDBEntities db = new MoversDBEntities())
            {
                var distance = db.tbl_companyMoveDistance.Where(d => d.companyID == model.CompanyId && d.serviceID == model.ServiceId).FirstOrDefault();
                if (distance != null)
                {
                    distance.minMoveDistance = model.MinMoveDistance;
                    distance.maxMoveDistance = model.MaxMoveDistance;

                    db.Entry(distance).State = EntityState.Modified;
                }
                else
                {
                    tbl_companyMoveDistance d = new tbl_companyMoveDistance
                    {
                        companyID = Convert.ToInt32(model.CompanyId),
                        serviceID = Convert.ToInt32(model.ServiceId),
                        minMoveDistance = model.MinMoveDistance,
                        maxMoveDistance = model.MaxMoveDistance,
                        stampDate = DateTime.UtcNow
                    };
                    db.tbl_companyMoveDistance.Add(d);

                }

                var areas = db.tbl_companyAreacode.Where(a => a.companyID == model.CompanyId && a.serviceID == model.ServiceId).ToList();

                if (areas.Count != 0 && areas != null)
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