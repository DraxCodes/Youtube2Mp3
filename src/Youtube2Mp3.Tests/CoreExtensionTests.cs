using Youtube2Mp3.Core.Extensions;
using Xunit;
using Youtube2Mp3.Core.Entities;

namespace Youtube2Mp3.Tests
{
    public class CoreExtensionTests
    {
        [Theory]
        [InlineData("Test/Char")]
        [InlineData(@"Test\Char")]
        [InlineData("Test?Char")]
        [InlineData("Test*Char")]
        [InlineData("Test:Char")]
        [InlineData("Test|Char")]
        [InlineData("Test\"Char")]
        [InlineData("Test<Char")]
        [InlineData("Test>Char")]
        public void ShouldReplaceBadFileNameChars(string input)
        {
            var expected = "Test_Char";
            var actual = input.RemoveIllegalPathCharacters();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TrackQueryFormatShouldReturnAppendedLyrics()
        {
            var track = new Track("Test Title", new[] { "Test Author" }, 23244);
            var expected = "Test Author - Test Title lyrics";
            var actual = track.QueryFormat(true, true);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TrackQueryFormatShouldNotAppendLyrics()
        {
            var track = new Track("Test Title", new[] { "Test Author" }, 23244);
            var expected = "Test Author - Test Title";
            var actual = track.QueryFormat(true, false);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TrackQueryFormatShouldOnlyReturnTitle()
        {
            var track = new Track("Test Title", new[] { "Test Author" }, 23244);
            var expected = "Test Title";
            var actual = track.QueryFormat(false, false);

            Assert.Equal(expected, actual);
        }
    }
}
