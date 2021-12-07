using System;
using UnityEngine.Scripting;
using UnityEngine.Serialization;

namespace YandexGames
{
    [Serializable]
    public class LeaderboardDescriptionResponse
    {
        [field: Preserve]
        public string appID;
        // Well, there's nothing I can do about it lol.
        //[field: Preserve]
        //public bool default;
    }
}
