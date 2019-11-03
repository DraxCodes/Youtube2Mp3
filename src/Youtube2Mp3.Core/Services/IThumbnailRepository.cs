using Youtube2Mp3.Core.Entities;

namespace Youtube2Mp3.Core.Services
{
    public interface IThumbnailRepository
    {
        string DownloadThumbnailAndGetFilePath(Track track);
    }
}
