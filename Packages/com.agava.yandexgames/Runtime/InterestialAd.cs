using System;
using System.Runtime.InteropServices;
using AOT;

namespace YandexGames
{
    public class InterestialAd
    {
        public void Show()
        {
            ShowInterestialAd();
        }

        [DllImport("__Internal")]
        private static extern bool ShowInterestialAd();

        [MonoPInvokeCallback(typeof(Action<bool>))]
        private static void OnCloseCallBack(bool wasShown)
        {
            
        }

        [MonoPInvokeCallback(typeof(Action))]
        private static void OnOpenCallBack()
        {

        }

        [MonoPInvokeCallback(typeof(Action<string>))]
        private static void OnErrorCallBack(string errorMessage)
        {

        }

        [MonoPInvokeCallback(typeof(Action))]
        private static void OnOfflineCallBack()
        {

        }
    }
}
