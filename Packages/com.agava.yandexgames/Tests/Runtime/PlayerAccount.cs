using System;
using System.Runtime.InteropServices;
using AOT;
using UnityEngine;

namespace YandexGames
{
    /// <summary>
    /// Proxy for player-related methods in YandexGames SDK.
    /// </summary>
    public static class PlayerAccount
    {
        //public 

        //private static Action<int> s_onPlayerIdentityCallback;

        //[DllImport("__Internal")]
        //public static extern bool RequestIdentity(Action<bool> onPlayerIdentityCallback = null);

        //[MonoPInvokeCallback(typeof(Action<int>))]
        //private static void OnPlayerIdentityCallback(int identity)
        //{
        //    if (YandexGamesSdk.CallbackLogging)
        //        Debug.Log($"{nameof(PlayerAccount)}.{nameof(OnPlayerIdentityCallback)} invoked, identity = {identity}");

        //    s_onPlayerIdentityCallback?.Invoke(identity);
        //}
    }
}
