using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstaSharp;
using InstaSharp.Models;
using InstaSharp.Models.Responses;

namespace InstaSync.API.Configuration
{
    public class Api
    {
        public static OAuthResponse Auth = new OAuthResponse();
        public static InstagramConfig Config = new InstagramConfig();

        static Api()
        {
            Config.ClientId = "8a03f259c13847e7883805602cca6b0e";
            Auth.AccessToken = "3047143526.1fb234f.4df2493fb853495785173e30b6c60970 ";
            Auth.User = new UserInfo { Id = 3047143526 };
        }
    }
}
