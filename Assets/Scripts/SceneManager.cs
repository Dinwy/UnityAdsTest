using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class SceneManager : MonoBehaviour
{
	public MoPubManager MoPubManager;
	public Button InitializeMoPubButton;
	public Button LoadInterstitialAd;
	public Button LoadRewardedVideoAd;
	public Button ShowInterstitialAd;
	public Button ShowRewardedVideoAd;

#if UNITY_ANDROID || UNITY_EDITOR
	private readonly string[] _bannerAdUnits = { "b195f8dd8ded45fe847ad89ed1d016da" };
	private readonly string[] _interstitialAdUnits = { "24534e1901884e398f1253216226017e" };
	private readonly string[] _rewardedAdUnits = { "920b6145fb1546cf8b5cf2ac34638bb7", "a96ae2ef41d44822af45c6328c4e1eb1" };
#endif

	private void Awake()
	{
		MoPubManager.OnAdLoadedEvent += (str, f) =>
		{
			Debug.Log($"{str}, {f}: ADLOADED");
		};
	}

	private void Start()
	{
		MoPubManager.OnSdkInitializedEvent += (adUnitId) =>
		{
			Debug.Log($"{this.GetType().Name}: Initialized SDK. {adUnitId}");
			// MoPub.ShowBanner(bannerAdUnits[0], true);
		};

		InitializeMoPubButton.onClick.AddListener(() =>
		{
			MoPub.InitializeSdk(MoPubManager.Instance.SdkConfiguration);
			MoPub.LoadBannerPluginsForAdUnits(_bannerAdUnits);
			MoPub.LoadInterstitialPluginsForAdUnits(_interstitialAdUnits);
			MoPub.LoadRewardedVideoPluginsForAdUnits(_rewardedAdUnits);
		});

		LoadInterstitialAd.onClick.AddListener(() =>
		{
			MoPub.RequestInterstitialAd(_interstitialAdUnits[0]);
		});
		LoadRewardedVideoAd.onClick.AddListener(() =>
		{
			// MoPub.RequestRewardedVideo("Rewarded_Android");
			MoPub.RequestRewardedVideo(_rewardedAdUnits[0]);
		});
		ShowInterstitialAd.onClick.AddListener(() =>
		{
			// MoPub.ShowRewardedVideo("Rewarded_Android");
			MoPub.ShowInterstitialAd(_interstitialAdUnits[0]);
		});
		ShowRewardedVideoAd.onClick.AddListener(() =>
		{
			// MoPub.ShowRewardedVideo("Rewarded_Android");
			MoPub.ShowRewardedVideo(_rewardedAdUnits[0]);
		});
	}

	public void LogIt(string log)
	{

	}

	private void OnDestroy()
	{
		// ShowInterstitialAd.onClick.RemoveAllListeners();
	}

}
