using System.Runtime.InteropServices;

namespace YandexGames
{
    public static class Leaderboard
    {
        private const string DefaultLeaderboardName = "default-leaderboard";

        /// <summary>
        /// LeaderboardService is initialized automatically on load.
        /// If either something fails or called way too early, this will return false.
        /// </summary>
        public static bool IsInitialized => VerifyLeaderboardInitialization();
        
        [DllImport("__Internal")]
        private static extern bool VerifyLeaderboardInitialization();

        /// <remarks>
        /// Use <see cref="PlayerAccount.IsAuthorized"/> to avoid automatic authorization window popup.
        /// </remarks>
        public static void SetScore(int score, string leaderboardName = DefaultLeaderboardName, string additionalData = "")
        {
            SetLeaderboardScore(score, leaderboardName, additionalData);
        }

        [DllImport("__Internal")]
        private static extern void SetLeaderboardScore(int score, string leaderboardName, string extraData);
    }
}
