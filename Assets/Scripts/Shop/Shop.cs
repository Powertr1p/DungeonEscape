using Core;
using UnityEngine;

namespace Shop
{
    public class Shop : MonoBehaviour
    {
        private float positionY = 0;

        public void SelectItem(int itemNumber)
        {
            switch (itemNumber)
            {
                case 1:
                    positionY = 164;
                    break;
                case 2:
                    positionY = 54;
                    break;
                case 3:
                    positionY = -54;
                    break;
            }
            ShopUIUpdater.Instance.UpdateSelectionLinePosition(positionY);
        }
    }
}
