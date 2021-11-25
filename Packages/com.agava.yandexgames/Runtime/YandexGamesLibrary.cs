using System.Runtime.InteropServices;
using UnityEngine;

namespace YandexGames
{
    public class YandexGamesLibrary
    {
        [RuntimeInitializeOnLoadMethod]
        static void Initialize()
        {
            Debug.Log("HELLOOOOOOOOO ??? ASJD POASJDOP JSOPAD");
            InitializeSdk();
        }

        [DllImport("__Internal")]
        private static extern bool InitializeSdk();
    }
}
