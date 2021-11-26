using System;
using System.Runtime.InteropServices;
using System.Text;
using AOT;
using UnityEngine;

namespace YandexGames
{
    /// <summary>
    /// Proxy for ysdk.adv.showRewardedVideo() method in YandexGames SDK.
    /// </summary>
    /// <remarks>
    /// Normally "VideoAd" class should not be static,
    /// but a proxy class has to match the SDK logic.
    /// </remarks>
    public static class VideoAd
    {
        // Mutable static fields. Disgusting.
        private static Action s_onOpenCallback;
        private static Action s_onRewardedCallback;
        private static Action s_onCloseCallback;
        private static Action<string> s_onErrorCallback;

        public static void Show(Action onOpenCallback = null, Action onRewardedCallback = null,
            Action onCloseCallback = null, Action<string> onErrorCallback = null)
        {
            // You should be ashamed of yourself.
            s_onOpenCallback = onOpenCallback;
            s_onRewardedCallback = onRewardedCallback;
            s_onCloseCallback = onCloseCallback;
            s_onErrorCallback = onErrorCallback;

            ShowVideoAd(OnOpenCallback, OnRewardedCallback, OnCloseCallback, OnErrorCallback);
        }

        [DllImport("__Internal")]
        private static extern bool ShowVideoAd(Action openCallback, Action rewardedCallback, Action closeCallback, Action<IntPtr, int> errorCallback);

        [MonoPInvokeCallback(typeof(Action))]
        private static void OnOpenCallback()
        {
            if (YandexGamesSdk.CallbackLogging)
                Debug.Log("OnOpenCallback invoked");

            s_onOpenCallback?.Invoke();
        }

        [MonoPInvokeCallback(typeof(Action))]
        private static void OnRewardedCallback()
        {
            if (YandexGamesSdk.CallbackLogging)
                Debug.Log("OnRewardedCallback invoked");

            s_onRewardedCallback?.Invoke();
        }

        [MonoPInvokeCallback(typeof(Action))]
        private static void OnCloseCallback()
        {
            if (YandexGamesSdk.CallbackLogging)
                Debug.Log("OnCloseCallback invoked");

            s_onCloseCallback?.Invoke();
        }

        [MonoPInvokeCallback(typeof(Action<IntPtr, int>))]
        private static void OnErrorCallback(IntPtr errorMessageBufferPtr, int errorMessageBufferLength)
        {
            byte[] errorMessageBuffer = new byte[errorMessageBufferLength];
            Marshal.Copy(errorMessageBufferPtr, errorMessageBuffer, 0, errorMessageBufferLength);
            string errorMessage = Encoding.UTF8.GetString(errorMessageBuffer);

            if (YandexGamesSdk.CallbackLogging)
                Debug.Log("OnErrorCallback invoked, errorMessage = " + errorMessage);

            s_onErrorCallback?.Invoke(errorMessage);
        }
    }
}
