using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Scripting;

[assembly: AlwaysLinkAssembly]
namespace YandexGames
{
    public static class YandexGamesSdk
    {
        public static bool Initialized => IsInitialized();

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        [DllImport("__Internal")]
        private static extern bool Initialize();

        [DllImport("__Internal")]
        private static extern bool IsInitialized();
    }
}
