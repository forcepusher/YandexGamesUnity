using System.Runtime.InteropServices;

namespace YandexGames
{
    public static class Leaderboard
    {
        /// <summary>
        /// Leaderboard is initialized automatically on load.
        /// If either something fails or called way too early, this will return false.
        /// </summary>
        public static bool IsInitialized => VerifyLeaderboardInitialization();
        
        [DllImport("__Internal")]
        private static extern bool VerifyLeaderboardInitialization();
    }
}
