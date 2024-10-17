using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public GameObject panelSetting;

    public void GameStart()
    {

        SceneManager.LoadScene("GameScene");
    }
    public void GameMenu()
    {
        SceneManager.LoadScene("Loading");
    }
    public void GameSettingOpen()
    {
        panelSetting.SetActive(true);
    }
    public void GameSettingClose()
    {
        panelSetting.SetActive(false);
    }
    public void GameExit()
    {
        Application.Quit();
    }
}
