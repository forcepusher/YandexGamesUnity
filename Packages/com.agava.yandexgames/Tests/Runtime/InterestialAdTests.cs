using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace YandexGames.Tests
{
    public class InterestialAdTests
    {
        [Test]
        public void ShouldInvokeErrorCallback()
        {
            InterestialAd.Show(onErrorCallback: (message) => Assert.Pass());
        }
    }
}
