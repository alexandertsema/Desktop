using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InstaSharp.Endpoints;
using InstaSharp.Models.Responses;
using InstaSync.API.Configuration;
using InstaSync.Models.Enums;
using InstaSync.Models.Models;

namespace InstaSync.API.Managers
{
    public class Content
    {
        private readonly Users users;
        
        public Content()
        {
            users = new Users(Api.Config, Api.Auth);
        }

        public async Task<InstaSharp.Models.User> GetUser(string name)
        {
            var result = await users.Search(name, null);
            return result.Data.FirstOrDefault();
        }

        public async Task<List<ContentItem>> GetContent(string name)
        {
            var user = await GetUser(name);
            
            var response = await users.Recent(user.Id.ToString());
            var imagesList = new List<ContentItem>();

            if (response == null) return imagesList;

            foreach (var item in response.Data)
            {
                var url = String.Empty;
                var type = ContentType.image;

                if (item.Type == ContentType.image.ToString())
                {
                    url = item.Images.StandardResolution.Url;
                }
                else if (item.Type == ContentType.video.ToString())
                {
                    url = item.Videos.StandardResolution.Url;
                    type = ContentType.video;
                }
                imagesList.Add(new ContentItem()
                {
                    Id = item.Id,
                    Url = url,
                    Type = type
                });
            }

            while (response?.Pagination?.NextMaxId != null)
            {
                response = await users.Recent(user.Id.ToString(), response.Pagination.NextMaxId, response.Pagination.NextMinId, null, null, null);

                if (response?.Data == null) continue;

                foreach (var item in response.Data)
                {
                    var url = String.Empty;
                    var type = ContentType.image;

                    if (item.Type == ContentType.image.ToString())
                    {
                        url = item.Images.StandardResolution.Url;
                    }
                    else if (item.Type == ContentType.video.ToString())
                    {
                        url = item.Videos.StandardResolution.Url;
                        type = ContentType.video;
                    }
                    imagesList.Add(new ContentItem()
                    {
                        Id = item.Id,
                        Url = url,
                        Type = type
                    });
                }
            }
            return imagesList;
        }

        public async Task<MediasResponse> GetNextGallaryByUserId(long id, string nextMaxId, string nextMinId)
        {
            var gallary = await users.Recent(id.ToString(), nextMaxId, nextMinId, null, null, null);
            return gallary;
        }
    }
}
