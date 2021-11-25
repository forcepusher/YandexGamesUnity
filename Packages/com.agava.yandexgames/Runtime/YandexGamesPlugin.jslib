const yandexGamesLibrary = {
  $yandexGamesState: {
    
  },

  initializeSdk: function() {
    const yandexGamesSdkScript = document.createElement("script");
    yandexGamesSdkScript.src = "https://yandex.ru/games/sdk/v2";
    document.head.appendChild(yandexGamesSdkScript);
  },
}

autoAddDeps(yandexGamesLibrary, '$yandexGamesState');
mergeInto(LibraryManager.library, yandexGamesLibrary);