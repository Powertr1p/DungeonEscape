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

        [SerializeField] private GameObject _successBoughtMessage;
        
        [SerializeField] private float _selectingLineMidPos = 54;
        [SerializeField] private float _selectingLineStep = 108;

        public bool IsShopEnabled;

        private TextMeshProUGUI _messageText;

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
            _messageText = _successBoughtMessage.GetComponentInChildren<TextMeshProUGUI>();
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
            _successBoughtMessage.SetActive(true);
            _messageText.text = $"You successfully bought {name}";
        }
        
        public void HideSuccessItemBoughtItemMessage()
        {
            _successBoughtMessage.SetActive(false);
        }
    }
}
