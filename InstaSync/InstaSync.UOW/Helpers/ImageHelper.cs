using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Imaging;

namespace InstaSync.UOW.Helpers
{
    public class ImageHelper
    {
        public static BitmapImage GetImage(string url)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(url);
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.EndInit();

            return bitmap;
        }

        public static List<String> GetImageIds(List<String> fileNames)
        {
            return fileNames.Select(item => item.Split('.')).Select(arr => arr[0]).ToList();
        }
    }
}
