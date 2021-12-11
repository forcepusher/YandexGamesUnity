using System;
using UnityEngine.Scripting;

namespace YandexGames
{
    [Serializable]
    public class LeaderboardEntryResponse
    {
        [field: Preserve]
        public int score;
        [field: Preserve]
        public string extraData;
        [field: Preserve]
        public int rank;
        [field: Preserve]
        public Player player;
        [field: Preserve]
        public string formattedScore;


        [Serializable]
        public class Player
        {
            [field: Preserve]
            public string lang;
            [field: Preserve]
            public string publicName;
            [field: Preserve]
            public ScopePermissions scopePermissions;
            [field: Preserve]
            public string uniqueID;


            [Serializable]
            public class ScopePermissions
            {
                [field: Preserve]
                public string avatar;
                [field: Preserve]
                public string public_name;
            }
        }
    }
}
