using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary> Manages the state of the level </summary>
public class LevelManager : MonoBehaviour
{
    public int Score { get; private set; }
    private int highScore;
    private bool isPaused = false;

    private float initialPlatformSpawnTimer;
    private float platformSpawnTimer;
    private float initialPlatformSpeed = 0.07f;
    private float platformSpeed;

    private PlatformManager platformManager;
    private GameUIController uiController;
    [SerializeField] private InitialGameSettingsSO initialGameSettingsSO;

    //Singleton, no other LevelManagers are allowed to exist
    public static LevelManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
        EventManager.OnOutOfBounds += GameOver;
        EventManager.OnPlatformHit += IncrementScore;
    }

    void Start()
    {
        platformManager = PlatformManager.Instance;
        uiController = GameUIController.Instance;
        highScore = SaveStateController.LoadFromFile();

        initialPlatformSpawnTimer = initialGameSettingsSO.initialSpawnRate;
        platformSpawnTimer = initialPlatformSpawnTimer;
        platformSpeed = initialPlatformSpeed;

        SpawnFirstPlatform();
    }

    /// <summary>
    /// If a new highscore has been reached, save it
    /// Inform the UIController to display the game over panel
    /// </summary>
    public void GameOver()
    {
        if (Score > highScore)
        {
            highScore = Score;
            SaveStateController.SaveToFile(highScore);
            uiController.ShowGameOver(Score, highScore, true);
        }
        else
        {
            uiController.ShowGameOver(Score, highScore, false);
        }
        Time.timeScale = 0;
    }

    void Update()
    {
        TriggerPlatformSpawn();
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            TogglePause();
        }
        IncreaseSpeedAndSpawnRate();
    }

    /// <summary>
    /// Pause or unpause the game, depending on current state
    /// </summary>
    public void TogglePause()
    {
        if (!isPaused)
        {
            Time.timeScale = 0;
            isPaused = true;
            uiController.TogglePausePanel(isPaused);
        }
        else
        {
            Time.timeScale = 1;
            isPaused = false;
            uiController.TogglePausePanel(isPaused);
        }

    }

    /// <summary>
    /// Inform the PlatformManager to spawn a new platform
    /// </summary>
    private void TriggerPlatformSpawn()
    {
        platformSpawnTimer -= Time.deltaTime;
        if (platformSpawnTimer < 0)
        {
            platformManager.SpawnPlatform(platformSpeed);
            platformSpawnTimer = initialPlatformSpawnTimer;
        }
    }

    /// <summary>
    /// Spawn the first method while disregarding the spawnTimer
    /// </summary>
    private void SpawnFirstPlatform()
    {
        platformManager.SpawnPlatform(platformSpeed);
    }

    /// <summary>
    /// Reduce the spawnTimer over time, and increase platform speed
    /// </summary>
    public void IncreaseSpeedAndSpawnRate()
    {
        initialPlatformSpawnTimer -= 0.04f * Time.deltaTime;
        platformSpeed += 0.005f * Time.deltaTime;
    }

    /// <summary>
    /// Increase the score and tell the UIController to update the display
    /// If score is close to highscore, tell platform manager to color the platforms
    /// </summary>
    public void IncrementScore()
    {
        Score++;
        uiController.UpdateScoreText(Score.ToString());
        if (Score >= highScore - 5 && Score < highScore)
        {
            platformManager.ColorPlatforms(true);
        }
    }

    /// <summary>
    /// Load the main menu screen
    /// </summary>
    public void LoadMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Home");
    }

    /// <summary>
    /// Reset game state to default for instant restarts with no scene loading required
    /// </summary>
    public void Reset()
    {
        Score = 0;
        EventManager.ResetGame();
        Time.timeScale = 1;
        initialPlatformSpawnTimer = initialGameSettingsSO.initialSpawnRate;
        initialPlatformSpeed = 0.07f;
        platformSpeed = initialPlatformSpeed;
        platformManager.ColorPlatforms(false);
    }

    private void OnDestroy()
    {
        EventManager.OnOutOfBounds -= GameOver;
        EventManager.OnPlatformHit -= IncrementScore;
    }
}
