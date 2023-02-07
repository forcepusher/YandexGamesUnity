using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Agava.YandexGames.Tests
{
    public class BillingTests
    {
        [UnitySetUp]
        public IEnumerator InitializeSdk()
        {
            if (!YandexGamesSdk.IsInitialized)
                yield return YandexGamesSdk.Initialize(SdkTests.TrackSuccessCallback);
        }

        [UnityTest]
        public IEnumerator PurchaseShouldInvokeErrorCallback()
        {
            bool callbackInvoked = false;
            Billing.PurchaseProduct("adsfjoisadjfojds", onErrorCallback: (message) =>
            {
                callbackInvoked = true;
            });

            yield return new WaitForSecondsRealtime(1);

            Assert.IsTrue(callbackInvoked);
        }

        [Test]
        public void GetProductCatalogResponseParsingTest()
        {
            //[{ "id":"TestProduct","title":"Тестлол","description":"","imageURI":"/default256x256","price":"1 YAN","priceValue":"1","priceCurrencyCode":"YAN"},{ "id":"AnotherTestProduct","title":"Желешечка","description":"","imageURI":"https://avatars.mds.yandex.net/get-games/2977039/2a0000018627c05340c1234f5ceb18517812//default256x256","price":"4 YAN","priceValue":"4","priceCurrencyCode":"YAN"}]
            string responseSampleJson = "[{ \"id\":\"TestProduct\",\"title\":\"Тестлол\",\"description\":\"\",\"imageURI\":\"/default256x256\",\"price\":\"1 YAN\",\"priceValue\":\"1\",\"priceCurrencyCode\":\"YAN\"},{ \"id\":\"AnotherTestProduct\",\"title\":\"Желешечка\",\"description\":\"\",\"imageURI\":\"https://avatars.mds.yandex.net/get-games/2977039/2a0000018627c05340c1234f5ceb18517812//default256x256\",\"price\":\"4 YAN\",\"priceValue\":\"4\",\"priceCurrencyCode\":\"YAN\"}]";
            
            GetProductCatalogResponse response = GetProductCatalogResponse.ParseJson(responseSampleJson);
            Assert.IsNotEmpty(response.products);
        }

        [Test]
        public void GetPurchasedProductsResponseParsingTest()
        {
            //[{"productID":"TestProduct","purchaseTime":0,"purchaseToken":"070f028a-90d9-42da-95c4-53ef988bfe98"},{"productID":"TestProduct","purchaseTime":0,"purchaseToken":"a7afeaac-a694-4d39-a2eb-583f1bc19b1b"},{"productID":"TestProduct","purchaseTime":0,"purchaseToken":"4b512664-a2f9-4906-ad26-66db76a69b82"}]
            string responseSampleJson = "[{\"productID\":\"TestProduct\",\"purchaseTime\":0,\"purchaseToken\":\"070f028a-90d9-42da-95c4-53ef988bfe98\"},{\"productID\":\"TestProduct\",\"purchaseTime\":0,\"purchaseToken\":\"a7afeaac-a694-4d39-a2eb-583f1bc19b1b\"},{\"productID\":\"TestProduct\",\"purchaseTime\":0,\"purchaseToken\":\"4b512664-a2f9-4906-ad26-66db76a69b82\"}]";

            GetPurchasedProductsResponse response = GetPurchasedProductsResponse.ParseJson(responseSampleJson);
            Assert.IsNotEmpty(response.purchasedProducts);
        }
    }
}
