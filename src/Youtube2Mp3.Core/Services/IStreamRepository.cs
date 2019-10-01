using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Youtube2Mp3.Core.Entities;

namespace Youtube2Mp3.Core.Services
{
    public interface IStreamRepository
    {
        Task<MemoryStream> GetStreamByTrackAsync(Track track, bool appendLyrics, bool useAuthor, bool shouldDefault);
        Task<MemoryStream> GetStreamByYoutubeTrackAsync(YoutubeTrack track);
        Task<IEnumerable<YoutubeTrack>> SearchAsync(Track track);
    }
}
