using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Agava.YandexGames.Samples
{
    public class PurchasedProductListPanel : MonoBehaviour
    {
        [SerializeField]
        private PurchasedProductPanel _purchasedProductPanelTemplate;
        [SerializeField]
        private LayoutGroup _purchasedProductsLayoutGroup;

        private readonly List<PurchasedProductPanel> _purchasedProductPanels = new List<PurchasedProductPanel>();

        private void Awake()
        {
            _purchasedProductPanelTemplate.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
#if UNITY_EDITOR
            string sampleResponseJson = "{\"purchasedProducts\":[{\"productID\":\"AnotherTestProduct\",\"purchaseTime\":0,\"purchaseToken\":\"c7a6a276-bf77-483f-b657-ae7bfce3fd8f\"},{\"productID\":\"AnotherTestProduct\",\"purchaseTime\":0,\"purchaseToken\":\"89d0621d-7789-4330-9b45-c9294084490f\"},{\"productID\":\"AnotherTestProduct\",\"purchaseTime\":0,\"purchaseToken\":\"944b3311-1e74-45c2-82db-a62bd49e9870\"},{\"productID\":\"AnotherTestProduct\",\"purchaseTime\":0,\"purchaseToken\":\"4b5639c3-3044-40cd-b075-928239a2269a\"},{\"productID\":\"AnotherTestProduct\",\"purchaseTime\":0,\"purchaseToken\":\"2b474ec1-6b9a-4e85-9e30-859704cef507\"},{\"productID\":\"AnotherTestProduct\",\"purchaseTime\":0,\"purchaseToken\":\"5ee6b72a-ed34-45f9-9fc4-5a05e99309d0\"},{\"productID\":\"AnotherTestProduct\",\"purchaseTime\":0,\"purchaseToken\":\"6a9f1a56-d24f-4cd8-ba92-261ee2441931\"},{\"productID\":\"AnotherTestProduct\",\"purchaseTime\":0,\"purchaseToken\":\"a39f433d-cbda-4fc7-8748-e1266d335251\"},{\"productID\":\"AnotherTestProduct\",\"purchaseTime\":0,\"purchaseToken\":\"aee6053e-3923-47f4-b703-3d21438274a4\"},{\"productID\":\"TestProduct\",\"purchaseTime\":0,\"purchaseToken\":\"070f028a-90d9-42da-95c4-53ef988bfe98\"},{\"productID\":\"TestProduct\",\"purchaseTime\":0,\"purchaseToken\":\"a7afeaac-a694-4d39-a2eb-583f1bc19b1b\"},{\"productID\":\"TestProduct\",\"purchaseTime\":0,\"purchaseToken\":\"4b512664-a2f9-4906-ad26-66db76a69b82\"}],\"signature\":\"SrZZA98vjdgmtiWVe0h3enJJfWeM3gPcE3K4jcNo2QY=.eyJhbGdvcml0aG0iOiJITUFDLVNIQTI1NiIsImlzc3VlZEF0IjoxNjc1OTA3NjIzLCJyZXF1ZXN0UGF5bG9hZCI6IiIsImRhdGEiOlt7InByb2R1Y3QiOnsiaWQiOiJBbm90aGVyVGVzdFByb2R1Y3QiLCJ0aXRsZSI6ItCW0LXQu9C10YjQtdGH0LrQsCIsImRlc2NyaXB0aW9uIjoiIiwicHJpY2UiOnsiY29kZSI6IllBTiIsInZhbHVlIjoiNCJ9LCJpbWFnZVByZWZpeCI6Imh0dHBzOi8vYXZhdGFycy5tZHMueWFuZGV4Lm5ldC9nZXQtZ2FtZXMvMjk3NzAzOS8yYTAwMDAwMTg2MjdjMDUzNDBjMTIzNGY1Y2ViMTg1MTc4MTIvIn0sInRva2VuIjoiYzdhNmEyNzYtYmY3Ny00ODNmLWI2NTctYWU3YmZjZTNmZDhmIiwiY3JlYXRlZCI6MTY3NTkwNDQ0Nn0seyJwcm9kdWN0Ijp7ImlkIjoiQW5vdGhlclRlc3RQcm9kdWN0IiwidGl0bGUiOiLQltC10LvQtdGI0LXRh9C60LAiLCJkZXNjcmlwdGlvbiI6IiIsInByaWNlIjp7ImNvZGUiOiJZQU4iLCJ2YWx1ZSI6IjQifSwiaW1hZ2VQcmVmaXgiOiJodHRwczovL2F2YXRhcnMubWRzLnlhbmRleC5uZXQvZ2V0LWdhbWVzLzI5NzcwMzkvMmEwMDAwMDE4NjI3YzA1MzQwYzEyMzRmNWNlYjE4NTE3ODEyLyJ9LCJ0b2tlbiI6Ijg5ZDA2MjFkLTc3ODktNDMzMC05YjQ1LWM5Mjk0MDg0NDkwZiIsImNyZWF0ZWQiOjE2NzU5MDI0MzF9LHsicHJvZHVjdCI6eyJpZCI6IkFub3RoZXJUZXN0UHJvZHVjdCIsInRpdGxlIjoi0JbQtdC70LXRiNC10YfQutCwIiwiZGVzY3JpcHRpb24iOiIiLCJwcmljZSI6eyJjb2RlIjoiWUFOIiwidmFsdWUiOiI0In0sImltYWdlUHJlZml4IjoiaHR0cHM6Ly9hdmF0YXJzLm1kcy55YW5kZXgubmV0L2dldC1nYW1lcy8yOTc3MDM5LzJhMDAwMDAxODYyN2MwNTM0MGMxMjM0ZjVjZWIxODUxNzgxMi8ifSwidG9rZW4iOiI5NDRiMzMxMS0xZTc0LTQ1YzItODJkYi1hNjJiZDQ5ZTk4NzAiLCJjcmVhdGVkIjoxNjc1OTAxOTgwfSx7InByb2R1Y3QiOnsiaWQiOiJBbm90aGVyVGVzdFByb2R1Y3QiLCJ0aXRsZSI6ItCW0LXQu9C10YjQtdGH0LrQsCIsImRlc2NyaXB0aW9uIjoiIiwicHJpY2UiOnsiY29kZSI6IllBTiIsInZhbHVlIjoiNCJ9LCJpbWFnZVByZWZpeCI6Imh0dHBzOi8vYXZhdGFycy5tZHMueWFuZGV4Lm5ldC9nZXQtZ2FtZXMvMjk3NzAzOS8yYTAwMDAwMTg2MjdjMDUzNDBjMTIzNGY1Y2ViMTg1MTc4MTIvIn0sInRva2VuIjoiNGI1NjM5YzMtMzA0NC00MGNkLWIwNzUtOTI4MjM5YTIyNjlhIiwiY3JlYXRlZCI6MTY3NTkwMTI4NH0seyJwcm9kdWN0Ijp7ImlkIjoiQW5vdGhlclRlc3RQcm9kdWN0IiwidGl0bGUiOiLQltC10LvQtdGI0LXRh9C60LAiLCJkZXNjcmlwdGlvbiI6IiIsInByaWNlIjp7ImNvZGUiOiJZQU4iLCJ2YWx1ZSI6IjQifSwiaW1hZ2VQcmVmaXgiOiJodHRwczovL2F2YXRhcnMubWRzLnlhbmRleC5uZXQvZ2V0LWdhbWVzLzI5NzcwMzkvMmEwMDAwMDE4NjI3YzA1MzQwYzEyMzRmNWNlYjE4NTE3ODEyLyJ9LCJ0b2tlbiI6IjJiNDc0ZWMxLTZiOWEtNGU4NS05ZTMwLTg1OTcwNGNlZjUwNyIsImNyZWF0ZWQiOjE2NzU5MDA3OTB9LHsicHJvZHVjdCI6eyJpZCI6IkFub3RoZXJUZXN0UHJvZHVjdCIsInRpdGxlIjoi0JbQtdC70LXRiNC10YfQutCwIiwiZGVzY3JpcHRpb24iOiIiLCJwcmljZSI6eyJjb2RlIjoiWUFOIiwidmFsdWUiOiI0In0sImltYWdlUHJlZml4IjoiaHR0cHM6Ly9hdmF0YXJzLm1kcy55YW5kZXgubmV0L2dldC1nYW1lcy8yOTc3MDM5LzJhMDAwMDAxODYyN2MwNTM0MGMxMjM0ZjVjZWIxODUxNzgxMi8ifSwidG9rZW4iOiI1ZWU2YjcyYS1lZDM0LTQ1ZjktOWZjNC01YTA1ZTk5MzA5ZDAiLCJjcmVhdGVkIjoxNjc1ODk4NTk3fSx7InByb2R1Y3QiOnsiaWQiOiJBbm90aGVyVGVzdFByb2R1Y3QiLCJ0aXRsZSI6ItCW0LXQu9C10YjQtdGH0LrQsCIsImRlc2NyaXB0aW9uIjoiIiwicHJpY2UiOnsiY29kZSI6IllBTiIsInZhbHVlIjoiNCJ9LCJpbWFnZVByZWZpeCI6Imh0dHBzOi8vYXZhdGFycy5tZHMueWFuZGV4Lm5ldC9nZXQtZ2FtZXMvMjk3NzAzOS8yYTAwMDAwMTg2MjdjMDUzNDBjMTIzNGY1Y2ViMTg1MTc4MTIvIn0sInRva2VuIjoiNmE5ZjFhNTYtZDI0Zi00Y2Q4LWJhOTItMjYxZWUyNDQxOTMxIiwiY3JlYXRlZCI6MTY3NTg5MTg3OX0seyJwcm9kdWN0Ijp7ImlkIjoiQW5vdGhlclRlc3RQcm9kdWN0IiwidGl0bGUiOiLQltC10LvQtdGI0LXRh9C60LAiLCJkZXNjcmlwdGlvbiI6IiIsInByaWNlIjp7ImNvZGUiOiJZQU4iLCJ2YWx1ZSI6IjQifSwiaW1hZ2VQcmVmaXgiOiJodHRwczovL2F2YXRhcnMubWRzLnlhbmRleC5uZXQvZ2V0LWdhbWVzLzI5NzcwMzkvMmEwMDAwMDE4NjI3YzA1MzQwYzEyMzRmNWNlYjE4NTE3ODEyLyJ9LCJ0b2tlbiI6ImEzOWY0MzNkLWNiZGEtNGZjNy04NzQ4LWUxMjY2ZDMzNTI1MSIsImNyZWF0ZWQiOjE2NzU4OTEwNTd9LHsicHJvZHVjdCI6eyJpZCI6IkFub3RoZXJUZXN0UHJvZHVjdCIsInRpdGxlIjoi0JbQtdC70LXRiNC10YfQutCwIiwiZGVzY3JpcHRpb24iOiIiLCJwcmljZSI6eyJjb2RlIjoiWUFOIiwidmFsdWUiOiI0In0sImltYWdlUHJlZml4IjoiaHR0cHM6Ly9hdmF0YXJzLm1kcy55YW5kZXgubmV0L2dldC1nYW1lcy8yOTc3MDM5LzJhMDAwMDAxODYyN2MwNTM0MGMxMjM0ZjVjZWIxODUxNzgxMi8ifSwidG9rZW4iOiJhZWU2MDUzZS0zOTIzLTQ3ZjQtYjcwMy0zZDIxNDM4Mjc0YTQiLCJjcmVhdGVkIjoxNjc1ODkxMDI3fSx7InByb2R1Y3QiOnsiaWQiOiJUZXN0UHJvZHVjdCIsInRpdGxlIjoi0KLQtdGB0YLQu9C+0LsiLCJkZXNjcmlwdGlvbiI6IiIsInByaWNlIjp7ImNvZGUiOiJZQU4iLCJ2YWx1ZSI6IjEifSwiaW1hZ2VQcmVmaXgiOiIifSwidG9rZW4iOiIwNzBmMDI4YS05MGQ5LTQyZGEtOTVjNC01M2VmOTg4YmZlOTgiLCJjcmVhdGVkIjoxNjc0ODI3MzE0fSx7InByb2R1Y3QiOnsiaWQiOiJUZXN0UHJvZHVjdCIsInRpdGxlIjoi0KLQtdGB0YLQu9C+0LsiLCJkZXNjcmlwdGlvbiI6IiIsInByaWNlIjp7ImNvZGUiOiJZQU4iLCJ2YWx1ZSI6IjEifSwiaW1hZ2VQcmVmaXgiOiIifSwidG9rZW4iOiJhN2FmZWFhYy1hNjk0LTRkMzktYTJlYi01ODNmMWJjMTliMWIiLCJjcmVhdGVkIjoxNjc0NzYzOTIwfSx7InByb2R1Y3QiOnsiaWQiOiJUZXN0UHJvZHVjdCIsInRpdGxlIjoi0KLQtdGB0YLQu9C+0LsiLCJkZXNjcmlwdGlvbiI6IiIsInByaWNlIjp7ImNvZGUiOiJZQU4iLCJ2YWx1ZSI6IjEifSwiaW1hZ2VQcmVmaXgiOiIifSwidG9rZW4iOiI0YjUxMjY2NC1hMmY5LTQ5MDYtYWQyNi02NmRiNzZhNjliODIiLCJjcmVhdGVkIjoxNjc0NzYzNzkzfV19\"}";
            UpdatePurchasedProducts(JsonUtility.FromJson<GetPurchasedProductsResponse>(sampleResponseJson).purchasedProducts);
#else
            Billing.GetPurchasedProducts(purchasedProductsResponse => UpdatePurchasedProducts(purchasedProductsResponse.purchasedProducts));
#endif
        }

        private void UpdatePurchasedProducts(PurchasedProduct[] purchasedProducts)
        {
            ClearPurchasedProducts();

            foreach (PurchasedProduct purchasedProduct in purchasedProducts)
            {
                PurchasedProductPanel purchasedProductPanel = Instantiate(_purchasedProductPanelTemplate, _purchasedProductsLayoutGroup.transform);
                _purchasedProductPanels.Add(purchasedProductPanel);

                purchasedProductPanel.gameObject.SetActive(true);
                purchasedProductPanel.PurchasedProduct = purchasedProduct;
            }
        }

        private void ClearPurchasedProducts()
        {
            foreach (PurchasedProductPanel purchasedProductPanel in _purchasedProductPanels)
                Destroy(purchasedProductPanel.gameObject);
        }
    }
}
