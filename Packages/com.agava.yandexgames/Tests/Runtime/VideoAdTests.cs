using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace YandexGames.Tests
{
    public class VideoAdTests
    {
        [UnityTest]
        public IEnumerator ShouldInvokeErrorCallback()
        {
            while (!YandexGamesSdk.VerifyInitialization())
                yield return null;

            VideoAd.Show(onErrorCallback: (message) => Assert.Pass());
            yield return new WaitForSecondsRealtime(1);
            Assert.Fail();
        }
    }
}
