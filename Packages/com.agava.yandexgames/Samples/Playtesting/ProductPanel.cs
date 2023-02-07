using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Agava.YandexGames.Samples
{
    public class ProductPanel : MonoBehaviour
    {
        [SerializeField]
        private RawImage _productImage;
        [SerializeField]
        private Text _productIdText;

        public ProductResponse Product
        {
            set
            {
                _productIdText.text = value.id;
                using (UnityWebRequest textureRequest = UnityWebRequestTexture.GetTexture(value.imageURI, true))
                {
                    textureRequest.SendWebRequest().completed += (requestAsyncOperation) =>
                    {
                        _productImage.texture = DownloadHandlerTexture.GetContent(textureRequest);
                    };
                }
            }
        }

        public void OnPurchaseButtonClick()
        {

        }

        public void OnPurchaseAndConsumeButtonClick()
        {

        }
    }
}
