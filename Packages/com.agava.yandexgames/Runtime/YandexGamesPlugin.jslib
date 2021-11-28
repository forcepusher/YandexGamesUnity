const library = {

  // Class definition.

  $yandexGames: {
    sdk: undefined,

    leaderboard: undefined,

    playerAccount: undefined,

    initialize: function () {
      const sdkScript = document.createElement('script');
      sdkScript.src = 'https://yandex.ru/games/sdk/v2';
      document.head.appendChild(sdkScript);

      sdkScript.onload = function () {
        window['YaGames'].init().then(function (sdk) {
          yandexGames.sdk = sdk;
          sdk.getLeaderboards().then(function (leaderboard) { yandexGames.leaderboard = leaderboard; });
        });
      }
    },

    verifySdkInitialization: function () {
      return yandexGames.sdk !== undefined;
    },

    verifyLeaderboardServiceInitialization: function () {
      return yandexGames.leaderboard !== undefined;
    },

    authenticatePlayerAccount: function (requestPermissions, onAuthenticatedCallbackPtr) {
      yandexGames.sdk.getPlayer({ scopes: requestPermissions }).then(function (playerAccount) {
        yandexGames.playerAccount = playerAccount;
        console.log(playerAccount);
        dynCall('v', onAuthenticatedCallbackPtr, []);
      });
    },

    showInterestialAd: function (openCallbackPtr, closeCallbackPtr, errorCallbackPtr, offlineCallbackPtr) {
      yandexGames.sdk.adv.showFullscreenAdv({
        callbacks: {
          onOpen: function () {
            dynCall('v', openCallbackPtr, []);
          },
          onClose: function (wasShown) {
            dynCall('vi', closeCallbackPtr, [wasShown]);
          },
          onError: function (error) {
            yandexGames.invokeErrorCallback(error, errorCallbackPtr);
          },
          onOffline: function () {
            dynCall('v', offlineCallbackPtr, []);
          },
        }
      });
    },

    showVideoAd: function (openCallbackPtr, rewardedCallbackPtr, closeCallbackPtr, errorCallbackPtr) {
      yandexGames.sdk.adv.showRewardedVideo({
        callbacks: {
          onOpen: function () {
            dynCall('v', openCallbackPtr, []);
          },
          onRewarded: function () {
            dynCall('v', rewardedCallbackPtr, []);
          },
          onClose: function () {
            dynCall('v', closeCallbackPtr, []);
          },
          onError: function (error) {
            yandexGames.invokeErrorCallback(error, errorCallbackPtr);
          },
        }
      });
    },

    invokeErrorCallback: function (error, errorCallbackPtr) {
      const errorMessage = error.message;
      const errorMessageBufferSize = lengthBytesUTF8(errorMessage) + 1;
      const errorMessageBufferPtr = _malloc(errorMessageBufferSize);
      stringToUTF8(errorMessage, errorMessageBufferPtr, errorMessageBufferSize);
      dynCall('vii', errorCallbackPtr, [errorMessageBufferPtr, errorMessageBufferSize]);
      _free(errorMessageBufferPtr);
    },
  },


  // External C# calls.

  Initialize: function () {
    yandexGames.initialize();
  },

  VerifySdkInitialization: function () {
    return yandexGames.verifySdkInitialization();
  },

  VerifyLeaderboardServiceInitialization: function () {
    return yandexGames.verifyLeaderboardServiceInitialization();
  },

  AuthenticatePlayerAccount: function (requestPermissions, onAuthenticatedCallbackPtr) {
    yandexGames.authenticatePlayerAccount(requestPermissions, onAuthenticatedCallbackPtr);
  },

  ShowInterestialAd: function (openCallbackPtr, closeCallbackPtr, errorCallbackPtr, offlineCallbackPtr) {
    yandexGames.showInterestialAd(openCallbackPtr, closeCallbackPtr, errorCallbackPtr, offlineCallbackPtr);
  },

  ShowVideoAd: function (openCallbackPtr, rewardedCallbackPtr, closeCallbackPtr, errorCallbackPtr) {
    yandexGames.showVideoAd(openCallbackPtr, rewardedCallbackPtr, closeCallbackPtr, errorCallbackPtr);
  },
}

autoAddDeps(library, '$yandexGames');
mergeInto(LibraryManager.library, library);
