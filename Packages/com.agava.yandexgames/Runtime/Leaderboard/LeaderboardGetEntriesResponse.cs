using System;

namespace YandexGames
{
    [Serializable]
    public class LeaderboardGetEntriesResponse
    {
        public LeaderboardDescriptionResponse leaderboard;
        public LeaderboardRangeResponse[] range;
        public int userRank;
    }
}
