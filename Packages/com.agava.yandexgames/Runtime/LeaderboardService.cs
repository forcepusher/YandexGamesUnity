using System.Runtime.InteropServices;

namespace YandexGames
{
    public static class LeaderboardService
    {
        /// <summary>
        /// LeaderboardService is initialized automatically on load.
        /// If either something fails or called way too early, this will return false.
        /// </summary>
        public static bool IsInitialized => VerifyLeaderboardServiceInitialization();
        
        [DllImport("__Internal")]
        private static extern bool VerifyLeaderboardServiceInitialization();
    }
}
