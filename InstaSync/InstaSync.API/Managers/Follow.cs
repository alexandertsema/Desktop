using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstaSharp.Endpoints;
using InstaSync.API.Configuration;

namespace InstaSync.API.Managers
{
    public class Follow
    {
        readonly Relationships relationships;
        public Follow()
        {
            relationships = new Relationships(Api.Config, Api.Auth);
        }
        public async void RequestFollow(long userId)
        {
            await relationships.Relationship((int)userId, Relationships.Action.Follow);
        }
    }
}
