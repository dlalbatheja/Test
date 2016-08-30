using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortStudentsData
{
    static class Utility
    {
        public static FileInfo GetFileInfo(string fileName)
        {
            FileInfo fileInfo = new FileInfo(fileName);

            if (fileInfo.Exists)
                return fileInfo;
            else
                return null;
        }

        public static string RemoveExtension(this string fileName)
        {
            int extIndex = fileName.LastIndexOf('.');

            if(extIndex > 0)
            {
                return fileName.Substring(0, extIndex);
            }

            return fileName;
        }
    }
}
