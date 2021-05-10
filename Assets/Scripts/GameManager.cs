using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary> Manages the state of the whole application </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] private string gameScene;
    [SerializeField] private Text highScoreText;

    void Start()
    {
        highScoreText.text = "Highscore: " + SaveStateController.LoadFromFile().ToString();
    }

    public void Play()
    {
        StartCoroutine(LoadScene(gameScene));
    }

    private IEnumerator LoadScene(string sceneName)
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneName);
    }
}