using System.Collections;
using System.Collections.Generic;
using Agava.YandexGames;
using UnityEngine;
using UnityEngine.UI;

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
        InitFlags();
    }

    private void InitFlags()
    {
        Debug.Log("SDK Initialized");

        flagInitedText.text = "Flags disabled";
#if !UNITY_EDITOR
        Flags.Get(pairs => _flags = pairs);
#else
        _flags = new KeyValuePair<string, string>[1];
        _flags[0] = new KeyValuePair<string, string>("AdEnabled", adsValue.ToString());
#endif

        Debug.Log("SharpFlags: " + _flags);
        Debug.Log("SharpFirstFlag: " + _flags[0]);
        Debug.Log("SharpFirstFlagKey: " + _flags[0].Key);
        Debug.Log("SharpFirstFlagValue: " + _flags[0].Value);

        flagInitedText.text = _flags[0].Key + _flags[0].Value;
    }

    public void OnClick()
    {
        if (_flags[0].Key == "AdEnabled" && _flags[0].Value == "1")
        {
#if !UNITY_EDITOR
            InterstitialAd.Show();
#endif
            Debug.Log("Interstitial enabled");
        }
        else
            Debug.Log("Interstitial disabled");
    }
}
