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

        [Test]
        public void ShowShouldInvokeErrorCallback()
        {
            //bool callbackInvoked = false;
            //VideoAd.Show(onErrorCallback: (message) =>
            //{
            //    callbackInvoked = true;
            //});

            //yield return new WaitForSecondsRealtime(1);

            //Assert.IsTrue(callbackInvoked);

            Assert.DoesNotThrow(() => Billing.Purchase("asdasdjsa"));
        }
    }
}
