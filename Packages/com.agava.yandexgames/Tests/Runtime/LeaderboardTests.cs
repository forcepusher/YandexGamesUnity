using NUnit.Framework;

namespace YandexGames.Tests
{
    public class LeaderboardTests
    {
        [Test]
        public void ShouldInitializeAutomatically()
        {
            Assert.IsTrue(Leaderboard.IsInitialized);
        }
    }
}
