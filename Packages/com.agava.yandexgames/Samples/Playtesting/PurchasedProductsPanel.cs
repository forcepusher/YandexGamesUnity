using UnityEngine;

namespace Agava.YandexGames.Samples
{
    public class PurchasedProductsPanel : MonoBehaviour
    {
        private void OnEnable()
        {
            Billing.GetPurchasedProducts();
        }
    }
}
