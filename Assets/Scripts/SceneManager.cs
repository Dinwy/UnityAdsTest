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
	public TMPro.TextMeshProUGUI StatusText;
	public TMPro.TextMeshProUGUI Counter;

#if UNITY_ANDROID || UNITY_EDITOR
	private readonly string[] _bannerAdUnits = { "b195f8dd8ded45fe847ad89ed1d016da" };
	private readonly string[] _interstitialAdUnits = { "24534e1901884e398f1253216226017e", "bb45288e5142444aae022b55a6035d0c" };
	private readonly string[] _rewardedAdUnits = { "920b6145fb1546cf8b5cf2ac34638bb7", "30c362fc0e0a46b991d8a543e7bfde7c" };
	private int interstitialAdIdx = 0;
	private int rewardedAdUnitsIdx = 0;
#endif

	private void OnConsentDialogLoaded()
	{
		MoPub.ShowConsentDialog();
	}

	private void Start()
	{
		MoPubManager.OnSdkInitializedEvent += (adUnitId) =>
		{
			Debug.Log($"{this.GetType().Name}: Initialized SDK. {adUnitId}");
			StatusText.text += $"{this.GetType().Name}: Initialized SDK. {adUnitId}";
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
			MoPub.RequestInterstitialAd(_interstitialAdUnits[interstitialAdIdx]);
		});

		LoadRewardedVideoAd.onClick.AddListener(() =>
		{
			MoPub.RequestRewardedVideo(_rewardedAdUnits[rewardedAdUnitsIdx]);
		});

		ShowInterstitialAd.onClick.AddListener(() =>
		{
			MoPub.ShowInterstitialAd(_interstitialAdUnits[interstitialAdIdx]);
			interstitialAdIdx = (interstitialAdIdx + 1) % _interstitialAdUnits.Length;
		});

		ShowRewardedVideoAd.onClick.AddListener(() =>
		{
			MoPub.ShowRewardedVideo(_rewardedAdUnits[rewardedAdUnitsIdx]);
			rewardedAdUnitsIdx = (rewardedAdUnitsIdx + 1) % _rewardedAdUnits.Length;
		});

		MoPubManager.OnAdLoadedEvent += (s, f) => Debug.Log($"OnAdLoadedEvent: {s}, {f}");
		MoPubManager.OnInterstitialLoadedEvent += (placementId) => StatusText.text = $"Interstitial loaded: {placementId}";
		MoPubManager.OnInterstitialFailedEvent += (placementId, e) => StatusText.text = $"Interstitial failed: {placementId}, {e}";
		MoPubManager.OnInterstitialDismissedEvent += (placementId) => StatusText.text = $"Interstitial dismissed: {placementId}";
		MoPubManager.OnInterstitialShownEvent += (placementId) => StatusText.text = $"Interstitial shown: {placementId}";

		MoPubManager.OnRewardedVideoLoadedEvent += (placementId) => StatusText.text = $"RewardedVideo loaded: {placementId}";
		MoPubManager.OnRewardedVideoFailedToPlayEvent += (s, e) => StatusText.text = $"RewardedVideo Failed to play: {s}, {e}";
		MoPubManager.OnRewardedVideoFailedEvent += (s, e) => StatusText.text = $"RewardedVideo Failed: {s}, {e}";
		MoPubManager.OnRewardedVideoShownEvent += (placementId) => StatusText.text = $"RewardedVideo shown: {placementId}";
		MoPubManager.OnRewardedVideoClosedEvent += (placementId) => StatusText.text = $"RewardedVideo Closed: {placementId}";
	}
}
