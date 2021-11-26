using System.Runtime.InteropServices;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Scripting;

[assembly: AlwaysLinkAssembly]
namespace YandexGames
{
    public static class YandexGamesSdk
    {
        public static LogLevel LogLevel = LogLevel.Error;

        /// <summary>
        /// Think of this as a static constructor.
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        [DllImport("__Internal")]
        private static extern bool Initialize();

        /// <summary>
        /// SDK is initialized automatically on load. If something fails, this will return false.
        /// </summary>
        [DllImport("__Internal")]
        public static extern bool VerifyInitialization();
    }
}
