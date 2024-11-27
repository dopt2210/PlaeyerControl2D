using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoadingCtrl : MonoBehaviour
{
    public static SceneLoadingCtrl instance {  get; private set; }
    [SerializeField] GameObject loadingObject;
    private bool _isLoading;
    private void Awake()
    {
        if(instance!=null) { Destroy(gameObject); return; }
        instance = this;
        _isLoading = false;
        loadingObject = transform.GetChild(0).gameObject;
    }
    private void Update()
    {
        StartLoading();
    }
    public void EnableLoading()
    {
        _isLoading = true;
    }
    public void DisabelLoading() { _isLoading = false; }
    private void StartLoading()
    {
        if (_isLoading)
        {
            loadingObject.SetActive(true);
        }
        else
        {
            loadingObject.SetActive(false);
        }
    }
}
