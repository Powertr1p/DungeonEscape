﻿using System;
using System.Collections;
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
        private const string video = "video";
        private const string gameId = "3651033";

        private bool isAdvOnCooldown;

        private void Start()
        {
            Advertisement.Initialize(gameId, false);

            OnAdsSuccess = () =>
            {
                GameEventsHandler.Instance.AddDiamonds(100);
                ShopDisplayUI.Instance.UpdatePlayerDiamonds();
                ShopDisplayUI.Instance.ShowSuccessAdShownMessage();
            };
        }
        
        public void ShowRewardedAds()
        {
            if (isAdvOnCooldown) return;

            if (Advertisement.IsReady(rewardedVideo))
            {
                var options = new ShowOptions
                {
                    resultCallback = HandleShowRewardedResult
                }; 
                
                Advertisement.Show(rewardedVideo, options);
                StartCoroutine(Cooldown());
            }
        }

        public void ShowVideo()
        {
            var deathCount = GameEventsHandler.Instance.PlayerDeathCount;
            if (deathCount % 3 != 0) return;
            
            if (Advertisement.IsReady(video))
                Advertisement.Show(video);
        }
        
        private void HandleShowRewardedResult(ShowResult result)
        {
            switch (result)
            {
                case ShowResult.Finished:
                    OnAdsSuccess?.Invoke();
                    break;
                case ShowResult.Skipped:
                    break;
                case ShowResult.Failed:
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
