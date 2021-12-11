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
        private static Action s_onRequestProfileDataPermissionSuccessCallback;
        private static Action<string> s_onRequestProfileDataPermissionErrorCallback;

        /// <summary>
        /// Use this before calling SDK methods that require authorization,
        /// so you can avoid unexpected authorization window popups.
        /// </summary>
        public static bool IsAuthorized => VerifyPlayerAccountAuthorization();

        [DllImport("__Internal")]
        private static extern bool VerifyPlayerAccountAuthorization();

        public static void Authorize()
        {

        }

        #region HasProfileDataPermission

        #endregion

        #region RequestProfileDataPermission
        /// <remarks>
        /// Requires authorization. Use <see cref="IsAuthorized"/> and <see cref="Authorize"/>.
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
    }
}
