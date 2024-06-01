using System.Collections;
using System.Collections.Generic;
using Agava.YandexGames;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class FlagChecker : MonoBehaviour
{
    [SerializeField] private Text flagInitedText;

    [SerializeField] [Range(0, 1)] private int adsValue = 0;

    private KeyValuePair<string, string>[] _flags;

    IEnumerator Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
#if !UNITY_EDITOR
        yield return YandexGamesSdk.Initialize();
#endif
        yield return new WaitForSeconds(2f);
        Flags.FlagsInit();
    }

    public void OnClick()
    {
//         if (_flags[0].Key == "AdEnabled" && _flags[0].Value == "1")
//         {
// #if !UNITY_EDITOR
//             InterstitialAd.Show();
// #endif
//             Debug.Log("Interstitial enabled");
//         }
//         else
//             Debug.Log("Interstitial disabled");

        string value = Flags.GetFlag("AdsEnabled");

        if (value == "0")
        {
            Debug.Log("Interstitial disabled");
        }
        else if (value == "1")
        {
            Debug.Log("Interstitial enabled");
        }

        flagInitedText.text = value;
    }
}
