using _123Movers.DataEntities;
using _123Movers.Models;

namespace _123Movers.BusinessEntities
{
    public partial class BusinessLayer
    {
        public static bool SaveMoveDistance(MoveDistanceModel model)
        {
            return DataLayer.SaveMoveDistance(model);
        }
        public static MoveDistanceModel GetCompanyMoveDistance(int? companyId, int? serviceId)
        {
            return DataLayer.GetCompanyMoveDistance(companyId, serviceId);
        }
    }
}