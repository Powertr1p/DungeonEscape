using UnityEngine;
using UnityEngine.UI;

namespace Shop
{
    public class ShopUIUpdater : MonoBehaviour
    {
        private static ShopUIUpdater _instance;
        [SerializeField] private GameObject _selectingLine;

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

        [SerializeField] private Text _diamondsCount;

        private void Awake()
        {
            _instance = this;
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
