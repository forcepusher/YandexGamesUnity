using UnityEngine;
using YandexGames;

public class PlaytestingCanvas : MonoBehaviour
{
    public void OnShowInterestialButtonClick()
    {
        var interestial = new InterestialAd();
        interestial.Show();
    }
}
