using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class RewardedAdsButton : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] public Button _showAdButtonVidas;
    [SerializeField] string _androidAdUnitId = "Rewarded_Android";    
    string _adUnitId = null; // This will remain null for unsupported platforms
    
    private int  currentLevel;
    private bool freeAds;

    void Awake()
    {
        // Get the Ad Unit ID for the current platform:    
        _adUnitId = _androidAdUnitId;

        //Disable the button until the ad is ready to show:        
        _showAdButtonVidas.interactable = false;

        LoadAd();
    }

    private void Start()
    {
        OnUnityAdsAdLoaded(_adUnitId);        
        currentLevel = SceneManager.GetActiveScene().buildIndex;
        freeAds = true;
    }

    private void Update()
    {
        if(currentLevel != SceneManager.GetActiveScene().buildIndex)
        {
            freeAds = true;
            currentLevel = SceneManager.GetActiveScene().buildIndex;
            LoadAd();
        }        
    }

    // Load content to the Ad Unit:
    public void LoadAd()
    {
        // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
        Debug.Log("Loading Ad: " + _adUnitId);
        Advertisement.Load(_adUnitId, this);
    }

    // If the ad successfully loads, add a listener to the button and enable it:
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("Ad Loaded: " + adUnitId);

        if (adUnitId.Equals(_adUnitId))
        {
            // Configure the button to call the ShowAd() method when clicked:          
            _showAdButtonVidas.onClick.AddListener(ShowAd);
            // Enable the button for users to click:           
            _showAdButtonVidas.interactable = true;
        }
    }

    // Implement a method to execute when the user clicks the button:
    public void ShowAd()
    {
        // Disable the button:        
        _showAdButtonVidas.interactable = false;
        // Then show the ad:
        Advertisement.Show(_adUnitId, this);
        freeAds = false;
    }

    // Implement the Show Listener's OnUnityAdsShowComplete callback method to determine if the user gets a reward:
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {                
        if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log("Unity Ads Rewarded Ad Completed");
            // Grant a reward.           
            FindObjectOfType<GameManager>().lifesManager();
            // Load another ad:
            if(freeAds)
            Advertisement.Load(_adUnitId, this);           
        }
    }

    // Implement Load and Show Listener error callbacks:
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Use the error details to determine whether to try to load another ad.
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Use the error details to determine whether to try to load another ad.
    }

    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }

    void OnDestroy()
    {
        // Clean up the button listeners:    
        _showAdButtonVidas.onClick.RemoveAllListeners();
    }
}