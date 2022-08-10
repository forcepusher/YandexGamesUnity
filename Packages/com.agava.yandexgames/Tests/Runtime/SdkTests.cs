using System.Collections;
using Agava.YandexGames;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace YandexGames.Tests
{
    public class SdkTests
    {
        [UnitySetUp]
        public IEnumerator InitializeSdk()
        {
            if (!YandexGamesSdk.IsInitialized)
                yield return YandexGamesSdk.Initialize();
        }

        [Test]
        public void ShouldReturnEnvironment()
        {
            Assert.IsNotEmpty(YandexGamesSdk.Environment.browser.lang);
        }
    }
}
