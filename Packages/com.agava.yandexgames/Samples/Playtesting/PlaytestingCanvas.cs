#pragma warning disable

using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace YandexGames.Samples
{
    public class PlaytestingCanvas : MonoBehaviour
    {
        [SerializeField]
        private Text _isAuthorizedText;

        private void Awake()
        {
            YandexGamesSdk.CallbackLogging = true;
        }

        private IEnumerator Start()
        {
            // {"entries":[{"score":99,"extraData":"","rank":1,"player":{"lang":"en","publicName":"","scopePermissions":{"avatar":"forbid","public_name":"forbid"},"uniqueID":"PYI9P2xFf4Z0+w0j/DU+p7wREIi3cwEcJGflNCrSlsY="},"formattedScore":"99"},{"score":95,"extraData":"","rank":2,"player":{"lang":"ru","publicName":"Dpyr C.","scopePermissions":{"avatar":"allow","public_name":"allow"},"uniqueID":"XhSPasVSsFoZ1P85fpN/YJMfTzRVWPdLtbn8PIzvo5U="},"formattedScore":"95"},{"score":74,"extraData":"","rank":3,"player":{"lang":"ru","publicName":"Петр Мустаев","scopePermissions":{"avatar":"allow","public_name":"allow"},"uniqueID":"ybDY/9WafCq6ChPUO6SyXfnZY9gxO0LPsd/mF1KzgBo="},"formattedScore":"74"},{"score":72,"extraData":"","rank":4,"player":{"lang":"ru","publicName":"","scopePermissions":{"avatar":"not_set","public_name":"not_set"},"uniqueID":"/cYcBitvABSVWKHEohLL3kFEfJ3lbTZq8AhrhDWu39c="},"formattedScore":"72"},{"score":48,"extraData":"","rank":5,"player":{"lang":"ru","publicName":"Рома А.","scopePermissions":{"avatar":"allow","public_name":"allow"},"uniqueID":"EWfli0tKXO5ag7dx53G2QtTSp35okhbTyJHLFxuRqBw="},"formattedScore":"48"},{"score":12,"extraData":"","rank":6,"player":{"lang":"ru","publicName":"Павел Д.","scopePermissions":{"avatar":"allow","public_name":"allow"},"uniqueID":"bq5R0Ks1tDBlT2nhV7XZr6pU0hHkruKbQAuaRf8e0qI="},"formattedScore":"12"}],"leaderboard":{"appID":179973,"name":"PlaytestBoard","default":true,"description":{"invert_sort_order":false,"score_format":{"type":"numeric","options":{"decimal_offset":0}}},"title":{"ru":""}},"ranges":[{"start":0,"size":9}],"userRank":4}
            string responseSampleJson = @"{""entries"":[{""score"":99,""extraData"":"""",""rank"":1,""player"":{""lang"":""en"",""publicName"":"""",""scopePermissions"":{""avatar"":""forbid"",""public_name"":""forbid""},""uniqueID"":""PYI9P2xFf4Z0+w0j/DU+p7wREIi3cwEcJGflNCrSlsY=""},""formattedScore"":""99""},{ ""score"":95,""extraData"":"""",""rank"":2,""player"":{ ""lang"":""ru"",""publicName"":""Dpyr C."",""scopePermissions"":{ ""avatar"":""allow"",""public_name"":""allow""},""uniqueID"":""XhSPasVSsFoZ1P85fpN/YJMfTzRVWPdLtbn8PIzvo5U=""},""formattedScore"":""95""},{ ""score"":74,""extraData"":"""",""rank"":3,""player"":{ ""lang"":""ru"",""publicName"":""Петр Мустаев"",""scopePermissions"":{ ""avatar"":""allow"",""public_name"":""allow""},""uniqueID"":""ybDY/9WafCq6ChPUO6SyXfnZY9gxO0LPsd/mF1KzgBo=""},""formattedScore"":""74""},{ ""score"":72,""extraData"":"""",""rank"":4,""player"":{ ""lang"":""ru"",""publicName"":"""",""scopePermissions"":{ ""avatar"":""not_set"",""public_name"":""not_set""},""uniqueID"":""/cYcBitvABSVWKHEohLL3kFEfJ3lbTZq8AhrhDWu39c=""},""formattedScore"":""72""},{ ""score"":48,""extraData"":"""",""rank"":5,""player"":{ ""lang"":""ru"",""publicName"":""Рома А."",""scopePermissions"":{ ""avatar"":""allow"",""public_name"":""allow""},""uniqueID"":""EWfli0tKXO5ag7dx53G2QtTSp35okhbTyJHLFxuRqBw=""},""formattedScore"":""48""},{ ""score"":12,""extraData"":"""",""rank"":6,""player"":{ ""lang"":""ru"",""publicName"":""Павел Д."",""scopePermissions"":{ ""avatar"":""allow"",""public_name"":""allow""},""uniqueID"":""bq5R0Ks1tDBlT2nhV7XZr6pU0hHkruKbQAuaRf8e0qI=""},""formattedScore"":""12""}],""leaderboard"":{ ""appID"":179973,""name"":""PlaytestBoard"",""default"":true,""description"":{ ""invert_sort_order"":false,""score_format"":{ ""type"":""numeric"",""options"":{ ""decimal_offset"":0} } },""title"":{ ""ru"":""""} },""ranges"":[{""start"":0,""size"":9}],""userRank"":4}";
            responseSampleJson = responseSampleJson.Replace(@"""default"":true,", "");

            LeaderboardGetEntriesResponse responseSample = JsonUtility.FromJson<LeaderboardGetEntriesResponse>(responseSampleJson);
            Debug.Log(responseSampleJson);
            Debug.Log(JsonUtility.ToJson(responseSample));
            Debug.Log(JsonUtility.ToJson(responseSample) == responseSampleJson);

#if !UNITY_WEBGL || UNITY_EDITOR
            yield break;
#endif

            // Always wait for it if invoking something immediately in the first scene.
            yield return YandexGamesSdk.WaitForInitialization();

            // Avoid unexpected authorization window popup that will freak out the user.
            if (PlayerAccount.IsAuthorized)
            {
                // Authenticate silently without requesting photo and real name permissions.
                PlayerAccount.Authenticate(false);
            }

            while (true)
            {
                _isAuthorizedText.color = PlayerAccount.IsAuthorized ? Color.green : Color.red;
                yield return new WaitForSecondsRealtime(0.25f);
            }
        }

        public void OnShowInterestialButtonClick()
        {
            InterestialAd.Show();
        }

        public void OnShowVideoButtonClick()
        {
            VideoAd.Show();
        }

        public void OnAuthenticateButtonClick()
        {
            PlayerAccount.Authenticate(false);
        }

        public void OnAuthenticateWithPermissionsButtonClick()
        {
            PlayerAccount.Authenticate(true);
        }

        public void OnSetLeaderboardScoreButtonClick()
        {
            Leaderboard.SetScore("PlaytestBoard", Random.Range(1, 100));
        }

        public void OnGetLeaderboardEntriesButtonClick()
        {
            Leaderboard.GetEntries("PlaytestBoard", (result) =>
            {
                // Use it
                Debug.Log($"My rank = {result.userRank}");
                foreach (var entry in result.entries)
                {
                    Debug.Log(entry.player.publicName + " " + entry.score);
                }
            });
        }
    }
}
