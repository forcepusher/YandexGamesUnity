const initializationLibrary = {
  // External C# call.
  InitializeYandexGames: function() {
    const yandexGamesSdkScript = document.createElement("script");
    yandexGamesSdkScript.src = "https://yandex.ru/games/sdk/v2";
    document.head.appendChild(yandexGamesSdkScript);

    yandexGamesSdkScript.onload = function() {
      window['YaGames'].init().then(function(yandexGamesSdk) {
        window['YandexGamesSdk'] = yandexGamesSdk;
      });
    }
  },
}

mergeInto(LibraryManager.library, initializationLibrary);
