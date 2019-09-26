using System.IO;

namespace Youtube2Mp3.Core.Services
{
    public interface IDownloadService
    {
        void DownloadMedia(Stream stream);
    }
}
