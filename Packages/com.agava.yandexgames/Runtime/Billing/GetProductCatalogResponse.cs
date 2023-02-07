using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace Agava.YandexGames
{
    [Serializable]
    public class GetProductCatalogResponse
    {
        [field: Preserve]
        public ProductResponse[] products;

        public static GetProductCatalogResponse ParseJson(string json)
        {
            return JsonUtility.FromJson<GetProductCatalogResponse>(WrapArrayJsonIntoObjectJson(json));
        }

        private static string WrapArrayJsonIntoObjectJson(string arrayJson)
        {
            return "{\"products\":" + arrayJson + "}";
        }
    }
}
