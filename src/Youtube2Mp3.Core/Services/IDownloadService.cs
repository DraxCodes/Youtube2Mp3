using System.Threading.Tasks;
using Youtube2Mp3.Core.Entities;

namespace Youtube2Mp3.Core.Services
{
    public interface IDownloadService
    {
        Task DownloadMediaAsync(Track track, string filePath, bool appendLyrics, bool useAuthor, bool allowFallback = true);
    }
}
