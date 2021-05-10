using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scriptable Object used to set the initial spawnrate of platforms.
/// </summary>
[CreateAssetMenu(menuName ="ScriptableObjects/InitialGameSettingsSO",fileName = "GameSettingsSO")]
[System.Serializable]
public class InitialGameSettingsSO : ScriptableObject
{
    [Range(1f, 2f)]
    public float initialSpawnRate = 1.2f;
}
