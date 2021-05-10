using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// EventManager class used to store delegates.
/// </summary>
public class EventManager
{
    public delegate void PlayerOutOfBounds();
    public static event PlayerOutOfBounds OnOutOfBounds;
    public static void GameOver()
    {
        OnOutOfBounds?.Invoke();
    }

    public delegate void PlayerPlatformHit();
    public static event PlayerPlatformHit OnPlatformHit;
    public static void IncreaseScore()
    {
        OnPlatformHit?.Invoke();
    }

    public delegate void GameReset();
    public static event GameReset OnGameReset;
    public static void ResetGame()
    {
        OnGameReset?.Invoke();
    }
}
