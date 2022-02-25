# YandexGamesUnity  
  
This is a development repository. To install the package, use this repo instead:  
https://github.com/forcepusher/com.agava.yandexgames  
  
To run unit tests: in Unity, go to "Window" -> "General" -> "Test Runner".  
Make sure your target platform is set to WebGL in build settings.  
Switch to "PlayMode" and click "Run All Tests (WebGL)". It'll take a while.  
  
Yandex Games doesn't support SDK unit testing outside of their platform, so we have to run manual tests as well.  
Make a regular build and upload it to Yandex Games according to documentation:  
https://yandex.ru/dev/games/doc/dg/concepts/about.html  
  
Before publishing a new version, rename "Samples" folder to "Samples~" and remove the meta file for the folder.  
https://forum.unity.com/threads/samples-in-packages-manual-setup.623080/#post-4991561  
This is stupid, but that's Unity. Not the worst thing they've done so far.
