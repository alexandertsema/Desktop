using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using InstaSync.API.Managers;
using InstaSync.Models.Enums;
using InstaSync.Models.Models;
using InstaSync.UOW.Helpers;

namespace InstaSync.UOW.Managers
{
    public class SynchronizationManager
    {
        #region public methods

        public async static Task<List<ContentItem>> IndexRemoteRepository(string userName)
        {
            return await new Content().GetContent(userName);
        }
        
        public static List<String> IndexLocalRepository(string path)
        {
            return DirectoryHelper.GetFiles(path);
        }

        public static async Task<bool> SaveFiles(List<ContentItem> remoteRepository, List<String> localRepository, string path)
        {
            GetNewRange(ref remoteRepository, localRepository);

            DirectoryHelper.CreateDirectory(path);

            foreach (var item in remoteRepository)
            {
                FileHelper.SaveFile(path, item.Id, item.Type == ContentType.image ? FileExtension.jpg : FileExtension.mp4, await DownloadHelper.GetUrlContentAsync(item.Url));
            }
            return true;
        }
        
        #endregion public methods
        
        #region private methods

        private static void GetNewRange(ref List<ContentItem> remoteRepository, List<String> localRepository)
        {
            var ids = GetIds(localRepository);

            foreach (
                var remoteItem in
                    from remoteItem in remoteRepository
                    from id in ids.Where(id => remoteItem.Id == id)
                    select remoteItem)
            {
                remoteItem.Redundand = true;
            }

            remoteRepository.RemoveAll(x => x.Redundand);
        }

        private static List<String> GetIds(List<String> localRepository)
        {
            return ImageHelper.GetImageIds(FileHelper.GetFilesNames(localRepository));
        }

        #endregion private methods
    }
}
