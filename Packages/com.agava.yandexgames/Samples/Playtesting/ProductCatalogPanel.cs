using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Agava.YandexGames.Samples
{
    public class ProductCatalogPanel : MonoBehaviour
    {
        [SerializeField]
        private ProductPanel _productPanelTemplate;
        [SerializeField]
        private LayoutGroup _productCatalogLayoutGroup;

        private readonly List<ProductPanel> _productPanels = new List<ProductPanel>();

        private void Awake()
        {
            _productPanelTemplate.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            if (Application.isEditor)
            {
                string sampleResponseJson = "[{ \"id\":\"TestProduct\",\"title\":\"Тестлол\",\"description\":\"\",\"imageURI\":\"/default256x256\",\"price\":\"1 YAN\",\"priceValue\":\"1\",\"priceCurrencyCode\":\"YAN\"},{ \"id\":\"AnotherTestProduct\",\"title\":\"Желешечка\",\"description\":\"\",\"imageURI\":\"https://avatars.mds.yandex.net/get-games/2977039/2a0000018627c05340c1234f5ceb18517812//default256x256\",\"price\":\"4 YAN\",\"priceValue\":\"4\",\"priceCurrencyCode\":\"YAN\"}]";
                UpdateProductCatalog(GetProductCatalogResponse.ParseJson(sampleResponseJson).products);
            }
            else
            {
                Billing.GetProductCatalog(productCatalogReponse => UpdateProductCatalog(productCatalogReponse.products));
            }
        }

        private void UpdateProductCatalog(ProductResponse[] products)
        {
            ClearProductCatalog();

            foreach (ProductResponse product in products)
            {
                ProductPanel productPanel = Instantiate(_productPanelTemplate, _productCatalogLayoutGroup.transform);
                _productPanels.Add(productPanel);

                productPanel.gameObject.SetActive(true);
                productPanel.Product = product;
            }
        }

        private void ClearProductCatalog()
        {
            foreach (ProductPanel productPanel in _productPanels)
                Destroy(productPanel.gameObject);
        }
    }
}
