using System.Collections;
using System.Collections.Generic;
using Agava.YandexGames;
using UnityEngine;
using UnityEngine.UI;

public class FlagChecker : MonoBehaviour
{
    [SerializeField] private Text flagInitedText;

    private KeyValuePair<string, string>[] _flags;

    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
        YandexGamesSdk.Initialize(InitFlags);
    }

    private void InitFlags()
    {
        flagInitedText.text = "Flags disabled";
        Flags.Get(pairs => _flags = pairs);

        Debug.Log(_flags);
        Debug.Log(_flags[0]);
        Debug.Log(_flags[0].Key);
        Debug.Log(_flags[0].Value);

        flagInitedText.text = _flags[0].Key.ToString() + " enabled";
    }

    public void OnClick()
    {
        if (_flags[0].Key == "AdEnabled" && _flags[0].Value == "1")
            InterstitialAd.Show();
    }
}
