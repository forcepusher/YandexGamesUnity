using NUnit.Framework;

namespace YandexGames.Tests
{
    public class YandexGamesSdkTests
    {
        [Test]
        public void SdkShouldInitializeAutomatically()
        {
            Assert.IsTrue(YandexGamesSdk.VerifyInitialization());
        }
    }
}
