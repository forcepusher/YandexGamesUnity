using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agava.YandexGames;
using Agava.YandexGames.Utility;

namespace Utility
{
    public static class FlagsUtility
    {
        public static void GetFlagsCollection(Action<IDictionary<string, string>> onSuccessCallback, IDictionary<string, string> defaultFlags = null,
            IDictionary<string, string> clientFeatures = null)
        {
            Flags.Get(flagsJson =>
            {
                var flags = Json.Deserialize(flagsJson);
                onSuccessCallback.Invoke(new Dictionary<string, string>(flags));
            }, SerializeArray(clientFeatures), Json.Serialize(defaultFlags));
        }

        public static string SerializeArray(IEnumerable<KeyValuePair<string, string>> stringPairsToSerialize)
        {
            if (stringPairsToSerialize == null)
                return "[]";

            var jsonStringBuilder = new StringBuilder();
            jsonStringBuilder.Append('[');

            foreach (KeyValuePair<string, string> pair in stringPairsToSerialize)
                jsonStringBuilder.Append($"{{name: \"{pair.Key}\", value: \"{pair.Value}\"}},");

            if (stringPairsToSerialize.Count() > 0)
                jsonStringBuilder.Length -= 1;

            jsonStringBuilder.Append(']');


            return jsonStringBuilder.ToString();
        }
    }
}
