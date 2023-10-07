using System;
using System.Runtime.InteropServices;
using AOT;
using UnityEngine;

namespace Agava.YandexGames
{
    public class ReviewPopup
    {
        private static Action<bool, string> s_onCanRequestReviewCallback;
        private static Action<bool> s_onRequestReviewCallback;

        public static void CanRequestReview(Action<bool, string> onResultCallback)
        {
            s_onCanRequestReviewCallback = onResultCallback;

            ReviewPopupCanRequestReview(OnCanRequestReviewCallback);
        }

        [DllImport("__Internal")]
        private static extern void ReviewPopupCanRequestReview(Action<bool, string> onResultCallback);

        [MonoPInvokeCallback(typeof(Action<bool, string>))]
        private static void OnCanRequestReviewCallback(bool result, string reason)
        {
            if (YandexGamesSdk.CallbackLogging)
                Debug.Log($"{nameof(Shortcut)}.{nameof(OnCanRequestReviewCallback)} called. {nameof(result)}={result} {nameof(reason)}={reason}");

            s_onCanRequestReviewCallback?.Invoke(result, reason);
        }

        public static void RequestReview(Action<bool> onResultCallback = null)
        {
            s_onRequestReviewCallback = onResultCallback;

            ReviewPopupRequestReview(OnRequestReviewCallback);
        }

        [DllImport("__Internal")]
        private static extern void ReviewPopupRequestReview(Action<bool> onResultCallback);

        [MonoPInvokeCallback(typeof(Action<bool>))]
        private static void OnRequestReviewCallback(bool result)
        {
            if (YandexGamesSdk.CallbackLogging)
                Debug.Log($"{nameof(Shortcut)}.{nameof(OnRequestReviewCallback)} called. {nameof(result)}={result}");

            s_onRequestReviewCallback?.Invoke(result);
        }
    }
}
