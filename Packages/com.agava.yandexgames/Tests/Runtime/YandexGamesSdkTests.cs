using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace YandexGames.Tests
{
    public class YandexGamesSdkTests
    {
        [UnityTest]
        public IEnumerator SdkShouldInitializeAutomatically()
        {
            // Needs a couple seconds to download the script.
            yield return new WaitForSecondsRealtime(3f);
            Assert.IsTrue(YandexGamesSdk.VerifyInitialization());
        }
    }
}
