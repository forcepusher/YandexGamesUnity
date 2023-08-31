using System;
using System.Runtime.InteropServices;
using AOT;
using UnityEngine;

namespace Agava.YandexGames
{
    public static class Shortcuts
    {
        private static Action<bool> m_canSuggestCallback;
        private static Action m_onSuggestionAcceptCallback;
        private static Action m_onSuggestionDeclineCallback;

        public static void Suggest(Action onAccept = null, Action onDecline = null)
        {
            m_onSuggestionAcceptCallback = onAccept;
            m_onSuggestionDeclineCallback = onDecline;

            SuggestShortcut(SuggestShortcutCallbackAccept, SuggestShortcutCallbackDecline);
        }

        public static void IsCanSuggest(Action<bool> onSuccessCallback)
        {
            m_canSuggestCallback = onSuccessCallback;

            CanSuggestShortcut(CanSuggestShortcutCallback);
        }

        [DllImport("__Internal")]
        private static extern void CanSuggestShortcut(Action<int> onSuccess);

        [DllImport("__Internal")]
        private static extern void SuggestShortcut(Action onAccept, Action onDecline);

        [MonoPInvokeCallback(typeof(Action<bool>))]
        private static void CanSuggestShortcutCallback(int isCan)
        {
            if (YandexGamesSdk.CallbackLogging)
                Debug.Log($"{nameof(Shortcuts)}.{nameof(CanSuggestShortcutCallback)} called. {nameof(isCan)}={isCan}");

            m_canSuggestCallback.Invoke(isCan == 1);
        }

        [MonoPInvokeCallback(typeof(Action))]
        private static void SuggestShortcutCallbackAccept()
        {
            if (YandexGamesSdk.CallbackLogging)
                Debug.Log($"{nameof(Shortcuts)}.{nameof(SuggestShortcutCallbackAccept)} called");

            m_onSuggestionAcceptCallback?.Invoke();
        }

        [MonoPInvokeCallback(typeof(Action))]
        private static void SuggestShortcutCallbackDecline()
        {
            if (YandexGamesSdk.CallbackLogging)
                Debug.Log($"{nameof(Shortcuts)}.{nameof(SuggestShortcutCallbackDecline)} called");

            m_onSuggestionDeclineCallback?.Invoke();
        }
    }
}
