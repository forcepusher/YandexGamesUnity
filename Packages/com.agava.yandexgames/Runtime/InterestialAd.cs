using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using AOT;

namespace YandexGames
{
    public class InterestialAd
    {
        public void Show()
        {
            ShowInterestialAd(OnOpenCallback, OnCloseCallback, OnErrorCallback, OnOfflineCallback);
        }

        [DllImport("__Internal")]
        private static extern bool ShowInterestialAd(Action openCallback, Action<bool> closeCallback, Action<IntPtr, int> errorCallback, Action offlineCallback);

        [MonoPInvokeCallback(typeof(Action))]
        private static void OnOpenCallback()
        {
            UnityEngine.Debug.Log("OnOpenCallback");
        }

        [MonoPInvokeCallback(typeof(Action<bool>))]
        private static void OnCloseCallback(bool wasShown)
        {
            UnityEngine.Debug.Log("OnCloseCallback " + wasShown);
        }

        [MonoPInvokeCallback(typeof(Action<IntPtr, int>))]
        private static void OnErrorCallback(IntPtr errorMessage, int messageByteLength)
        {
            UnityEngine.Debug.Log("OnErrorCallback");
        }

        [MonoPInvokeCallback(typeof(Action))]
        private static void OnOfflineCallback()
        {
            UnityEngine.Debug.Log("OnOfflineCallback");
        }
    }
}
