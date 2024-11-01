using CrazyGames;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdManager : MonoBehaviour
{

    public static AdManager Instance { get; private set; }
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    private void Start()
    {
        CrazySDK.Init(OnSDKInitialized);
    }

    public void ShowRewardedAd()
    {
        CrazySDK.Ad.RequestAd(CrazyAdType.Rewarded, () =>
        {
            Debug.Log("Ad started");
        }, (error) =>
        {
            Debug.Log("Ad error");
        }, () =>
        {
            RewardPlayer();
        });
    }

    private void RewardPlayer()
    {
        // Implement your logic to reward the player (e.g., allow them to continue the game)
        GameManager.Instance.WatchedAnAdToContinue();
        Debug.Log("Player rewarded!");
    }

    private void OnSDKInitialized()
    {
        Debug.Log("CrazyGames SDK initialized successfully");
        // You can now safely call other SDK methods here
    }
}
