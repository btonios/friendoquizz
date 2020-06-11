using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AdsManager : MonoBehaviour
{
    private InterstitialAd interstitial;
    private BannerView banner;

    //keep AdsManager between scenes
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("AdsManager");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        
        // Configure TagForChildDirectedTreatment and test device IDs.
        List<String> deviceIds = new List<String>() { AdRequest.TestDeviceSimulator };

        deviceIds.Add("B984E1CBBC92E8E487DCFAB5D4E5A0E3");

        RequestConfiguration requestConfiguration =
            new RequestConfiguration.Builder()
            .SetTagForChildDirectedTreatment(TagForChildDirectedTreatment.Unspecified)
            .SetTestDeviceIds(deviceIds).build();

        MobileAds.SetRequestConfiguration(requestConfiguration);

        MobileAds.Initialize(initStatus => { });
    }
  
    public void RequestInterstitial()
    {
        DestroyInterstitialAd();
        #if UNITY_ANDROID
            string adUnitId = "ca-app-pub-3940256099942544/1033173712";
        #elif UNITY_IPHONE
            string adUnitId = "";
        #else
            string adUnitId = "unexpected_platform";
        #endif

        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(adUnitId);

        AdRequest request = new AdRequest.Builder().Build();
        this.interstitial.LoadAd(request);
    }


    public void ShowInterstitialAd()
    {

        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
        }
    }

     public void DestroyInterstitialAd()
    {

        if (this.interstitial != null)
        {
            this.interstitial.Destroy();
        }
    }


    public void RequestBanner()
    {
        #if UNITY_ANDROID
            string bannerId = "ca-app-pub-3940256099942544/6300978111";
        #elif UNITY_IPHONE
            string bannerId = "";
        #else
            string bannerId = "unexpected_platform";
        #endif
        
        this.banner = new BannerView(bannerId, AdSize.SmartBanner, AdPosition.Bottom);
        
        AdRequest request = new AdRequest.Builder().Build();
        this.banner.LoadAd(request);
    }

    // FOR EVENTS AND DELEGATES FOR ADS
    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
        }
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        Debug.Log("Ad Failed To Load");
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeavingApplication event received");
    }




}

