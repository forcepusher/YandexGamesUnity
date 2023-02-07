using UnityEngine;

namespace Agava.YandexGames.Samples
{
    public class ProductCatalogPanel : MonoBehaviour
    {
        private void OnEnable()
        {
            Billing.GetProductCatalog(productCatalogReponse => UpdateProductCatalog(productCatalogReponse.products));
        }

        private void UpdateProductCatalog(ProductResponse[] products)
        {
            ClearProductCatalog();

            foreach (ProductResponse product in products)
            {
                
            }
        }

        private void ClearProductCatalog()
        {

        }
    }
}
