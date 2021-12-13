using System.Threading.Tasks;
using UnityEngine;

namespace YandexGames
{
    /// <summary>
    /// Mutes audio in background while "Run In Background" option is set to true.
    /// Workaround for https://trello.com/c/PjW4j3st
    /// </summary>
    public static class WebBackgroundMute
    {
        public static bool Enabled = false;

#if UNITY_WEBGL && !UNITY_EDITOR
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
#endif
        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "Fuck off. TYVM.")]
        private static async void MuteUnmuteLoop()
        {
            while (true)
            {
                bool mute = Enabled && Time.deltaTime == Time.maximumDeltaTime && Time.unscaledDeltaTime > Time.maximumDeltaTime;
                Object.FindObjectOfType<AudioListener>().enabled = !mute;
                Debug.Log(Time.deltaTime + " " + Time.unscaledDeltaTime);
                await Task.Yield();
            }
        }
    }
}
