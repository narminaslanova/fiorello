using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Fiorello.Helpers
{
    public static class Helper
    {
        public static void  DeleteFile(string root, string folder, string fileName)
        {
            string path = Path.Combine(root, folder, fileName);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
    public enum Roles
    {
        Admin,
        Manager,
        Member
    }
}
