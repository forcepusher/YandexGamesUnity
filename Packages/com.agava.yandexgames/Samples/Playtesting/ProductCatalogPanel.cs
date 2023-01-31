using UnityEngine;

namespace Agava.YandexGames.Samples
{
    public class ProductCatalogPanel : MonoBehaviour
    {
        private void OnEnable()
        {
            Billing.GetProductCatalog();
        }
    }
}
