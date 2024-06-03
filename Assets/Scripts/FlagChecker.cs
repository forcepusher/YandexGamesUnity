using System.Collections;
using System.Collections.Generic;
using Agava.YandexGames;
using UnityEngine;
using UnityEngine.UI;
using Utility;

public class FlagChecker : MonoBehaviour
{
    [SerializeField] private Text flagInitedText;

    [SerializeField] [Range(0, 1)] private int adsValue = 0;

    private Dictionary<string, string> _flags;

    private const string Ads_Enabled_Key = "AdsEnabled";

    IEnumerator Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
#if !UNITY_EDITOR
        yield return YandexGamesSdk.Initialize();
#endif
        yield return new WaitForSeconds(2f);
        FlagsUtility.GetFlagsCollection(dictionary => _flags = (Dictionary<string, string>)dictionary);
    }

    public void OnClick()
    {
        string value = _flags[Ads_Enabled_Key];

        Debug.Log($"{_flags[Ads_Enabled_Key]} + {value}");

        flagInitedText.text = value;
    }
}
