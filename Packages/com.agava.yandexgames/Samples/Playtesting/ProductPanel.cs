using UnityEngine;
using UnityEngine.UI;

namespace Agava.YandexGames.Samples
{
    public class ProductPanel : MonoBehaviour
    {
        [SerializeField]
        private Text _productIdText;

        public ProductResponse Product
        {
            set
            {
                _productIdText.text = value.id;
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
