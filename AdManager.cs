using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdManager : MonoBehaviour {
        
        public static AdManager Instance;

        public BannerView bannerView;
        public RewardBasedVideoAd rewardBasedVideo;
		
		// Application Specific Data
        public string appId;
        public string bannerId;
        public string inertialId;
        public string rewardAdId;


        private void Awake(){
                if (Instance != null) {
                        Destroy(this);
                }
                else {
                        Instance = this;
                        DontDestroyOnLoad(this);
                }
        }


        public void Start()
        {
                // Initialize the Google Mobile Ads SDK.
                MobileAds.Initialize(appId);

                // Get singleton reward based video ad reference.
                rewardBasedVideo = RewardBasedVideoAd.Instance;

                // Called when an ad request has successfully loaded.
                rewardBasedVideo.OnAdLoaded += HandleRewardBasedVideoLoaded;
                // Called when an ad request failed to load.
                rewardBasedVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
                // Called when the user should be rewarded for watching a video.
                rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
                // Called when the ad is closed.
                rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;

                RequestBanner();
                RequestInterstitial();
                RequestRewardBasedVideo();
        }


        public void RequestBanner()
        {
                string adUnitId = bannerId;

                // Create a 320x50 banner at the top of the screen.
                bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);

                // Create an empty ad request.
                AdRequest request = new AdRequest.Builder().Build();

                // Load the banner with the request.
                bannerView.LoadAd(request);
        }

        private InterstitialAd _interstitial;

        private void RequestInterstitial()
        {
                string adUnitId = inertialId;
                
                // Initialize an InterstitialAd.
                _interstitial = new InterstitialAd(adUnitId);

                // Called when an ad request failed to load.
                _interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
                // Called when the ad is closed.
                _interstitial.OnAdClosed += HandleOnAdClosed;

                // Create an empty ad request.
                AdRequest request = new AdRequest.Builder().Build();
                // Load the interstitial with the request.
                _interstitial.LoadAd(request);
        }

        public void ShowInterestitialAd()
        {
                if (_interstitial.IsLoaded()) {
                        _interstitial.Show();
                }
        }

        private void RequestRewardBasedVideo() {
                string adUnitId = rewardAdId;

                // Create an empty ad request.
                AdRequest request = new AdRequest.Builder().Build();
                // Load the rewarded video ad with the request.
                this.rewardBasedVideo.LoadAd(request, adUnitId);
        }

        public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
        {
                print("HandleRewardBasedVideoLoaded event received");
        }

        public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
        {
                RequestRewardBasedVideo();  
        }

        public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
        {
                RequestRewardBasedVideo();
        }

        public void HandleRewardBasedVideoRewarded(object sender, Reward args)
        { 
			
        }

        public void ShowRewardVideoAd()
        {
                Debug.Log("Show video ad.");
                if (rewardBasedVideo.IsLoaded ()) {
                        rewardBasedVideo.Show ();
                } else {
                        RequestRewardBasedVideo();  
                }
        }

        public void HideBannerAd() {
                bannerView.Hide();
                bannerView.Destroy();
        }

        void OnDestroy() {
                bannerView.Hide();
                bannerView.Destroy();
        }

        public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
        {
			
        }


        public void HandleOnAdClosed(object sender, EventArgs args)
        {
			
        }
}



