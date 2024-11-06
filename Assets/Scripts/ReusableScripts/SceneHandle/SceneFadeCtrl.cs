using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneFadeCtrl : MonoBehaviour
{
    public static SceneFadeCtrl Instance { get; private set; }

    [SerializeField] private Image _fadeImage;
    [SerializeField] private float _fadeInSpeed = 5f;
    [SerializeField] private float _fadeOutSpeed = 5f;
    [SerializeField] private Color _fadeColor;

    public bool _isFadeIn { get; private set; }
    public bool _isFadeOut { get; private set ; }

    private void Awake()
    {
        if (Instance != null) return;
        Instance = this;

        _fadeColor.a = 0;
    }
    private void Update()
    {
        if (_isFadeOut)
        {
            if (_fadeColor.a < 1f)
            {
                _fadeColor.a += Time.deltaTime * _fadeOutSpeed;
                _fadeImage.color = _fadeColor;
            }
            else
            {
                _isFadeOut = false;
            }
        }
        if (_isFadeIn)
        {
            if (_fadeColor.a > 0f)
            {
                _fadeColor.a -= Time.deltaTime * _fadeInSpeed;
                _fadeImage.color = _fadeColor;
            }
            else
            {
                _isFadeIn = false;
            }
        }
    }

    public void StartFadeOut()
    {
        _fadeImage.color = _fadeColor;
        _isFadeOut = true;
    }
    public void StartFadeIn()
    {
        if (_fadeImage.color.a >= 1f)
        {
            _fadeImage.color = _fadeColor;
            _isFadeIn = true;
        }
    }
}
