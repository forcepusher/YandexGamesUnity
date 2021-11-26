using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Scripting;

[assembly: AlwaysLinkAssembly]
namespace YandexGames
{
    public static class YandexGamesSdk
    {
        /// <summary>
        /// SDK is initialized automatically on load. If something fails, this will return false.
        /// </summary>
        public static bool Initialized => IsInitialized();

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        [DllImport("__Internal")]
        private static extern bool Initialize();

        [DllImport("__Internal")]
        private static extern bool IsInitialized();
    }
}
