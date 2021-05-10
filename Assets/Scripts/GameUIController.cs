using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Interacts with all UI components
/// </summary>
public class GameUIController : MonoBehaviour
{
    [SerializeField] private Text currentScoreText;
    [SerializeField] private Text gameOverScoreText;
    [SerializeField] private Text gameOverHighscoreText;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject pausePanel;

    //Singleton
    public static GameUIController Instance { get; private set; }

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
        EventManager.OnGameReset += ClearGameOver;
    }

    private void Start()
    {
        gameOverPanel.SetActive(false);
        pausePanel.SetActive(false);
    }

    /// <summary>
    /// Update the value of the score text component
    /// </summary>
    /// <param name="score">Score value acquired from LevelManager</param>
    public void UpdateScoreText(string score)
    {
        currentScoreText.text = "Score: " + score;
    }

    /// <summary>
    /// Active the Game Over screen
    /// </summary>
    /// <param name="score">Score value acquired from LevelManager</param>
    /// <param name="highScore">Highscore value acquired from LevelManager</param>
    /// <param name="newHighscore">Whether it's a new highscore or not</param>
    public void ShowGameOver(int score, int highScore, bool newHighscore)
    {
        if (!newHighscore)
        {
            gameOverScoreText.text = "Your score: " + score;
            gameOverHighscoreText.text = "Your highscore: " + highScore;
            gameOverPanel.SetActive(true);
        }
        else
        {
            gameOverScoreText.text = "Your score: " + score;
            gameOverHighscoreText.text = "Your have a new highscore! " + highScore;
            gameOverPanel.SetActive(true);
        }
    }

    /// <summary>
    /// Disable the Game Over screen, used for restarting
    /// </summary>
    public void ClearGameOver()
    {
        gameOverPanel.SetActive(false);
    }

    /// <summary>
    /// Toggle the pause screen
    /// </summary>
    /// <param name="paused"></param>
    public void TogglePausePanel(bool paused)
    {
        if (paused)
        {
            pausePanel.SetActive(true);
        }
        else
        {
            pausePanel.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        EventManager.OnGameReset -= ClearGameOver;
    }
}
