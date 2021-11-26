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
  IsInitialized: function () {
    return yandexGames.sdk !== undefined;
  },

  // External C# call.
  ShowInterestialAd: function (openCallbackPtr, closeCallbackPtr, errorCallbackPtr, offlineCallbackPtr) {
    yandexGames.sdk.adv.showFullscreenAdv({
      callbacks: {
        // onOpen: function () {
        //   dynCall('v', openCallbackPtr, []);
        // },
        onClose: function (wasShown) {
          dynCall('vi', closeCallbackPtr, [wasShown]);
        },
        // onError: function (error) {
        //   dynCall('vii', errorCallbackPtr);
        // },
        // onOffline: function () {
        //   dynCall('v', offlineCallbackPtr, []);
        // },
      }
    });
  },

  // External C# call.
  ShowVideoAd: function () {
    yandexGames.sdk.adv.showRewardedVideo();
  },
}

autoAddDeps(library, '$yandexGames');
mergeInto(LibraryManager.library, library);
