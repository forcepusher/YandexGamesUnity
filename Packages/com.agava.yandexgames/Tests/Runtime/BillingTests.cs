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
    }
}
