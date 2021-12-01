using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace YandexGames.Tests
{
    public class YandexGamesSdkTests
    {
        private const float InitializationTimeoutSeconds = 5;

        [UnityTest]
        public IEnumerator ShouldInitializeAutomatically()
        {
            float waitTime = 0;
            while (waitTime <= InitializationTimeoutSeconds)
            {
                if (YandexGamesSdk.IsInitialized)
                {
                    Assert.Pass($"Initialized in {waitTime} seconds.");
                    yield break;
                }

                yield return null;
                waitTime += Time.unscaledDeltaTime;
            }

            Assert.Fail($"Failed to initialize in {InitializationTimeoutSeconds} seconds.");
        }
    }
}
