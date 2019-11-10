using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Youtube2Mp3.Core.Entities;

namespace Youtube2Mp3.Core.Services
{
    public interface IStreamRepository
    {
        Task<MemoryStream> GetStreamByTrackAsync(ITrack track, bool appendLyrics, bool useAuthor, bool shouldDefault);
        Task<MemoryStream> GetStreamByYoutubeTrackAsync(YoutubeTrack track);
        Task<IEnumerable<ITrack>> SearchAsync(ITrack track);
        Task<IEnumerable<ITrack>> SearchAsync(string query);
    }
}
