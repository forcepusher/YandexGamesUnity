using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace YandexGames.Tests
{
    public class LeaderboardTests
    {
        [Test]
        public void ShouldInitializeAutomatically()
        {
            Assert.IsTrue(Leaderboard.IsInitialized);
        }

        [Test]
        public void SetScoreShouldNotThrow()
        {
            Assert.DoesNotThrow(() => Leaderboard.SetScore("NonExistingBoard", 228));
            Assert.DoesNotThrow(() => Leaderboard.SetScore("NonExistingBoard", 0, additionalData: "henlo"));
        }

        [UnityTest]
        public IEnumerator SetScoreShouldInvokeErrorCallback()
        {
            bool callbackInvoked = false;
            Leaderboard.SetScore("NonExistingBoard", 228, onErrorCallback: (message) => {
                callbackInvoked = true;
            });

            yield return new WaitForSecondsRealtime(1);

            Assert.IsTrue(callbackInvoked);
        }
    }
}
