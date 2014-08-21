using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _123Movers.DataEntities;

namespace _123Movers.BusinessEntities
{
    public partial class BusinessLayer
    {
        public static bool AddExternalUser(string userName)
        {
          return DataLayer.AddExternalUser(userName);
        }
    }
}