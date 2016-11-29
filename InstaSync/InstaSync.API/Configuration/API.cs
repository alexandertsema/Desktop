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
            //Config.ClientId = "a34ef1b512094db788de0b2ccab900ac";
            //Auth.AccessToken = "2097338967.1fb234f.7f6a2393717c4b419d685b905d801447";
            //Auth.User = new UserInfo { Id = 2097338967 };
            //Config.ClientId = "8a03f259c13847e7883805602cca6b0e";
            //Auth.AccessToken = "3047143526.1fb234f.4df2493fb853495785173e30b6c60970 ";
            //Auth.User = new UserInfo { Id = 3047143526 };
            
            ///"username": "instasynsapp", pass: 13june13, e-mail:  tsemalex@gmail.com
            Config.ClientId = "d23af31957f4425bb596949d838f9c53";
            Auth.AccessToken = "3612683455.d23af31.983d947054b4456ebfa6bc36330c4657";
            Auth.User = new UserInfo { Id = 3612683455 };
        }
    }
}
