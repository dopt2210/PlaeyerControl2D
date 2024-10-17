using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioCtrl : MonoBehaviour
{
    private static AudioCtrl instance;
    public static AudioCtrl Instance => instance;
    public AudioSource audioSource;
    public AudioClip clip;

    private void Awake()
    {
        if (instance != null) return;
        instance = this;    
        DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}
