using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Scripting;

[assembly: AlwaysLinkAssembly]
namespace YandexGames
{
    public class YandexGamesLibrary
    {
        [RuntimeInitializeOnLoadMethod]
        private static void Initialize()
        {
            InitializeLibrary();
        }

        [DllImport("__Internal")]
        private static extern bool InitializeLibrary();
    }
}
