using System.IO;

namespace Recon.File
{
    public class FileHelper
    {
        public static FileInfo GetFileInfo(string file)
        {
            FileInfo fi = null;
            if (!string.IsNullOrEmpty(file) && System.IO.File.Exists(file))
            {
                fi = new FileInfo(file);
            }
            return fi;
        }

        public static Stream GetFileStream(FileInfo fi)
        {
            return new FileStream(fi.FullName, FileMode.Open, FileAccess.Read);
        }
    }
}