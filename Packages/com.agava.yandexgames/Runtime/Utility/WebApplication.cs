using System.Threading.Tasks;
using UnityEngine;

namespace YandexGames.Utility
{
    /// <summary>
    /// Workaround class for <see href="https://trello.com/c/PjW4j3st"/>
    /// </summary>
    public static class WebApplication
    {
        public static float BackgroundDetectionDeltaTimeThreshold = 0.5f;
        public static int TicksAboveThresholdToTriggerBackground = 2;

        /// <summary>
        /// Detects when app is background while "Run In Background" option is set to true.
        /// </summary>
        /// <remarks>
        /// This isn't a static event because we can't trust developers to reliably unsubscribe from it.
        /// </remarks>
        public static bool InBackground { get; private set; }

        private static int s_ticksAboveThreshold = 0;

#if UNITY_WEBGL && !UNITY_EDITOR
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
#endif
        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "Fuck off. TYVM.")]
        private static async void UpdateLoopAsync()
        {
            while (true)
            {
                if (Time.unscaledDeltaTime > BackgroundDetectionDeltaTimeThreshold)
                    s_ticksAboveThreshold += 1;
                else
                    s_ticksAboveThreshold = 0;

                InBackground = s_ticksAboveThreshold >= TicksAboveThresholdToTriggerBackground;

                await Task.Yield();
            }
        }
    }
}
