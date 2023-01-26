using System;
using System.Runtime.InteropServices;
using AOT;
using UnityEngine;

namespace Agava.YandexGames
{
    public static class Billing
    {
        private static Action s_onSuccessCallback;
        private static Action<string> s_onErrorCallback;

        public static void Purchase(string productId, Action onSuccessCallback = null, Action<string> onErrorCallback = null, string developerPayload = "")
        {
            s_onSuccessCallback = onSuccessCallback;
            s_onErrorCallback = onErrorCallback;

            BillingPurchase(productId, OnSuccessCallback, OnErrorCallback, developerPayload);
        }

        [DllImport("__Internal")]
        private static extern void BillingPurchase(string productId, Action successCallback, Action<string> errorCallback, string developerPayload);

        [MonoPInvokeCallback(typeof(Action))]
        private static void OnSuccessCallback()
        {
            if (YandexGamesSdk.CallbackLogging)
                Debug.Log($"{nameof(Billing)}.{nameof(OnSuccessCallback)} invoked");

            s_onSuccessCallback?.Invoke();
        }

        [MonoPInvokeCallback(typeof(Action<string>))]
        private static void OnErrorCallback(string errorMessage)
        {
            if (YandexGamesSdk.CallbackLogging)
                Debug.Log($"{nameof(Billing)}.{nameof(OnErrorCallback)} invoked, {nameof(errorMessage)} = {errorMessage}");

            s_onErrorCallback?.Invoke(errorMessage);
        }
    }
}
