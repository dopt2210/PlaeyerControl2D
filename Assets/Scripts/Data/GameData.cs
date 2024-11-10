using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int deathCount;
    public Vector3 playerPosition;
    public float musicVolume;
    public float sfxVolume;
    public string graphicsQuality; // Add this line

    public GameData()
    {
        this.deathCount = 0;
        playerPosition = Vector3.zero;
        musicVolume = 0;
        sfxVolume = 0;
        graphicsQuality = "High"; // Default to High
    }
}