using System.Threading.Tasks;
using Youtube2Mp3.Core.Entities;

namespace Youtube2Mp3.Core.Services
{
    public interface IDownloadService
    {
        Task DownloadMedia(Track track, string filePath);
    }
}
