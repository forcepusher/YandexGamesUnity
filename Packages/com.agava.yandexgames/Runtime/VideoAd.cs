using System.Runtime.InteropServices;

namespace YandexGames
{
    public class VideoAd
    {
        public void Show()
        {
            ShowVideoAd();
        }

        [DllImport("__Internal")]
        private static extern bool ShowVideoAd();
    }
}
