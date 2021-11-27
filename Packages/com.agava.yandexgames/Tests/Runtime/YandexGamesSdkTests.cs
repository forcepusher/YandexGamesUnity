using NUnit.Framework;

namespace YandexGames.Tests
{
    public class YandexGamesSdkTests
    {
        [Test]
        public void ShouldInitializeAutomatically()
        {
            Assert.IsTrue(YandexGamesSdk.IsInitialized);
        }
    }
}
