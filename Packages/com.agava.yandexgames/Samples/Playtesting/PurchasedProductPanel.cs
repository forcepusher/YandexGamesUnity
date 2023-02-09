using UnityEngine;
using UnityEngine.UI;

namespace Agava.YandexGames.Samples
{
    public class PurchasedProductPanel : MonoBehaviour
    {
        [SerializeField]
        private Text _purchasedProductIdText;

        private PurchasedProduct _purchasedProduct;

        public PurchasedProduct PurchasedProduct
        {
            set
            {
                _purchasedProduct = value;

                _purchasedProductIdText.text = value.productID;
            }
        }

        public void OnConsumeButtonClick()
        {
            Billing.ConsumeProduct(_purchasedProduct.purchaseToken, () =>
            {
                Debug.Log($"Consumed {_purchasedProduct.productID}");
                Destroy(gameObject);
            });
        }
    }
}
