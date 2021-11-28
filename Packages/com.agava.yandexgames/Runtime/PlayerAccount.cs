using System;
using System.Runtime.InteropServices;
using AOT;
using UnityEngine;

namespace YandexGames
{
    /// <summary>
    /// Proxy for player-related methods in YandexGames SDK.
    /// </summary>
    public static class PlayerAccount
    {
        private static Action s_onAuthenticatedCallback;

        public static void Authenticate(bool requestPermissions, Action onAuthenticatedCallback = null)
        {
            s_onAuthenticatedCallback = onAuthenticatedCallback;

            AuthenticatePlayerAccount(requestPermissions, OnAuthenticatedCallback);
        }

        [DllImport("__Internal")]
        private static extern void AuthenticatePlayerAccount(bool requestPermissions, Action onAuthenticatedCallback);

        [MonoPInvokeCallback(typeof(Action))]
        private static void OnAuthenticatedCallback()
        {
            if (YandexGamesSdk.CallbackLogging)
                Debug.Log($"{nameof(PlayerAccount)}.{nameof(OnAuthenticatedCallback)} invoked");

            s_onAuthenticatedCallback?.Invoke();
        }
    }
}
