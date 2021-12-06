using System;
using UnityEngine.Scripting;

namespace YandexGames
{
    [Serializable]
    public class LeaderboardRangeResponse
    {
        [field: Preserve]
        public int start;
        [field: Preserve]
        public int size;
    }
}
