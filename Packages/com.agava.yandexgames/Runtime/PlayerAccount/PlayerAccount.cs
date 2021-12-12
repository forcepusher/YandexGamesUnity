using System;
using System.Runtime.InteropServices;
using AOT;
using UnityEngine;
using YandexGames.Utility;

namespace YandexGames
{
    /// <summary>
    /// Proxy for player-related methods in YandexGames SDK.
    /// </summary>
    public static class PlayerAccount
    {
        private static Action s_onAuthorizeSuccessCallback;
        private static Action<string> s_onAuthorizeErrorCallback;
        private static Action s_onRequestProfileDataPermissionSuccessCallback;
        private static Action<string> s_onRequestProfileDataPermissionErrorCallback;
        private static Action s_onGetProfileDataSuccessCallback;
        private static Action<string> s_onGetProfileDataErrorCallback;

        /// <summary>
        /// Use this before calling SDK methods that require authorization.
        /// </summary>
        public static bool Authorized => CheckAuthorization();

        [DllImport("__Internal")]
        private static extern bool CheckAuthorization();

        /// <summary>
        /// Permission to use name and profile picture from the Yandex account.
        /// </summary>
        /// <remarks>
        /// Requires authorization. Use <see cref="Authorized"/> and <see cref="Authorize"/>.
        /// </remarks>
        public static bool HasProfileDataPermission => CheckProfileDataPermission();

        [DllImport("__Internal")]
        private static extern bool CheckProfileDataPermission();

        #region Authorize
        /// <summary>
        /// Calls a scary authorization window upon the user. Be very afraid.
        /// </summary>
        public static void Authorize(Action onSuccessCallback = null, Action<string> onErrorCallback = null)
        {
            s_onAuthorizeSuccessCallback = onSuccessCallback;
            s_onAuthorizeErrorCallback = onErrorCallback;

            Authorize(OnAuthorizeSuccessCallback, OnAuthorizeErrorCallback);
        }

        [DllImport("__Internal")]
        private static extern void Authorize(Action successCallback, Action<IntPtr, int> errorCallback);

        [MonoPInvokeCallback(typeof(Action))]
        private static void OnAuthorizeSuccessCallback()
        {
            if (YandexGamesSdk.CallbackLogging)
                Debug.Log($"{nameof(PlayerAccount)}.{nameof(OnAuthorizeSuccessCallback)} invoked");

            s_onAuthorizeSuccessCallback?.Invoke();
        }

        [MonoPInvokeCallback(typeof(Action<IntPtr, int>))]
        private static void OnAuthorizeErrorCallback(IntPtr errorMessageBufferPtr, int errorMessageBufferLength)
        {
            string errorMessage = new UnmanagedString(errorMessageBufferPtr, errorMessageBufferLength).ToString();

            if (YandexGamesSdk.CallbackLogging)
                Debug.Log($"{nameof(PlayerAccount)}.{nameof(OnAuthorizeErrorCallback)} invoked, {nameof(errorMessage)} = {errorMessage}");

            s_onAuthorizeErrorCallback?.Invoke(errorMessage);
        }
        #endregion

        #region RequestProfileDataPermission
        /// <summary>
        /// Be aware, if user rejects the request - you're not getting another chance.
        /// </summary>
        /// <remarks>
        /// Requires authorization. Use <see cref="Authorized"/> and <see cref="Authorize"/>.
        /// </remarks>
        public static void RequestProfileDataPermission(Action onSuccessCallback = null, Action<string> onErrorCallback = null)
        {
            s_onRequestProfileDataPermissionSuccessCallback = onSuccessCallback;
            s_onRequestProfileDataPermissionErrorCallback = onErrorCallback;

            RequestProfileDataPermission(OnRequestProfileDataPermissionSuccessCallback, OnRequestProfileDataPermissionErrorCallback);
        }

        [DllImport("__Internal")]
        private static extern void RequestProfileDataPermission(Action successCallback, Action<IntPtr, int> errorCallback);

        [MonoPInvokeCallback(typeof(Action))]
        private static void OnRequestProfileDataPermissionSuccessCallback()
        {
            if (YandexGamesSdk.CallbackLogging)
                Debug.Log($"{nameof(PlayerAccount)}.{nameof(OnRequestProfileDataPermissionSuccessCallback)} invoked");

            s_onRequestProfileDataPermissionSuccessCallback?.Invoke();
        }

        [MonoPInvokeCallback(typeof(Action<IntPtr, int>))]
        private static void OnRequestProfileDataPermissionErrorCallback(IntPtr errorMessageBufferPtr, int errorMessageBufferLength)
        {
            string errorMessage = new UnmanagedString(errorMessageBufferPtr, errorMessageBufferLength).ToString();

            if (YandexGamesSdk.CallbackLogging)
                Debug.Log($"{nameof(PlayerAccount)}.{nameof(OnRequestProfileDataPermissionErrorCallback)} invoked, {nameof(errorMessage)} = {errorMessage}");

            s_onRequestProfileDataPermissionErrorCallback?.Invoke(errorMessage);
        }
        #endregion

        #region GetProfileData
        /// <summary>
        /// Will only return identity unless <see cref="HasProfileDataPermission"/>.
        /// </summary>
        /// <remarks>
        /// Requires authorization. Use <see cref="PlayerAccount.Authorized"/> and <see cref="PlayerAccount.Authorize"/>.
        /// </remarks>
        public static void GetProfileData(Action<LeaderboardEntryResponse> onSuccessCallback, Action<string> onErrorCallback = null)
        {
            s_onGetProfileDataSuccessCallback = onSuccessCallback;
            s_onGetProfileDataErrorCallback = onErrorCallback;

            GetLeaderboardPlayerEntry(OnGetLeaderboardPlayerEntrySuccessCallback, OnGetLeaderboardPlayerEntryErrorCallback);
        }

        [DllImport("__Internal")]
        private static extern void GetLeaderboardPlayerEntry(string leaderboardName, Action<IntPtr, int> successCallback, Action<IntPtr, int> errorCallback);

        [MonoPInvokeCallback(typeof(Action<IntPtr, int>))]
        private static void OnGetLeaderboardPlayerEntrySuccessCallback(IntPtr entryMessageBufferPtr, int entryMessageBufferLength)
        {
            string entryResponseJson = new UnmanagedString(entryMessageBufferPtr, entryMessageBufferLength).ToString();

            if (YandexGamesSdk.CallbackLogging)
                Debug.Log($"{nameof(Leaderboard)}.{nameof(OnGetLeaderboardPlayerEntrySuccessCallback)} invoked, {nameof(entryResponseJson)} = {entryResponseJson}");

            LeaderboardEntryResponse entryResponse = JsonUtility.FromJson<LeaderboardEntryResponse>(entryResponseJson);

            s_onGetPlayerEntrySuccessCallback?.Invoke(entryResponse);
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
