using System.IO;

namespace TURTLECHALLENGE.extensions
{
    public static class FileExt
    {
        public static bool IsNotValidPath(this string path)
        {
            return !File.Exists(path);
        }
    }
}
