using NUnit.Framework;

namespace YandexGames.Tests
{
    public class YandexGamesSdkTests
    {
        [Test]
        public void InitializationTest()
        {
            Assert.IsTrue(YandexGamesSdk.Initialized);
        }
    }
}
