const yandexGamesSdkScript = document.createElement("script");
yandexGamesSdkScript.src = "https://yandex.ru/games/sdk/v2";
document.head.appendChild(yandexGamesSdkScript);

const yandexGamesLibrary = {
  $yandexGamesState: {
    
  }

  
}

autoAddDeps(yandexGamesLibrary, '$yandexGamesState');
mergeInto(LibraryManager.library, yandexGamesLibrary);