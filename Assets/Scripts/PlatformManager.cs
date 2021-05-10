using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the moving platforms in a list for Object Pooling
/// </summary>
public class PlatformManager : MonoBehaviour
{
    private List<GameObject> platformPool;
    public GameObject platformPrefab;

    //Singleton
    public static PlatformManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
        EventManager.OnGameReset += ResetAllPlatforms;
    }

    void Start()
    {
        platformPool = new List<GameObject>();

        for (int i = 0; i < 10; i++)
        {
            GameObject newPlatform = GameObject.Instantiate(platformPrefab, new Vector3(0,0,0), Quaternion.identity);
            newPlatform.SetActive(false);
            platformPool.Add(newPlatform);
        }
    }

    /// <summary>
    /// "Spawn" a new platform by taking an unactivated one from the pool
    /// </summary>
    /// <param name="startingSpeed">Speed parameter acquired from LevelManager</param>
    public void SpawnPlatform(float startingSpeed)
    {
        foreach (GameObject platform in platformPool)
        {
            if (!platform.activeInHierarchy)
            {
                platform.SetActive(true);
                platform.transform.position = new Vector3(0, -6.5f, 0);
                platform.GetComponent<MovingPlatform>().speed = startingSpeed;
                return;
            }
        }
    }

    /// <summary>
    /// Deactivate all platforms, used for restarting
    /// </summary>
    void ResetAllPlatforms()
    {
        foreach (GameObject platform in platformPool)
        {
            platform.SetActive(false);
        }
    }

    /// <summary>
    /// Change the color of all platforms
    /// </summary>
    /// <param name="highscoreState">Whether to change the color to red or back to white</param>
    public void ColorPlatforms(bool highscoreState)
    {
        foreach (GameObject platform in platformPool)
        {
            MovingPlatform platformScript = platform.GetComponent<MovingPlatform>();
            if (highscoreState)
            {
                platformScript.platformRendererLeft.material.color = Color.red;
                platformScript.platformRendererRight.material.color = Color.red;
            }
            else
            {
                platformScript.platformRendererLeft.material.color = Color.white;
                platformScript.platformRendererRight.material.color = Color.white;
            }
        }
    }

    private void OnDestroy()
    {
        EventManager.OnGameReset -= ResetAllPlatforms;
    }
}
