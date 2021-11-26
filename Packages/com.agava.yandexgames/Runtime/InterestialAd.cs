using System;
using System.Runtime.InteropServices;
using System.Text;
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
        private static void OnErrorCallback(IntPtr errorMessageBufferPtr, int errorMessageBufferLength)
        {
            byte[] errorMessageBuffer = new byte[errorMessageBufferLength];
            Marshal.Copy(errorMessageBufferPtr, errorMessageBuffer, 0, errorMessageBufferLength);
            string errorMessage = Encoding.UTF8.GetString(errorMessageBuffer);
            UnityEngine.Debug.Log("OnErrorCallback " + errorMessage);
        }

        [MonoPInvokeCallback(typeof(Action))]
        private static void OnOfflineCallback()
        {
            UnityEngine.Debug.Log("OnOfflineCallback");
        }
    }
}
