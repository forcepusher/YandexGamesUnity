const library = {
  $yandexGames: {
    sdk: undefined,
  },

  // External C# call.
  Initialize: function () {
    const sdkScript = document.createElement('script');
    sdkScript.src = 'https://yandex.ru/games/sdk/v2';
    document.head.appendChild(sdkScript);

    sdkScript.onload = function () {
      window['YaGames'].init().then(function (sdk) {
        yandexGames.sdk = sdk;
      });
    }
  },

  // External C# call.
  VerifyInitialization: function () {
    return yandexGames.sdk !== undefined;
  },

  // External C# call.
  ShowInterestialAd: function (openCallbackPtr, closeCallbackPtr, errorCallbackPtr, offlineCallbackPtr) {
    yandexGames.sdk.adv.showFullscreenAdv({
      callbacks: {
        onOpen: function () {
          dynCall('v', openCallbackPtr, []);
        },
        onClose: function (wasShown) {
          dynCall('vi', closeCallbackPtr, [wasShown]);
        },
        onError: function (error) {
          invokeErrorCallback(error, errorCallbackPtr);
        },
        onOffline: function () {
          dynCall('v', offlineCallbackPtr, []);
        },
      }
    });
  },

  // External C# call.
  ShowVideoAd: function (openCallbackPtr, rewardedCallbackPtr, closeCallbackPtr, errorCallbackPtr) {
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
          invokeErrorCallback(error, errorCallbackPtr);
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
}

autoAddDeps(library, '$yandexGames');
mergeInto(LibraryManager.library, library);
