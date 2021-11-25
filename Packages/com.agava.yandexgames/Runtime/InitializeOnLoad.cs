using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Scripting;

[assembly: AlwaysLinkAssembly]
namespace YandexGames
{
    internal sealed class InitializeOnLoad
    {
        [RuntimeInitializeOnLoadMethod]
        [DllImport("__Internal")]
        private static extern bool Initialize();
    }
}
