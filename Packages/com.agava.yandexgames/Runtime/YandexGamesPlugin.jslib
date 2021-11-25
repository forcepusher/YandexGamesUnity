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
  ShowInterestialAd: function () {
    yandexGames.sdk.adv.showFullscreenAdv({
      callbacks: {
        onOpen: function () {

        },
        onClose: function (wasShown) {
          
        },
        onError: function (error) {
          
        },
        onOffline: function () {
          
        },
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
