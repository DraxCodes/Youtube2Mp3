using System.IO;
using System.Text.RegularExpressions;

namespace Youtube2Mp3.Core.Extensions
{
    public static class StringExtensions
    {
        public static string RemoveIllegalPathCharacters(this string path)
            => string.Join("_", path.Split(Path.GetInvalidFileNameChars()));
    }
}
