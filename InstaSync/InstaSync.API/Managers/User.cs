
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstaSharp.Endpoints;
using InstaSharp.Models.Responses;
using InstaSync.API.Configuration;

namespace InstaSync.API.Managers
{
    public class User
    {
        private readonly Users users;

        public User()
        {
            users = new Users(Api.Config, Api.Auth);
        }
        
        public async Task<Models.Models.User> GetUser(string name)
        {
            var result = await users.Search(name, null);
            var userInsta = result.Data.FirstOrDefault();
            var user = new Models.Models.User();
            if (userInsta == null) return user;
            user.Image = userInsta.ProfilePicture;
            user.Id = userInsta.Id.ToString();
            user.UserName = userInsta.Username;
            user.FullName = userInsta.FullName;
            return user;
        }
    }
}
