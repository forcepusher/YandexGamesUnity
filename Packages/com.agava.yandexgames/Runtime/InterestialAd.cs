using System;
using System.Runtime.InteropServices;
using System.Text;
using AOT;
using UnityEngine;

namespace YandexGames
{
    /// <summary>
    /// Proxy for ysdk.adv.showFullscreenAdv() method in YandexGames SDK.
    /// </summary>
    /// <remarks>
    /// Normally "InterestialAd" class should not be static,
    /// but a proxy class has to match the SDK logic.
    /// </remarks>
    public static class InterestialAd
    {
        // Mutable static fields. Disgusting.
        private static Action s_onOpenCallback;
        private static Action<bool> s_onCloseCallback;
        private static Action<string> s_onErrorCallback;
        private static Action s_onOfflineCallback;

        public static void Show(Action onOpenCallback = null, Action<bool> onCloseCallback = null,
            Action<string> onErrorCallback = null, Action onOfflineCallback = null)
        {
            // You should be ashamed of yourself.
            s_onOpenCallback = onOpenCallback;
            s_onCloseCallback = onCloseCallback;
            s_onErrorCallback = onErrorCallback;
            s_onOfflineCallback = onOfflineCallback;

            ShowInterestialAd(OnOpenCallback, OnCloseCallback, OnErrorCallback, OnOfflineCallback);
        }

        [DllImport("__Internal")]
        private static extern bool ShowInterestialAd(Action openCallback, Action<bool> closeCallback, Action<IntPtr, int> errorCallback, Action offlineCallback);

        [MonoPInvokeCallback(typeof(Action))]
        private static void OnOpenCallback()
        {
            if (YandexGamesSdk.CallbackLogging)
                Debug.Log($"{nameof(InterestialAd)}.{nameof(OnOpenCallback)} invoked");

            s_onOpenCallback?.Invoke();
        }

        [MonoPInvokeCallback(typeof(Action<bool>))]
        private static void OnCloseCallback(bool wasShown)
        {
            if (YandexGamesSdk.CallbackLogging)
                Debug.Log($"{nameof(InterestialAd)}.{nameof(OnCloseCallback)} invoked, wasShown = {wasShown}");

            s_onCloseCallback?.Invoke(wasShown);
        }

        [MonoPInvokeCallback(typeof(Action<IntPtr, int>))]
        private static void OnErrorCallback(IntPtr errorMessageBufferPtr, int errorMessageBufferLength)
        {
            byte[] errorMessageBuffer = new byte[errorMessageBufferLength];
            Marshal.Copy(errorMessageBufferPtr, errorMessageBuffer, 0, errorMessageBufferLength);
            string errorMessage = Encoding.UTF8.GetString(errorMessageBuffer);

            if (YandexGamesSdk.CallbackLogging)
                Debug.Log($"{nameof(InterestialAd)}.{nameof(OnErrorCallback)} invoked, errorMessage = {errorMessage}");

            s_onErrorCallback?.Invoke(errorMessage);
        }

        [MonoPInvokeCallback(typeof(Action))]
        private static void OnOfflineCallback()
        {
            if (YandexGamesSdk.CallbackLogging)
                Debug.Log($"{nameof(InterestialAd)}.{nameof(OnOfflineCallback)} invoked");

            s_onOfflineCallback?.Invoke();
        }
    }
}
