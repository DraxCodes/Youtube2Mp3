using System.Threading.Tasks;
using Youtube2Mp3.Core.Entities;

namespace Youtube2Mp3.Core.Services
{
    public interface ISpotifyService
    {
        Task Initialize(string clientId, string clientSecret);
        Task<SpotifyTrack[]> LoadPlaylistAsync(string url);
    }
}
