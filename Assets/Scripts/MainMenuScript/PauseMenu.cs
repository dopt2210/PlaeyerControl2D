using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance;
    private void Awake()
    {
        if(instance != null) { Destroy(gameObject); return; }
        instance = this;
    }
    void Update()
    {
        if (PlayerCtrl.instance.MenuOpen)
        {
            if (!MenuManager.instance.IsPaused)
            {
                Pause();
            }
        }
        if (PlayerCtrl.instance.MenuClose)
        {
            if (MenuManager.instance.IsPaused)
            {
                Resume();
            }
        }
    }
    public void Resume()
    {
        MenuManager.instance.PauseUI.SetActive(false);
        MenuManager.instance.ResumeGame();
    }
    public void Pause()
    {
        MenuManager.instance.PauseGame();
        MenuManager.instance.PauseUI.SetActive(true);
    }
}
