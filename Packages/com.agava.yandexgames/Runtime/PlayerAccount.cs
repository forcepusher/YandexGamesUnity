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
        private static Action s_onGetProfileDataPermissionSuccessCallback;
        private static Action<string> s_onGetProfileDataPermissionErrorCallback;

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

        /// <remarks>
        /// Requires authorization. Use <see cref="IsAuthorized"/> and <see cref="Authorize"/>.
        /// </remarks>
        public static void GetProfileDataPermission(Action onSuccessCallback = null, Action<string> onErrorCallback = null)
        {
            s_onGetProfileDataPermissionSuccessCallback = onSuccessCallback;
            s_onGetProfileDataPermissionErrorCallback = onErrorCallback;

            GetProfileDataPermission(OnGetProfileDataPermissionSuccessCallback, OnGetProfileDataPermissionErrorCallback);
        }

        [DllImport("__Internal")]
        private static extern void GetProfileDataPermission(Action successCallback, Action<IntPtr, int> errorCallback);

        [MonoPInvokeCallback(typeof(Action))]
        private static void OnGetProfileDataPermissionSuccessCallback()
        {
            if (YandexGamesSdk.CallbackLogging)
                Debug.Log($"{nameof(PlayerAccount)}.{nameof(OnGetProfileDataPermissionSuccessCallback)} invoked");

            s_onGetProfileDataPermissionSuccessCallback?.Invoke();
        }

        [MonoPInvokeCallback(typeof(Action<IntPtr, int>))]
        private static void OnGetProfileDataPermissionErrorCallback(IntPtr errorMessageBufferPtr, int errorMessageBufferLength)
        {
            string errorMessage = new UnmanagedString(errorMessageBufferPtr, errorMessageBufferLength).ToString();

            if (YandexGamesSdk.CallbackLogging)
                Debug.Log($"{nameof(PlayerAccount)}.{nameof(OnGetProfileDataPermissionErrorCallback)} invoked, {nameof(errorMessage)} = {errorMessage}");

            s_onGetProfileDataPermissionErrorCallback?.Invoke(errorMessage);
        }
    }
}
