using System.Runtime.InteropServices;

namespace YandexGames
{
    public class InterestialAd
    {
        [DllImport("__Internal")]
        private static extern bool ShowInterestialAd();
    }
}
