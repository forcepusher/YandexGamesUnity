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

            ReviewPopupCanRequestReview(CanRequestReviewCallback);
        }

        [DllImport("__Internal")]
        private static extern void ReviewPopupCanRequestReview(Action<bool, string> onResultCallback);

        [MonoPInvokeCallback(typeof(Action<bool, string>))]
        private static void CanRequestReviewCallback(bool canRequestReview, string reason)
        {
            if (YandexGamesSdk.CallbackLogging)
                Debug.Log($"{nameof(Shortcut)}.{nameof(CanRequestReviewCallback)} called. {nameof(canRequestReview)}={canRequestReview} {nameof(reason)}={reason}");

            s_onCanRequestReviewCallback.Invoke(canRequestReview, reason);
        }

        public static void RequestReview(Action<bool> onResultCallback)
        {
            s_onRequestReviewCallback = onResultCallback;

            ReviewPopupRequestReview(RequestReviewCallback);
        }

        [DllImport("__Internal")]
        private static extern void ReviewPopupRequestReview(Action<bool> onResultCallback);

        [MonoPInvokeCallback(typeof(Action<bool>))]
        private static void RequestReviewCallback(bool reviewResult)
        {
            if (YandexGamesSdk.CallbackLogging)
                Debug.Log($"{nameof(Shortcut)}.{nameof(RequestReviewCallback)} called. {nameof(reviewResult)}={reviewResult}");

            s_onRequestReviewCallback.Invoke(reviewResult);
        }
    }
}
