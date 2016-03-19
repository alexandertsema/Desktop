using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaSync.UOW.Helpers
{
    public class DirectoryHelper
    {
        public static void CreateDirectory(string path)
        {
            if (Directory.Exists(path))
            {
                return;
            }
            
            Directory.CreateDirectory(path);
        }

        public static List<String> GetFiles(string path)
        { 
            return Directory.GetFiles(path).ToList();
        }
    }
}
