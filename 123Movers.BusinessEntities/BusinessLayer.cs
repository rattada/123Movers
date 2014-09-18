using System.Collections.Generic;
using _123Movers.Models;
using _123Movers.DataEntities;

namespace _123Movers.BusinessEntities
{
    public partial class BusinessLayer
    {
        public static CompanyModel GetCompany(int? companyId)
        {
           return DataLayer.GetCompany(companyId);
        }
        public static IEnumerable<SearchModel> SearchCompany(SearchModel search)
        {
            return DataLayer.SearchCompany(search);
        }
    }
}
