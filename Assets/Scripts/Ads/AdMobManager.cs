using UnityEngine;
using GoogleMobileAds.Api;
using System;
using Random = UnityEngine.Random;

public class AdMobManager : MonoBehaviour
{
    public bool test = false;
    private string rewarded_coin_video = "ca-app-pub-8223583860856544/8518329635";
    private string rewarded_exp_video = "ca-app-pub-8223583860856544/2216096421";
    private string interesticial = "ca-app-pub-8223583860856544/2545794133";
    private string rewarded_coin_video_test = "ca-app-pub-3940256099942544/5224354917";
    private string rewarded_exp_video_test = "ca-app-pub-3940256099942544/5224354917";
    private string interesticial_test = "ca-app-pub-3940256099942544/1033173712";

    private RewardedAd rewardedCoinAd;
    private RewardedAd rewardedExpAd;
    private InterstitialAd interstitialAd;

    public LevelSystem levelSystem;
    public GameController gameController;
    private bool canShowAd = false;

    // Start is called before the first frame update
    void Start()
    {
        MobileAds.Initialize(initStatus => { });
        this.rewardedCoinAd = RequestRewardedVideo(test ? rewarded_coin_video_test : rewarded_coin_video);
        this.rewardedExpAd = RequestRewardedVideo(test ? rewarded_exp_video_test : rewarded_exp_video);
        this.interstitialAd = RequestInteresticialAd(test ? interesticial_test : interesticial);
    }

    private InterstitialAd RequestInteresticialAd(string adUnitId)
    {
        InterstitialAd interstitialAd = new InterstitialAd(adUnitId);

        interstitialAd.OnAdLoaded += HandleOnAdLoaded;
        interstitialAd.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        interstitialAd.OnAdOpening += HandleOnAdOpened;
        interstitialAd.OnAdClosed += HandleOnAdClosed;
        interstitialAd.OnAdLeavingApplication += HandleOnAdLeavingApplication;

        AdRequest request = new AdRequest.Builder().Build();
        interstitialAd.LoadAd(request);
        return interstitialAd;
    }

    private RewardedAd RequestRewardedVideo(string adUnitId)
    {
        RewardedAd rewardedAd = new RewardedAd(adUnitId);


        rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        rewardedAd.OnAdClosed += HandleRewardedAdClosed;
        rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        rewardedAd.LoadAd(request);
        return rewardedAd;
    }

    public void ShowInteresticialAd()
    {
        int randomNumber = Random.Range(0, 4);
        if (randomNumber == 1)
        {
            if (this.interstitialAd == null)
            {
                this.interstitialAd = RequestInteresticialAd(test ? interesticial_test : interesticial);
            }
            if (this.interstitialAd.IsLoaded())
            {
                this.interstitialAd.Show();
                this.interstitialAd = RequestInteresticialAd(test ? interesticial_test : interesticial);
            }
        }

    }

    public void ShowRewardedCoinAd()
    {
        canShowAd = true;
        if(this.rewardedCoinAd == null)
        {
            this.rewardedCoinAd = RequestRewardedVideo(test ? rewarded_coin_video_test : rewarded_coin_video);
        }
        rewardedCoinAd.OnUserEarnedReward += HandleCoinEarnedReward;
        if (this.rewardedCoinAd.IsLoaded())
        {
            this.rewardedCoinAd.Show();
        }
    }

    public void ShowRewardedExpAd()
    {
        canShowAd = true;
        if (this.rewardedExpAd == null)
        {
            this.rewardedExpAd = RequestRewardedVideo(test ? rewarded_exp_video_test : rewarded_exp_video);
        }
        rewardedExpAd.OnUserEarnedReward += HandleExpEarnedReward;
        if (this.rewardedExpAd.IsLoaded())
        {
            this.rewardedExpAd.Show();
        }
    }


    // Rewarded video events
    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdLoaded event received");
        if (canShowAd)
        {
            if (this.rewardedCoinAd != null)
            {
                if (this.rewardedCoinAd.IsLoaded())
                {
                    this.rewardedCoinAd.Show();
                }
            }
            if (this.rewardedExpAd != null)
            {
                if (this.rewardedExpAd.IsLoaded())
                {
                    this.rewardedExpAd.Show();
                }
            }
        }
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToLoad event received with message: "
                             + args.Message);
        this.rewardedCoinAd = null;
        this.rewardedExpAd = null;
    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdOpening event received");
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToShow event received with message: "
                             + args.Message);
        this.rewardedCoinAd = null;
        this.rewardedExpAd = null;
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdClosed event received");
        this.rewardedCoinAd = null;
        this.rewardedExpAd = null;
    }

    public void HandleCoinEarnedReward(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        Debug.Log("HandleRewardedAdRewarded event received for "
                        + amount.ToString() + " " + type);
        gameController.RewardedCoins((int)amount);
        this.rewardedCoinAd = RequestRewardedVideo(test ? rewarded_coin_video_test : rewarded_coin_video);
        this.rewardedExpAd = null;
    }

    public void HandleExpEarnedReward(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        Debug.Log("HandleRewardedAdRewarded event received for "
                        + amount.ToString() + " " + type);
        levelSystem.AddExperience((int)amount);
        this.rewardedCoinAd = null;
        this.rewardedExpAd = RequestRewardedVideo(test ? rewarded_exp_video_test : rewarded_exp_video);
    }


    // Interesticial events


    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
                            + args.Message);
        this.interstitialAd = null;
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
        this.interstitialAd = null;
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
        this.interstitialAd = null;
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeavingApplication event received");
        this.interstitialAd = null;
    }
}
