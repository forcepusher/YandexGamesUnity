# YandexGamesUnity

This is a development repository. To install the package, use this repo instead:<br>
https://github.com/forcepusher/com.agava.yandexgames<br>
<br>
To run unit tests: in Unity, go to "Window" -> "General" -> "Test Runner".<br>
Make sure your target platform is set to WebGL in build settings.<br>
Switch to "PlayMode" and click "Run All Tests (WebGL)". It'll take a while.<br>
<br>
Yandex Games doesn't support SDK unit testing outside of their platform, so we have to run manual tests as well.<br>
Make a regular build and upload it to Yandex Games according to documentation:<br>
https://yandex.ru/dev/games/doc/dg/concepts/about.html<br>
<br>
Before publishing a new version, rename "Samples" folder to "Samples~" and remove the meta file for the folder.<br>
https://forum.unity.com/threads/samples-in-packages-manual-setup.623080/#post-4991561<br>
This is stupid, but that's Unity. Not the worst thing they've done so far.<br>