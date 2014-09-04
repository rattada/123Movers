using _123Movers.DataEntities;
using _123Movers.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace _123Movers.BusinessEntities
{
    public partial class BusinessLayer
    {
        public static MoveWeightModel GetMoveWeights(int? companyId, int? serviceId)
        {
            return DataLayer.GetMoveWeights(companyId, serviceId);
        }

        public static List<MoveWeightModel> GetMoveSizeLookup()
        {
            return DataLayer.GetMoveSizeLookup();
        }

        public static bool SaveMoveWeight(MoveWeightModel model)
        {
            return DataLayer.SaveMoveWeight(model);
        }
    }
}