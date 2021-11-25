using NUnit.Framework;
using YandexGames;

public class InterestialAdTests
{
    [Test]
    public void ShowTest()
    {
        var interestialAd = new InterestialAd();
        Assert.DoesNotThrow(() => interestialAd.Show());
    }
}
