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
         #region Flags

		private static Action<KeyValuePair<string, string>[]> s_onGetFlagsSuccessCallback;
        private static Action<string> s_onGetFlagsErrorCallback;

        /// <summary>
        ///
        /// </summary>
        /// <param name="flag"></param>
        /// <returns></returns>
        [DllImport("__Internal")]
        private static extern void FlagsGet(string defaultFlags, string clientFeatures, Action<string> successCallback, Action<string> errorCallback);


        [MonoPInvokeCallback(typeof(Action<string>))]
        private static void OnGetFlagsSuccessCallback(string flags)
        {
            if (YandexGamesSdk.CallbackLogging)
                Debug.Log($"{nameof(Flags)}.{nameof(OnGetFlagsSuccessCallback)} invoked, {nameof(flags)} = {flags}");

            var dict = Json.Deserialize(flags).ToArray();

            s_onGetFlagsSuccessCallback?.Invoke(dict);
        }

        [MonoPInvokeCallback(typeof(Action<string>))]
        private static void OnGetFlagsErrorCallback(string errorMessage)
        {
            if (YandexGamesSdk.CallbackLogging)
                Debug.Log(
                    $"{nameof(Flags)}.{nameof(OnGetFlagsErrorCallback)} invoked, {nameof(errorMessage)} = {errorMessage}");

            s_onGetFlagsErrorCallback?.Invoke(errorMessage);
        }

		public static void Get(Action<KeyValuePair<string, string>[]> onSuccessCallback,
 KeyValuePair<string, string>[] defaultFlags = null, KeyValuePair<string, string>[] clientFeatures = null)
        {
			s_onGetFlagsSuccessCallback = onSuccessCallback;

			var serializedDefaultFlags = Json.Serialize(defaultFlags);
			var serializedClientFeatures = Json.Serialize(clientFeatures);

			FlagsGet(serializedDefaultFlags, serializedClientFeatures, OnGetFlagsSuccessCallback, OnGetFlagsErrorCallback);
        }
        #endregion
    }
}
