using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace YG
{
    public partial class YandexGame
    {
        public static Flag[] flags;

        [DllImport("__Internal")]
        private static extern string FlagsInit_js();

        [InitYG]
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
            flags = Instance.infoYG.flags;
#endif
        }

        public static string GetFlag(string name)
        {
            for (int i = 0; i < flags.Length; i++)
            {
                if (flags[i].name == name)
                    return flags[i].value;
            }
            return null;
        }

        [Serializable]
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
    }
}