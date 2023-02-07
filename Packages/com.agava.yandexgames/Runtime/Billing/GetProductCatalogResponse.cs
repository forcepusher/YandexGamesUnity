using System;
using UnityEngine.Scripting;

namespace Agava.YandexGames
{
    [Serializable]
    public class GetProductCatalogResponse
    {
        [field: Preserve]
        public ProductResponse[] products;

        public static string WrapJson(string arrayJson)
        {
            return "{\"products\":" + arrayJson + "}";
        }
    }
}
