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
          // The { scopes: false } ensures personal data permission won't pop up,
          // but if permissions were granted before, playerAccount will contain personal data.
          sdk.getPlayer({ scopes: false }).then(function (playerAccount) {
            yandexGames.playerAccount = playerAccount;
          }).catch(function () { });
        });
      }
    },

    verifySdkInitialization: function () {
      return yandexGames.sdk !== undefined;
    },

    verifyLeaderboardInitialization: function () {
      return yandexGames.leaderboard !== undefined;
    },

    verifyPlayerAccountAuthorization: function () {
      return yandexGames.playerAccount !== undefined;
    },

    checkProfileDataPermission: function () {
      if (!yandexGames.verifyPlayerAccountAuthorization()) {
        console.error('HasProfileDataPermission requires authorization. Assuming profile data permissions were not granted.');
        return false;
      }

      var publicNamePermission;
      if ('_personalInfo' in playerAccount && 'scopePermissions' in playerAccount._personalInfo) {
        publicNamePermission = playerAccount._personalInfo.scopePermissions.public_name;
      }

      switch (publicNamePermission) {
        case 'forbid':
          return false;
        case 'not_set':
          return false;
        case 'allow':
          return true;
        default:
          console.warn('Unexpected response from Yandex. Assuming profile data permissions were not granted. playerAccount = ' + JSON.stringify(playerAccount));
          return false;
      }
    },

    throwIfSdkNotInitialized: function () {
      if (!yandexGames.verifySdkInitialization()) {
        throw new Error('SDK was not fast enough to initialize. Use YandexGamesSdk.IsInitialized or WaitForInitialization.');
      }
    },

    throwIfLeaderboardNotInitialized: function () {
      if (!yandexGames.verifyLeaderboardInitialization()) {
        throw new Error('Leaderboard was not fast enough to initialize. Use Leaderboard.IsInitialized or WaitForInitialization.');
      }
    },

    // TODO: This has to be deleted
    authorizePlayerAccountIfNotAuthorized: function () {
      return new Promise(function (resolve, reject) {
        if (yandexGames.verifyPlayerAccountAuthorization()) {
          resolve(yandexGames.playerAccount);
        } else {
          yandexGames.sdk.auth.openAuthDialog().then(function () {
            yandexGames.sdk.getPlayer({ scopes: false }).then(function (playerAccount) {
              yandexGames.playerAccount = playerAccount;
              resolve(playerAccount);
            }).catch(function (error) {
              reject(error);
            });
          }).catch(function (error) {
            reject(error);
          });
        }
      });
    },

    requestProfileDataPermission: function (onAuthenticatedCallbackPtr, errorCallbackPtr) {
      yandexGames.throwIfSdkNotInitialized();

      if (!yandexGames.ensureAuthorization(errorCallbackPtr)) { return; }

      yandexGames.sdk.getPlayer({ scopes: true }).then(function (playerAccount) {
        var publicNamePermission;
        if ('_personalInfo' in playerAccount && 'scopePermissions' in playerAccount._personalInfo) {
          publicNamePermission = playerAccount._personalInfo.scopePermissions.public_name;
        }

        switch (publicNamePermission) {
          case 'forbid':
            yandexGames.invokeErrorCallback(new Error('User has refused the permission request.'), errorCallbackPtr);
            return;
          case 'not_set':
            yandexGames.invokeErrorCallback(new Error('User has closed the permission request.'), errorCallbackPtr);
            return;
          case 'allow':
            break;
          default:
            console.warn('Unexpected response from Yandex. Assuming profile data permissions were not granted. playerAccount = ' + JSON.stringify(playerAccount));
            yandexGames.invokeErrorCallback(new Error('Unexpected response from Yandex.'), errorCallbackPtr);
            return;
        }

        yandexGames.playerAccount = playerAccount;
        dynCall('v', onAuthenticatedCallbackPtr, []);
      }).catch(function (error) {
        yandexGames.invokeErrorCallback(error, errorCallbackPtr);
      });
    },

    showInterestialAd: function (openCallbackPtr, closeCallbackPtr, errorCallbackPtr, offlineCallbackPtr) {
      yandexGames.throwIfSdkNotInitialized();

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
      yandexGames.throwIfSdkNotInitialized();

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
      yandexGames.throwIfLeaderboardNotInitialized();

      yandexGames.authorizePlayerAccountIfNotAuthorized().then(function () {
        yandexGames.leaderboard.setLeaderboardScore(leaderboardName, score, extraData).then(function () {
          dynCall('v', successCallbackPtr, []);
        }).catch(function (error) {
          yandexGames.invokeErrorCallback(error, errorCallbackPtr);
        });
      }).catch(function (error) {
        yandexGames.invokeErrorCallback(error, errorCallbackPtr);
      });
    },

    getLeaderboardEntries: function (leaderboardName, successCallbackPtr, errorCallbackPtr, topPlayersCount, competingPlayersCount, includeSelf) {
      yandexGames.throwIfLeaderboardNotInitialized();

      yandexGames.authorizePlayerAccountIfNotAuthorized().then(function () {
        yandexGames.leaderboard.getLeaderboardEntries(leaderboardName, {
          includeUser: includeSelf, quantityAround: competingPlayersCount, quantityTop: topPlayersCount
        }).then(function (response) {
          // TODO: This is repetitive code. Make a class.
          const entriesMessage = JSON.stringify(response);
          const entriesMessageBufferSize = lengthBytesUTF8(entriesMessage) + 1;
          const entriesMessageBufferPtr = _malloc(entriesMessageBufferSize);
          stringToUTF8(entriesMessage, entriesMessageBufferPtr, entriesMessageBufferSize);
          dynCall('vii', successCallbackPtr, [entriesMessageBufferPtr, entriesMessageBufferSize]);
          _free(entriesMessageBufferPtr);
        }).catch(function (error) {
          yandexGames.invokeErrorCallback(error, errorCallbackPtr);
        });
      }).catch(function (error) {
        yandexGames.invokeErrorCallback(error, errorCallbackPtr);
      });
    },

    ensureAuthorization: function (errorCallbackPtr) {
      if (!yandexGames.verifyPlayerAccountAuthorization()) {
        yandexGames.invokeErrorCallback(new Error('Needs authorization.'), errorCallbackPtr);
        return false;
      }
      return true;
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

  VerifyLeaderboardInitialization: function () {
    return yandexGames.verifyLeaderboardInitialization();
  },

  VerifyPlayerAccountAuthorization: function () {
    return yandexGames.verifyPlayerAccountAuthorization();
  },

  CheckProfileDataPermission: function () {
    return yandexGames.checkProfileDataPermission();
  },

  RequestProfileDataPermission: function (onSuccessCallbackPtr, errorCallbackPtr) {
    yandexGames.requestProfileDataPermission(onSuccessCallbackPtr, errorCallbackPtr);
  },

  ShowInterestialAd: function (openCallbackPtr, closeCallbackPtr, errorCallbackPtr, offlineCallbackPtr) {
    yandexGames.showInterestialAd(openCallbackPtr, closeCallbackPtr, errorCallbackPtr, offlineCallbackPtr);
  },

  ShowVideoAd: function (openCallbackPtr, rewardedCallbackPtr, closeCallbackPtr, errorCallbackPtr) {
    yandexGames.showVideoAd(openCallbackPtr, rewardedCallbackPtr, closeCallbackPtr, errorCallbackPtr);
  },

  SetLeaderboardScore: function (leaderboardNamePtr, score, successCallbackPtr, errorCallbackPtr, extraDataPtr) {
    const leaderboardName = UTF8ToString(leaderboardNamePtr);
    var extraData = UTF8ToString(extraDataPtr);
    if (extraData.length === 0) { extraData = undefined; }
    yandexGames.setLeaderboardScore(leaderboardName, score, successCallbackPtr, errorCallbackPtr, extraData);
  },

  GetLeaderboardEntries: function (leaderboardNamePtr, successCallbackPtr, errorCallbackPtr, topPlayersCount, competingPlayersCount, includeSelf) {
    const leaderboardName = UTF8ToString(leaderboardNamePtr);
    // Booleans are transferred as either 1 or 0, so using !! to convert them to true or false.
    includeSelf = !!includeSelf;
    yandexGames.getLeaderboardEntries(leaderboardName, successCallbackPtr, errorCallbackPtr, topPlayersCount, competingPlayersCount, includeSelf);
  },
}

autoAddDeps(library, '$yandexGames');
mergeInto(LibraryManager.library, library);
