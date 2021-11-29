#pragma warning disable

using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace YandexGames.Samples
{
    public class PlaytestingCanvas : MonoBehaviour
    {
        [SerializeField]
        private Text _isAuthorizedText;

        [SerializeField]
        private RectTransform _applicationAliveIndicator;

        [SerializeField]
        private Text _debugText;

        private void Awake()
        {
            YandexGamesSdk.CallbackLogging = true;
        }

        private IEnumerator Start()
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            yield break;
#endif

            while (true)
            {
                _isAuthorizedText.color = PlayerAccount.IsAuthorized ? Color.green : Color.red;
                yield return new WaitForSecondsRealtime(0.25f);
            }
        }

        public void Update()
        {
            if (Input.touchCount > 0)
                Debug.Log("TOUCHING. touchCount = " + Input.touchCount);

            //if (Input.anyKey)
            //    Debug.Log("ANY KEY DOWN.");

            //if (Input.GetMouseButton(0))
            //    Debug.Log("MOUSE DOWN");

            _debugText.text = EventSystem.current.ToString();

            _applicationAliveIndicator.Rotate(0f, 0f, -Time.unscaledDeltaTime * 100f);
        }

        public void OnShowInterestialButtonClick()
        {
            InterestialAd.Show();
        }

        public void OnShowVideoButtonClick()
        {
            VideoAd.Show();
        }

        public void OnAuthenticateButtonClick()
        {
            PlayerAccount.Authenticate(false);
        }

        public void OnAuthenticateWithPermissionsButtonClick()
        {
            PlayerAccount.Authenticate(true);
        }
    }
}
