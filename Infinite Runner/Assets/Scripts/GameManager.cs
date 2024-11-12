using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using CrazyGames;

public class GameManager : MonoBehaviour
{
    public List<GameObject> allObstacles;
    public float speed = 30f;
    public float maxSpeed;
    public float accelerationRate;
    public float score;

    public GameObject youDiedMenu;
    public GameObject pausedMenu;

    public Tsunami tsunami;

    public Volume volume;
    public DepthOfField depthOfField;

    public TMP_Text scoreText;
    public TMP_Text highScoreText;

    public bool isOnRoad1;
    public bool isOnRoad2;
    public bool isOnRoad3;
    public bool areaFlooded;

    public bool isGamePaused = false;

    public static GameManager Instance { get; private set; }

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

            // Add listener for scene load events
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Main")
        {
            KeepTrackOfScore();
            IncreaseSpeedOverTime();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pausedMenu.SetActive(true);
            StopOrStartGame();
        }

    }

    public void PlayerDeath()
    {
        StopOrStartGame();
        youDiedMenu.SetActive(true);
        SaveHighScore();
        LoadHighScore();
    }

    private void KeepTrackOfScore()
    {
        score += speed * Time.deltaTime;
        int scoreAsInt = (int)score;
        scoreText.text = scoreAsInt.ToString();
    }

    private void IncreaseSpeedOverTime()
    {
        // Increment speed with acceleration rate
        speed += accelerationRate * Time.deltaTime;

        // Cap the speed to maxSpeed
        if (speed > maxSpeed)
        {
            speed = maxSpeed;
        }
    }

    public void StopOrStartGame()
    {
        if (!isGamePaused)
        {
            TurnOffOrOnBlur(true);
            SetTimeScale(0f);
        }else
        {
            TurnOffOrOnBlur(false);
            SetTimeScale(1f);
        }

        isGamePaused = !isGamePaused;
    }

    public void WatchedAnAdToContinue()
    {
        youDiedMenu.SetActive(false);

        foreach (GameObject obstacle in allObstacles)
        {
            Destroy(obstacle);
        }
        allObstacles.Clear();
        tsunami.Reset();
        StopOrStartGame();
    }

    private void TurnOffOrOnBlur(bool blur)
    {
        depthOfField.active = blur;
    }

    public void SetTimeScale(float timeScale)
    {
        Time.timeScale = timeScale;
    }

    public void RestartGame(string sceneName)
    {
        isGamePaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
        GameManager.Instance.speed = 30f;
        tsunami.Reset();
    }

    public void SaveHighScore()
    {
        int currentHighScore = PlayerPrefs.GetInt("HighScore", 0); // Retrieve saved high score, default to 0
        if (score > currentHighScore)
        {
            PlayerPrefs.SetInt("HighScore", (int)score); // Save new high score
            PlayerPrefs.Save(); // Ensure the score is saved to disk
            Debug.Log("New high score saved: " + score);
        }
    }

    public void LoadHighScore()
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0); // Load high score, default to 0
        highScoreText.text = "Current Highscore: " + highScore; // Update text field
    }

    private void OnSDKInitialized()
    {
        Debug.Log("CrazyGames SDK initialized successfully");
        // You can now safely call other SDK methods here
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Main")
        {
            // Try to fetch the Depth of Field component when the Main scene is loaded
            if (volume != null && volume.profile != null)
            {
                volume.profile.TryGet(out depthOfField);
                Debug.Log("Depth of Field component loaded in Main scene.");
            }
        }
    }
}
