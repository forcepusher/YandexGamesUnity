using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace Agava.YandexGames
{
    [Serializable]
    public class GetPurchasedProductsResponse
    {
        [field: Preserve]
        public PurchasedProductResponse[] purchasedProducts;

        public static GetPurchasedProductsResponse ParseJson(string json)
        {
            return JsonUtility.FromJson<GetPurchasedProductsResponse>(WrapArrayJsonIntoObjectJson(json));
        }

        private static string WrapArrayJsonIntoObjectJson(string arrayJson)
        {
            return "{\"purchasedProducts\":" + arrayJson + "}";
        }
    }
}
