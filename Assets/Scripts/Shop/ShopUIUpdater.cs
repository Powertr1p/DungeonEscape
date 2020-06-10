﻿using UnityEngine;
using UnityEngine.UI;

namespace Shop
{
    public class ShopUIUpdater : MonoBehaviour
    {
        private static ShopUIUpdater _instance;
        
        [SerializeField] private GameObject _selectingLine;
        [SerializeField] private ShopItems _itemsInStock;

        [SerializeField] private Text _diamondsCount;
        
        [SerializeField] private Text[] _itemNamesToDisplay;
        [SerializeField] private Text[] _itemPricesToDisplay;
        
        public bool IsShopEnabled;

        public static ShopUIUpdater Instance
        {
            get
            {
                if (_instance == null)
                {
                    Debug.LogError("Instance is null");
                }

                return _instance;
            }
        }

        private void Awake()
        {
            _instance = this;
        }

        public void PrintItemAttributes()
        {
            for (int i = 0; i < _itemNamesToDisplay.Length; i++)
            {
                _itemNamesToDisplay[i].text = _itemsInStock.GetItemById(i).ItemName;
                _itemPricesToDisplay[i].text = $"{_itemsInStock.GetItemById(i).ItemPrice}G";
            }
        }

        public void UpdateDiamondsCount(int gemsCount)
        {
            _diamondsCount.text = gemsCount + "G";
        }

        public void UpdateSelectionLinePosition(float yPosition)
        {
            _selectingLine.transform.localPosition = new Vector3(_selectingLine.transform.localPosition.x, yPosition,
                _selectingLine.transform.localPosition.z);
            _selectingLine.SetActive(true);
        }
    }
}
