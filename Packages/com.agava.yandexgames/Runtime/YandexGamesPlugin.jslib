const YandexGamesLibrary = {
  // External C# call.
  Initialize: function() {
    const yandexGamesSdkScript = document.createElement("script");
    yandexGamesSdkScript.src = "https://yandex.ru/games/sdk/v2";
    document.head.appendChild(yandexGamesSdkScript);

    yandexGamesSdkScript.onload = function() {
      window['YaGames'].init().then(function(yandexGamesSdk) {
        window['YandexGamesSdk'] = yandexGamesSdk;
      });
    }
  },

  // External C# call.
  ShowInterestialAd: function() {
    window['YandexGamesSdk'].showFullscreenAdv();
  },

  // External C# call.
  ShowVideoAd: function() {
    window['YandexGamesSdk'].showRewardedVideo();
  },
}

mergeInto(LibraryManager.library, YandexGamesLibrary);
