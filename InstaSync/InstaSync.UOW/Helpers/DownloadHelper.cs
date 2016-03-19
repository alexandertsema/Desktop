using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace InstaSync.UOW.Helpers
{
    public class DownloadHelper
    {
        public static async Task<byte[]> GetUrlContentAsync(string url)
        {
            var content = new MemoryStream();

            var webReq = (HttpWebRequest) WebRequest.Create(url);

            using (WebResponse response = await webReq.GetResponseAsync())
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    if (responseStream != null)
                    {
                        await responseStream.CopyToAsync(content);
                    }
                }
            }

            return content.ToArray();
        }
    }
}
