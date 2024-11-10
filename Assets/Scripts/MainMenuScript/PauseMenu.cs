using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    //public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    // Update is called once per frame
    void Update()
    {
        if (PlayerCtrl.instance.MenuOpen)
        {
            if (!PauseManager.instance.IsPaused)
            {
                Pause();
            }
        }
        if (PlayerCtrl.instance.MenuClose)
        {
            if (PauseManager.instance.IsPaused)
            {
                Resume();
            }
        }
    }
    public void Resume()
    {
        PauseManager.instance.ResumeGame();
        pauseMenuUI.SetActive(false);
    }
    public void Pause()
    {
        PauseManager.instance.PauseGame();
        pauseMenuUI.SetActive(true);
    }
}
