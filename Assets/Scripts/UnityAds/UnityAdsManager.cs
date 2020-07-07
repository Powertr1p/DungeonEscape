﻿using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using Shop;
using UnityEngine;
using UnityEngine.Advertisements;

namespace UnityAds
{
    public class UnityAdsManager : MonoBehaviour
    {
        public Action OnAdsSuccess;
        
        private const string rewardedVideo = "rewardedVideo";
        private const string gameId = "3651032";

        private bool isAdvOnCooldown;

        private void Start()
        {
            Advertisement.Initialize(gameId, true);

            OnAdsSuccess = () =>
            {
                Debug.Log("Ad finished");
                GameEventsHandler.Instance.AddDiamonds(100);
                ShopDisplayUI.Instance.UpdatePlayerDiamonds();
                ShopDisplayUI.Instance.ShowSuccessAdShownMessage();
            };
        }
        
        public void ShowRewardedAds()
        {
            if (isAdvOnCooldown) return;

            Debug.Log("Showing ads");

            if (Advertisement.IsReady(rewardedVideo))
            {
                var options = new ShowOptions
                {
                    resultCallback = HandleShowResult
                }; 
                
                Advertisement.Show(rewardedVideo, options);
                StartCoroutine(Cooldown());
            }
        }

        private void HandleShowResult(ShowResult result)
        {
            switch (result)
            {
                case ShowResult.Finished:
                    OnAdsSuccess?.Invoke();
                    break;
                case ShowResult.Skipped:
                    Debug.Log("Ad skipped");
                    break;
                case ShowResult.Failed:
                    Debug.Log("Ad failed");
                    break;
            }
        }

        private IEnumerator Cooldown()
        {
            isAdvOnCooldown = true;
            yield return new WaitForSeconds(120);
            isAdvOnCooldown = false;
        }
    }
}
