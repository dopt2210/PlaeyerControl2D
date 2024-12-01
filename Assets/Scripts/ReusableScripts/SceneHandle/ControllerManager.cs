using UnityEngine;

public class ControllerManager : MonoBehaviour
{
    public static ControllerManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(this);
    }
}
