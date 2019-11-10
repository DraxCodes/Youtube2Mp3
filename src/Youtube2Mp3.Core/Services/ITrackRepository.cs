using System.Collections.Generic;
using System.Threading.Tasks;
using Youtube2Mp3.Core.Entities;

namespace Youtube2Mp3.Core.Services
{
    public interface ITrackRespository
    {
        Task<IEnumerable<ITrack>> LoadPlaylistAsync(string url);
        void InitializeSpotifyAuth(string clientId, string clientSecret);
    }
}
