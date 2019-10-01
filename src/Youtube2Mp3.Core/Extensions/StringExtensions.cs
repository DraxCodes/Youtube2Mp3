using System.IO;

namespace Youtube2Mp3.Core.Extensions
{
    public static class StringExtensions
    {
        public static string RemoveIllegalPathCharacters(this string path)
            => string.Join("_", path.Split(Path.GetInvalidFileNameChars()));
    }
}
