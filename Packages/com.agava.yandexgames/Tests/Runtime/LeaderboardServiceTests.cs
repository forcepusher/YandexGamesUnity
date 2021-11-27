using NUnit.Framework;

namespace YandexGames.Tests
{
    public class LeaderboardServiceTests
    {
        [Test]
        public void ShouldInitializeAutomatically()
        {
            Assert.IsTrue(LeaderboardService.IsInitialized);
        }
    }
}
