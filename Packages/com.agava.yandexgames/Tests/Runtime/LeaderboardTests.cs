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

        [Test]
        public void SetScoreShouldNotThrow()
        {
            Assert.DoesNotThrow(() => Leaderboard.SetScore(228));
            Assert.DoesNotThrow(() => Leaderboard.SetScore(0, additionalData: "henlo"));
        }
    }
}
