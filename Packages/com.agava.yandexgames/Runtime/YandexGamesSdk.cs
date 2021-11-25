using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Scripting;

[assembly: AlwaysLinkAssembly]
namespace YandexGames
{
    public static class YandexGamesSdk
    {
        [RuntimeInitializeOnLoadMethod]
        [DllImport("__Internal")]
        private static extern bool Initialize();
    }
}
