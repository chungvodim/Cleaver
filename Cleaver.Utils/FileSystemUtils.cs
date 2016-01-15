using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleaver.Utils
{
    public class FileSystemUtils
    {
        public static void SaveFileContent(string directory, string filename, string content, Encoding encoding)
        {
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            File.WriteAllText(filename, content, encoding);
        }

        public static void SaveFileContentUTF8(string directory, string filename, string content)
        {
            SaveFileContent(directory, filename, content, Encoding.UTF8);
        }
    }
}
