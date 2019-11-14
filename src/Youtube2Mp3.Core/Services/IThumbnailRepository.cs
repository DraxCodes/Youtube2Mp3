using System.Threading.Tasks;
using Youtube2Mp3.Core.Entities;

namespace Youtube2Mp3.Core.Services
{
    public interface IThumbnailRepository
    {
        Task<string> DownloadThumbnailAndGetFilePathAsync(ITrack track);
    }
}
