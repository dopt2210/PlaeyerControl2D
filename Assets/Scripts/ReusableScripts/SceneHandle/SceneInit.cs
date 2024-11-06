using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneInit
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Excute()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            return;
        }
        Object.DontDestroyOnLoad(Object.Instantiate(Resources.Load("CONTROLLER")));
    }
}
