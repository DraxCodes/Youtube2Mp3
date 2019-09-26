using System.IO;
using System.Text.RegularExpressions;

namespace Youtube2Mp3.Core.Extensions
{
    public static class StringExtensions
    {
        public static string RemoveIllegalPathCharacters(this string path)
        {
            string regexSearch = $"{Path.GetInvalidFileNameChars().ToString()}{Path.GetInvalidPathChars().ToString()}";
            var r = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));
            return r.Replace(path, "");
        }
    }
}
