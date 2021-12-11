using System;
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
        private static Action<LeaderboardGetEntriesResponse> s_onGetEntriesSuccessCallback;
        private static Action<string> s_onGetEntriesErrorCallback;

        // We shouldn't normally use regions, but my eyes hurt from statics.

        #region SetScore
        /// <remarks>
        /// Requires authorization. Use <see cref="Authorized"/> and <see cref="Authorize"/>.
        /// </remarks>
        public static void SetScore(string leaderboardName, int score, Action onSuccessCallback = null, Action<string> onErrorCallback = null, string extraData = "")
        {
            s_onSetScoreSuccessCallback = onSuccessCallback;
            s_onSetScoreErrorCallback = onErrorCallback;

            SetLeaderboardScore(leaderboardName, score, OnSetLeaderboardScoreSuccessCallback, OnSetLeaderboardScoreErrorCallback, extraData);
        }

        [DllImport("__Internal")]
        private static extern void SetLeaderboardScore(string leaderboardName, int score, Action successCallback, Action<IntPtr, int> errorCallback, string extraData);

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
                Debug.Log($"{nameof(Leaderboard)}.{nameof(OnSetLeaderboardScoreErrorCallback)} invoked, {nameof(errorMessage)} = {errorMessage}");

            s_onSetScoreErrorCallback?.Invoke(errorMessage);
        }
        #endregion

        #region GetEntries
        /// <summary>
        /// Returns result JSON as a string in onSuccessCallback - for now.
        /// </summary>
        /// <remarks>
        /// Requires authorization. Use <see cref="Authorized"/> and <see cref="Authorize"/>.
        /// </remarks>
        public static void GetEntries(string leaderboardName, Action<LeaderboardGetEntriesResponse> onSuccessCallback, Action<string> onErrorCallback = null, int topPlayersCount = 5, int competingPlayersCount = 5, bool includeSelf = true)
        {
            s_onGetEntriesSuccessCallback = onSuccessCallback;
            s_onGetEntriesErrorCallback = onErrorCallback;

            GetLeaderboardEntries(leaderboardName, OnGetLeaderboardEntriesSuccessCallback, OnGetLeaderboardEntriesErrorCallback, topPlayersCount, competingPlayersCount, includeSelf);
        }

        [DllImport("__Internal")]
        private static extern void GetLeaderboardEntries(string leaderboardName, Action<IntPtr, int> successCallback, Action<IntPtr, int> errorCallback, int topPlayersCount, int competingPlayersCount, bool includeSelf);

        [MonoPInvokeCallback(typeof(Action<IntPtr, int>))]
        private static void OnGetLeaderboardEntriesSuccessCallback(IntPtr entriesMessageBufferPtr, int entriesMessageBufferLength)
        {
            string entriesResponseJson = new UnmanagedString(entriesMessageBufferPtr, entriesMessageBufferLength).ToString();

            if (YandexGamesSdk.CallbackLogging)
                Debug.Log($"{nameof(Leaderboard)}.{nameof(OnGetLeaderboardEntriesSuccessCallback)} invoked, {nameof(entriesResponseJson)} = {entriesResponseJson}");

            LeaderboardGetEntriesResponse entriesResponse = JsonUtility.FromJson<LeaderboardGetEntriesResponse>(entriesResponseJson);

            s_onGetEntriesSuccessCallback?.Invoke(entriesResponse);
        }

        [MonoPInvokeCallback(typeof(Action<IntPtr, int>))]
        private static void OnGetLeaderboardEntriesErrorCallback(IntPtr errorMessageBufferPtr, int errorMessageBufferLength)
        {
            string errorMessage = new UnmanagedString(errorMessageBufferPtr, errorMessageBufferLength).ToString();

            if (YandexGamesSdk.CallbackLogging)
                Debug.Log($"{nameof(Leaderboard)}.{nameof(OnGetLeaderboardEntriesErrorCallback)} invoked, {nameof(errorMessage)} = {errorMessage}");

            s_onGetEntriesErrorCallback?.Invoke(errorMessage);
        }
        #endregion
    }
}
