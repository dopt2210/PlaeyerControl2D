using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour, IGameData
{
    public AudioMixer audioMixer;
    public Slider musicSlider;
    public Slider sfxSlider;
    public Graphic graphic; // Reference to the Graphic script

    private void Start()
    {
        MusicManager.Instance.PlayMusic("MainMenu");

        // Ensure graphic is assigned
        if (graphic == null)
        {
            graphic = FindObjectOfType<Graphic>();
            if (graphic == null)
            {
                Debug.LogError("Graphic component not found!");
            }
        }
    }

    public void Play()
    {
        SceneManager.LoadScene("GameScene");
        MusicManager.Instance.PlayMusic("Game");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void GameMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void UpdateMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", volume);
    }

    public void UpdateSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", volume);
    }

    public void LoadData(GameData gameData)
    {
        musicSlider.value = gameData.musicVolume;
        sfxSlider.value = gameData.sfxVolume;
        UpdateMusicVolume(gameData.musicVolume);
        UpdateSFXVolume(gameData.sfxVolume);
        graphic.LoadGraphicsState(gameData.graphicsQuality); // Load graphics state
    }

    public void SaveData(ref GameData gameData)
    {
        gameData.musicVolume = musicSlider.value;
        gameData.sfxVolume = sfxSlider.value;
        gameData.graphicsQuality = graphic.GetCurrentGraphicsQuality(); // Save graphics state
    }
}