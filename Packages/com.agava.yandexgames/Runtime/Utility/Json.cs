using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Agava.YandexGames.Utility
{
    public static class Json
    {
        public static string Serialize(IEnumerable<KeyValuePair<string, string>> stringPairsToSerialize)
        {
            var jsonStringBuilder = new StringBuilder();
            jsonStringBuilder.Append('{');

            Debug.Log("Pairs: " + stringPairsToSerialize);

            foreach (KeyValuePair<string, string> pair in stringPairsToSerialize)
            {
                Debug.Log("Key: " + pair.Key);
                Debug.Log("Value: " + pair.Value);
                jsonStringBuilder.Append($"\"{pair.Key}\":\"{pair.Value}\",");
            }

            if (stringPairsToSerialize.Count() > 0)
                jsonStringBuilder.Length -= 1;

            jsonStringBuilder.Append('}');

            Debug.Log("Builder: " + jsonStringBuilder);

            return jsonStringBuilder.ToString();
        }

        public static string SerializeArray(IEnumerable<KeyValuePair<string, string>> stringPairsToSerialize)
        {
            var jsonStringBuilder = new StringBuilder();
            jsonStringBuilder.Append('[');

            foreach (KeyValuePair<string, string> pair in stringPairsToSerialize)
                jsonStringBuilder.Append($"name: \"{pair.Key}\", \"{pair.Value}\"");

            if (stringPairsToSerialize.Count() > 0)
                jsonStringBuilder.Length -= 1;

            jsonStringBuilder.Append(']');


            return jsonStringBuilder.ToString();
        }

        public static IEnumerable<KeyValuePair<string, string>> Deserialize(string jsonData)
        {
            if (string.IsNullOrEmpty(jsonData))
                jsonData = "{}";

            string unparsedData = jsonData.Trim('{', '}');

            var key = new StringBuilder();
            var value = new StringBuilder();

            Dictionary<string, string> dictToReturn = new();

            IterationState iterationState = IterationState.StartingKeyQuote;

            int characterIterator = 0;
            while (characterIterator < unparsedData.Length)
            {
                char character = unparsedData[characterIterator];

                switch (iterationState)
                {
                    case IterationState.StartingKeyQuote:
                        if (character == '"')
                        {
                            iterationState = IterationState.Key;
                        }

                        break;

                    case IterationState.Key:
                        if (character == '"')
                        {
                            iterationState = IterationState.StartingValueQuote;
                        }
                        else
                        {
                            key.Append(character);
                        }

                        break;

                    case IterationState.StartingValueQuote:
                        if (character == '"')
                        {
                            iterationState = IterationState.Value;
                        }

                        break;

                    case IterationState.Value:
                        if (character == '"')
                        {
                            iterationState = IterationState.StartingKeyQuote;

                            PlayerPrefs.WritePrefsKeyValue(key, value);
                            dictToReturn.Add(key.ToString(), value.ToString());
                            key.Clear();
                            value.Clear();
                        }
                        else
                        {
                            value.Append(character);
                        }

                        break;
                }



                characterIterator += 1;
            }
            return dictToReturn;
        }


        enum IterationState
        {
            StartingKeyQuote,
            Key,
            StartingValueQuote,
            Value
        }
    }
}
