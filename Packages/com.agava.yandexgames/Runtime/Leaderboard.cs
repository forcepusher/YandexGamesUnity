using System;
using System.Runtime.InteropServices;
using AOT;
using UnityEngine;
using YandexGames.Utility;

namespace YandexGames
{
    public static class Leaderboard
    {
        private static Action<string> s_onErrorCallback;

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
        public static void SetScore(string leaderboardName, int score, string additionalData = "", Action<string> onErrorCallback = null)
        {
            s_onErrorCallback = onErrorCallback;

            SetLeaderboardScore(leaderboardName, score, additionalData, OnSetLeaderboardScoreErrorCallback);
        }

        [DllImport("__Internal")]
        private static extern void SetLeaderboardScore(string leaderboardName, int score, string additionalData, Action<IntPtr, int> errorCallback);

        [MonoPInvokeCallback(typeof(Action<IntPtr, int>))]
        private static void OnSetLeaderboardScoreErrorCallback(IntPtr errorMessageBufferPtr, int errorMessageBufferLength)
        {
            string errorMessage = new StringBuffer(errorMessageBufferPtr, errorMessageBufferLength).ToString();

            if (YandexGamesSdk.CallbackLogging)
                Debug.Log($"{nameof(Leaderboard)}.{nameof(OnSetLeaderboardScoreErrorCallback)} invoked, errorMessage = {errorMessage}");

            s_onErrorCallback?.Invoke(errorMessage);
        }
    }
}
