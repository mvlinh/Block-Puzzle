using UnityEngine;
using UnityEngine.Advertisements;
using Gemmob;
public class sdk : SingletonBind<sdk>, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    //[SerializeField] string _androidAdUnitId;
    //[SerializeField] string _iOsAdUnitId;
    [SerializeField] string _androidGameId;
    [SerializeField] string _iOSGameId;
    private string _gameId;
    string _adUnitId;

    //[SerializeField] string _androidAdUnitId = "Interstitial_Android";
    //[SerializeField] string _iOsAdUnitId = "Interstitial_iOS";

    [SerializeField] string _androidAdUnitId = "Android";
    [SerializeField] string _iOsAdUnitId = "iOS";

    [SerializeField] BannerPosition _bannerPosition = BannerPosition.CENTER;
   
    [SerializeField] bool _testMode = true;
   

    void Awake()
    {
        InitializeAds();
        _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
                ? _iOsAdUnitId
                : _androidAdUnitId;
    }
    private void Start()
    {
        Advertisement.Banner.SetPosition(_bannerPosition);
        ShowBannerAd();
    }
    public void InitializeAds()
    {
        _gameId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOSGameId
            : _androidGameId;
        Advertisement.Initialize(_gameId, _testMode, this);
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }
    //interads
    public void LoadAd()
    {
        Debug.Log("Loading Ad: " + _adUnitId);
        Advertisement.Load(_adUnitId, this);
    }
    public void ShowAd()
    {
        LoadAd();
        Debug.Log("Showing Ad: " + _adUnitId);
        Advertisement.Show(_adUnitId, this);
    }
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("Ad Loaded: " + adUnitId);

        if (adUnitId.Equals(_adUnitId))
        {
            // Configure the button to call the ShowAd() method when clicked:
            // Enable the button for users to click:
        }
    }

    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit: {adUnitId} - {error.ToString()} - {message}");
        // Optionally execute code if the Ad Unit fails to load, such as attempting to try again.
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Optionally execute code if the Ad Unit fails to show, such as loading another ad.
    }

    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState) { }
    //interads
    void OnDestroy()
    {
        // Clean up the button listeners:
    }
    // banner

    public void LoadBanner()
    {
        // Set up options to notify the SDK of load events:
        BannerLoadOptions options = new BannerLoadOptions
        {
            loadCallback = OnBannerLoaded,
            errorCallback = OnBannerError
        };

        // Load the Ad Unit with banner content:
        Advertisement.Banner.Load(_adUnitId, options);
    }

    // Implement code to execute when the loadCallback event triggers:
    void OnBannerLoaded()
    {
        Debug.Log("Banner loaded");

        // Configure the Show Banner button to call the ShowBannerAd() method when clicked:
        //_showBannerButton.onClick.AddListener(ShowBannerAd);
        // Configure the Hide Banner button to call the HideBannerAd() method when clicked:
        //_hideBannerButton.onClick.AddListener(HideBannerAd);

        // Enable both buttons:
        //_showBannerButton.interactable = true;
        //_hideBannerButton.interactable = true;
    }

    // Implement code to execute when the load errorCallback event triggers:
    void OnBannerError(string message)
    {
        Debug.Log($"Banner Error: {message}");
        // Optionally execute additional code, such as attempting to load another ad.
    }

    // Implement a method to call when the Show Banner button is clicked:
    void ShowBannerAd()
    {
        Debug.Log("banner");    
        LoadBanner();
        // Set up options to notify the SDK of show events:
        BannerOptions options = new BannerOptions
        {
            clickCallback = OnBannerClicked,
            hideCallback = OnBannerHidden,
            showCallback = OnBannerShown
        };
        // Show the loaded Banner Ad Unit:
        Advertisement.Banner.Show(_adUnitId, options);
    }

    // Implement a method to call when the Hide Banner button is clicked:
    void HideBannerAd()
    {
        // Hide the banner:
        Advertisement.Banner.Hide();
    }

    void OnBannerClicked() { }
    void OnBannerShown() { }
    void OnBannerHidden() { }


}