using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using InstaSync.Models.Enums;
using System.Text;
using System.Threading.Tasks;

namespace InstaSync.UOW.Helpers
{
    public class FileHelper
    {
        public static void SaveFile(string path, string name, FileExtension extension, byte[] stream)
        {
            File.WriteAllBytes($"{path}/{name}.{extension}", stream);
        }

        public static List<String> GetFilesNames(List<String> paths)
        {
            return paths.Select(Path.GetFileName).ToList();
        }
    }
}
