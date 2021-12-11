const library = {

  // Class definition.

  $yandexGames: {
    initialized: false,

    authorized: false,

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

          // The { scopes: false } ensures personal data permission request window won't pop up,
          const playerAccountInitializationPromise = sdk.getPlayer({ scopes: false }).then(function (playerAccount) {
            yandexGames.authorized = true;
            // Always contains permission info. Contains personal data as well if permissions were granted before.
            yandexGames.playerAccount = playerAccount;

            // Catch the error that gets thrown when user is not authorized.
            // This IS the intended way to check for player authorization, not even kidding:
            // https://yandex.ru/dev/games/doc/dg/sdk/sdk-player.html#sdk-player__auth
          }).catch(function () { });

          const leaderboardInitializationPromise = sdk.getLeaderboards().then(function (leaderboard) {
            yandexGames.leaderboard = leaderboard;
          }).catch(function () { });

          Promise.allSettled([leaderboardInitializationPromise, playerAccountInitializationPromise]).then(function () {
            if (yandexGames.leaderboard === undefined) {
              throw new Error('Leaderboard caused Yandex Games SDK to fail initialization.');
            }

            yandexGames.initialized = true;
          });
        });
      }
    },

    throwIfSdkNotInitialized: function () {
      if (!yandexGames.initialized) {
        throw new Error('SDK was not fast enough to initialize. Use YandexGamesSdk.Initialized or WaitForInitialization.');
      }
    },

    invokeErrorCallback: function (error, errorCallbackPtr) {
      const errorMessage = error.message;
      const errorMessageBufferSize = lengthBytesUTF8(errorMessage) + 1;
      const errorMessageBufferPtr = _malloc(errorMessageBufferSize);
      stringToUTF8(errorMessage, errorMessageBufferPtr, errorMessageBufferSize);
      dynCall('vii', errorCallbackPtr, [errorMessageBufferPtr, errorMessageBufferSize]);
      _free(errorMessageBufferPtr);
    },

    invokeErrorCallbackIfNotAuthorized: function (errorCallbackPtr) {
      if (!yandexGames.authorized) {
        yandexGames.invokeErrorCallback(new Error('Needs authorization.'), errorCallbackPtr);
        return true;
      }
      return false;
    },

    authorize: function (successCallbackPtr, errorCallbackPtr) {
      yandexGames.sdk.auth.openAuthDialog().then(function () {
        yandexGames.sdk.getPlayer({ scopes: false }).then(function (playerAccount) {
          yandexGames.playerAccount = playerAccount;
        }).catch(function (error) {
          console.error('authorize failed to update playerAccount. Possible problems ahead. Error was: ' + error.message);
        }).finally(function () {
          yandexGames.authorized = true;
          dynCall('v', successCallbackPtr, []);
        });
      }).catch(function (error) {
        yandexGames.invokeErrorCallback(error, errorCallbackPtr);
      });
    },

    checkProfileDataPermission: function () {
      if (!yandexGames.authorized) {
        console.error('checkProfileDataPermission requires authorization. Assuming profile data permissions were not granted.');
        return false;
      }

      var publicNamePermission = undefined;
      if ('_personalInfo' in yandexGames.playerAccount && 'scopePermissions' in yandexGames.playerAccount._personalInfo) {
        publicNamePermission = yandexGames.playerAccount._personalInfo.scopePermissions.public_name;
      }

      switch (publicNamePermission) {
        case 'forbid':
          return false;
        case 'not_set':
          return false;
        case 'allow':
          return true;
        default:
          console.error('Unexpected response from Yandex. Assuming profile data permissions were not granted. playerAccount = '
            + JSON.stringify(yandexGames.playerAccount));
          return false;
      }
    },

    requestProfileDataPermission: function (onAuthenticatedCallbackPtr, errorCallbackPtr) {
      if (yandexGames.invokeErrorCallbackIfNotAuthorized(errorCallbackPtr)) {
        console.error('requestProfileDataPermission requires authorization. Assuming profile data permissions were not granted.');
        return;
      }

      yandexGames.sdk.getPlayer({ scopes: true }).then(function (playerAccount) {
        yandexGames.playerAccount = playerAccount;

        if (yandexGames.checkProfileDataPermission()) {
          dynCall('v', onAuthenticatedCallbackPtr, []);
        } else {
          yandexGames.invokeErrorCallback(new Error('User has refused the permission request.'), errorCallbackPtr);
        }
      }).catch(function (error) {
        yandexGames.invokeErrorCallback(error, errorCallbackPtr);
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

    setLeaderboardScore: function (leaderboardName, score, successCallbackPtr, errorCallbackPtr, extraData) {
      if (yandexGames.invokeErrorCallbackIfNotAuthorized(errorCallbackPtr)) {
        console.error('setLeaderboardScore requires authorization.');
        return;
      }

      yandexGames.leaderboard.setLeaderboardScore(leaderboardName, score, extraData).then(function () {
        dynCall('v', successCallbackPtr, []);
      }).catch(function (error) {
        yandexGames.invokeErrorCallback(error, errorCallbackPtr);
      });
    },

    getLeaderboardEntries: function (leaderboardName, successCallbackPtr, errorCallbackPtr, topPlayersCount, competingPlayersCount, includeSelf) {
      if (yandexGames.invokeErrorCallbackIfNotAuthorized(errorCallbackPtr)) {
        console.error('getLeaderboardEntries requires authorization.');
        return;
      }

      yandexGames.leaderboard.getLeaderboardEntries(leaderboardName, {
        includeUser: includeSelf, quantityAround: competingPlayersCount, quantityTop: topPlayersCount
      }).then(function (response) {
        const entriesMessage = JSON.stringify(response);

        // TODO: This string stuff is repetitive code. Make a class.
        const entriesMessageBufferSize = lengthBytesUTF8(entriesMessage) + 1;
        const entriesMessageBufferPtr = _malloc(entriesMessageBufferSize);
        stringToUTF8(entriesMessage, entriesMessageBufferPtr, entriesMessageBufferSize);
        dynCall('vii', successCallbackPtr, [entriesMessageBufferPtr, entriesMessageBufferSize]);
        _free(entriesMessageBufferPtr);
      }).catch(function (error) {
        yandexGames.invokeErrorCallback(error, errorCallbackPtr);
      });
    },
  },


  // External C# calls.

  Initialize: function () {
    yandexGames.initialize();
  },

  CheckSdkInitialization: function () {
    return yandexGames.initialized;
  },

  Authorize: function (successCallbackPtr, errorCallbackPtr) {
    yandexGames.throwIfSdkNotInitialized();

    yandexGames.authorize(successCallbackPtr, errorCallbackPtr);
  },

  CheckAuthorization: function () {
    yandexGames.throwIfSdkNotInitialized();

    return yandexGames.authorized;
  },

  CheckProfileDataPermission: function () {
    yandexGames.throwIfSdkNotInitialized();

    return yandexGames.checkProfileDataPermission();
  },

  RequestProfileDataPermission: function (successCallbackPtr, errorCallbackPtr) {
    yandexGames.throwIfSdkNotInitialized();

    yandexGames.requestProfileDataPermission(successCallbackPtr, errorCallbackPtr);
  },

  ShowInterestialAd: function (openCallbackPtr, closeCallbackPtr, errorCallbackPtr, offlineCallbackPtr) {
    yandexGames.throwIfSdkNotInitialized();

    yandexGames.showInterestialAd(openCallbackPtr, closeCallbackPtr, errorCallbackPtr, offlineCallbackPtr);
  },

  ShowVideoAd: function (openCallbackPtr, rewardedCallbackPtr, closeCallbackPtr, errorCallbackPtr) {
    yandexGames.throwIfSdkNotInitialized();

    yandexGames.showVideoAd(openCallbackPtr, rewardedCallbackPtr, closeCallbackPtr, errorCallbackPtr);
  },

  SetLeaderboardScore: function (leaderboardNamePtr, score, successCallbackPtr, errorCallbackPtr, extraDataPtr) {
    yandexGames.throwIfSdkNotInitialized();

    const leaderboardName = UTF8ToString(leaderboardNamePtr);
    var extraData = UTF8ToString(extraDataPtr);
    if (extraData.length === 0) { extraData = undefined; }
    yandexGames.setLeaderboardScore(leaderboardName, score, successCallbackPtr, errorCallbackPtr, extraData);
  },

  GetLeaderboardEntries: function (leaderboardNamePtr, successCallbackPtr, errorCallbackPtr, topPlayersCount, competingPlayersCount, includeSelf) {
    yandexGames.throwIfSdkNotInitialized();

    const leaderboardName = UTF8ToString(leaderboardNamePtr);
    // Booleans are transferred as either 1 or 0, so using !! to convert them to true or false.
    includeSelf = !!includeSelf;
    yandexGames.getLeaderboardEntries(leaderboardName, successCallbackPtr, errorCallbackPtr, topPlayersCount, competingPlayersCount, includeSelf);
  },
}

autoAddDeps(library, '$yandexGames');
mergeInto(LibraryManager.library, library);
