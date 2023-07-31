using System;
using System.Collections.Generic;
using System.Text;

namespace Agava.YandexGames
{
    public static class PlayerPrefs
    {
        private static Dictionary<string, string> _prefs = new Dictionary<string, string>();
        private static Action<string> s_onLoadSuccessCallback;
        private static Action<string> s_onLoadErrorCallback;

        private static void Save()
        {
            var jsonStringBuilder = new StringBuilder();
            jsonStringBuilder.Append('{');

            foreach (KeyValuePair<string, string> pref in _prefs)
            {
                jsonStringBuilder.Append($"\"{pref.Key}\": \"{pref.Value}\", ");
            }

            if (_prefs.Count > 0)
                jsonStringBuilder.Length -= 2;

            jsonStringBuilder.Append('}');

            string jsonData = jsonStringBuilder.ToString();
            PlayerAccount.SetPlayerData(jsonData);
        }

        public static void Load(Action<string> onSuccessCallback = null, Action<string> onErrorCallback = null)
        {
            s_onLoadSuccessCallback = onSuccessCallback;
            s_onLoadErrorCallback = onErrorCallback;

            PlayerAccount.GetPlayerData(OnLoadSuccessCallback, OnLoadErrorCallback);
        }

        private static void OnLoadSuccessCallback(string jsonData)
        {
            if (!string.IsNullOrEmpty(jsonData))
            {
                var jsonDict = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonData);

                _prefs.Clear();
                foreach (KeyValuePair<string, string> pref in jsonDict)
                {
                    _prefs[pref.Key] = pref.Value;
                }
            }

            s_onLoadSuccessCallback?.Invoke(jsonData);
        }

        private static void OnLoadErrorCallback(string errorMessage)
        {
            s_onLoadErrorCallback?.Invoke(errorMessage);
        }

        public static bool HasKey(string key)
        {
            if (_prefs.Count == 0)
                Load();

            return _prefs.ContainsKey(key);
        }

        public static void DeleteKey(string key)
        {
            if (_prefs.Count == 0)
                Load();

            if (_prefs.Remove(key))
                Save();
        }

        public static void DeleteAll()
        {
            _prefs.Clear();
            Save();
        }

        public static void SetString(string key, string value)
        {
            if (_prefs.Count == 0)
                Load();

            if (_prefs.ContainsKey(key))
                _prefs[key] = value;
            else
                _prefs.Add(key, value);

            Save();
        }

        public static string GetString(string key, string defaultValue)
        {
            if (_prefs.Count == 0)
                Load();

            if (_prefs.ContainsKey(key))
            {
                return _prefs[key];
            }
            else
            {
                SetString(key, defaultValue);
                return defaultValue;
            }
        }

        public static string GetString(string key)
        {
            string defaultValue = "";
            return GetString(key, defaultValue);
        }

        public static void SetInt(string key, int value)
        {
            if (_prefs.Count == 0)
                Load();

            if (_prefs.ContainsKey(key))
                _prefs[key] = value.ToString();
            else
                _prefs.Add(key, value.ToString());

            Save();
        }

        public static int GetInt(string key, int defaultValue)
        {
            if (_prefs.Count == 0)
                Load();

            int value;

            if (_prefs.ContainsKey(key) && int.TryParse(_prefs[key], out value))
            {
                return value;
            }
            else
            {
                SetInt(key, defaultValue);
                return defaultValue;
            }
        }

        public static int GetInt(string key)
        {
            int defaultValue = 0;
            return GetInt(key, defaultValue);
        }

        public static void SetFloat(string key, float value)
        {
            if (_prefs.Count == 0)
                Load();

            if (_prefs.ContainsKey(key))
                _prefs[key] = value.ToString();
            else
                _prefs.Add(key, value.ToString());

            Save();
        }

        public static float GetFloat(string key, float defaultValue)
        {
            if (_prefs.Count == 0)
                Load();

            float value;

            if (_prefs.ContainsKey(key) && float.TryParse(_prefs[key], out value))
            {
                return value;
            }
            else
            {
                SetFloat(key, defaultValue);
                return defaultValue;
            }
        }

        public static float GetFloat(string key)
        {
            float defaultValue = 0;
            return GetFloat(key, defaultValue);
        }
    }
}
