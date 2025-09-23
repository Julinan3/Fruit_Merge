using Unity.Services.LevelPlay;
using UnityEngine;
using System.Collections.Generic;

public class MultiRewardedAdManager : MonoBehaviour
{
    private LevelPlayBannerAd bannerAd;
    private LevelPlayInterstitialAd interstitialAd;
    private List<LevelPlayRewardedAd> rewardedAds = new List<LevelPlayRewardedAd>();

    [Header("Rewarded Ads (7 adet)")]
    [SerializeField] private List<string> rewardedAdUnitIds = new List<string>();

    [Header("Banner & Interstitial")]
    [SerializeField] private string bannerAdUnitId;
    [SerializeField] private string interstitialAdUnitId;

    private void Start()
    {
        LevelPlay.ValidateIntegration();

        LevelPlay.Init(AdConfig.AppKey);

        LevelPlay.OnInitSuccess += OnInitSuccess;
        LevelPlay.OnInitFailed += OnInitFailed;
    }

    private void OnInitSuccess(LevelPlayConfiguration config)
    {
        Debug.Log("LevelPlay SDK initialized successfully");

        // Banner
        if (!string.IsNullOrEmpty(bannerAdUnitId))
        {
            bannerAd = new LevelPlayBannerAd(bannerAdUnitId);
            bannerAd.OnAdLoaded += adInfo => Debug.Log("Banner loaded");
            bannerAd.OnAdLoadFailed += error => Debug.LogError("Banner load failed: " + error);
            bannerAd.OnAdDisplayed += adInfo => Debug.Log("Banner displayed");
            bannerAd.OnAdDisplayFailed += (adInfo, error) => Debug.LogError("Banner display failed: " + error);
            bannerAd.OnAdClicked += adInfo => Debug.Log("Banner clicked");
            bannerAd.OnAdCollapsed += adInfo => Debug.Log("Banner collapsed");
            bannerAd.OnAdExpanded += adInfo => Debug.Log("Banner expanded");
            bannerAd.OnAdLeftApplication += adInfo => Debug.Log("Banner left application");
        }

        // Interstitial
        if (!string.IsNullOrEmpty(interstitialAdUnitId))
        {
            interstitialAd = new LevelPlayInterstitialAd(interstitialAdUnitId);
            interstitialAd.OnAdLoaded += adInfo => Debug.Log("Interstitial loaded");
            interstitialAd.OnAdLoadFailed += error => Debug.LogError("Interstitial load failed: " + error);
            interstitialAd.OnAdDisplayed += adInfo => Debug.Log("Interstitial displayed");
            interstitialAd.OnAdDisplayFailed += (adInfo, error) => Debug.LogError("Interstitial display failed: " + error);
            interstitialAd.OnAdClicked += adInfo => Debug.Log("Interstitial clicked");
            interstitialAd.OnAdClosed += adInfo => Debug.Log("Interstitial closed");
            interstitialAd.OnAdInfoChanged += adInfo => Debug.Log("Interstitial info changed");
        }

        // Rewarded Ads
        for (int i = 0; i < rewardedAdUnitIds.Count; i++)
        {
            string adUnitId = rewardedAdUnitIds[i];
            var rewardedAd = new LevelPlayRewardedAd(adUnitId);

            int index = i; // closure için
            rewardedAd.OnAdLoaded += adInfo => Debug.Log($"Rewarded {index} loaded");
            rewardedAd.OnAdLoadFailed += error => Debug.LogError($"Rewarded {index} load failed: {error}");
            rewardedAd.OnAdDisplayed += adInfo => Debug.Log($"Rewarded {index} displayed");
            rewardedAd.OnAdDisplayFailed += (adInfo, error) => Debug.LogError($"Rewarded {index} display failed: {error}");
            rewardedAd.OnAdRewarded += (adInfo, reward) =>
            {
                Debug.Log($"Rewarded {index} ödül verildi: {reward.Amount}");
                // Oyun içi ödül verme fonksiyonu ekleyebilirsiniz
            };
            rewardedAd.OnAdClicked += adInfo => Debug.Log($"Rewarded {index} clicked");
            rewardedAd.OnAdClosed += adInfo => Debug.Log($"Rewarded {index} closed");
            rewardedAd.OnAdInfoChanged += adInfo => Debug.Log($"Rewarded {index} info changed");

            rewardedAds.Add(rewardedAd);
        }
    }

    private void OnInitFailed(LevelPlayInitError error)
    {
        Debug.LogError("LevelPlay SDK init failed: " + error);
    }

    #region Banner
    public void LoadBanner() => bannerAd?.LoadAd();
    public void HideBanner() => bannerAd?.HideAd();
    #endregion

    #region Interstitial
    public void LoadInterstitial() => interstitialAd?.LoadAd();
    public void ShowInterstitial()
    {
        if (interstitialAd != null && interstitialAd.IsAdReady())
            interstitialAd.ShowAd();
        else
            Debug.LogWarning("Interstitial not ready");
    }
    #endregion

    #region Rewarded
    public void LoadRewarded(int index)
    {
        if (index >= 0 && index < rewardedAds.Count)
            rewardedAds[index].LoadAd();
        else
            Debug.LogWarning("Rewarded index out of range");
    }

    public void ShowRewarded(int index)
    {
        if (index >= 0 && index < rewardedAds.Count)
        {
            if (rewardedAds[index].IsAdReady())
                rewardedAds[index].ShowAd();
            else
                Debug.LogWarning($"Rewarded {index} not ready");
        }
        else
            Debug.LogWarning("Rewarded index out of range");
        }
    #endregion

    private void OnDisable()
    {
        bannerAd?.DestroyAd();
        interstitialAd?.DestroyAd();

        foreach (var ad in rewardedAds)
            ad?.DestroyAd();
    }
}
