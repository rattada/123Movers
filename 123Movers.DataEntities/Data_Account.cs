using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _123MoversEntity;

namespace _123Movers.DataEntities
{
    public partial class DataLayer
    {
        public static bool AddExternalUser(string userName)
        {
            
            using (MoversDBEntities db = new MoversDBEntities())
            {
                UserProfile user = db.UserProfiles.FirstOrDefault(u => u.UserName.ToLower() == userName.ToLower());
                if (user == null)
                {
                    db.UserProfiles.AddObject(new UserProfile { UserName = userName });
                    db.SaveChanges();
                    return true;
                }
                else {
                    return false;
                }
                
            }
        }
    }
}