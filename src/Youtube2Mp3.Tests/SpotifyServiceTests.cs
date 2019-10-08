using Xunit;
using Youtube2Mp3.Spotify.Extensions;

namespace Youtube2Mp3.Tests
{
    public class SpotifyServiceTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void ShouldReturnNullIfEmptyUrl(string input)
        {
            string expected = null!;
            var actual = SpotifyClientExtensions.ParseSpotifyIdFromUrl(input);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("https://open.spotify.com/playlist/0apX36HEcBc4qRsPoZcdRQ?si=iechbOUfT-W2pTto6tpQag", "0apX36HEcBc4qRsPoZcdRQ")]
        [InlineData("spotify:playlist:211hNyfHUmLVlVUFSZhxXP", "211hNyfHUmLVlVUFSZhxXP")]
        public void ShouldReturnIdIfValidUrl(string input, string expected)
        {
            var actual = SpotifyClientExtensions.ParseSpotifyIdFromUrl(input);

            Assert.Equal(expected, actual);
        }
    }
}
