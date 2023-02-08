using System;
using System.Collections;
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

        private ProductResponse _product;

        public ProductResponse Product
        {
            set
            {
                _product = value;

                _productIdText.text = value.id;

                if (Uri.IsWellFormedUriString(value.imageURI, UriKind.Absolute))
                    StartCoroutine(DownloadAndSetProductImage(value.imageURI));
            }
        }

        private IEnumerator DownloadAndSetProductImage(string imageUri)
        {
            using (UnityWebRequest textureRequest = UnityWebRequestTexture.GetTexture(imageUri))
            {
                yield return textureRequest.SendWebRequest();
                _productImage.texture = DownloadHandlerTexture.GetContent(textureRequest);
            }
        }

        public void OnPurchaseButtonClick()
        {
            Billing.PurchaseProduct(_product.id, () =>
            {
                Debug.Log($"Purchased {_product.id}");
            });
        }

        public void OnPurchaseAndConsumeButtonClick()
        {
            Billing.PurchaseProduct(_product.id, () =>
            {
                Debug.Log($"Purchased {_product.id}");

                Billing.ConsumeProduct(_product.id, () =>
                {
                    Debug.Log($"Consumed {_product.id}");
                });
            });
        }
    }
}
