using System;
using NLog.MessageTemplates;
using Xunit;

namespace Movie_Library
{
    public class TestRandomFilm
    {
        [Theory]
        [InlineData(10)]
        [InlineData(200)]
        [InlineData(350)]
        [InlineData(9125)]
        public void IsMiniGameReturnTrue(int t)
        {
            Assert.True(Program.OptionalMiniGame(t));
        }
    }
}