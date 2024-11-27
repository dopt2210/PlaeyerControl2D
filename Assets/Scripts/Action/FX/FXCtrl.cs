using System.Collections;
using UnityEngine;

public class FXCtrl : MonoBehaviour
{
    [SerializeField] private GameObject _shieldFX;
    [SerializeField] private Material _material;

    private static int _distance = Shader.PropertyToID("_ShockWaveDistance");
    public static bool isInvicible { get; private set; }

    private float shockWaveTime = 0.75f;
    [SerializeField] private float waitTime = 5f;
    [SerializeField] private float shieldTime = 1f;

    private float _shockWaveStartPos = -0.1f;
    private float _shockWaveEndPos = 1f;

    [SerializeField] private bool _isShieldActive = false;
    private void Start()
    {
        _shieldFX = GameObject.FindGameObjectWithTag("Player").transform.GetChild(6).gameObject;
        _material = GameObject.FindGameObjectWithTag("Player").transform.GetChild(7).GetComponent<SpriteRenderer>().material;
        ShieldOff();


    }
    void Update()
    {
        waitTime -= Time.deltaTime;
        if (PlayerCtrl.instance.SkillDown)
        {
            CallShockWave();
        }

    }
    private void CallShockWave()
    {
        if (!_isShieldActive && waitTime < 0f)
        {
            StartCoroutine(ShockWaveAciton(_shockWaveStartPos, _shockWaveEndPos));
            StartCoroutine(ShieldAction());

        }
    }
    private IEnumerator ShockWaveAciton(float startPos, float endPos)
    {
        _material.SetFloat(_distance, shockWaveTime);
        float lerpAmount = 0f;
        float elapsedTime = 0f;
        while (elapsedTime < shockWaveTime)
        {
            elapsedTime += Time.deltaTime;
            lerpAmount = Mathf.Lerp(startPos, endPos, (elapsedTime / shockWaveTime));
            _material.SetFloat(_distance, lerpAmount);
            yield return null;
        }
    }
    private IEnumerator ShieldAction()
    {
        float lerpAmount = 0f;
        float elapsedTime = 0f;
        while (elapsedTime < shieldTime)
        {
            elapsedTime += Time.deltaTime;
            lerpAmount = Mathf.Lerp(0, 2f, (elapsedTime / shieldTime));
            ShieldOn();
            yield return null;
        }
        ShieldOff();

    }


    private void ShieldOn()
    {
        _shieldFX.SetActive(true);
        _isShieldActive = true;
        isInvicible = true;

    } 
    private void ShieldOff()
    {
        waitTime = 5f;
        _shieldFX.SetActive(false);
        isInvicible = false;
        _isShieldActive = false;
    }
}
