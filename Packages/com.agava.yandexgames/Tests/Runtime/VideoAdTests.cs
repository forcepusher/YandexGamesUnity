using NUnit.Framework;
using YandexGames;

public class VideoAdTests
{
    [Test]
    public void ShowTest()
    {
        var videoAd = new VideoAd();
        Assert.DoesNotThrow(() => videoAd.Show());
    }
}
