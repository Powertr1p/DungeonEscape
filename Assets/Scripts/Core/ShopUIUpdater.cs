using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class ShopUIUpdater : MonoBehaviour
    {
        private static ShopUIUpdater _instance;

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

        public void OpenShop(int gemsCount)
        {
            _diamondsCount.text = gemsCount.ToString() + "G";
        }
    }
}
