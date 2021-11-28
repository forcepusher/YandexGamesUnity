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

          // Cache the playerAccount immediately so it's ready for verifyPlayerAccountAuthorization call.
          // This IS the intended way to check for player authorization, not even kidding:
          // https://yandex.ru/dev/games/doc/dg/sdk/sdk-player.html#sdk-player__auth
          sdk.getPlayer({ scopes: false }).then(function (playerAccount) {
            yandexGames.playerAccount = playerAccount;
          }).catch(function () {});
        });
      }
    },

    verifySdkInitialization: function () {
      return yandexGames.sdk !== undefined;
    },

    verifyLeaderboardServiceInitialization: function () {
      return yandexGames.leaderboard !== undefined;
    },

    verifyPlayerAccountAuthorization: function () {
      return yandexGames.playerAccount !== undefined;
    },

    authenticatePlayerAccount: function (requestPermissions, onAuthenticatedCallbackPtr, authenticationErrorCallbackPtr) {
      yandexGames.sdk.getPlayer({ scopes: requestPermissions }).then(function (playerAccount) {
        yandexGames.playerAccount = playerAccount;
        console.log(playerAccount);
        dynCall('v', onAuthenticatedCallbackPtr, []);
      }).catch(function (error) {
        yandexGames.invokeErrorCallback(error, authenticationErrorCallbackPtr);
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

  VerifyPlayerAccountAuthorization: function () {
    return yandexGames.verifyPlayerAccountAuthorization();
  },

  AuthenticatePlayerAccount: function (requestPermissions, onAuthenticatedCallbackPtr, authenticationErrorCallbackPtr) {
    yandexGames.authenticatePlayerAccount(requestPermissions, onAuthenticatedCallbackPtr, authenticationErrorCallbackPtr);
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
