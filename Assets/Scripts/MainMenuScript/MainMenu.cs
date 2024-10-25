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
    
    private void Start()
    {
        MusicManager.Instance.PlayMusic("MainMenu");
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
    // public void SaveVolume()
    // {
    //     audioMixer.GetFloat("MusicVolume", out float musicVolume);
    //     PlayerPrefs.SetFloat("MusicVolume", musicVolume);
    //     audioMixer.GetFloat("SFXVolume", out float sfxVolume);
    //     PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
    // }
    // public void LoadVolume()
    // {
    //     musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
    //     sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
    // }
    public void LoadData(GameData gameData)
    {
        musicSlider.value = gameData.musicVolume;
        sfxSlider.value = gameData.sfxVolume;
        UpdateMusicVolume(gameData.musicVolume);
        UpdateSFXVolume(gameData.sfxVolume);
    }

    public void SaveData(ref GameData gameData)
    {
        gameData.musicVolume = musicSlider.value;
        gameData.sfxVolume = sfxSlider.value;
    }

}
 