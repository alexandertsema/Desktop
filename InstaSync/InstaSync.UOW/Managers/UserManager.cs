using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstaSync.Models.Models;
using InstaSync.UOW.Helpers;

namespace InstaSync.UOW.Managers
{
    public class UserManager
    {
        public static void SaveUser(User settings)
        {
            XmlHelper.SaveXml(settings);
        }

        public static User GetUser()
        {
            return (User)XmlHelper.GetModel("User.xml", new User());
        }

        public static async Task<User> GetNewUser(string userName)
        {
            return await ApiHelper.GetUser(userName);
        }
    }
}
