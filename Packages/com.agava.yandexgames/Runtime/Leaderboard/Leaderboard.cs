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
        private static Action<LeaderboardEntryResponse> s_onGetPlayerEntrySuccessCallback;
        private static Action<string> s_onGetPlayerEntryErrorCallback;

        // We shouldn't normally use regions, but my eyes hurt from statics.

        #region SetScore
        /// <remarks>
        /// <para />To prevent overwriting a better result, use <see cref="GetPlayerEntry"/>.
        /// <para />If user did not give <see cref="PlayerAccount.HasPersonalProfileDataPermission"/> or rejected it, the result will be posted anonymously.
        /// <para />Requires authorization. Use <see cref="PlayerAccount.Authorized"/> and <see cref="PlayerAccount.Authorize"/>.
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
        /// Returns a fully parsed result object in onSuccessCallback.
        /// </summary>
        /// <remarks>
        /// Requires authorization. Use <see cref="PlayerAccount.Authorized"/> and <see cref="PlayerAccount.Authorize"/>.
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

        #region GetPlayerEntry
        /// <summary>
        /// Returns a fully parsed result object in onSuccessCallback, or returns null if player is not present.
        /// </summary>
        /// <remarks>
        /// Requires authorization. Use <see cref="PlayerAccount.Authorized"/> and <see cref="PlayerAccount.Authorize"/>.
        /// </remarks>
        public static void GetPlayerEntry(string leaderboardName, Action<LeaderboardEntryResponse> onSuccessCallback, Action<string> onErrorCallback = null)
        {
            s_onGetPlayerEntrySuccessCallback = onSuccessCallback;
            s_onGetPlayerEntryErrorCallback = onErrorCallback;

            GetLeaderboardPlayerEntry(leaderboardName, OnGetLeaderboardPlayerEntrySuccessCallback, OnGetLeaderboardPlayerEntryErrorCallback);
        }

        [DllImport("__Internal")]
        private static extern void GetLeaderboardPlayerEntry(string leaderboardName, Action<string> successCallback, Action<IntPtr, int> errorCallback);

        [MonoPInvokeCallback(typeof(Action<string>))]
        private static void OnGetLeaderboardPlayerEntrySuccessCallback(string entryResponseJson)
        {
            Debug.Log("C# 1");

            //string entryResponseJson = new UnmanagedString(entryMessageBufferPtr, entryMessageBufferLength).ToString();

            Debug.Log("C# 2");

            if (YandexGamesSdk.CallbackLogging)
                Debug.Log($"{nameof(Leaderboard)}.{nameof(OnGetLeaderboardPlayerEntrySuccessCallback)} invoked, {nameof(entryResponseJson)} = {entryResponseJson}");

            Debug.Log("C# 3");
             
            LeaderboardEntryResponse entryResponse;

            Debug.Log("C# 4.0");

            Debug.Log(entryResponseJson.Length);

            Debug.Log("C# 4.1");

            Debug.Log(entryResponseJson);

            Debug.Log("C# 4.2");

            Debug.Log(string.Equals(entryResponseJson, "null"));

            Debug.Log("C# 4.3");

            Debug.Log((entryResponseJson == "null").ToString());

            Debug.Log("C# 4.4");

            //if (entryResponseJson == "null")
            if (string.Equals(entryResponseJson, "null"))
            {
                Debug.Log("C# 5");
                entryResponse = null;
                Debug.Log("C# 6");
            }
            else
            {
                Debug.Log("C# 7");
                entryResponse = JsonUtility.FromJson<LeaderboardEntryResponse>(entryResponseJson);
                Debug.Log("C# 8");
            }

            //LeaderboardEntryResponse entryResponse = entryResponseJson == "null" ? null : JsonUtility.FromJson<LeaderboardEntryResponse>(entryResponseJson);

            Debug.Log("C# 9");

            s_onGetPlayerEntrySuccessCallback?.Invoke(entryResponse);

            Debug.Log("C# 10");
        }

        [MonoPInvokeCallback(typeof(Action<IntPtr, int>))]
        private static void OnGetLeaderboardPlayerEntryErrorCallback(IntPtr errorMessageBufferPtr, int errorMessageBufferLength)
        {
            string errorMessage = new UnmanagedString(errorMessageBufferPtr, errorMessageBufferLength).ToString();

            if (YandexGamesSdk.CallbackLogging)
                Debug.Log($"{nameof(Leaderboard)}.{nameof(OnGetLeaderboardPlayerEntryErrorCallback)} invoked, {nameof(errorMessage)} = {errorMessage}");

            s_onGetPlayerEntryErrorCallback?.Invoke(errorMessage);
        }
        #endregion
    }
}
