using System.Runtime.InteropServices;

namespace YandexGames
{
    public class VideoAd
    {
        [DllImport("__Internal")]
        private static extern bool ShowVideoAd();
    }
}
