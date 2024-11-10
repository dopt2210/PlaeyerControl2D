using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static PauseManager instance;

    public bool IsPaused { get; private set; }

    private void Awake()
    {
        if (instance != null) return;
        instance = this;
    }

    public void PauseGame()
    {
        IsPaused = true;
        Time.timeScale = 0f;
        PlayerCtrl._inputPlayer.SwitchCurrentActionMap("UI");
    }

    public void ResumeGame()
    {
        IsPaused = false;
        Time.timeScale = 1f;
        PlayerCtrl._inputPlayer.SwitchCurrentActionMap("Player");
    }
}
