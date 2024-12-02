using UnityEngine;

public class SceneLoadingCtrl : MonoBehaviour
{
    public static SceneLoadingCtrl instance {  get; private set; }
    [SerializeField] private SceneLoading loadingObject;
    private bool _isLoading;
    private void Awake()
    {
        if (instance != null) { Destroy(gameObject); return; }
        instance = this;

        _isLoading = false;
    }
    private void Update()
    {
        StartLoading();
    }
    public void EnableLoading() => _isLoading = true;
    public void DisabelLoading() => _isLoading = false; 
    private void StartLoading()
    {
        if (_isLoading)
        {
            loadingObject.gameObject.SetActive(true);
        }
        else
        {
            loadingObject.gameObject.SetActive(false);
        }
    }
}
