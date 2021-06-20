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
	// private readonly string[] _interstitialAdUnits = { "252412d5e9364a05ab77d9396346d73d" };
	// private readonly string[] _rewardedAdUnits = { "920b6145fb1546cf8b5cf2ac34638bb7" };
	private readonly string[] _interstitialAdUnits = { "bb45288e5142444aae022b55a6035d0c" };
	private readonly string[] _rewardedAdUnits = { "30c362fc0e0a46b991d8a543e7bfde7c" };
#endif

	private void OnConsentDialogLoaded()
	{
		MoPub.ShowConsentDialog();
	}

	private void Start()
	{
		MoPubManager.OnAdLoadedEvent += (str, f) =>
		{
			Debug.Log($"AdLoadEvent: {str}, {f}");
		};

		MoPubManager.OnSdkInitializedEvent += (adUnitId) =>
		{
			Debug.Log($"{this.GetType().Name}: Initialized SDK. {adUnitId}");

			// Start loading the consent dialog. This call fails if the user has opted out of ad personalization
			// MoPub.LoadConsentDialog();

			// // If you have subscribed to listen to events, in the OnConsentDialogLoadedEvent callback, show the consent dialog that the SDK has prepared for you.
			// MoPubManager.OnConsentDialogLoadedEvent += OnConsentDialogLoaded;
		};

		InitializeMoPubButton.onClick.AddListener(() =>
		{
			MoPub.InitializeSdk(MoPubManager.Instance.SdkConfiguration);
			MoPub.LoadBannerPluginsForAdUnits(_bannerAdUnits);
			MoPub.LoadInterstitialPluginsForAdUnits(_interstitialAdUnits);
			MoPub.LoadRewardedVideoPluginsForAdUnits(_rewardedAdUnits);
			MoPub.RequestBanner(_bannerAdUnits[0], MoPub.AdPosition.BottomCenter);

			MoPub.ShowBanner(_bannerAdUnits[0], true);
		});

		LoadInterstitialAd.onClick.AddListener(() =>
		{
			MoPub.RequestInterstitialAd(_interstitialAdUnits[0]);
		});
		LoadRewardedVideoAd.onClick.AddListener(() =>
		{
			foreach (var item in _rewardedAdUnits)
			{
				MoPub.RequestRewardedVideo(item);
			}
		});
		ShowInterstitialAd.onClick.AddListener(() =>
		{
			MoPub.ShowInterstitialAd(_interstitialAdUnits[0]);
		});
		ShowRewardedVideoAd.onClick.AddListener(() =>
		{
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
