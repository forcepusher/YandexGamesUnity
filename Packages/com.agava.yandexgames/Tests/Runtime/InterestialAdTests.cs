using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace YandexGames.Tests
{
    public class InterestialAdTests
    {
        [UnityTest]
        public IEnumerator InterestialAdShouldInvokeErrorCallback()
        {
            while (!YandexGamesSdk.VerifyInitialization())
                yield return null;

            bool callbackInvoked = false;
            InterestialAd.Show(onErrorCallback: (message) => {
                callbackInvoked = true;
            });

            yield return new WaitForSecondsRealtime(1);

            Assert.IsTrue(callbackInvoked);
        }
    }
}
