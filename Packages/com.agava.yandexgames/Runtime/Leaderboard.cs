using System.Runtime.InteropServices;

namespace YandexGames
{
    public static class Leaderboard
    {
        /// <summary>
        /// Leaderboard is initialized automatically on load. If something fails, this will return false.
        /// </summary>
        [DllImport("__Internal")]
        public static extern bool VerifyLeaderboardInitialization();
    }
}
