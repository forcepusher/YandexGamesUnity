using System.Runtime.InteropServices;

namespace YandexGames
{
    public class InterestialAd
    {
        public void Show()
        {
            ShowInterestialAd();
        }

        [DllImport("__Internal")]
        private static extern bool ShowInterestialAd();
    }
}
