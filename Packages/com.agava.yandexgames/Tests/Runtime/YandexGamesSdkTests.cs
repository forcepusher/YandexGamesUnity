using NUnit.Framework;
using YandexGames;

public class YandexGamesSdkTests
{
    [Test]
    public void InitializationTest()
    {
        Assert.IsTrue(YandexGamesSdk.Initialized);
    }
}
