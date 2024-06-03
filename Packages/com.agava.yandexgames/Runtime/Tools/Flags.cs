using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Agava.YandexGames.Utility;
using AOT;
using UnityEngine;

namespace Agava.YandexGames
{
    public static class Flags
    {
        private static Action<string> s_onGetFlagsSuccessCallback;
        private static Action<string> s_onGetFlagsErrorCallback;

        [DllImport("__Internal")]
        private static extern void GetFlags(string defaultFlags, string clientFeatures, Action<string> onResultSuccess, Action<string> onErrorCallback);


        [MonoPInvokeCallback(typeof(Action<string>))]
        private static void OnGetFlagsSuccessCallback(string flags)
        {
            if (YandexGamesSdk.CallbackLogging)
                Debug.Log($"{nameof(Flags)}.{nameof(OnGetFlagsSuccessCallback)} invoked, {nameof(flags)} = {flags}");

            s_onGetFlagsSuccessCallback?.Invoke(flags);
        }

        [MonoPInvokeCallback(typeof(Action<string>))]
        private static void OnGetFlagsErrorCallback(string errorMessage)
        {
            if (YandexGamesSdk.CallbackLogging)
                Debug.Log(
                    $"{nameof(Flags)}.{nameof(OnGetFlagsErrorCallback)} invoked, {nameof(errorMessage)} = {errorMessage}");

            s_onGetFlagsErrorCallback?.Invoke(errorMessage);
        }

        public static void Get(Action<string> onSuccessCallback, string clientFeatures = null, string defaultFlags = null, Action<string> onErrorCallback = null)
        {
            s_onGetFlagsSuccessCallback = onSuccessCallback;
            s_onGetFlagsErrorCallback = onErrorCallback;

            if (clientFeatures == null)
                clientFeatures = "{}";
            if (defaultFlags == null)
                defaultFlags = "[]";

            GetFlags(defaultFlags, clientFeatures, OnGetFlagsSuccessCallback, OnGetFlagsErrorCallback);
        }
    }
}
