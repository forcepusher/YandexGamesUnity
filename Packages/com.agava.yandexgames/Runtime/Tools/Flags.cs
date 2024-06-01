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

        public static Flag[] flags;


        private static Action<KeyValuePair<string, string>[]> s_onGetFlagsSuccessCallback;
        private static Action<string> s_onGetFlagsErrorCallback;

        public static string GetFlag(string name)
        {
            for (int i = 0; i < flags.Length; i++)
            {
                if (flags[i].name == name)
                    return flags[i].value;
            }
            return null;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="flag"></param>
        /// <returns></returns>
        [DllImport("__Internal")]
        private static extern void GetFlags();


        [MonoPInvokeCallback(typeof(Action<string>))]
        private static void OnGetFlagsSuccessCallback(string flags)
        {
            if (YandexGamesSdk.CallbackLogging)
                Debug.Log($"{nameof(Flags)}.{nameof(OnGetFlagsSuccessCallback)} invoked, {nameof(flags)} = {flags}");

            var dict = Json.Deserialize(flags).ToArray();

            Debug.Log("FirstFlagSuccessKey: " + dict[0].Key);
            Debug.Log("FirstFlagSuccessValue: " + dict[0].Value);
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

            string serializedClientFeatures = "{}";
            string serializedDefaultFlags = "{}";

            if (defaultFlags != null)
                serializedDefaultFlags = Json.Serialize(defaultFlags);
            if (clientFeatures != null)
                serializedClientFeatures = Json.Serialize(clientFeatures);

            GetFlags();
        }



        [DllImport("__Internal")]
        private static extern string FlagsInit_js();

        public static void FlagsInit()
        {
#if !UNITY_EDITOR
            string data = FlagsInit_js();
            Debug.Log("Init Flags inGame");

            JsonFlags jsonFlags = JsonUtility.FromJson<JsonFlags>(FlagsInit_js());
            flags = new Flag[jsonFlags.names.Length];

            for (int i = 0; i < jsonFlags.names.Length; i++)
            {
                flags[i].name = jsonFlags.names[i];
                flags[i].value = jsonFlags.values[i];
            }
#else
            flags = YandexGames.Flags.flags;
#endif
        }

        public struct Flag
        {
            public string name;
            public string value;
        }

        [Serializable]
        private struct JsonFlags
        {
            public string[] names;
            public string[] values;
        }

        #endregion
    }
}
