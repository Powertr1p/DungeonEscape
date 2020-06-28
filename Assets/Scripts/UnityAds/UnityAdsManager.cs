using System.Runtime.CompilerServices;
using Core;
using Shop;
using UnityEngine;
using UnityEngine.Advertisements;

namespace UnityAds
{
    public class UnityAdsManager : MonoBehaviour
    {
        private const string rewardedVideo = "rewardedVideo";
        private const string gameId = "3651032";

        private void Start()
        {
            Advertisement.Initialize(gameId, true);
        }
        
        public void ShowRewardedAds()
        {
            Debug.Log("Showing ads");

            if (Advertisement.IsReady(rewardedVideo))
            {
                var options = new ShowOptions
                {
                    resultCallback = HandleShowResult
                };
                
                Advertisement.Show(rewardedVideo, options);
            }
        }

        private void HandleShowResult(ShowResult result)
        {
            switch (result)
            {
                case ShowResult.Finished:
                    Debug.Log("Ad finished");
                    GameEventsHandler.Instance.AddDiamonds(100);
                    ShopDisplayUI.Instance.DisplayPlayerDiamonds(GameEventsHandler.Instance.PlayerDiamondsCount);
                    break;
                case ShowResult.Skipped:
                    Debug.Log("Ad skipped");
                    break;
                case ShowResult.Failed:
                    Debug.Log("Ad failed");
                    break;
            }
        }
    }
}
