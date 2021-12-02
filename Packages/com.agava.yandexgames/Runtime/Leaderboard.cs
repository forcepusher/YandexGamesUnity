using System;
using System.Collections;
using System.Runtime.InteropServices;
using AOT;
using UnityEngine;
using YandexGames.Utility;

namespace YandexGames
{
    public static class Leaderboard
    {
        // This is what we deserve for using Unity.
        private static Action s_onSetScoreSuccessCallback;
        private static Action<string> s_onSetScoreErrorCallback;
        private static Action s_onGetEntriesSuccessCallback;
        private static Action<string> s_onGetEntriesErrorCallback;

        /// <summary>
        /// LeaderboardService is initialized automatically on load.
        /// If either something fails or called way too early, this will return false.
        /// </summary>
        public static bool IsInitialized => VerifyLeaderboardInitialization();
        
        [DllImport("__Internal")]
        private static extern bool VerifyLeaderboardInitialization();

        /// <summary>
        /// Coroutine waiting for <see cref="IsInitialized"/> to return true.
        /// </summary>
        public static IEnumerator WaitForInitialization()
        {
            while (!IsInitialized)
                yield return null;
        }

        // We shouldn't normally use regions, but my eyes hurt from statics.

        #region SetScore
        /// <remarks>
        /// Use <see cref="PlayerAccount.IsAuthorized"/> to avoid automatic authorization window popup.
        /// </remarks>
        public static void SetScore(string leaderboardName, int score, string additionalData = "", Action onSuccessCallback = null, Action<string> onErrorCallback = null)
        {
            s_onSetScoreSuccessCallback = onSuccessCallback;
            s_onSetScoreErrorCallback = onErrorCallback;

            SetLeaderboardScore(leaderboardName, score, additionalData, OnSetLeaderboardScoreSuccessCallback, OnSetLeaderboardScoreErrorCallback);
        }

        [DllImport("__Internal")]
        private static extern void SetLeaderboardScore(string leaderboardName, int score, string additionalData, Action successCallback, Action<IntPtr, int> errorCallback);

        [MonoPInvokeCallback(typeof(Action))]
        private static void OnSetLeaderboardScoreSuccessCallback()
        {
            if (YandexGamesSdk.CallbackLogging)
                Debug.Log($"{nameof(Leaderboard)}.{nameof(OnSetLeaderboardScoreSuccessCallback)} invoked");

            s_onSetScoreSuccessCallback?.Invoke();
        }

        [MonoPInvokeCallback(typeof(Action<IntPtr, int>))]
        private static void OnSetLeaderboardScoreErrorCallback(IntPtr errorMessageBufferPtr, int errorMessageBufferLength)
        {
            string errorMessage = new UnmanagedString(errorMessageBufferPtr, errorMessageBufferLength).ToString();

            if (YandexGamesSdk.CallbackLogging)
                Debug.Log($"{nameof(Leaderboard)}.{nameof(OnSetLeaderboardScoreErrorCallback)} invoked, errorMessage = {errorMessage}");

            s_onSetScoreErrorCallback?.Invoke(errorMessage);
        }
        #endregion

        #region GetEntries
        public static void GetEntries(string leaderboardName, Action onSuccessCallback = null, Action<string> onErrorCallback = null, int topPlayersCount = 5, int competingPlayersCount = 5, bool includeSelf = true)
        {
            s_onGetEntriesSuccessCallback = onSuccessCallback;
            s_onGetEntriesErrorCallback = onErrorCallback;

            GetLeaderboardEntries(leaderboardName, OnGetLeaderboardEntriesSuccessCallback, OnGetLeaderboardEntriesErrorCallback, topPlayersCount, competingPlayersCount, includeSelf);
        }

        [DllImport("__Internal")]
        private static extern void GetLeaderboardEntries(string leaderboardName, Action successCallback, Action<IntPtr, int> errorCallback, int topPlayersCount, int competingPlayersCount, bool includeSelf);

        [MonoPInvokeCallback(typeof(Action))]
        private static void OnGetLeaderboardEntriesSuccessCallback()
        {
            if (YandexGamesSdk.CallbackLogging)
                Debug.Log($"{nameof(Leaderboard)}.{nameof(OnGetLeaderboardEntriesSuccessCallback)} invoked");

            s_onSetScoreSuccessCallback?.Invoke();
        }

        [MonoPInvokeCallback(typeof(Action<IntPtr, int>))]
        private static void OnGetLeaderboardEntriesErrorCallback(IntPtr errorMessageBufferPtr, int errorMessageBufferLength)
        {
            string errorMessage = new UnmanagedString(errorMessageBufferPtr, errorMessageBufferLength).ToString();

            if (YandexGamesSdk.CallbackLogging)
                Debug.Log($"{nameof(Leaderboard)}.{nameof(OnGetLeaderboardEntriesErrorCallback)} invoked, errorMessage = {errorMessage}");

            s_onSetScoreErrorCallback?.Invoke(errorMessage);
        }
        #endregion
    }
}
