using System.Linq;
using System.Net.Mime;
using Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Shop
{
    public class ShopDisplayUI : MonoBehaviour
    {
        private static ShopDisplayUI _instance;
        
        [SerializeField] private GameObject _selectingLine;
        [SerializeField] private ShopItems _itemsInStock;

        [SerializeField] private TextMeshProUGUI _diamondsCount;
        
        [SerializeField] private Text[] _itemNamesToDisplay;
        [SerializeField] private Text[] _itemPricesToDisplay;

        [SerializeField] private GameObject _shopMessage;
        
        [SerializeField] private float _selectingLineMidPos = 54;
        [SerializeField] private float _selectingLineStep = 108;

        public bool IsShopEnabled;

        private TextMeshProUGUI _messageText;
        private Image _selectingLineImage;

        public static ShopDisplayUI Instance
        {
            get
            {
                if (_instance == null)
                    Debug.LogError("Instance is null");
                
                return _instance;
            }
        }

        private void Awake()
        {
            _instance = this;
            _messageText = _shopMessage.GetComponentInChildren<TextMeshProUGUI>();
            _selectingLineImage = _selectingLine.GetComponent<Image>();
        }

        private void Start()
        {
            GameEventsHandler.Instance.DiamondsCountUpdated += UpdatePlayerDiamonds;
        }

        public void PrintItemsNameAndPrice()
        {
            for (int i = 0; i < _itemNamesToDisplay.Length; i++)
            {
                _itemNamesToDisplay[i].text = _itemsInStock.GetItemById(i).ItemName;
                _itemPricesToDisplay[i].text = $"{_itemsInStock.GetItemById(i).ItemPrice}G";
            }
        }
        
        public void UpdatePlayerDiamonds()
        {
            var diamondsCount = GameEventsHandler.Instance.PlayerDiamondsCount;
            _diamondsCount.text = $"{diamondsCount}G";
        }

        public void UpdateSelectionLinePosition(int itemId)
        {
            var position = _selectingLine.transform.localPosition;
            _selectingLineImage.color = new Color(1,1,1,1);

            switch (itemId)
            {
                case 0:
                    _selectingLine.transform.localPosition = new Vector3(position.x, _selectingLineMidPos + _selectingLineStep);
                    break;
                case 1:
                    _selectingLine.transform.localPosition = new Vector3(position.x, _selectingLineMidPos);
                    break;
                case 2:
                    _selectingLine.transform.localPosition = new Vector3(position.x, _selectingLineMidPos - _selectingLineStep);
                    break;
            }
            
            _selectingLine.SetActive(true);
        }

        public void ShowSuccessItemBoughtItemMessage(string name)
        {
            ShowMessage("You successfully bought " + name);
        }

        public void ShowNotEnoughDiamodsMessage()
        {
            ShowMessage("You don't have enough diamonds!");
        }

        public void ShowSuccessAdShownMessage()
        {
            ShowMessage("You successfully earned 100G. Come back after 2 minutes.");
        }

        private void ShowMessage(string message)
        {
            _shopMessage.SetActive(true);
            _messageText.text = $"{message}";
        }

        public void HideShopMessage()
        {
            _shopMessage.SetActive(false);
        }

        public void ToggleOffBoughtItem(int id)
        {
            _itemNamesToDisplay[id].GetComponentInParent<Button>().interactable = false;
            _itemNamesToDisplay[id].color = new Color(1,1,1,0.2f);
            _itemPricesToDisplay[id].color = new Color(1, 1, 1, 0.2f);
            _selectingLineImage.color = new Color(1,1,1,0);
        }
    }
}
