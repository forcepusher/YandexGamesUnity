using System;
using UnityEngine.Scripting;

namespace YandexGames
{
    [Serializable]
    public class LeaderboardGetEntriesResponse
    {
        [field: Preserve]
        public LeaderboardDescriptionResponse leaderboard;
        [field: Preserve]
        public LeaderboardRangeResponse[] range;
        [field: Preserve]
        public int userRank;
    }
}
