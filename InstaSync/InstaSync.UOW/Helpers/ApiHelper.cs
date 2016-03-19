using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstaSync.API.Managers;

namespace InstaSync.UOW.Helpers
{
    public class ApiHelper
    {
        public static async Task<Models.Models.User> GetUser(string userName)
        {
            return await new User().GetUser(userName);
        }
    }
}
