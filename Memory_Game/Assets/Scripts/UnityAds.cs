using UnityEngine;
using UnityEngine.Advertisements;

public class UnityAdsBannerTest : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] string _androidGameId;
    [SerializeField] string _iOSGameId;
    [SerializeField] bool _testMode = true;

    [SerializeField] string _androidAdUnitId = "Banner_Android"; // Change to match Dashboard
    [SerializeField] string _iOSAdUnitId = "Banner_iOS";         // Change to match Dashboard

    private string _gameId;
    private string _adUnitId;

    void Start()
    {
#if UNITY_IOS
        _gameId = _iOSGameId;
        _adUnitId = _iOSAdUnitId;
#elif UNITY_ANDROID
        _gameId = _androidGameId;
        _adUnitId = _androidAdUnitId;
#else
        _gameId = _androidGameId; // Editor uses Android ID for testing
        _adUnitId = _androidAdUnitId;
#endif

        Debug.Log($"Initializing Unity Ads with Game ID: {_gameId}");
        Advertisement.Initialize(_gameId, _testMode, this);
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete — loading banner...");
        Advertisement.Banner.SetPosition(BannerPosition.TOP_CENTER);

        BannerLoadOptions options = new BannerLoadOptions
        {
            loadCallback = () =>
            {
                Debug.Log("Banner loaded — showing now.");
                Advertisement.Banner.Show(_adUnitId);
            },
            errorCallback = (message) =>
            {
                Debug.LogError($"Banner Load Failed: {message}");
            }
        };

        Advertisement.Banner.Load(_adUnitId, options);
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.LogError($"Unity Ads Initialization Failed: {error} - {message}");
    }
}